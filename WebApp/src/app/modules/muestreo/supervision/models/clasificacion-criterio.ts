import { Criterio } from './criterio';

export interface ClasificacionCriterio {
  id: number;
  descripcion: string;
  criterios: Array<Criterio>;
}
