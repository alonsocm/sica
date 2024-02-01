using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ValidacionEvidencia
{
    public long Id { get; set; }

    public long MuestreoId { get; set; }

    public bool CumpleEvidenciasEsperadas { get; set; }

    public bool FolioBm { get; set; }

    public bool CumpleFechaRealizacionBm { get; set; }

    public bool CumpleTiempoMuestreoBm { get; set; }

    public bool CumpleClaveConalabbm { get; set; }

    public bool CumpleClaveMuestreoBm { get; set; }

    public bool CumpleLiderBrigadaBm { get; set; }

    public bool CumpleClaveBrigadaBm { get; set; }

    public bool CumpleGeocercaBm { get; set; }

    public bool CalibracionVerificacionEquiposBm { get; set; }

    public bool RegistroResultadosCampoBm { get; set; }

    public bool FirmadoyCanceladoBm { get; set; }

    public bool FotografiaGpspuntoMuestreoBm { get; set; }

    public bool RegistrosVisiblesBm { get; set; }

    public bool CumpleMetadatosFm { get; set; }

    public bool LiderBrigadaCuerpoAguaFm { get; set; }

    public bool FotoUnicaFm { get; set; }

    public bool CumpleMetadatosFs { get; set; }

    public bool RegistroRecipientesFs { get; set; }

    public bool MuestrasPreservadasFs { get; set; }

    public bool FotoUnicaFs { get; set; }

    public bool CumpleMetadatosFa { get; set; }

    public bool MetodologiaFa { get; set; }

    public bool FotoUnicaFa { get; set; }

    public bool FormatoLlenadoCorrectoFf { get; set; }

    public bool CumpleGeocercaFf { get; set; }

    public bool RegistrosLegiblesFf { get; set; }

    public bool CumplePlacasTr { get; set; }

    public bool CumpleClaveConalabtr { get; set; }

    public bool LlenadoCorrectoCc { get; set; }

    public bool RegistrosLegiblesCc { get; set; }

    public bool Rechazo { get; set; }

    public string ObservacionesRechazo { get; set; } = null!;

    public int PorcentajePago { get; set; }

    public DateTime FechaRegistro { get; set; }

    public long UsuarioValidoId { get; set; }

    public long? AvisoRealizacionId { get; set; }

    public virtual AvisoRealizacion? AvisoRealizacion { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual Usuario UsuarioValido { get; set; } = null!;
}
