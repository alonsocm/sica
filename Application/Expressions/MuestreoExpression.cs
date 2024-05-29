using Application.DTOs;
using Application.Wrappers;
using System.Linq.Expressions;

namespace Application.Expressions
{
    static internal class MuestreoExpression
    {
        private static Expression<Func<MuestreoDto, bool>> GetExpression(Filter filter)
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
        public static List<Expression<Func<MuestreoDto, bool>>> GetExpressionList(List<Filter> filters)
        {
            List<Expression<Func<MuestreoDto, bool>>> expressions = new();

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Conditional))
                {
                    expressions.Add(MuestreoExpression.GetExpression(filter));
                }
                else
                {
                    if (filter.Values != null && filter.Values.Any())
                    {
                        expressions.Add(MuestreoExpression.GetContainsExpression(filter.Column, filter.Values));
                    }
                }
            }

            return expressions;
        }

        #region StringExpressions
        public static Expression<Func<MuestreoDto, bool>> GetExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "estatus" => muestreo => muestreo.Estatus == value,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega == value,
                "clavesitio" => muestreo => muestreo.ClaveSitio == value,
                "claveMonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "tipositio" => muestreo => muestreo.TipoSitio == value,
                "nombresitio" => muestreo => muestreo.NombreSitio == value,
                "ocdl" => muestreo => muestreo.OCDL == value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua == value,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua == value,
                "programaanual" => muestreo => muestreo.ProgramaAnual == value,
                "laboratorio" => muestreo => muestreo.Laboratorio == value,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado == value,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada == value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion == value,
                "horainicio" => muestreo => muestreo.HoraInicio == value,
                "horafin" => muestreo => muestreo.HoraFin == value,
                "fechacarga" => muestreo => muestreo.FechaCarga == value,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo == value,
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetContainsExpression(string column, List<string> value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => value.Contains(muestreo.Estatus),
                "numeroentrega" => muestreo => value.Contains(muestreo.NumeroEntrega),
                "clavesitio" => muestreo => value.Contains(muestreo.ClaveSitio),
                "clavemonitoreo" => muestreo => value.Contains(muestreo.ClaveMonitoreo),
                "tipositio" => muestreo => value.Contains(muestreo.TipoSitio),
                "nombresitio" => muestreo => value.Contains(muestreo.NombreSitio),
                "ocdl" => muestreo => value.Contains(muestreo.OCDL),
                "tipocuerpoagua" => muestreo => value.Contains(muestreo.TipoCuerpoAgua),
                "subtipocuerpoagua" => muestreo => value.Contains(muestreo.SubTipoCuerpoAgua),
                "programaanual" => muestreo => value.Contains(muestreo.ProgramaAnual),
                "laboratorio" => muestreo => value.Contains(muestreo.Laboratorio),
                "laboratoriosubrogado" => muestreo => value.Contains(muestreo.LaboratorioSubrogado),
                "fechaprogramada" => muestreo => value.Contains(muestreo.FechaProgramada),
                "fecharealizacion" => muestreo => value.Contains(muestreo.FechaRealizacion),
                "horainicio" => muestreo => value.Contains(muestreo.HoraInicio),
                "horafin" => muestreo => value.Contains(muestreo.HoraFin),
                "fechacarga" => muestreo => value.Contains(muestreo.FechaCarga),
                "fechaentregamuestreo" => muestreo => value.Contains(muestreo.FechaEntregaMuestreo),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.Contains(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.Contains(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.Contains(value),
                "tipositio" => muestreo => muestreo.TipoSitio.Contains(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.Contains(value),
                "ocdl" => muestreo => muestreo.OCDL.Contains(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.Contains(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.Contains(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.Contains(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.Contains(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.Contains(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.Contains(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.Contains(value),
                "horainicio" => muestreo => muestreo.HoraInicio.Contains(value),
                "horafin" => muestreo => muestreo.HoraFin.Contains(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.Contains(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetNotContainsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.Contains(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.Contains(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.Contains(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.Contains(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.Contains(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.Contains(value),
                "ocdl" => muestreo => !muestreo.OCDL.Contains(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.Contains(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.Contains(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.Contains(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.Contains(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.Contains(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.Contains(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.Contains(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.Contains(value),
                "horafin" => muestreo => !muestreo.HoraFin.Contains(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.Contains(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.Contains(value),
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }

        public static Expression<Func<MuestreoDto, bool>> GetEqualsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus == value,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega == value,
                "clavesitio" => muestreo => muestreo.ClaveSitio == value,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo == value,
                "tipositio" => muestreo => muestreo.TipoSitio == value,
                "nombresitio" => muestreo => muestreo.NombreSitio == value,
                "ocdl" => muestreo => muestreo.OCDL == value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua == value,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua == value,
                "programaanual" => muestreo => muestreo.ProgramaAnual == value,
                "laboratorio" => muestreo => muestreo.Laboratorio == value,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado == value,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada == value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion == value,
                "horainicio" => muestreo => muestreo.HoraInicio == value,
                "horafin" => muestreo => muestreo.HoraFin == value,
                "fechacarga" => muestreo => muestreo.FechaCarga == value,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo == value,    
                _ => muestreo => muestreo.ClaveMonitoreo == ""
            };
        }

        public static Expression<Func<MuestreoDto, bool>> GetNotEqualsExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus != value,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega != value,
                "clavesitio" => muestreo => muestreo.ClaveSitio != value,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo != value,
                "tipositio" => muestreo => muestreo.TipoSitio != value,
                "nombresitio" => muestreo => muestreo.NombreSitio != value,
                "ocdl" => muestreo => muestreo.OCDL != value,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua != value,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua != value,
                "programaanual" => muestreo => muestreo.ProgramaAnual != value,
                "laboratorio" => muestreo => muestreo.Laboratorio != value,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado != value,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada != value,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion != value,
                "horainicio" => muestreo => muestreo.HoraInicio != value,
                "horafin" => muestreo => muestreo.HoraFin != value,
                "fechacarga" => muestreo => muestreo.FechaCarga != value,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo != value,
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.StartsWith(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.StartsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.StartsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.StartsWith(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.StartsWith(value),
                "ocdl" => muestreo => muestreo.OCDL.StartsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.StartsWith(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.StartsWith(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.StartsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.StartsWith(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.StartsWith(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.StartsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.StartsWith(value),
                "horainicio" => muestreo => muestreo.HoraInicio.StartsWith(value),
                "horafin" => muestreo => muestreo.HoraFin.StartsWith(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.StartsWith(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetNotBeginsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.StartsWith(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.StartsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.StartsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.StartsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.StartsWith(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.StartsWith(value),
                "ocdl" => muestreo => !muestreo.OCDL.StartsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.StartsWith(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.StartsWith(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.StartsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.StartsWith(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.StartsWith(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.StartsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.StartsWith(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.StartsWith(value),
                "horafin" => muestreo => !muestreo.HoraFin.StartsWith(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.StartsWith(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.StartsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus.EndsWith(value),
                "numeroentrega" => muestreo => muestreo.NumeroEntrega.EndsWith(value),
                "clavesitio" => muestreo => muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo.EndsWith(value),
                "tipositio" => muestreo => muestreo.TipoSitio.EndsWith(value),
                "nombresitio" => muestreo => muestreo.NombreSitio.EndsWith(value),
                "ocdl" => muestreo => muestreo.OCDL.EndsWith(value),
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua.EndsWith(value),
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua.EndsWith(value),
                "programaanual" => muestreo => muestreo.ProgramaAnual.EndsWith(value),
                "laboratorio" => muestreo => muestreo.Laboratorio.EndsWith(value),
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado.EndsWith(value),
                "fechaprogramada" => muestreo => muestreo.FechaProgramada.EndsWith(value),
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion.EndsWith(value),
                "horainicio" => muestreo => muestreo.HoraInicio.EndsWith(value),
                "horafin" => muestreo => muestreo.HoraFin.EndsWith(value),
                "fechacarga" => muestreo => muestreo.FechaCarga.EndsWith(value),
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetNotEndsWithExpression(string column, string value)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => !muestreo.Estatus.EndsWith(value),
                "numeroentrega" => muestreo => !muestreo.NumeroEntrega.EndsWith(value),
                "clavesitio" => muestreo => !muestreo.ClaveSitio.EndsWith(value),
                "clavemonitoreo" => muestreo => !muestreo.ClaveMonitoreo.EndsWith(value),
                "tipositio" => muestreo => !muestreo.TipoSitio.EndsWith(value),
                "nombresitio" => muestreo => !muestreo.NombreSitio.EndsWith(value),
                "ocdl" => muestreo => !muestreo.OCDL.EndsWith(value),
                "tipocuerpoagua" => muestreo => !muestreo.TipoCuerpoAgua.EndsWith(value),
                "subtipocuerpoagua" => muestreo => !muestreo.SubTipoCuerpoAgua.EndsWith(value),
                "programaanual" => muestreo => !muestreo.ProgramaAnual.EndsWith(value),
                "laboratorio" => muestreo => !muestreo.Laboratorio.EndsWith(value),
                "laboratoriosubrogado" => muestreo => !muestreo.LaboratorioSubrogado.EndsWith(value),
                "fechaprogramada" => muestreo => !muestreo.FechaProgramada.EndsWith(value),
                "fecharealizacion" => muestreo => !muestreo.FechaRealizacion.EndsWith(value),
                "horainicio" => muestreo => !muestreo.HoraInicio.EndsWith(value),
                "horafin" => muestreo => !muestreo.HoraFin.EndsWith(value),
                "fechacarga" => muestreo => !muestreo.FechaCarga.EndsWith(value),
                "fechaentregamuestreo" => muestreo => !muestreo.FechaEntregaMuestreo.EndsWith(value),
                _ => muestreo => muestreo.ClaveMonitoreo != ""
            };
        }
        #endregion

        #region NumericExpressions
        public static Expression<Func<MuestreoDto, bool>> GetGreaterThanExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) > value,
                "programaanual" => muestreo => Convert.ToInt64(muestreo.ProgramaAnual) > value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetGreaterThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) >= value,
                "programaanual" => muestreo => Convert.ToInt64(muestreo.ProgramaAnual) >= value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetLessThanExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) < value,
                "programaaAnual" => muestreo => Convert.ToInt64(muestreo.ProgramaAnual) < value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetLessThanOrEqualToExpression(string column, int value)
        {
            return column.ToLower() switch
            {
                "numeroentrega" => muestreo => Convert.ToInt64(muestreo.NumeroEntrega) <= value,
                "programaanual" => muestreo => Convert.ToInt64(muestreo.ProgramaAnual) <= value,
                _ => muestreo => true
            };
        }
        #endregion

        #region DateExpression
        public static Expression<Func<MuestreoDto, bool>> GetBeforeExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fechaprogramada" => muestreo => DateTime.Parse(muestreo.FechaProgramada) < value,
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) < value,
                "fechacarga" => muestreo => DateTime.Parse(muestreo.FechaCarga) < value,
                "fechaentregamuestreo" => muestreo => DateTime.Parse(muestreo.FechaEntregaMuestreo) < value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetAfterExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fechaprogramada" => muestreo => DateTime.Parse(muestreo.FechaProgramada) > value,
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) > value,
                "fechacarga" => muestreo => DateTime.Parse(muestreo.FechaCarga) > value,
                "fechaentregamuestreo" => muestreo => DateTime.Parse(muestreo.FechaEntregaMuestreo) > value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetBeforeOrEqualExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fechaprogramada" => muestreo => DateTime.Parse(muestreo.FechaProgramada) <= value,
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) <= value,
                "fechacarga" => muestreo => DateTime.Parse(muestreo.FechaCarga) <= value,
                "fechaentregamuestreo" => muestreo => DateTime.Parse(muestreo.FechaEntregaMuestreo) <= value,
                _ => muestreo => true
            };
        }
        public static Expression<Func<MuestreoDto, bool>> GetAfterOrEqualExpression(string column, DateTime value)
        {
            return column.ToLower() switch
            {
                "fechaprogramada" => muestreo => DateTime.Parse(muestreo.FechaProgramada) >= value,
                "fecharealizacion" => muestreo => DateTime.Parse(muestreo.FechaRealizacion) >= value,
                "fechacarga" => muestreo => DateTime.Parse(muestreo.FechaCarga) >= value,
                "fechaentregamuestreo" => muestreo => DateTime.Parse(muestreo.FechaEntregaMuestreo) >= value,
                _ => muestreo => true
            };
        }
        #endregion

        #region OrderBy
        public static Expression<Func<MuestreoDto, object>> GetOrderByExpression(string column)
        {
            return column.ToLower() switch
            {
                "estatus" => muestreo => muestreo.Estatus,
                "numeroentrega" => muestreo => muestreo.NumeroEntrega,
                "clavesitio" => muestreo => muestreo.ClaveSitio,
                "clavemonitoreo" => muestreo => muestreo.ClaveMonitoreo,
                "tipositio" => muestreo => muestreo.TipoSitio,
                "nombresitio" => muestreo => muestreo.NombreSitio,
                "ocdl" => muestreo => muestreo.OCDL,
                "tipocuerpoagua" => muestreo => muestreo.TipoCuerpoAgua,
                "subtipocuerpoagua" => muestreo => muestreo.SubTipoCuerpoAgua,
                "programaanual" => muestreo => muestreo.ProgramaAnual,
                "laboratorio" => muestreo => muestreo.Laboratorio,
                "laboratoriosubrogado" => muestreo => muestreo.LaboratorioSubrogado,
                "fechaprogramada" => muestreo => muestreo.FechaProgramada,
                "fecharealizacion" => muestreo => muestreo.FechaRealizacion,
                "horainicio" => muestreo => muestreo.HoraInicio,
                "horafin" => muestreo => muestreo.HoraFin,
                "fechacarga" => muestreo => muestreo.FechaCarga,
                "fechaentregamuestreo" => muestreo => muestreo.FechaEntregaMuestreo,
                _ => muestreo => muestreo.ClaveMonitoreo
            };
        }
        #endregion
    }
}
