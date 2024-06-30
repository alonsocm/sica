using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ValidacionEvidencia
{
    /// <summary>
    /// Identificador principal de la tabla ValidacionEvidencia
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Muestreo
    /// </summary>
    public long MuestreoId { get; set; }

    /// <summary>
    /// Campo que describe si se cumplieron las evidencias esperadas
    /// </summary>
    public bool CumpleEvidenciasEsperadas { get; set; }

    /// <summary>
    /// Campo que describe si existe el folio en el criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool FolioBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió la fecha de realización referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleFechaRealizacionBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió el Tiempo de muestreo referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleTiempoMuestreoBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con la clave CONALAB referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleClaveConalabbm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con la clave muestreo referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleClaveMuestreoBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con el líder de la brigada referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleLiderBrigadaBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con la clave brigada referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleClaveBrigadaBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con la geocerca referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CumpleGeocercaBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con la calibración y/o verificación de equipos al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool CalibracionVerificacionEquiposBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumplió con el registro de resultados de campo al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool RegistroResultadosCampoBm { get; set; }

    /// <summary>
    /// Campo que describe si es firmado y cancelado referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool FirmadoyCanceladoBm { get; set; }

    /// <summary>
    /// Campo que describe si existe fotografía GPS punto de muestreo referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool FotografiaGpspuntoMuestreoBm { get; set; }

    /// <summary>
    /// Campo que describe si existe registros visibles referente al criterio de bitácora de muestreo (BM)
    /// </summary>
    public bool RegistrosVisiblesBm { get; set; }

    /// <summary>
    /// Campo que describe si se cumple los metadatos referente a los criterios foto de muestreo (FM)
    /// </summary>
    public bool CumpleMetadatosFm { get; set; }

    /// <summary>
    /// Campo que verifica el líder de brigada y cuerpo de agua referente a los criterios foto de muestreo (FM)
    /// </summary>
    public bool LiderBrigadaCuerpoAguaFm { get; set; }

    /// <summary>
    /// Campo que indica si existe foto única referente a los criterios foto de muestreo (FM)
    /// </summary>
    public bool FotoUnicaFm { get; set; }

    /// <summary>
    /// Campo que indica si se cumple los metadatos referente a los criterios foto de muestras (FS)
    /// </summary>
    public bool CumpleMetadatosFs { get; set; }

    /// <summary>
    /// Campo que verifica el registro de los recipientes referente a los criterios foto de muestras (FS)
    /// </summary>
    public bool RegistroRecipientesFs { get; set; }

    /// <summary>
    /// Campo que verifica las muestras preservadas  referente a los criterios foto de muestras (FS)
    /// </summary>
    public bool MuestrasPreservadasFs { get; set; }

    /// <summary>
    /// Campo que verifica la foto única referente a los criterios foto de muestras (FS)
    /// </summary>
    public bool FotoUnicaFs { get; set; }

    /// <summary>
    /// Campo que indica si se cumplió con los metadatos referente a los criterios foto de aforo (FA)
    /// </summary>
    public bool CumpleMetadatosFa { get; set; }

    /// <summary>
    /// Campo que indica si se cumplió la metodología referente a los criterios foto de aforo (FA)
    /// </summary>
    public bool MetodologiaFa { get; set; }

    /// <summary>
    /// Campo que verifica la foto única referente a los criterios foto de aforo (FA)
    /// </summary>
    public bool FotoUnicaFa { get; set; }

    /// <summary>
    /// Campo indica si fue llenado el formato correctamente referente al criterio formato de aforo (FF)
    /// </summary>
    public bool FormatoLlenadoCorrectoFf { get; set; }

    /// <summary>
    /// Campo indica si se cumple la geocerca referente al criterio formato de aforo (FF)
    /// </summary>
    public bool CumpleGeocercaFf { get; set; }

    /// <summary>
    /// Campo verifica si los registros son legibles referente al criterio formato de aforo (FF)
    /// </summary>
    public bool RegistrosLegiblesFf { get; set; }

    /// <summary>
    /// Campo que indica si se cumple con las placas de la unidad referente al criterio Track de ruta (TR)
    /// </summary>
    public bool CumplePlacasTr { get; set; }

    /// <summary>
    /// Campo que indica si se cumple con la clave CONALAB referente al criterio Track de ruta (TR)
    /// </summary>
    public bool CumpleClaveConalabtr { get; set; }

    /// <summary>
    /// Campo que indica si el llenado fue correcto referente al criterio cadena de custodia (CC)
    /// </summary>
    public bool LlenadoCorrectoCc { get; set; }

    /// <summary>
    /// Capo que indica si los registros son legibles referente al criterio cadena de custodia (CC)
    /// </summary>
    public bool RegistrosLegiblesCc { get; set; }

    /// <summary>
    /// Campo que describe si es rechazado 
    /// </summary>
    public bool Rechazo { get; set; }

    /// <summary>
    /// Campo que describe las observaciones de rechazo
    /// </summary>
    public string ObservacionesRechazo { get; set; } = null!;

    /// <summary>
    /// Campo que describe el porcentaje de pago
    /// </summary>
    public int PorcentajePago { get; set; }

    /// <summary>
    /// Campo que registra la fecha en la que se realizó la validación
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Llave foránea que hace relación a la tabla de Usuarios describiendo el usuario que realizo dicha validación
    /// </summary>
    public long UsuarioValidoId { get; set; }

    /// <summary>
    /// Llave foánea que hace referencia a la tabla de AvisoRealizacion
    /// </summary>
    public long? AvisoRealizacionId { get; set; }

    public virtual AvisoRealizacion? AvisoRealizacion { get; set; }

    public virtual Muestreo Muestreo { get; set; } = null!;

    public virtual Usuario UsuarioValido { get; set; } = null!;
}
