using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PlantillaInformeMensualSupervision
{
    /// <summary>
    /// Identificador principal del catálogo Plantilla
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Campo que describe el contrato
    /// </summary>
    public string Contrato { get; set; } = null!;

    /// <summary>
    /// Campo que describe la denominación del contrato
    /// </summary>
    public string DenominacionContrato { get; set; } = null!;

    /// <summary>
    /// Campo que describe el mínimo y máximo de sitios
    /// </summary>
    public string SitiosMiniMax { get; set; } = null!;

    /// <summary>
    /// Campo que indica las indicaciones
    /// </summary>
    public string Indicaciones { get; set; } = null!;

    /// <summary>
    /// Campo que indica el año
    /// </summary>
    public string Anio { get; set; } = null!;

    /// <summary>
    /// Campo que indica la imagen de pie de página
    /// </summary>
    public string ImagenPiePagina { get; set; } = null!;

    /// <summary>
    /// Campo que indica la imagen del encabezado
    /// </summary>
    public string ImagenEncabezado { get; set; } = null!;
}
