using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasReporte
{
    /// <summary>
    /// Identificador principal de la tabla ReglasReporte
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe la clave de la regla
    /// </summary>
    public string ClaveRegla { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catalogo ClasificacionRegla
    /// </summary>
    public long ClasificacionReglaId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catalogo de ParametroGrupo
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a cero
    /// 
    /// </summary>
    public bool EsValidoResultadoCero { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a &lt;0
    /// </summary>
    public bool EsValidoMenorCero { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a espacios en blanco
    /// </summary>
    public bool EsValidoEspaciosBlanco { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a un NA
    /// </summary>
    public bool EsValidoResultadoNa { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a un NE
    /// </summary>
    public bool EsValidoResultadoNe { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a un IM
    /// </summary>
    public bool EsValidoResultadoIm { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a que sea &lt;LD
    /// </summary>
    public bool EsValidoResultadoMenorLd { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a que sea &lt;CMC
    /// </summary>
    public bool EsValidoResultadoMenorCmc { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a un ND
    /// </summary>
    public bool EsValidoResultadoNd { get; set; }

    /// <summary>
    /// Bandera que indica si el resultado es valido a que sea &lt;LPC
    /// </summary>
    public bool EsValidoResultadoMenorLpc { get; set; }

    public virtual ClasificacionRegla ClasificacionRegla { get; set; } = null!;

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual ICollection<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; } = new List<ReglaReporteResultadoTca>();
}
