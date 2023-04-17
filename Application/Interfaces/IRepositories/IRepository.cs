using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        Task<bool> ExisteElemento(Expression<Func<T, bool>> predicado);

    }
}
