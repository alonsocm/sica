using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReglasLaboratorioLdmLpc
{
    /// <summary>
    /// Identificador principal de tabla de ReglasLaboratorioLDM_LPC
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Clave conformada por la concatenación del laboratorio con el parámetro
    /// </summary>
    public string ClaveUnicaLabParametro { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace referencia al catálogo Laboratorios
    /// </summary>
    public long LaboratorioId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo ParametrosGrupo
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Campo que describe el LDM
    /// </summary>
    public string Ldm { get; set; } = null!;

    /// <summary>
    /// Campo que describe el LPC
    /// </summary>
    public string Lpc { get; set; } = null!;

    /// <summary>
    /// Campo que describe si es LDM
    /// </summary>
    public bool? EsLdm { get; set; }

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual ParametrosGrupo Parametro { get; set; } = null!;
}
