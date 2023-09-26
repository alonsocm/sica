using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PlantillaInformeMensualSupervision
{
    public int Id { get; set; }

    public string Contrato { get; set; } = null!;

    public string DenominacionContrato { get; set; } = null!;

    public string SitiosMiniMax { get; set; } = null!;

    public string Indicaciones { get; set; } = null!;

    public string Anio { get; set; } = null!;

    public string ImagenPiePagina { get; set; } = null!;

    public string ImagenEncabezado { get; set; } = null!;
}
