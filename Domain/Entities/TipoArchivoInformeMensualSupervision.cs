using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoArchivoInformeMensualSupervision
{
    public int Id { get; set; }

    public byte[] Descripcion { get; set; } = null!;

    public virtual ICollection<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; } = new List<ArchivoInformeMensualSupervision>();
}
