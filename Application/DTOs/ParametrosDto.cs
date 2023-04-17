using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ParametrosDto
    {
        public long Id { get; set; }
        public long MuestreoId { get; set; }
        public string Resulatdo { get; set; }
        public string ClaveParametro { get; set; }

        public string? ObservacionesLaboratorio { get; set; }
        public string ObservacionesOCDL { get; set; }
        public long? ObservacionesOCDLId { get; set; }


        public string ObservacionesSECAIA { get; set; }
        public long? ObservacionesSECAIAId { get; set; }

        public bool? IsCorrecto { get; set; }
        public string NombreParametro { get; set; }
        public string Descripcion { get; set; } = null!;
        public string ClaveUnica { get; set; }

        public long Orden { get; set; }

        public ParametrosDto()
        {
            this.Id = 0;
            this.MuestreoId = 0;
            this.Resulatdo = string.Empty;
            this.ClaveParametro = string.Empty;
            this.ObservacionesLaboratorio = string.Empty;
            this.NombreParametro = string.Empty;
            this.ObservacionesOCDL = string.Empty;
            this.IsCorrecto = false;
            this.ClaveUnica = string.Empty;
            this.Orden = 0;
            this.Descripcion = string.Empty;
            this.ObservacionesSECAIA = string.Empty;
        }


    }
}
