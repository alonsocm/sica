namespace Application.DTOs.Users
{
    public class CargaMuestreoDto
    {
        public long Id { get; set; }
        public string Muestreo { get; set; } = null!;
        public string ClaveConalab { get; set; } = null!;
        public string Claveconagua { get; set; } = null!;
        //public DateTime FechaProgramadaVisita { get; set; }
        public string FechaRealVisita { get; set; }
        public string HoraInicioMuestreo { get; set; }
        public string HoraFinMuestreo { get; set; }
        public string TipoCuerpoAgua { get; set; } = null!;
        public string SubtipoCuerpoAgua { get; set; } = null!;
        public string LaboratorioBasedeDatos { get; set; } = null!;
        public string LaboratorioRealizoMuestreo { get; set; } = null!;
        public string LaboratorioSubrogado { get; set; } = null!;
        public string GrupodeParametro { get; set; } = null!;
        public string SubgrupoParametro { get; set; } = null!;
        public string ClaveParametro { get; set; } = null!;
        public string Parametro { get; set; } = null!;
        public string UnidadMedida { get; set; } = null!;
        public string? Resultado { get; set; }
        public string? Lpclaboratorio { get; set; }
        public string? Ldmlaboratorio { get; set; }
        public string? ObservacionesLaboratorio { get; set; }
        public string? NoVecesLiberadoConagua { get; set; }
        public string? PreciodeParametro { get; set; }
        public string? ObservacionesConagua { get; set; }
        public string? ObservacionesConagua2 { get; set; }
        public string? ObservacionesConagua3 { get; set; }
        public string? AnioOperacion { get; set; }
        public string? IdResultado { get; set; }
        public string FechaEntrega { get; set; }
        public string NoCarga { get; set; }
        public int? NoEntrega { get; set; }
        public int Linea { get; set; }
    }
}
