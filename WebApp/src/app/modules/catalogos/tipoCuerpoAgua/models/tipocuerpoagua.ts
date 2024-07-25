import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface TipoCuerpoAgua extends Row {
  id: number;
  descripcion: string;
  tipoHomologadoId:number;
  tipoHomologadoDescripcion:string;
  activo:Boolean;
  frecuencia: string;
  evidenciasEsperadas:number;
  tiempoMinimoMuestreo:number;
  selected:boolean;
}
interface TipoHomologado {
  id: number;
  descripcion: string;
  activo: boolean;
}