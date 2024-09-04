using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Catalogos
{
    public class ReplicasResultadosReglasValidacionDto
    {
        public long Id { get; set; }
        public string NumeroCarga { get; set; }

        
        public long ResultadoMuestreoId { get; set; }

        public bool AceptaRechazo { get; set; }

        public string ResultadoReplica { get; set; } = null!;

        public bool MismoResultado { get; set; }

        public string ObservacionLaboratorio { get; set; } = null!;

        public DateTime FechaReplicaLaboratorio { get; set; }

        public string? ObservacionSrenameca { get; set; }

        public bool? EsDatoCorrectoSrenameca { get; set; }

        public DateTime? FechaObservacionSrenameca { get; set; }

        public string? ObservacionesReglasReplica { get; set; }

        public bool? ApruebaResultadoReplica { get; set; }

        public DateTime FechaEstatusFinal { get; set; }

        public long UsuarioIdReviso { get; set; }

        public List<EvidenciaDto> Evidencias { get; set; }

        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoHomologado { get; set; }
        public string Resultado { get; set; }
        public bool CorrectoResultadoReglaValidacion { get; set; }
        public string ObservacionReglaValidacion { get; set; }

        public int EstatusResultadoId { get; set; }

        public ReplicasResultadosReglasValidacionDto()
        {
            Evidencias = new List<EvidenciaDto>();
            ResultadoReplica = string.Empty;
            ObservacionLaboratorio = string.Empty;


        }
    }

    
   
}
