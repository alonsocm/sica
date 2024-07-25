using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class LimitesParametroLaboratorioDto
    {
        public long Id { get; set; }
        public string ClaveParametro { get; set; }
        public string NombreParametro { get; set; }
        public long ParametroId { get; set; }
        public string Laboratorio { get; set; }
        public long LaboratorioId { get; set; }
        public string? RealizaLaboratorioMuestreo { get; set; }
        public int? RealizaLaboratorioMuestreoId { get; set; }
        public string? LaboratorioMuestreo { get; set; }
        public long? LaboratorioMuestreoId { get; set; }
        public int? Periodo { get; set; }
        public bool Activo { get; set; }
        public string? LDMaCumplir { get; set; }
        public string? LPCaCumplir { get; set; }
        public bool? LoMuestra { get; set; }
        public string? LoSubroga { get; set; }
        public int? LoSubrogaId { get; set; }
        public string? LaboratorioSubrogado { get; set; }
        public long? LaboratorioSubrogadoId { get; set; }
        public string? MetodoAnalitico { get; set; }
        public string? LDM { get; set; }
        public string? LPC { get; set; }
        public string? Anio { get; set; }
        public long? AnioId { get; set; }


    }
}
