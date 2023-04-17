import { Filter } from "./filtro.interface";

export interface Columna {
    nombre: string;
    etiqueta: string;
    orden: number;
  filtro: Filter;

  
}


