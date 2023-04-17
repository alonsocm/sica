using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RevisionReplicasDto : ReplicasExcel
    {
        public string SeAceptaRechazo { get; set; }
        public string ResultadoReplica { get; set; }
        public string ObservacionLaboratorio { get; set; }
        public string NombreArchivoEvidencia { get; set; }
        public int Linea { get; set; }
    }
}
