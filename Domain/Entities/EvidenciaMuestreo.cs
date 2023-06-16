using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class EvidenciaMuestreo
    {
        public long Id { get; set; }
        public long MuestreoId { get; set; }
        public long TipoEvidenciaMuestreoId { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public byte[]? Archivo { get; set; }

        public virtual Muestreo Muestreo { get; set; } = null!;
        public virtual TipoEvidenciaMuestreo TipoEvidenciaMuestreo { get; set; } = null!;
    }
}
