namespace Domain.Entities;

public partial class EvidenciaSupervisionMuestreo
{
    public long Id { get; set; }

    public long SupervisionMuestreoId { get; set; }

    public long TipoEvidenciaId { get; set; }

    public string NombreArchivo { get; set; } = null!;
    public virtual SupervisionMuestreo SupervisionMuestreo { get; set; } = null!;

    public virtual TipoEvidenciaMuestreo TipoEvidencia { get; set; } = null!;
}
