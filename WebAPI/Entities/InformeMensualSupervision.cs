using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class InformeMensualSupervision
{
    /// <summary>
    /// Identificador principal de la tabla InformeMensualSupervisión
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Campo que describe el número de memorando de la plantilla
    /// </summary>
    public string Memorando { get; set; } = null!;

    /// <summary>
    /// Campo que describe el lugar del informe mensual
    /// </summary>
    public string Lugar { get; set; } = null!;

    /// <summary>
    /// Campo que describe la fecha que se mostrara en el reporte
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Directorio para describir el director o responsable de calidad del organismo de cuenca o dirección local
    /// </summary>
    public int DirectorioFirmaId { get; set; }

    /// <summary>
    /// Campo que describe las iniciales de las personas involucradas separadas por una &quot;/&quot;
    /// </summary>
    public string Iniciales { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación al catálogo de Mes el cual describe el mes que se reporta el informe mensual
    /// </summary>
    public int MesId { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que se capturo el reporte
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario registrando el usuario que creo el reporte
    /// </summary>
    public long UsuarioRegistroId { get; set; }

    /// <summary>
    /// Campo que indica el año al que pertenece el informe
    /// </summary>
    public int Anio { get; set; }

    public virtual ICollection<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; } = new List<ArchivoInformeMensualSupervision>();

    public virtual ICollection<CopiaInformeMensualSupervision> CopiaInformeMensualSupervision { get; set; } = new List<CopiaInformeMensualSupervision>();

    public virtual Directorio DirectorioFirma { get; set; } = null!;

    public virtual Mes Mes { get; set; } = null!;

    public virtual Usuario UsuarioRegistro { get; set; } = null!;
}
