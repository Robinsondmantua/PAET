using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAET.Comun;
using PAET.ServiciosBasicos.Behaviours;

namespace PAET.ServiciosBasicos.CoreService.Contracts
{
    public interface IServiceBase<TDtoEntity>
    {
        [Cacheable]
        IEnumerable<TDtoEntity> GetAll();
        IEnumerable<TDtoEntity> Find(Expression<Func<TDtoEntity, bool>> queryExpression);
        TDtoEntity FindSingle(Expression<Func<TDtoEntity, bool>> queryExpression);
        ResultadoAccion<TDtoEntity> Save(TDtoEntity dtoEntity);

        bool Exists(Expression<Func<TDtoEntity, bool>> queryExpression);
        ResultadoAccion Delete(TDtoEntity dtoEntity);

        ResultadoPaginacion<TDtoEntity> Paginate(FiltroPaginacion optsPaginacion = null);
        ResultadoPaginacion<TDtoEntity> Paginate(Expression<Func<TDtoEntity, Boolean>> queryExpression, FiltroPaginacion optsPaginacion = null);
    }
}