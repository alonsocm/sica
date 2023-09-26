using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class InformeMensualSupervision
{
    public long Id { get; set; }

    public string Memorando { get; set; } = null!;

    public string Lugar { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int DirectorioFirmaId { get; set; }

    public string Iniciales { get; set; } = null!;

    public int MesId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public long UsuarioRegistroId { get; set; }

    public virtual ICollection<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; } = new List<ArchivoInformeMensualSupervision>();

    public virtual ICollection<CopiaInformeMensualSupervision> CopiaInformeMensualSupervision { get; set; } = new List<CopiaInformeMensualSupervision>();

    public virtual Directorio DirectorioFirma { get; set; } = null!;

    public virtual Mes Mes { get; set; } = null!;

    public virtual Usuario UsuarioRegistro { get; set; } = null!;
}
