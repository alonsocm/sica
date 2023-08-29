import { ClasificacionCriterio } from './clasificacion-criterio';

export interface Supervision {
  clasificaciones: Array<ClasificacionCriterio>;
  archivoPdfSupervision?: any;
  archivosEvidencias?: Array<any>;
}
