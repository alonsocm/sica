import { Filter, FilterFinal } from "./filtro.interface";

export interface Columna {
    nombre: string;
    etiqueta: string;
    orden: number;
  filtro: Filter;


}


export interface ColumnaFinal {
  nombre: string;
  etiqueta: string;
  orden: number;
  filtro: FilterFinal;
  esfiltrado: boolean;
  filtrobusqueda: any[];

  
}

