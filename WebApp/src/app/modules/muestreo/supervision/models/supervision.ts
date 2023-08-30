import { ClasificacionCriterio } from './clasificacion-criterio';

export interface Supervision {
  fechaMuestreo?: Date;
  horaInicio?: string;
  horaFin?: string;
  horaTomaMuestra?: string;
  puntajeObtenido?: Date;
  ocdlRealiza?: string;
  nombreSupervisor?: string;
  ocdlReporta?: string;
  claveSitio?: string;
  claveMuestreo?: string;
  nombreSitio?: string;
  tipoCuerpoAgua?: string;
  latitudSitio?: number;
  longitudSitio?: number;
  latitudToma?: number;
  longitudToma?: number;
  coordenadasMuestra?: string;
  laboratorio?: string;
  nombreResponsableMuestra?: string;
  nombreResponsableMediciones?: string;
  observacionesMuestreo?: string;
  clasificaciones: Array<ClasificacionCriterio>;
  archivoPdfSupervision?: any;
  archivosEvidencias?: Array<any>;
}
