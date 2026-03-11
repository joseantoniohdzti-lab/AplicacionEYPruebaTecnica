using AccesoDatos.Data;
using AccesoDatos.InterfazRepositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> _dbSet;

        public RepositorioGenerico(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task<bool> Agregar(T entidad)
        {

            try
            {
                await _db.Set<T>().AddAsync(entidad);

                bool bandera = await Guardar();

                return bandera;

            }
            catch (Exception ex)
            {
                
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task Eliminar(T entidad)
        {
            try
            {
                _db.Set<T>().Remove(entidad);
                
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
             
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task EliminarRango(IEnumerable<T> entidad)
        {
            try
            {
                _db.Set<T>().RemoveRange(entidad);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<T> GetObtener(int id)
        {
            try
            {
                var entidad = await _db.Set<T>().FindAsync(id);

                return entidad;
            }
            catch (Exception ex)
            {
                
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<T> GetObtenerPrimero(Expression<Func<T, bool>>? filtro = null
            , string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = _dbSet;
            try
            {
                if (filtro != null)
                {
                    query = query.Where(filtro);
                }

                if (incluirPropiedades != null)
                {

                    string[] arrayPropiedades = incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var relacion in arrayPropiedades)
                    {
                        query = query.Include(relacion);
                    }

                }

              

                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }


                return await query.FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetObtenerTodos(Expression<Func<T, bool>>? filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = _dbSet;
            try
            {
                if (filtro != null)
                {
                    query = query.Where(filtro);
                }

                if (incluirPropiedades != null)
                {

                    string[] arrayPropiedades = incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var relacion in arrayPropiedades)
                    {
                        query = query.Include(relacion);
                    }

                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }


                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }


                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<bool> Guardar()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
