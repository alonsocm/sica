using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class EvidenciaReplica
    {
        public long Id { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string ClaveUnica { get; set; } = null!;
        public byte[]? Archivo { get; set; }

        public virtual ResultadoMuestreo ResultadoMuestreo { get; set; } = null!;
    }
}
