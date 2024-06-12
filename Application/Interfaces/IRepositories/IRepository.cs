using System.Linq.Expressions;

namespace Application.Interfaces.IRepositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        long Insertar(T entidad);
        long InsertarRango(List<T> entidades);
        Task<bool> InsertarBulkAsync(IEnumerable<T> entidad);
        void Actualizar(T entidad);
        Task ActualizarBulkAsync(List<T> entidad);
        void Eliminar(T entidad);
        void Eliminar(Expression<Func<T, bool>> predicado);
        Task<T> ObtenerElementoPorIdAsync(long id);
        Task<IEnumerable<T>> ObtenerTodosElementosAsync();
        IEnumerable<T> ObtenerElementosPorCriterio(Expression<Func<T, bool>> predicado);
        Task<IEnumerable<T>> ObtenerElementosPorCriterioAsync(Expression<Func<T, bool>> predicado);
        Task<bool> ExisteElementoAsync(Expression<Func<T, bool>> predicado);
        IQueryable<T> ObtenerElementoConInclusiones(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] propiedades);
        IEnumerable<object> GetDistinctValuesFromColumn<T>(string column, IEnumerable<T> data);
    }
}
