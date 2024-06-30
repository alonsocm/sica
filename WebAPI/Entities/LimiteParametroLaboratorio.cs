using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class LimiteParametroLaboratorio
{
    /// <summary>
    /// Identificador principal de la tabla LimiteParametrosLaboratorio
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Parámetros indicando el parámetro
    /// </summary>
    public long ParametroId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Laboratorios indicando el laboratorio que debería de revisar dicho parámetro
    /// </summary>
    public long LaboratorioId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de AccionLaboratorio
    /// </summary>
    public int? RealizaLaboratorioMuestreoId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Laboratorios indicando el laboratorio que realiza el muestreo
    /// </summary>
    public long? LaboratorioMuestreoId { get; set; }

    /// <summary>
    /// Campo que indica el periodo
    /// </summary>
    public int? Periodo { get; set; }

    /// <summary>
    /// Campo que indica si es activo, es una bandera del histórico
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    /// Campo que indica el LDMA a cumplir
    /// </summary>
    public string? LdmaCumplir { get; set; }

    /// <summary>
    /// Campo que indica el LPC a cumplir
    /// </summary>
    public string? LpcaCumplir { get; set; }

    /// <summary>
    /// Campo que indica si muestra
    /// </summary>
    public bool? LoMuestra { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de AccionLaboratorio indicando si lo subroga
    /// </summary>
    public int? LoSubrogaId { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Laboratorios indicando el Laboratorio subrogado
    /// </summary>
    public long? LaboratorioSubrogaId { get; set; }

    /// <summary>
    /// Campo que indica el método analítico
    /// </summary>
    public string? MetodoAnalitico { get; set; }

    /// <summary>
    /// Campo que indica el limite de LDM
    /// </summary>
    public string? Ldm { get; set; }

    /// <summary>
    /// Campo que indica el limite del LPC
    /// </summary>
    public string? Lpc { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo ProgramaAnio indicando el año al que pertenece el limite de dicho parámetro
    /// </summary>
    public long AnioId { get; set; }

    public virtual ProgramaAnio Anio { get; set; } = null!;

    public virtual Laboratorios Laboratorio { get; set; } = null!;

    public virtual Laboratorios? LaboratorioMuestreo { get; set; }

    public virtual Laboratorios? LaboratorioSubroga { get; set; }

    public virtual AccionLaboratorio? LoSubroga { get; set; }

    public virtual ParametrosGrupo Parametro { get; set; } = null!;

    public virtual AccionLaboratorio? RealizaLaboratorioMuestreo { get; set; }
}
