namespace Domain.Entities;

public partial class AccionLaboratorio
{
    public int Id { get; set; }

    public string LoSubroga { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioLoSubroga { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorioRealizaLaboratorioMuestreo { get; set; } = new List<LimiteParametroLaboratorio>();
}
