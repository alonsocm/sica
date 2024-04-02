import { Filter, FilterFinal } from "./filtro.interface";

export interface Columna {
    nombre: string;
    etiqueta: string;
    orden: number;
  filtro: Filter;


}


export interface Column {
  nombre: string;
  etiqueta: string;
  orden: number;
  filtro: FilterFinal;
  esfiltrado: boolean;
  filtrobusqueda: any[];
 

  
}

export class FiltroBusqueda {
  valor: string;
  checked: boolean;
  constructor() {
    this.valor = '';
    this.checked = true;
  }

}

