import { Row } from 'src/app/modules/muestreo/formatoResultado/models/row';

export interface Parametro extends Row {
  id: number;
  clave: string;
  descripcion: string;
  grupo: string;
  grupoId: number;
  subGrupo: string;
  subgrupoId: number;
  unidadMedida: string;
  unidadMedidaId: number;
  orden: number;
  parametroPadre: string;
  parametroPadreId: number;
}
