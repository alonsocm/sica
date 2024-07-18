﻿
namespace Application.DTOs
{
    public class RegistroOriginalDto
    {
        public long MuestreoId { get; set; }
        public string Anio { get; set; } = string.Empty;
        public string NumeroCarga { get; set; }
        public string ClaveSitioOriginal { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string FechaRealizacion { get; set; }
        public string Laboratorio { get; set; }
        public long TipoCuerpoAguaId { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public long TipoHomologadoId { get; set; }
        public string TipoHomologado { get; set; }
        public string TipoSitio { get; set; }
        public int EstatusId { get; set; }
        public IEnumerable<ParametroDto> Parametros { get; set; }

        public string Estatus { get; set; }
    }
}