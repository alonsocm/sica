import { ArchivoSupervision } from './archivo-supervision';
import { ClasificacionCriterio } from './clasificacion-criterio';

export interface Supervision {
  id?: number;
  fechaMuestreo?: Date;
  horaInicio?: string;
  horaTermino?: string;
  horaTomaMuestra?: string;
  puntajeObtenido?: number;
  organismosDireccionesRealizaId?: string;
  organismoCuencaReportaId?: string;
  supervisorConagua?: string;
  sitioId?: number;
  claveMuestreo?: string;
  latitudToma?: number;
  longitudToma?: number;
  laboratorioRealizaId?: string;
  responsableTomaId?: string;
  responsableMedicionesId?: string;
  observacionesMuestreo?: string;
  claveSitio?: string;
  nombreSitio?: string;
  tipoCuerpoAgua?: string;
  latitudSitio?: number;
  longitudSitio?: number;
  coordenadasMuestra?: string;
  clasificaciones?: Array<ClasificacionCriterio>;
  archivoPdfSupervision?: any;
  archivosEvidencias?: Array<any>;
  archivos?: Array<ArchivoSupervision>;
}
