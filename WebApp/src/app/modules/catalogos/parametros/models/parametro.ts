import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface Parametro extends Row {
  id: number;
  clave: string;
  descripcion: string;
  grupo: string;
  subGrupo: string;
  unidadMedida: string;
  orden: number;
  parametroPadre: string;
}
