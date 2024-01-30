using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ValidacionEvidenciaRepository : Repository<ValidacionEvidencia>, IValidacionEvidenciaRepository
    {
        public ValidacionEvidenciaRepository(SicaContext context) : base(context)
        {
        }

        public ValidacionEvidencia ConvertirValidacionEvidencia(vwValidacionEvienciasDto validacionMuestreo, long usuarioId)
        {



            ValidacionEvidencia validacionEvidencia = new ValidacionEvidencia();
            validacionEvidencia.MuestreoId = validacionMuestreo.muestreoId;
            validacionEvidencia.CumpleEvidenciasEsperadas = (validacionMuestreo.CumpleEvidencias == "SI") ? true : false;
            validacionEvidencia.FolioBm = validacionMuestreo.folioBM;
            validacionEvidencia.CumpleFechaRealizacionBm = (validacionMuestreo.CumpleFechaRealizacion == "SI") ? true : false;
            validacionEvidencia.CumpleTiempoMuestreoBm = (validacionMuestreo.CumpleTiempoMuestreo == "SI") ? true : false;
            validacionEvidencia.CumpleClaveConalabbm = (validacionMuestreo.CumpleClaveConalab == "SI") ? true : false;
            validacionEvidencia.CumpleClaveMuestreoBm = (validacionMuestreo.CumpleClaveMuestreo == "SI") ? true : false;
            validacionEvidencia.CumpleLiderBrigadaBm = validacionMuestreo.cumpleLiderBrigadaBM;
            validacionEvidencia.CumpleClaveBrigadaBm = (validacionMuestreo.CumpleClaveBrigada == "SI") ? true : false;
            validacionEvidencia.CumpleGeocercaBm = validacionMuestreo.cumpleGeocercaBM;
            validacionEvidencia.CalibracionVerificacionEquiposBm = validacionMuestreo.calibracionVerificacionEquiposBM;
            validacionEvidencia.RegistroResultadosCampoBm = validacionMuestreo.registroResultadosCampoBM;
            validacionEvidencia.FirmadoyCanceladoBm = validacionMuestreo.firmadoyCanceladoBM;
            validacionEvidencia.FotografiaGpspuntoMuestreoBm = validacionMuestreo.fotografiaGPSPuntoMuestreoBM;
            validacionEvidencia.RegistrosVisiblesBm = validacionMuestreo.registrosVisiblesBM;
            validacionEvidencia.CumpleMetadatosFm = validacionMuestreo.cumpleMetadatosFM;
            validacionEvidencia.LiderBrigadaCuerpoAguaFm = validacionMuestreo.liderBrigadaCuerpoAguaFM;
            validacionEvidencia.FotoUnicaFm = validacionMuestreo.fotoUnicaFM;
            validacionEvidencia.CumpleMetadatosFs = validacionMuestreo.cumpleMetadatosFS;
            validacionEvidencia.RegistroRecipientesFs = validacionMuestreo.registroRecipientesFS;
            validacionEvidencia.MuestrasPreservadasFs = validacionMuestreo.muestrasPreservadasFS;
            validacionEvidencia.FotoUnicaFs = validacionMuestreo.fotoUnicaFS;
            validacionEvidencia.CumpleMetadatosFa = validacionMuestreo.cumpleMetadatosFA;
            validacionEvidencia.MetodologiaFa = validacionMuestreo.metodologiaFA;
            validacionEvidencia.FotoUnicaFa = validacionMuestreo.fotoUnicaFA;
            validacionEvidencia.FormatoLlenadoCorrectoFf = validacionMuestreo.formatoLlenadoCorrectoFF;
            validacionEvidencia.CumpleGeocercaFf = validacionMuestreo.cumpleGeocercaFF;
            validacionEvidencia.RegistrosLegiblesFf = validacionMuestreo.registrosLegiblesFF;
            validacionEvidencia.CumplePlacasTr = validacionMuestreo.cumplePlacasTr;
            validacionEvidencia.CumpleClaveConalabtr = validacionMuestreo.cumpleClaveConalabTr;
            validacionEvidencia.LlenadoCorrectoCc = validacionMuestreo.llenadoCorrectoCC;
            validacionEvidencia.RegistrosLegiblesCc = validacionMuestreo.registrosLegiblesCC;
            validacionEvidencia.Rechazo = validacionMuestreo.rechazo;
            validacionEvidencia.ObservacionesRechazo = validacionMuestreo.observacionesRechazo;
            validacionEvidencia.PorcentajePago = (!validacionMuestreo.rechazo && !validacionMuestreo.ConEventualidades) ? 100 : 0;
            validacionEvidencia.FechaRegistro = DateTime.Now;
      
            return validacionEvidencia;
        }

    }
}
