using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using System.Data.Entity.Migrations;
using AutoMapper.QueryableExtensions;
using System.Diagnostics;
using PAET.ServiciosBasicos.CoreService.Contracts;
using PAET.ServiciosBasicos.Enums;
using PAET.DominioBase.Contracts;
using PAET.Comun;
using PAET.Log.Log4Net;

namespace PAET.ServiciosBasicos
{
    public abstract class ServiceBase<TDtoEntity, TModelEntity> : IServiceBase<TDtoEntity> where TModelEntity : class
    {
        protected DbContext _serviceContext;
        protected ServiceBase(DbContext serviceContext)
        {
            _serviceContext = serviceContext;
            _serviceContext.Configuration.ProxyCreationEnabled = false;
            _serviceContext.Configuration.LazyLoadingEnabled = false;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Services.DBLogHabilitado"]))
            {
                _serviceContext.Database.Log = (x) => FicheroLog.Debug(x);
            }            
            _serviceContext.Database.Log = message => Debug.Write(message);
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public virtual IEnumerable<TDtoEntity> GetAll()
        {
            FicheroLog.Debug($"Obteniendo todos los registros de [{typeof(TModelEntity).Name.ToUpper()}]");
            var entities = _serviceContext.Set<TModelEntity>();
            return Mapper.Map<IEnumerable<TModelEntity>, IEnumerable<TDtoEntity>>(entities);
        }
        public virtual TDtoEntity FindSingle(Expression<Func<TDtoEntity, Boolean>> queryExpression)
        {
            return this.Find(queryExpression).FirstOrDefault();
        }
        public virtual IEnumerable<TDtoEntity> Find(Expression<Func<TDtoEntity, Boolean>> queryExpression)
        {            
            var entities = _serviceContext.Set<TModelEntity>();
            return entities.UseAsDataSource().For<TDtoEntity>().Where(queryExpression.Compile()).ToList();
        }

        public bool Exists(Expression<Func<TDtoEntity, bool>> queryExpression)
        {
            return _serviceContext.Set<TModelEntity>().UseAsDataSource().For<TDtoEntity>().Any(queryExpression.Compile());
        }

        public virtual ResultadoPaginacion<TDtoEntity> Paginate(FiltroPaginacion optsPaginacion = null)
        {
            var entities = _serviceContext.Set<TModelEntity>();
            return entities.Paginar<TModelEntity, TDtoEntity>(optsPaginacion);
        }
        public virtual ResultadoPaginacion<TDtoEntity> Paginate(Expression<Func<TDtoEntity, Boolean>> queryExpression, FiltroPaginacion optsPaginacion = null)
        {

            return this.Find(queryExpression).Paginar(optsPaginacion);
        }

        public virtual ResultadoPaginacion<TDtoEntity> Paginate(Expression<Func<TDtoEntity, Boolean>> queryExpression, FiltroPaginacion optsPaginacion = null, FiltroOrdenacion optsOrdenacion = null)
        {
            return this.Find(queryExpression).AsQueryable().OrdenarPor(optsOrdenacion).Paginar(optsPaginacion);
        }

        public virtual ResultadoAccion<TDtoEntity> Save(TDtoEntity dtoEntity)
        {
            ResultadoAccion<TDtoEntity> resultSave;
            var dbEntity = Mapper.Map<TModelEntity>(dtoEntity);
            if (!EntityExists(dbEntity))
            {
                resultSave = this.Insert(dtoEntity);
            }
            else
            {
                resultSave = this.Update(dtoEntity);
            }
            return resultSave;
        }
        public virtual ResultadoAccion Delete(TDtoEntity dtoEntity)
        {
            ResultadoAccion resultDelete;
            FicheroLog.Info($"Borrando entidad [{typeof(TDtoEntity).Name.ToUpper()}]...");

            var resultBeforeDelete = this.BeforeDelete(dtoEntity);
            if (resultBeforeDelete.Ok())
            {
                var dbEntity = Mapper.Map<TDtoEntity, TModelEntity>(dtoEntity);
                dbEntity = GetModelFromContextByKey(dbEntity);

                _serviceContext.Set<TModelEntity>().Remove(dbEntity);
                resultDelete = this.CommitChanges();

                this.OnAccionHistorico(dtoEntity, TipoOperacionHistoricoEnum.Borrado);

                this.AfterDelete(ResultadoAccion<TDtoEntity>.CreateFromResultado(resultDelete, dtoEntity));
                
            }
            else
            {
                resultDelete = resultBeforeDelete;
                FicheroLog.LogResult(resultDelete);
            }
            return resultDelete;
        }
        private ResultadoAccion<TDtoEntity> Insert(TDtoEntity dtoEntity)
        {
            ResultadoAccion<TDtoEntity> resultSave = new ResultadoAccion<TDtoEntity>();
            if (dtoEntity.GetType().GetInterfaces().Any((x) => x == typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)dtoEntity).FechaCreacion = resultSave.FechaAccion;
                ((IAuditableEntity)dtoEntity).UsuarioCreacion = resultSave.Usuario;
            }

            FicheroLog.Info($"Guardando entidad [{typeof(TModelEntity).Name.ToUpper()}]...");


            var dbEntity = Mapper.Map<TDtoEntity, TModelEntity>(dtoEntity);

            var beforeSaveResult = this.BeforeSave(dtoEntity);
            if (beforeSaveResult.Ok())
            {
                _serviceContext.Set<TModelEntity>().Add(dbEntity);
                var result = this.CommitChanges();
                resultSave = ResultadoAccion<TDtoEntity>.CreateFromResultado(result);
                if (resultSave.ResultCode == ResultadoAccion.CodigoResultado.OK)
                {
                    resultSave.Entidad = Mapper.Map<TDtoEntity>(dbEntity);

                    OnAccionHistorico(resultSave.Entidad, TipoOperacionHistoricoEnum.Alta);
                }
                this.AfterSave(resultSave);
            }
            else
            {
                resultSave = ResultadoAccion<TDtoEntity>.CreateFromResultado(beforeSaveResult);
            }




            return resultSave;
        }


