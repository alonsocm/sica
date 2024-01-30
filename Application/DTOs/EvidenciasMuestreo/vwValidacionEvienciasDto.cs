using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.EvidenciasMuestreo
{
    public class vwValidacionEvienciasDto
    {
        public string ClaveMuestreo { get; set; } = null!;

        public string Sitio { get; set; } = null!;

        public string ClaveConalab { get; set; } = null!;

        public string TipoCuerpoAgua { get; set; } = null!;

        public string? Laboratorio { get; set; }

        public bool ConEventualidades { get; set; }

        public DateTime FechaProgramadaVisita { get; set; }

        public DateTime? FechaRealVisita { get; set; }

        public string BrigadaProgramaMuestreo { get; set; } = null!;

        public bool ConQcmuestreo { get; set; }

        public string TipoSupervision { get; set; } = null!;

        public string? TipoEventualidad { get; set; }

        public DateTime? FechaReprogramacion { get; set; }

        public int? EvidenciasEsperadas { get; set; }

        public int? TotalEvidencias { get; set; }

        public string CumpleEvidencias { get; set; } = null!;

        public DateTime FechaRealizacion { get; set; }

        public string CumpleFechaRealizacion { get; set; } = null!;

        public TimeSpan? HoraIncioMuestreo { get; set; }

        public TimeSpan? HoraFinMuestreo { get; set; }

        public int? TiempoMinimoMuestreo { get; set; }

        public string ClaveConalbaArm { get; set; } = null!;

        public string CumpleClaveConalab { get; set; } = null!;

        public string ClaveMuestreoArm { get; set; } = null!;

        public string CumpleClaveMuestreo { get; set; } = null!;

        public string? LiderBrigadaArm { get; set; }

        public string? LiderBrigadaBase { get; set; }

        public string ClaveBrigadaArm { get; set; } = null!;

        public string CumpleClaveBrigada { get; set; } = null!;

        public string? PlacasDeMuestreo { get; set; }

        public double Lat1MuestreoPrograma { get; set; }

        public double Log1MuestreoPrograma { get; set; }

        public string? LatSitioResultado { get; set; }

        public string? LongSitioResultado { get; set; }

        public List<PuntosMuestreoDto> lstPuntosMuestreo { get; set; }
        public List<InformacionEvidenciaDto> lstEvidencias { get; set; }

        public vwValidacionEvienciasDto()
        {
            this.ConEventualidades = false;
            this.FechaProgramadaVisita = DateTime.MaxValue;
            this.FechaRealizacion = DateTime.MaxValue;
            this.ConQcmuestreo = false;
            this.Lat1MuestreoPrograma = 0;
            this.Log1MuestreoPrograma = 0;
            this.lstPuntosMuestreo = new List<PuntosMuestreoDto>();
            this.lstEvidencias = new List<InformacionEvidenciaDto>();
        }
    }
}
