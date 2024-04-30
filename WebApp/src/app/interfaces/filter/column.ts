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
}
