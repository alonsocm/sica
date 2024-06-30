using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PerfilPagina
{
    /// <summary>
    /// Identificador principal del catálogo PerfilPagina
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Perfil
    /// </summary>
    public long IdPerfil { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de Pagina
    /// </summary>
    public long IdPagina { get; set; }

    /// <summary>
    /// Campo que describe el estatus del registro
    /// </summary>
    public bool Estatus { get; set; }

    public virtual Pagina IdPaginaNavigation { get; set; } = null!;

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual ICollection<PerfilPaginaAccion> PerfilPaginaAccion { get; set; } = new List<PerfilPaginaAccion>();
}
