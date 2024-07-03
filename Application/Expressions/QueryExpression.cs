using Application.Wrappers;
using System.Linq.Expressions;

namespace Application.Expressions
{
    static internal class QueryExpression<T>
    {
        public static List<Expression<Func<T, bool>>> GetExpressionList(List<Filter> filters)
        {
            List<Expression<Func<T, bool>>> expressions = new();

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Conditional))
                {
                    expressions.Add(QueryExpression<T>.GetExpression(filter));
                }
                else
                {
                    if (filter.Values != null && filter.Values.Any())
                    {
                        expressions.Add(QueryExpression<T>.GetContainsExpression(filter.Column, filter.Values));
                    }
                }
            }

            return expressions;
        }
        private static Expression<Func<T, bool>> GetExpression(Filter filter)
        {
            return filter.Conditional switch
            {
                #region Text
                "equals" => GetEqualsExpression(filter.Column, filter.Value),
                "notequals" => GetNotEqualsExpression(filter.Column, filter.Value),
                "beginswith" => GetBeginsWithExpression(filter.Column, filter.Value),
                "notbeginswith" => GetNotBeginsWithExpression(filter.Column, filter.Value),
                "endswith" => GetEndsWithExpression(filter.Column, filter.Value),
                "notendswith" => GetNotEndsWithExpression(filter.Column, filter.Value),
                "contains" => GetContainsExpression(filter.Column, filter.Value),
                "notcontains" => GetNotContainsExpression(filter.Column, filter.Value),
                #endregion
                #region Numeric
                "greaterthan" => GetGreaterThanExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "lessthan" => GetLessThanExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "greaterthanorequalto" => GetGreaterThanOrEqualToExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "lessthanorequalto" => GetLessThanOrEqualToExpression(filter.Column, Convert.ToInt32(filter.Value)),
                #endregion
                #region Date
                "before" => GetBeforeExpression(filter.Column, DateTime.Parse(filter.Value)),
                "after" => GetAfterExpression(filter.Column, DateTime.Parse(filter.Value)),
                "beforeorequal" => GetBeforeOrEqualExpression(filter.Column, DateTime.Parse(filter.Value)),
                "afterorequal" => GetAfterOrEqualExpression(filter.Column, DateTime.Parse(filter.Value)),
                #endregion
                _ => muestreo => true,
            };
        }

        #region StringExpressions
        public static Expression<Func<T, bool>> GetContainsExpression(string column, List<string> values)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(List<string>).GetMethod("Contains", new[] { typeof(string) });
            var constant = Expression.Constant(values, typeof(List<string>));
            var contains = Expression.Call(constant, method, property);

            return Expression.Lambda<Func<T, bool>>(contains, parameter);
        }
        public static Expression<Func<T, bool>> GetContainsExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var contains = Expression.Call(property, method, constant);

            return Expression.Lambda<Func<T, bool>>(contains, parameter);
        }
        public static Expression<Func<T, bool>> GetNotContainsExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var contains = Expression.Call(property, method, constant);
            var notContains = Expression.Not(contains);

            return Expression.Lambda<Func<T, bool>>(notContains, parameter);
        }

        public static Expression<Func<T, bool>> GetEqualsExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var constant = Expression.Constant(value);
            var notEqual = Expression.Equal(property, constant);

            return Expression.Lambda<Func<T, bool>>(notEqual, parameter);
        }

        public static Expression<Func<T, bool>> GetNotEqualsExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var constant = Expression.Constant(value);
            var notEqual = Expression.NotEqual(property, constant);

            return Expression.Lambda<Func<T, bool>>(notEqual, parameter);
        }

        public static Expression<Func<T, bool>> GetBeginsWithExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var startsWith = Expression.Call(property, method, constant);

            return Expression.Lambda<Func<T, bool>>(startsWith, parameter);
        }

        public static Expression<Func<T, bool>> GetNotBeginsWithExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var startsWith = Expression.Call(property, method, constant);
            var notStartsWith = Expression.Not(startsWith);

            return Expression.Lambda<Func<T, bool>>(notStartsWith, parameter);
        }
        public static Expression<Func<T, bool>> GetEndsWithExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var endsWith = Expression.Call(property, method, constant);

            return Expression.Lambda<Func<T, bool>>(endsWith, parameter);
        }
        public static Expression<Func<T, bool>> GetNotEndsWithExpression(string column, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            var constant = Expression.Constant(value, typeof(string));
            var endsWith = Expression.Call(property, method, constant);
            var notEndsWith = Expression.Not(endsWith);

            return Expression.Lambda<Func<T, bool>>(notEndsWith, parameter);
        }
        #endregion

        #region NumericExpressions
        public static Expression<Func<T, bool>> GetGreaterThanExpression(string column, int value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(Convert).GetMethod("ToInt64", new[] { typeof(object) });
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var convertedProperty = Expression.Call(method, propertyAsObject);
            var constant = Expression.Constant((long)value, typeof(long));
            var greaterThan = Expression.GreaterThan(convertedProperty, constant);

            return Expression.Lambda<Func<T, bool>>(greaterThan, parameter);
        }
        public static Expression<Func<T, bool>> GetGreaterThanOrEqualToExpression(string column, int value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(Convert).GetMethod("ToInt64", new[] { typeof(object) });
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var convertedProperty = Expression.Call(method, propertyAsObject);
            var constant = Expression.Constant((long)value, typeof(long));
            var greaterThanOrEqual = Expression.GreaterThanOrEqual(convertedProperty, constant);

            return Expression.Lambda<Func<T, bool>>(greaterThanOrEqual, parameter);
        }
        public static Expression<Func<T, bool>> GetLessThanExpression(string column, int value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(Convert).GetMethod("ToInt64", new[] { typeof(object) });
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var convertedProperty = Expression.Call(method, propertyAsObject);
            var constant = Expression.Constant((long)value, typeof(long));
            var lessThan = Expression.LessThan(convertedProperty, constant);

            return Expression.Lambda<Func<T, bool>>(lessThan, parameter);
        }
        public static Expression<Func<T, bool>> GetLessThanOrEqualToExpression(string column, int value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(Convert).GetMethod("ToInt64", new[] { typeof(object) });
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var convertedProperty = Expression.Call(method, propertyAsObject);
            var constant = Expression.Constant((long)value, typeof(long));
            var lessThanOrEqual = Expression.LessThanOrEqual(convertedProperty, constant);

            return Expression.Lambda<Func<T, bool>>(lessThanOrEqual, parameter);
        }
        #endregion

        #region DateExpression
        public static Expression<Func<T, bool>> GetBeforeExpression(string column, DateTime value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(DateTime).GetMethod("Parse", new[] { typeof(string) });
            var propertyAsString = Expression.Call(property, typeof(object).GetMethod("ToString"));
            var parsedDate = Expression.Call(method, propertyAsString);
            var constant = Expression.Constant(value, typeof(DateTime));
            var before = Expression.LessThan(parsedDate, constant);

            return Expression.Lambda<Func<T, bool>>(before, parameter);
        }
        public static Expression<Func<T, bool>> GetAfterExpression(string column, DateTime value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(DateTime).GetMethod("Parse", new[] { typeof(string) });
            var propertyAsString = Expression.Call(property, typeof(object).GetMethod("ToString"));
            var parsedDate = Expression.Call(method, propertyAsString);
            var constant = Expression.Constant(value, typeof(DateTime));
            var after = Expression.GreaterThan(parsedDate, constant);

            return Expression.Lambda<Func<T, bool>>(after, parameter);
        }
        public static Expression<Func<T, bool>> GetBeforeOrEqualExpression(string column, DateTime value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(DateTime).GetMethod("Parse", new[] { typeof(string) });
            var propertyAsString = Expression.Call(property, typeof(object).GetMethod("ToString"));
            var parsedDate = Expression.Call(method, propertyAsString);
            var constant = Expression.Constant(value, typeof(DateTime));
            var beforeOrEqual = Expression.LessThanOrEqual(parsedDate, constant);

            return Expression.Lambda<Func<T, bool>>(beforeOrEqual, parameter);
        }
        public static Expression<Func<T, bool>> GetAfterOrEqualExpression(string column, DateTime value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var method = typeof(DateTime).GetMethod("Parse", new[] { typeof(string) });
            var propertyAsString = Expression.Call(property, typeof(object).GetMethod("ToString"));
            var parsedDate = Expression.Call(method, propertyAsString);
            var constant = Expression.Constant(value, typeof(DateTime));
            var afterOrEqual = Expression.GreaterThanOrEqual(parsedDate, constant);

            return Expression.Lambda<Func<T, bool>>(afterOrEqual, parameter);
        }
        #endregion

        #region OrderBy
        public static Expression<Func<T, object>> GetOrderByExpression(string column)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, column);
            var converted = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(converted, parameter);
        }
        #endregion
    }
}
