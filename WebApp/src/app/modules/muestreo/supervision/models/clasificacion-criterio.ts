import { Criterio } from './criterio';

export interface ClasificacionCriterio {
  numero: number;
  descripcion: string;
  criterios: Array<Criterio>;
}
