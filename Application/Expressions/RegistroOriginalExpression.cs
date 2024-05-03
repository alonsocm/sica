using Application.DTOs;
using System.Linq.Expressions;

namespace Application.Expressions
{
    static internal class RegistroOriginalExpression
    {
        #region StringExpressions
        public static Expression<Func<RegistroOriginalDto, bool>> GetExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => muestreo.NumeroEntrega == value,
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
                "numeroentrega" => muestreo => value.Contains(muestreo.NumeroEntrega),
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
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.Contains(value),
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
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.Contains(value),
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
                "numeroentrega" => muestreo => muestreo.NumeroEntrega != value,
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
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.StartsWith(value),
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
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.StartsWith(value),
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
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.EndsWith(value),
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
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.EndsWith(value),
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
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) > value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetGreaterThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) >= value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetLessThanExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) < value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<RegistroOriginalDto, bool>> GetLessThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) <= value,
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
                "numeroentrega" => muestreo => muestreo.NumeroEntrega,
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
    }
}
