using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosGrupo
{
    /// <summary>
    /// Identificador principal de catálogo ParametrosGrupo
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave de parámetro
    /// </summary>
    public string ClaveParametro { get; set; } = null!;

    /// <summary>
    /// Campo que describe el nombre del parámetro
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de SubgrupoAnalitico
    /// </summary>
    public long? IdSubgrupo { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de UnidadMedida
    /// </summary>
    public long? IdUnidadMedida { get; set; }

    /// <summary>
    /// Campo que indica el orden del parámetro, en este orden se mostrara en la tabla en forma horizontal
    /// </summary>
    public long? Orden { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de GrupoParametro
    /// </summary>
    public int? GrupoParametroId { get; set; }

    /// <summary>
    /// Campo que indica si se tomara el limite de LDM
    /// </summary>
    public bool? EsLdm { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la misma tabla indicando si este parámetro pertenece a un parámetro padre para la relación de suma de parámetros 
    /// </summary>
    public long? ParametroPadreId { get; set; }

    public virtual ICollection<FormaReporteEspecifica> FormaReporteEspecifica { get; set; } = new List<FormaReporteEspecifica>();

    public virtual GrupoParametro? GrupoParametro { get; set; }

    public virtual SubgrupoAnalitico? IdSubgrupoNavigation { get; set; }

    public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }

    public virtual ICollection<ParametrosGrupo> InverseParametroPadre { get; set; } = new List<ParametrosGrupo>();

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorio { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<MuestreoEmergencia> MuestreoEmergencia { get; set; } = new List<MuestreoEmergencia>();

    public virtual ParametrosGrupo? ParametroPadre { get; set; }

    public virtual ICollection<ParametrosCostos> ParametrosCostos { get; set; } = new List<ParametrosCostos>();

    public virtual ICollection<ParametrosReglasNoRelacion> ParametrosReglasNoRelacion { get; set; } = new List<ParametrosReglasNoRelacion>();

    public virtual ICollection<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; } = new List<ParametrosSitioTipoCuerpoAgua>();

    public virtual ICollection<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; } = new List<ReglasLaboratorioLdmLpc>();

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();

    public virtual ICollection<ReglasRelacionParametro> ReglasRelacionParametro { get; set; } = new List<ReglasRelacionParametro>();

    public virtual ICollection<ReglasReporte> ReglasReporte { get; set; } = new List<ReglasReporte>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
