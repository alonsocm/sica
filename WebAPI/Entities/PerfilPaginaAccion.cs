using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PerfilPaginaAccion
{
    /// <summary>
    /// Identificador principal del catálogo PerfilPaginaAccion
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace refrencia al catálogo de PerfilPagina
    /// </summary>
    public long IdPerfilPagina { get; set; }

    /// <summary>
    /// Llave foránea que hace refrencia al catálogo de Accion
    /// </summary>
    public long IdAccion { get; set; }

    /// <summary>
    /// Campo que indica el estatus del registro
    /// </summary>
    public bool Estatus { get; set; }

    public virtual Accion IdAccionNavigation { get; set; } = null!;

    public virtual PerfilPagina IdPerfilPaginaNavigation { get; set; } = null!;
}
