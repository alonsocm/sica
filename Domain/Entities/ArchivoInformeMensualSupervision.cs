using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ArchivoInformeMensualSupervision
{
    /// <summary>
    /// Identificador principal de la tabla ArchivoInformeMensualSupervision
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la tabla de InformeMensualSupervision
    /// </summary>
    public long InformeMensualSupervisionId { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al catálogo de TipoArchivoInformeMensualSupervision 
    /// </summary>
    public int TipoArchivoInformeMensualSupervisionId { get; set; }

    /// <summary>
    /// Campo que describe el nombre del archivo
    /// </summary>
    public string NombreArchivo { get; set; } = null!;

    /// <summary>
    /// Campo que describe el archivo en formato varbinary
    /// </summary>
    public byte[] Archivo { get; set; } = null!;

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuario describiendo el usuario que cargo el archivo
    /// </summary>
    public long UsuarioCargaId { get; set; }

    /// <summary>
    /// Campo que describe la fecha en la que se cargo el archivo
    /// </summary>
    public DateTime FechaCarga { get; set; }

    public virtual InformeMensualSupervision InformeMensualSupervision { get; set; } = null!;

    public virtual Usuario UsuarioCarga { get; set; } = null!;
}
