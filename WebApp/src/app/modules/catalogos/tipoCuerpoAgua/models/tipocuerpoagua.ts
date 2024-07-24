import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface tipocuerpoagua extends Row {
  id: number;
  descripcion: string;
  tipohomologadoid:number;
  tipoHomologadoIdDescripcion:string;
  activo:Boolean;
  freuencia: string;
  evidenciaesperada:number;
  tiempominimomuestreo:number;
}