import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface TipoCuerpoAgua extends Row {
  id: number;
  descripcion: string;
  tipoHomologadoId:number | null;
  tipoHomologadoDescripcion:string;
  activo:boolean;
  frecuencia:string;
  evidenciasEsperadas:number;
  tiempoMinimoMuestreo:number;
  selected:boolean;
}
export interface TipoHomologado extends Row {
  id: number;
  descripcion: string;
  activo: boolean;
  selected:boolean;
}
export interface PostTipoCuerpoAgua extends Row{
  id: number;
  descripcion: string;
  tipoHomologadoId:number;
  tipoHomologadoDescripcion:string;
  activo:boolean;
  frecuencia:string;
  evidenciasEsperadas:number;
  tiempoMinimoMuestreo:number;
  selected:boolean;
}
