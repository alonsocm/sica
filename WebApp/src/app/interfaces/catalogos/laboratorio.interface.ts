import { Row } from "../../modules/muestreo/formatoResultado/models/row";

export interface Laboratorio extends Row {
  id: number | null,
  descripcion: string,
  nomenclatura:string
}
