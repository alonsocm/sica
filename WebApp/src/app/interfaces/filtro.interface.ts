import { FiltroBusqueda } from "./columna-inferface";

export class Filter {
  values: string[];
  selectedValue: string;


  constructor() {
    this.values = ["Seleccionar todos"];
    this.selectedValue = "Seleccione";

  }
}




export class FilterFinal {
  values: FiltroBusqueda[];
  seleccionarTodosChckFiltro: boolean;


  constructor() {
  /*  this.values = ["Seleccionar todos"];*/
    this.values = [];  
    this.seleccionarTodosChckFiltro = true;
   
  }
}
