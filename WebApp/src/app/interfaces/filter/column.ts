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
  datype: string;
  datosSeleccionados: string; //Cadena donde se guarda las opciones seleccionadas
  opctionFiltro?: string;
  segundaopctionFiltro?: string;
  filtroEspecial?: string;
  segundofiltroEspecial?: string;
  esUltimoFiltro?: boolean;
}
