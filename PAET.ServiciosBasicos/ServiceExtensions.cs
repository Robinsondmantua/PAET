using AutoMapper;
using PAET.ServiciosBasicos.Properties;
using LinqKit;
using PAET.Comun;
using PAET.Log.Log4Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PAET.Comun.EnumeradosInternos;

namespace PAET.ServiciosBasicos
{
    public static class ServiceExtensions
    {
        public static ResultadoPaginacion<T> Paginar<T>(this IEnumerable<T> source, FiltroPaginacion optsPaginacion = null)
        {
            if (optsPaginacion == null)
                optsPaginacion = new FiltroPaginacion();
            var totalItems = source.Count();
            source = source.OrderBy(x => 1).Skip(optsPaginacion.Pagina * optsPaginacion.ItemsPorPagina);

            if (optsPaginacion.ItemsPorPagina != 0)
            {
                source = source.Take(optsPaginacion.ItemsPorPagina);
            }

            return new ResultadoPaginacion<T>(source, optsPaginacion.Pagina, optsPaginacion.ItemsPorPagina, totalItems);
        }

        public static ResultadoPaginacion<TDto> Paginar<TEntity, TDto>(this IQueryable<TEntity> source, FiltroPaginacion optsPaginacion, bool isOrdered = false)
        {
            if (optsPaginacion == null)
                optsPaginacion = new FiltroPaginacion();

            var totalItems = source.Count();

            if (!isOrdered)
                source = source.OrderBy(s => 1);

            source = source.Skip(optsPaginacion.Pagina * optsPaginacion.ItemsPorPagina);

            if (optsPaginacion.ItemsPorPagina != 0)
            {
                source = source.Take(optsPaginacion.ItemsPorPagina);
            }

            var result = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(source);
            return new ResultadoPaginacion<TDto>(result, optsPaginacion.Pagina, optsPaginacion.ItemsPorPagina, totalItems);
        }


        public static Expression<Func<T, bool>> AndOr<T>(this Expression<Func<T, bool>> expresion, Expression<Func<T, bool>> filter, OperadorLogico operadorlogico)
        {
            if (operadorlogico == OperadorLogico.AND)
                return expresion.And(filter);
            else
                return expresion.Or(filter);
        }
        public static IOrderedQueryable<TSource> OrdenarPor<TSource>(this IEnumerable<TSource> query, FiltroOrdenacion filtro)
        {
            if (string.IsNullOrEmpty(filtro.PropiedadOrdenacion)) return query.AsQueryable().OrderBy(x=>1);

            var entityType = typeof(TSource);
            //Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(filtro.PropiedadOrdenacion);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, filtro.PropiedadOrdenacion);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            //Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(System.Linq.Queryable);

            var methodName = (filtro.Direccion == 0 ? "OrderBy" : "OrderByDescending");

            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
           
                     return parameters.Count == 2;
                 }).Single();
       
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }




        public static void LogDbChanges(this DbContext context)
        {
            if (!Settings.Default.TrazarCambiosEnDB) return;
            
      
            var changeInfo = context.ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified || t.State == EntityState.Added)
            .Select(t => new
            {
                Entity = t.Entity,
                State = t.State,
                Original = t.State == EntityState.Modified ? t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn]) : null,
                Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn]),
            });


            foreach (var item in changeInfo)
            {
                StringBuilder sb = new StringBuilder();
                if (item.State == EntityState.Added)
                {
                    sb.AppendLine($"[INSERT] Se Añadieron Valores en la entidad: {item.Entity.GetType().Name}");
                    sb.AppendLine("----------------------------------------------------------------------");
                    foreach (var key in item.Current.Keys)
                    {
                        sb.AppendLine($"[{key}]: {item.Current[key]}");
                    }
                    sb.AppendLine("----------------------------------------------------------------------");
                }
                else
                {
                    sb.AppendLine($"[UPDATE] Se Modificaron Valores en la entidad: {item.Entity.GetType().Name}");
                    sb.AppendLine("----------------------------------------------------------------------");
                    foreach (var key in item.Original.Keys)
                    {
                        if (item.Current[key] != item.Original[key])
                        {
                            sb.AppendLine($"Se detecto un cambio en [{key}]:   {item.Original[key]}  ->  {item.Current[key]}");
                        }
                    }
                    sb.AppendLine("----------------------------------------------------------------------");
                }
                FicheroLog.Debug(sb.ToString());
            }

        }

    }
}
