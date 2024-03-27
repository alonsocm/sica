export class Filter {
  values: string[];
  selectedValue: string;


  constructor() {
    this.values = ["Seleccionar todos"];
    this.selectedValue = "Seleccione";

  }
}




export class FilterFinal {
  values: string[];
  selectedValue: string;
  seleccionarTodosChckFiltro: boolean;
  isCheckedFiltro: boolean;


  constructor() {
    this.values = ["Seleccionar todos"];
    this.selectedValue = "Seleccione";
    this.seleccionarTodosChckFiltro = true;
    this.isCheckedFiltro = true;
  }
}
