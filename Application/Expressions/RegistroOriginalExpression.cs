using Application.DTOs;
using Application.Wrappers;
using System.Linq.Expressions;

namespace Application.Expressions
{
    static internal class RegistroOriginalExpression
    {
        public static List<Expression<Func<RegistroOriginalDto, bool>>> GetExpressionList(List<Filter> filters)
        {
            List<Expression<Func<RegistroOriginalDto, bool>>> expressions = new();

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Conditional))
                {
                    expressions.Add(GetExpression(filter));
                }
                else
                {
                    if (filter.Values != null && filter.Values.Any() && !filter.IsParameter)
                    {
                        expressions.Add(GetContainsExpression(filter.Column, filter.Values));
                    }

                    if (filter.Values != null && filter.Values.Any() && filter.IsParameter)
                    {
                        expressions.Add(GetFilterParameterExpression(filter.Column, filter.Values));
                    }
                }
            }

            return expressions;
        }
        private static Expression<Func<RegistroOriginalDto, bool>> GetExpression(Filter filter)
        {
            if (filter.IsParameter)
            {
                return GetFilterParameterExpression(filter.Column, filter.Value, filter.Conditional);
            }
            else
            {
                return filter.Conditional switch
                {
                    #region Text
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
        }

        #region StringExpressions
        public static Expression<Func<RegistroOriginalDto, bool>> GetExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => muestreo.NumeroCarga == value,
                "clavesitiooriginal" => muestreo => muestreo.ClaveSitioOriginal == value,
                "clavesitio" => muestreo => muestreo.ClaveSitio == value,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion == value,
                "laboratorio" => muestreo => muestreo.Laboratorio == value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua == value,
                "tipohomologado" => muestreo => muestreo.TipoHomologado == value,
                "tipositio" => muestreo => muestreo.TipoSitio == value,
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetContainsExpression(string column, List<string> value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => value.Contains(muestreo.NumeroCarga),
                "clavesitiooriginal" => muestreo => value.Contains(muestreo.ClaveSitioOriginal),
                "clavesitio" => muestreo => value.Contains(muestreo.ClaveSitio),
                "clavemonitoreo" => muestreo => value.Contains(muestreo.ClaveMonitoreo),
                "fecharealizacion" => muestreo => value.Contains(muestreo.FechaRealizacion),
                "laboratorio" => muestreo => value.Contains(muestreo.Laboratorio),
                "tipocuerpoagua" => muestreo => value.Contains(muestreo.TipoCuerpoAgua),
                "tipohomologado" => muestreo => value.Contains(muestreo.TipoHomologado),
                "tipositio" => muestreo => value.Contains(muestreo.TipoSitio),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => muestreo.NumeroCarga.Contains(value),
                "clavesitiooriginal" => muestreo => muestreo.ClaveSitioOriginal.Contains(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.Contains(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.Contains(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.Contains(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.Contains(value),
                "tipohomologado" => muestreo => muestreo.TipoHomologado.Contains(value),
                "tipositio" => muestreo => muestreo.TipoSitio.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetNotContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => !muestreo.NumeroCarga.Contains(value),
                "clavesitiooriginal" => muestreo => !muestreo.ClaveSitioOriginal.Contains(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.Contains(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.Contains(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.Contains(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.Contains(value),
                "tipohomologado" => muestreo => !muestreo.TipoHomologado.Contains(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetNotEqualsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => muestreo.NumeroCarga != value,
                "clavesitiooriginal" => muestreo => muestreo.ClaveSitioOriginal != value,
                "clavesitio" => muestreo => muestreo.ClaveSitio != value,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo != value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion != value,
                "laboratorio" => muestreo => muestreo.Laboratorio != value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua != value,
                "tipohomologado" => muestreo => muestreo.TipoHomologado != value,
                "tipositio" => muestreo => muestreo.TipoSitio != value,
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => muestreo.NumeroCarga.StartsWith(value),
                "clavesitiooriginal" => muestreo => muestreo.ClaveSitioOriginal.StartsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.StartsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.StartsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.StartsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.StartsWith(value),
                "tipohomologado" => muestreo => muestreo.TipoHomologado.StartsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetNotBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => !muestreo.NumeroCarga.StartsWith(value),
                "clavesitiooriginal" => muestreo => !muestreo.ClaveSitioOriginal.StartsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.StartsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.StartsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.StartsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.StartsWith(value),
                "tipohomologado" => muestreo => !muestreo.TipoHomologado.StartsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => muestreo.NumeroCarga.EndsWith(value),
                "clavesitiooriginal" => muestreo => muestreo.ClaveSitioOriginal.EndsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.EndsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.EndsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.EndsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.EndsWith(value),
                "tipohomologado" => muestreo => muestreo.TipoHomologado.EndsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetNotEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => !muestreo.NumeroCarga.EndsWith(value),
                "clavesitiooriginal" => muestreo => !muestreo.ClaveSitioOriginal.EndsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.EndsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.EndsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.EndsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.EndsWith(value),
                "tipohomologado" => muestreo => !muestreo.TipoHomologado.EndsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        #endregion

        #region NumericExpressions
        public static Expression<Func<RegistroOriginalDto, bool>> GetGreaterThanExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => Convert.ToInt64(muestreo.NumeroCarga) > value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetGreaterThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => Convert.ToInt64(muestreo.NumeroCarga) >= value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetLessThanExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => Convert.ToInt64(muestreo.NumeroCarga) < value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetLessThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => Convert.ToInt64(muestreo.NumeroCarga) <= value,
                _ => muestreo => true
            };
        }
        #endregion

        #region DateExpression
        public static Expression<Func<RegistroOriginalDto, bool>> GetBeforeExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) < value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetAfterExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) > value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetBeforeOrEqualExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) <= value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetAfterOrEqualExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) >= value,
                _ => muestreo => true
            };
        }
        #endregion

        #region OrderBy
        public static Expression<Func<RegistroOriginalDto, object>> GetOrderByExpression(string column)
        {
            return column.ToLower() switch
            {
                "numerocarga" => muestreo => muestreo.NumeroCarga,
                "clavesitio" => muestreo => muestreo.ClaveSitio,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo,
                "tipositio" => muestreo => muestreo.TipoSitio,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua,
                "laboratorio" => muestreo => muestreo.Laboratorio,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion,
                _ => muestreo => muestreo.ClaveMonitoreo
            };
        }
        #endregion

        public static Expression<Func<RegistroOriginalDto, bool>> GetFilterParameterExpression(string parameter, string value, string conditional)
        {
            return conditional switch
            {
                "equals" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && s.Resultado == value),
                "notequals" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && s.Resultado != value),
                "beginswith" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && s.Resultado.StartsWith(value)),
                "notbeginswith" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && !s.Resultado.StartsWith(value)),
                "endswith" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && s.Resultado.EndsWith(value)),
                "notendswith" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && !s.Resultado.EndsWith(value)),
                "contains" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && s.Resultado.Contains(value)),
                "notcontains" => muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && !s.Resultado.Contains(value)),
            };
        }

        public static Expression<Func<RegistroOriginalDto, bool>> GetFilterParameterExpression(string parameter, List<string> values)
        {
            return muestreo => muestreo.Parametros.Any(s => s.ClaveParametro == parameter.ToUpper() && values.Contains(s.Resultado));
        }
    }
}