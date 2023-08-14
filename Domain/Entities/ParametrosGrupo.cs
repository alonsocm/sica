using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParametrosGrupo
{
    public long Id { get; set; }

    public string ClaveParametro { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public long? IdSubgrupo { get; set; }

    public long? IdUnidadMedida { get; set; }

    public long? Orden { get; set; }

    public int? GrupoParametroId { get; set; }

    public bool? EsLdm { get; set; }

    public virtual ICollection<FormaReporteEspecifica> FormaReporteEspecifica { get; set; } = new List<FormaReporteEspecifica>();

    public virtual GrupoParametro? GrupoParametro { get; set; }

    public virtual SubgrupoAnalitico? IdSubgrupoNavigation { get; set; }

    public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }

    public virtual ICollection<LimiteParametroLaboratorio> LimiteParametroLaboratorio { get; set; } = new List<LimiteParametroLaboratorio>();

    public virtual ICollection<MuestreoEmergencia> MuestreoEmergencia { get; set; } = new List<MuestreoEmergencia>();

    public virtual ICollection<ParametrosCostos> ParametrosCostos { get; set; } = new List<ParametrosCostos>();

    public virtual ICollection<ParametrosReglasNoRelacion> ParametrosReglasNoRelacion { get; set; } = new List<ParametrosReglasNoRelacion>();

    public virtual ICollection<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; } = new List<ParametrosSitioTipoCuerpoAgua>();

    public virtual ICollection<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; } = new List<ReglasLaboratorioLdmLpc>();

    public virtual ICollection<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; } = new List<ReglasMinimoMaximo>();

    public virtual ICollection<ReglasRelacionParametro> ReglasRelacionParametro { get; set; } = new List<ReglasRelacionParametro>();

    public virtual ICollection<ReglasReporte> ReglasReporte { get; set; } = new List<ReglasReporte>();

    public virtual ICollection<ResultadoMuestreo> ResultadoMuestreo { get; set; } = new List<ResultadoMuestreo>();
}
