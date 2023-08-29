﻿namespace Application.DTOs
{
    public class ResultadoParaSustitucionLimitesDto
    {
        public long IdMuestreo { get; set; }
        public long IdResultado { get; set; }
        public long IdParametro { get; set; }
        public string ClaveParametro { get; set; }
        public string ValorOriginal { get; set; }
        public string ValorSustituido { get; set; }
        public long LaboratorioId { get; set; }
        public long? LaboratorioSubrogadoId { get; set; }
        public int Anio { get; set; }
        public string? LaboratorioMuestreo { get; set; }
        public string? LaboratorioSubrogado { get; set; }
        public bool esSustitucionLaboratorio { get; set; }
    }
}
