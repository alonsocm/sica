using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime Creado { get; set; }
        public string? UltimaModificacionPor { get; set; }
        public DateTime? UltimaModificacion { get; set; }
    }
}