        private ResultadoAccion<TDtoEntity> Update(TDtoEntity dtoEntity)
        {
            ResultadoAccion<TDtoEntity> resultUpdate = new ResultadoAccion<TDtoEntity>();
            if (dtoEntity.GetType().GetInterfaces().Any((x) => x == typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)dtoEntity).FechaModificacion = resultUpdate.FechaAccion;
                ((IAuditableEntity)dtoEntity).UsuarioModificacion = resultUpdate.Usuario;
            }


            FicheroLog.Info($"Actualizando entidad [{typeof(TModelEntity).Name.ToUpper()}]...");

            var dbEntity = Mapper.Map<TDtoEntity, TModelEntity>(dtoEntity);

            var beforeUpdateResult = this.BeforeUpdate(dtoEntity);
            if (beforeUpdateResult.Ok())
            {
                _serviceContext.Set<TModelEntity>().AddOrUpdate(dbEntity);
                var result = this.CommitChanges();
                resultUpdate = ResultadoAccion<TDtoEntity>.CreateFromResultado(result);
                if (resultUpdate.ResultCode == ResultadoAccion.CodigoResultado.OK)
                {
                    resultUpdate.Entidad = Mapper.Map<TDtoEntity>(dbEntity);

                    OnAccionHistorico(resultUpdate.Entidad, TipoOperacionHistoricoEnum.Modificacion);
                }
                this.AfterUpdate(resultUpdate);
            }
            else
            {
                resultUpdate = ResultadoAccion<TDtoEntity>.CreateFromResultado(beforeUpdateResult);
            }
            return resultUpdate;
        }
        //--------------------------------------------------------------------------------------------------------------------------
        protected virtual ResultadoAccion CommitChanges()
        {
            FicheroLog.Debug("Persistiendo cambios en DB.");
            ResultadoAccion result;
            try
            {
                _serviceContext.LogDbChanges();
                var saveCount = _serviceContext.SaveChanges();
                if (saveCount > 0)
                {
                    FicheroLog.Debug($"Se realizaron {saveCount} cambios en la base de datos.");
                    result = ResultadoAccion.ResultadoOK("La operación se realizo correctamente.");
                }
                else
                {
                    result = new ResultadoAccion() { ResultCode = ResultadoAccion.CodigoResultado.WARNING };
                    result.AddMensajeError("No se realizó ningun cambio en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                result = ResultadoAccion.ResultadoError(ResultadoAccion.CodigoResultado.ERR);
                result.ResultException = ex;
                result.AddMensajeError(ex.Message);
            }
            FicheroLog.LogResult(result);
            return result;
        }
        #region "Events" 
        #region "Save Callbacks"
        protected virtual ResultadoAccion BeforeSave(TDtoEntity dtoEntity)
        {
            return ResultadoAccion.ResultadoOK();
        }
        protected virtual ResultadoAccion AfterSave(ResultadoAccion<TDtoEntity> resultSave)
        {
            return ResultadoAccion<TDtoEntity>.ResultadoOK();
        }
        #endregion

        #region "Update Callbacks"
        protected virtual ResultadoAccion BeforeUpdate(TDtoEntity dtoEntity)
        {
            return ResultadoAccion.ResultadoOK();
        }
        protected virtual ResultadoAccion AfterUpdate(ResultadoAccion<TDtoEntity> Update)
        {
            return ResultadoAccion<TDtoEntity>.ResultadoOK();
        }
        #endregion

        #region "Delete Callbacks"
        protected virtual ResultadoAccion BeforeDelete(TDtoEntity dtoEntity)
        {
            return ResultadoAccion.ResultadoOK();
        }
        protected virtual ResultadoAccion AfterDelete(ResultadoAccion<TDtoEntity> resultDelete)
        {
            return ResultadoAccion<TDtoEntity>.ResultadoOK();
        }
        #endregion


        

        #region "Historico"
        protected virtual void OnAccionHistorico(TDtoEntity entidad, TipoOperacionHistoricoEnum operation) { }
        #endregion

        #endregion
        #region "private Methods"
        private bool EntityExists(TModelEntity entity)
        {
            var objContext = ((IObjectContextAdapter)_serviceContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<TModelEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            // Detach it here to prevent side-effects
            if (exists) { objContext.Detach(foundEntity); }
            return exists;
        }


        private TModelEntity GetModelFromContextByKey(TModelEntity entity)
        {
            var objContext = ((IObjectContextAdapter)_serviceContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<TModelEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (foundEntity == null) return null;
            return (TModelEntity)foundEntity;
        }
        #endregion


        protected ResultadoAccion SaveHistoricoEntity<TEntityDb>(IHistoricoEntity entity, TipoOperacionHistoricoEnum operation) where TEntityDb : class
        {
            var result = ResultadoAccion.ResultadoOK();
            if (entity.GetType().GetInterfaces().Any((x) => x == typeof(IHistoricoEntity)))
            {
                try
                {
                    entity.FechaOperacion = result.FechaAccion;
                    entity.TipoOperacion = (int)operation;
                    entity.UsuarioOperacion = result.Usuario;

                    var entidad = Mapper.Map<TEntityDb>(entity);
                    _serviceContext.Set<TEntityDb>().Add(entidad);
                    return CommitChanges();
                }
                catch (Exception ex)
                {
                    FicheroLog.Err("Error al grabar el historico", ex);
                    return ResultadoAccion.ResultadoError(ResultadoAccion.CodigoResultado.ERR, "Error al grabar el historico");
                }
            }
            else
            {
                return ResultadoAccion.ResultadoError(ResultadoAccion.CodigoResultado.ERR,
                        $"No se pudo guardar en el historico porque la entidad {entity.GetType().Name} no implementa la interfaz 'IHistoricoEntity'");
            }
        }


    }
}

