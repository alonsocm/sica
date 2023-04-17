using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AprobacionResultadoMuestreoDto
    {
        public long MuestreoId { get; set; }
        public long ParametroId { get; set; }
        public int? EstatusResultadoId { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public string NoEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoCuerpoAguaOriginal { get; set; }
        public string Resultado { get; set; }
        public string? EsCorrectoOCDL { get; set; }
        public string? ObservacionOCDL { get; set; }
        public string? EsCorrectoSECAIA { get; set; }
        public string? ObservacionSECAIA { get; set; }
        public string? ClasificacionObservacion { get; set; }
        public long? AprobacionResulMuestreoId { get; set; }
        public string? ApruebaResultado { get; set; }
        public string? ComentariosAprobacionResultados { get; set; }
        public string? FechaAprobRechazo { get; set; }
        public long? UsuarioRevisionId { get; set; }
        public string? UsuarioRevision { get; set; }        
        public string estatusResultado { get; set; }  

    }
    public class ResultadosconEstatus
    {
        public string No_Entrega { get; set; }
        public string Clave_Unica { get; set; }
        public string Clave_Sitio { get; set; }
        public string Clave_Monitoreo { get; set; }
        public string Nombre_Sitio { get; set; }
        public string Clave_Parametro { get; set; }
        public string Laboratorio { get; set; }
        public string Tipo_Cuerpo_Agua { get; set; }
        public string Tipo_Cuerpo_Agua_Original { get; set; }
        public string Resultado { get; set; }
        public string Es_Correcto_OCDL { get; set; }
        public string? Observacion_OCDL { get; set; }
        public string? Es_Correcto_SECAIA { get; set; }
        public string? Observacion_SECAIA { get; set; }
        public string? Clasificacion_Observacion { get; set; }        
        public string? Aprueba_Resultado { get; set; }
        public string? Comentarios_Aprobacion_Resultados { get; set; }
    }

    public class GeneralDescarga
    {
        public string No_Entrega { get; set; }
        public string Clave_Unica { get; set; }
        public string Clave_Sitio { get; set; }
        public string Clave_Monitoreo { get; set; }
        public string Nombre_Sitio { get; set; }
        public string Clave_Parametro { get; set; }
        public string Laboratorio { get; set; }
        public string Tipo_Cuerpo_Agua { get; set; }
        public string Tipo_Cuerpo_Agua_Original { get; set; }
        public string Resultado { get; set; }
        public string Es_Correcto_OCDL { get; set; }
        public string? Observacion_OCDL { get; set; }
        public string? Es_Correcto_SECAIA { get; set; }
        public string? Observacion_SECAIA { get; set; }
        public string? Clasificacion_Observacion { get; set; }
        public string? Aprueba_Resultado { get; set; }
        public string? Comentarios_Aprobacion_Resultados { get; set; }
        public string? Fecha_Aprobación { get; set; }
        public string? Usuario { get; set; }
        public string? estatusResultado { get; set; }

        public GeneralDescarga()
        {
            this.estatusResultado = string.Empty;
        }
    }

    public class CargaRevisionModel
    {
        public IFormFile formFile { get; set; }
        public string usuarioId { get; set; }
    }

    public class GeneralDescargaDiferente
    {
        public string NoEntrega { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string NombreSitio { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoCuerpoAguaOriginal { get; set; }
        public string ResultadoActualizadoporReplica { get; set; }
        public string Es_CorrectoOCDL { get; set; }
        public string ObservacionOCDL { get; set; }
        public string EsCorrectoSECAIA { get; set; }
        public string ObservacionSECAIA { get; set; }
        public string ClasificacionObservacion { get; set; }
        public string ObservacionSRENAMECA { get; set; }
        public string ComentariosAprobacionResultados { get; set; }
        public int Linea { get; set; }

    }


    public class GeneralDescargaDiferenteExcel
    {
        public string? No_Entrega { get; set; }
        public string? Clave_Unica { get; set; }
        public string? Clave_Sitio { get; set; }
        public string? Clave_Monitoreo { get; set; }
        public string? Nombre_Sitio { get; set; }
        public string? Clave_Parametro { get; set; }
        public string? Laboratorio { get; set; }
        public string? Tipo_Cuerpo_Agua { get; set; }
        public string? Tipo_Cuerpo_Agua_Original { get; set; }
        public string? Resultado_Actualizado_por_Replica { get; set; }
        public string? Es_Correcto_OCDL { get; set; }
        public string? Observacion_OCDL { get; set; }
        public string? Es_Correcto_SECAIA { get; set; }
        public string? Observacion_SECAIA { get; set; }
        public string? Clasificacion_Observacion { get; set; }
        public string? ObservacionSRENAMECA { get; set; }
        public string? Comentarios_Aprobacion_Resultados { get; set; }
    }


    public class GeneralDescargaDiferenteDto
    {
        public string? NoEntrega { get; set; }
        public string? ClaveUnica { get; set; }
        public string? ClaveSitio { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? NombreSitio { get; set; }
        public string? ClaveParametro { get; set; }
        public string? Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string? ResultadoActualizadoporReplica { get; set; }
        public string? EsCorrectoOCDL { get; set; }
        public string? ObservacionOCDL { get; set; }
        public string? EsCorrectoSECAIA { get; set; }
        public string? ObservacionSECAIA { get; set; }
        public string? ClasificacionObservacion { get; set; }
        public string? ObservacionSRENAMECA { get; set; }
        public string? ComentariosAprobacionResultados { get; set; }
        public string? FechaObservacionSRENAMECA { get; set; }
        public string? SeApruebaResultadodespuesdelaReplica { get; set; }
        public string? FechaEstatusFinal { get; set; }
        public string? UsuarioRevision { get; set; }
        public string? Estatus { get; set; }
        
        
    }

    public class ReplicaDiferenteObtenerDto
    {
        public long MuestreoId { get; set; }
        public long ParametroId { get; set; }
        public int? EstatusResultadoId { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public string? NoEntrega { get; set; }
        public string? ClaveUnica { get; set; }
        public string? ClaveSitio { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? NombreSitio { get; set; }
        public string? ClaveParametro { get; set; }
        public string? Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string? ResultadoActualizadoporReplica { get; set; }
        public string? EsCorrectoOCDL { get; set; }
        public string? ObservacionOCDL { get; set; }
        public string? EsCorrectoSECAIA { get; set; }
        public string? ObservacionSECAIA { get; set; }
        public string? ClasificacionObservacion { get; set; }
        public string? ObservacionSRENAMECA { get; set; }
        public string? ComentariosAprobacionResultados { get; set; }
        public string? FechaObservacionSRENAMECA { get; set; }
        public string? SeApruebaResultadodespuesdelaReplica { get; set; }
        public string? FechaEstatusFinal { get; set; }
        public string? UsuarioRevision { get; set; }
        public long? UsuarioRevisionId { get; set; }
        public string? estatusResultado { get; set; }


    }

}
