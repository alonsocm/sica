using System.Linq.Expressions;

namespace Application.Helpers
{
    public static class QueryHelper
    {
        public static IEnumerable<object> GetDistinctValuesFromColumn<T>(string column, IEnumerable<T> data)
        {
            var muestreos = data.AsQueryable();
            var select = GenerateDynamicSelect<T>(column);

            return muestreos.Select(select).Distinct();
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
