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
}
