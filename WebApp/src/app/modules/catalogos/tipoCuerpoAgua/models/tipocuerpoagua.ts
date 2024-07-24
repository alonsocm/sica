import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface tipocuerpoagua extends Row {
  id: number;
  descripcion: string;
  tipoHomologadoId:number;
  tipoHomologadoDescripcion:string;
  activo:Boolean;
  frecuencia: string;
  evidenciaEsperada:number;
  tiempoMinimoMuestreo:number;
}