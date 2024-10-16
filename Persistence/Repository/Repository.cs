using Application.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly SicaContext _dbContext;

        public Repository(SicaContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Actualizar(T entidad)
        {
            _dbContext.Set<T>().Update(entidad);
            _dbContext.SaveChanges();
            _dbContext.Entry(entidad).State = EntityState.Detached;
        }

        public async Task ActualizarAsync(IEnumerable<T> entidades)
        {
            foreach (var entidad in entidades)
            {
                _dbContext.Set<T>().Update(entidad);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ActualizarBulkAsync(List<T> entidad)
        {
            await _dbContext.Set<T>().BulkUpdateAsync(entidad);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;
            _dbContext.Dispose();
        }

        public void Eliminar(T entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad));
            }

            _dbContext.Set<T>().Remove(entidad);
            _dbContext.SaveChanges();
        }

        public async Task EliminarAsync(Expression<Func<T, bool>> predicado)
        {
            var records = ObtenerElementosPorCriterio(predicado).ToList();

            foreach (var record in records)
            {
                _dbContext.Set<T>().Remove(record);
            }

            await _dbContext.SaveChangesAsync();
        }

        public long Insertar(T entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException("No se puede insertar, a entidad esta nula");
            }

            _dbContext.Set<T>().Add(entidad);
            _dbContext.SaveChanges();

            return Convert.ToInt64(_dbContext.Set<T>().Count());
        }

        public long InsertarRango(List<T> entidades)
        {
            if (entidades == null)
            {
                throw new ArgumentNullException("No se puede insertar, a entidad esta nula");
            }

            _dbContext.Set<T>().AddRange(entidades);
            _dbContext.SaveChanges();

            return Convert.ToInt64(_dbContext.Set<T>().Count());
        }

        public async Task<bool> InsertarBulkAsync(IEnumerable<T> entidad)
        {
            await _dbContext.BulkInsertAsync<T>(entidad);
            _dbContext.SaveChanges();
            return true;
        }

        public virtual async Task<T> ObtenerElementoPorIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> ObtenerElementosPorCriterio(Expression<Func<T, bool>> predicado)
        {
            return _dbContext.Set<T>().AsNoTracking().Where(predicado).AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> ObtenerElementosPorCriterioAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbContext.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodosElementosAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<bool> ExisteElementoAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbContext.Set<T>().AnyAsync(predicado);
        }

        public virtual IQueryable<T> ObtenerElementoConInclusiones(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] propiedades)
        {
            IQueryable<T> queryable = _dbContext.Set<T>().Where(predicado ?? (p => true)).AsQueryable();

            foreach (Expression<Func<T, object>> includeProperty in propiedades)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        [Obsolete("This method is deprecated. Use GetDistinctFromColumnAsync instead.")]
        public IEnumerable<object> GetDistinctValuesFromColumn<T>(string column, IEnumerable<T> data)
        {
            var muestreos = data.AsQueryable();
            var select = GenerateDynamicSelect<T>(column);

            return muestreos.Select(select).Distinct();
        }

        public async Task<IEnumerable<object>> GetDistinctFromColumnAsync<T>(string column, IQueryable<T> data)
        {
            var select = GenerateDynamicSelect<T>(column);

            return await data.Select(select).Distinct().ToListAsync();
        }

        public static Expression<Func<T, object>> GenerateDynamicSelect<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(conversion, parameter);
        }
    }
}
