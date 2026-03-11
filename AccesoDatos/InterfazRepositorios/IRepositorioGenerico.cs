using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AccesoDatos.InterfazRepositorios
{
    public interface IRepositorioGenerico<T> where T : class 
    {
        Task<T> GetObtener(int id);
        Task<T> GetObtenerPrimero(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null, bool isTracking = true);
        Task<IEnumerable<T>> GetObtenerTodos(Expression<Func<T, bool>>? filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true);
        Task<bool> Agregar(T entidad);
        Task Eliminar(T entidad);
        Task EliminarRango(IEnumerable<T> entidad);
        Task<bool> Guardar();
    }
}
