using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pagina
{
    /// <summary>
    /// Identificador principal del catálogo Pagina
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a esta misma tabla indicando la página padre
    /// </summary>
    public long? IdPaginaPadre { get; set; }

    /// <summary>
    /// Campo que indica el nombre de la página, nombre que se mostrara en el menú
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Campoq ue indica la url de la página
    /// </summary>
    public string Url { get; set; } = null!;

    /// <summary>
    /// Campo que indica el orden de la página
    /// </summary>
    public long Orden { get; set; }

    /// <summary>
    /// Campo qe indica si se encuentra activa la página
    /// </summary>
    public bool Activo { get; set; }

    public virtual Pagina? IdPaginaPadreNavigation { get; set; }

    public virtual ICollection<Pagina> InverseIdPaginaPadreNavigation { get; set; } = new List<Pagina>();

    public virtual ICollection<PerfilPagina> PerfilPagina { get; set; } = new List<PerfilPagina>();
}
