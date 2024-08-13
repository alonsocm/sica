import { Item } from './item';

export interface Column {
  name: string;
  label: string;
  order: number;
  data: Array<Item>;
  filteredData: Array<Item>;
  selectAll: boolean;
  filtered: boolean;
  asc?: boolean;
  desc?: boolean;
  dataType: string;
  selectedData: string; //Cadena donde se guarda las opciones seleccionadas
  optionFilter?: string;
  secondOptionFilter?: string;
  specialFilter?: string;
  secondSpecialFilter?: string;
  isLatestFilter?: boolean;
  pinned?: boolean;
  //#region Propiedades para establecer el ancho de una columna. Cuando son columnas fijas.
  width?: number;
  startAt?: number;
  //#endregion
  parameter?: boolean; //Indicamos si la columna es un parámetro. Con esto sabemos que el endpoint a consultar, es diferente.
  color?: string; //Propiedad para establecer el color del encabezado
}
