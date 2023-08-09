namespace Application.DTOs
{
    public class CargaMuestreoEmergenciaDto
    {
        public string Numero { get; set; }
        public string NombreEmergencia { get; set; }
        public string ClaveUnica { get; set; } = null!;
        public string IdLaboratorio { get; set; } = null!;
        public string Sitio { get; set; } = null!;
        public string FechaProgramada { get; set; }
        public string FechaRealVisita { get; set; }
        public string HoraMuestreo { get; set; }
        public string TipoCuerpoAgua { get; set; } = null!;
        public string SubtipoCuerpoAgua { get; set; } = null!;
        public string LaboratorioRealizoMuestreo { get; set; } = null!;
        public string LaboratorioSubrogado { get; set; } = null!;
        public string GrupoParametro { get; set; } = null!;
        public string ClaveParametro { get; set; } = null!;
        public string Parametro { get; set; } = null!;
        public string Resultado { get; set; }
        public string UnidadMedida { get; set; }
        public int Linea { get; set; }
    }
}
