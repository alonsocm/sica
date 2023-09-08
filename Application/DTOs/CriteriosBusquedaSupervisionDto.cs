using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CriteriosBusquedaSupervisionDto
    {
        public long? OrganismosDireccionesRealizaId { get; set; }
        public long? SitioId { get; set; }
        public string? FechaMuestreo { get; set; }
        public decimal? PuntajeObtenido { get; set; }
        public long? LaboratorioRealizaId { get; set; }
        public string? ClaveMuestreo { get; set; }
        public long? TipoCuerpoAguaId { get; set; }

        
    }
}
