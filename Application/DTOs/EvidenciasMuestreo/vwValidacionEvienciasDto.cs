using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.EvidenciasMuestreo
{
    public class vwValidacionEvienciasDto
    {
        public long muestreoId { get; set; }
        public string? ClaveMuestreo { get; set; } = null!;

        public string? Sitio { get; set; } = null!;

        public string? ClaveConalab { get; set; } = null!;

        public string? TipoCuerpoAgua { get; set; } = null!;

        public string? Laboratorio { get; set; }

        public bool ConEventualidades { get; set; }

        public DateTime FechaProgramadaVisita { get; set; }

        public DateTime? FechaRealVisita { get; set; }

        public string? BrigadaProgramaMuestreo { get; set; } = null!;

        public bool ConQcmuestreo { get; set; }

        public string? TipoSupervision { get; set; } = null!;

        public string? TipoEventualidad { get; set; }

        public DateTime? FechaReprogramacion { get; set; }

        public int? EvidenciasEsperadas { get; set; }

        public int? TotalEvidencias { get; set; }

        public string? CumpleEvidencias { get; set; } = null!;

        public DateTime FechaRealizacion { get; set; }

        public string? CumpleFechaRealizacion { get; set; } = null!;

        public TimeSpan? HoraIncioMuestreo { get; set; }

        public TimeSpan? HoraFinMuestreo { get; set; }

        public int? TiempoMinimoMuestreo { get; set; }
        public int? Calculo { get; set; }

        public string? CumpleTiempoMuestreo { get; set; } = null!;

        public string? ClaveConalbaArm { get; set; } = null!;

        public string? CumpleClaveConalab { get; set; } = null!;

        public string? ClaveMuestreoArm { get; set; } = null!;

        public string? CumpleClaveMuestreo { get; set; } = null!;

        public string? LiderBrigadaArm { get; set; }

        public string? LiderBrigadaBase { get; set; }

        public string? ClaveBrigadaArm { get; set; } = null!;

        public string? CumpleClaveBrigada { get; set; } = null!;

        public string? PlacasDeMuestreo { get; set; }

        public double Lat1MuestreoPrograma { get; set; }

        public double Log1MuestreoPrograma { get; set; }

        public string? LatSitioResultado { get; set; }

        public string? LongSitioResultado { get; set; }

        public List<PuntosMuestreoDto> lstPuntosMuestreo { get; set; }
        public List<InformacionEvidenciaDto> lstEvidencias { get; set; }

        public bool folioBM { get; set; }
   


   public bool cumpleLiderBrigadaBM { get; set; }
   public bool cumpleGeocercaBM{ get; set; }
   public bool calibracionVerificacionEquiposBM{ get; set; }
   public bool registroResultadosCampoBM{ get; set; }
   public bool firmadoyCanceladoBM{ get; set; }
   public bool fotografiaGPSPuntoMuestreoBM{ get; set; }
   public bool registrosVisiblesBM{ get; set; }
   public bool cumpleMetadatosFM{ get; set; }
   public bool liderBrigadaCuerpoAguaFM{ get; set; }
   public bool fotoUnicaFM{ get; set; }
   public bool cumpleMetadatosFS{ get; set; }
   public bool registroRecipientesFS{ get; set; }
   public bool muestrasPreservadasFS{ get; set; }
   public bool fotoUnicaFS{ get; set; }
   public bool cumpleMetadatosFA{ get; set; }
   public bool metodologiaFA{ get; set; }
   public bool fotoUnicaFA{ get; set; }
   public bool formatoLlenadoCorrectoFF{ get; set; }
   public bool cumpleGeocercaFF{ get; set; }
   public bool registrosLegiblesFF{ get; set; }
   public bool llenadoCorrectoCC{ get; set; }
   public bool registrosLegiblesCC{ get; set; }
   public bool rechazo{ get; set; }
   public string observacionesRechazo{ get; set; }
        public bool cumplePlacasTr { get; set; }
        public bool cumpleClaveConalabTr { get; set; }



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
            this.observacionesRechazo = string.Empty;
        }
    }
}
