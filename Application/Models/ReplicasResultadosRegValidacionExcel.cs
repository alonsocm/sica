using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ReplicasResultadosRegValidacionExcel
    {
        public string NumCarga { get; set; }
        public string ClaveUnica { get; set; }
        public string ClaveSitio { get; set; }
        public string ClaveMonitoreo { get; set; }
        public string Nombre { get; set; }
        public string ClaveParametro { get; set; }
        public string Laboratorio { get; set; }
        public string TipoCuerpoAgua { get; set; }
        public string TipoHomologado { get; set; }
        public string Resultado { get; set; }
        public string CorrectoResultadoReglaValidacion { get; set; }
        public string ObservacionReglaValidacion { get; set; }
        public string AceptaRechazo { get; set; }
        public string ResultadoReplica { get; set; } = null!;
        public string MismoResultado { get; set; }
        public string ObservacionLaboratorio { get; set; } = null!;
        public DateTime FechaReplicaLaboratorio { get; set; }
        public string? ObservacionSrenameca { get; set; }
        public string? EsDatoCorrectoSrenameca { get; set; }
        public DateTime? FechaObservacionSrenameca { get; set; }
        public string? ObservacionesReglasReplica { get; set; }
        public string? ApruebaResultadoReplica { get; set; }
        public DateTime FechaEstatusFinal { get; set; }
        public string UsuarioReviso { get; set; }
        public string Estatus { get; set; }
    }
}
