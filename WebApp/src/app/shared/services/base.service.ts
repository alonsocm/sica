import { ElementRef, Injectable, ViewChild, ViewChildren } from '@angular/core';

import { Columna } from 'src/app/interfaces/columna-inferface';
import { Column } from 'src/app/interfaces/filter/column';

import {
  filtrosEspeciales,
  filtrosEspecialesFecha,
  filtrosEspecialesNumeral,
  mustreoExpression,
} from '../enums/filtrosEspeciales';

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  public page: number = 1;
  public NoPage: number = 30;
  public pageSize: number = 30;
  public totalItems: number = 0;

  //#region variables para la selección
  selectedPage: boolean = false;
  selectAllOption: boolean = false;
  allSelected: boolean = false;
  //#endregion

  noRegistro: string = 'No se encontraron registros';
  filtroResumen: string = 'Seleccionar filtro';
  keyword: string = 'label';
  tipoAlerta: string = '';
  mensajeAlerta: string = '';

  loading: boolean = false;
  mostrarAlerta: boolean = false;
  seleccionarTodosChck: boolean = false;

  columnas: Array<Columna> = [];
  columnasF: Array<Column> = [];

  resultadosFiltradosn: Array<any> = [];

  resultadosn: Array<any> = [];
  sufijos: Array<string> = ['A', 'C', 'D', 'E', 'M', 'O', 'R', 'S', 'V'];

  @ViewChild('mensajes') mensajes: any;
  @ViewChildren('filtros') filtros: any;

  filtradoEspecial: filtrosEspeciales = new filtrosEspeciales();
  filtradoEspecialNumeral: filtrosEspecialesNumeral =
    new filtrosEspecialesNumeral();
  filtradoEspecialFecha: filtrosEspecialesFecha = new filtrosEspecialesFecha();
  mustreoExpression: mustreoExpression = new mustreoExpression();
  columnaFiltroEspecial: Column = {
    name: 'estatus',
    label: 'ESTATUS',
    order: 1,
    selectAll: true,
    filtered: false,
    asc: false,
    desc: false,
    data: [],
    filteredData: [],
    dataType: 'string',
    optionFilter: '',
    secondOptionFilter: '',
    specialFilter: '',
    secondSpecialFilter: '',
    selectedData: '',
  };
  indicesopcionesFiltros: Array<number> = []; //Indices para indicar en que posicion se pone linea divisora
  opcionesFiltros: Array<string> = []; //Arreglo para submenu de filtro especial conforme al tipo de la columna string/number/date
  opcionesFiltrosModal: Array<string> = []; //Arreglo para combo en modal autofiltro personalizado conforme al tipo de la columna string/number/date
  columns: Array<Column> = [];
  cadena: string = '';

  //variable para almacenar el ordenamiento
  orderBy: { column: string; type: string } = { column: '', type: '' };
  esHistorial: boolean = false;
  existeFiltrado: boolean = false;

  esfiltrofoco: string = '';
  cabeceroSeleccionado: boolean;
  headers: Array<any> = []; //Listado de cabeceros utilizado en el drop para redirigir al usuario al cabecero seleccionado

  opcionFiltrar: string = ''; //variable para guardar la opcion a filtrar en filtro especial
  leyendaFiltrosEspeciales: string = ''; //Leyenda para indicar si es filtro de texto/número/fecha
  numeroEntrega: string = '';
  anioOperacion: string = '';
  initialValue: string = '';

  existeEliminacionFiltro: boolean = false;

  constructor() {
    this.cabeceroSeleccionado = false;
  }

  mostrarMensaje(mensaje: string, tipo: string): void {
    this.mensajeAlerta = mensaje;
    this.tipoAlerta = tipo;
    this.mostrarAlerta = true;
    setTimeout(() => {
      this.mostrarAlerta = false;
    }, 10000);
  }

  hacerScroll() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }

  generarArchivoDeErrores(errores: string) {
    const blob = new Blob([String(errores).replaceAll(',', '\n')], {
      type: 'application/octet-stream',
    });

    return blob;
  }

  filtrarn() {
    this.resultadosFiltradosn = this.resultadosn;
    this.columnas.forEach((columna) => {
      this.resultadosFiltradosn = this.resultadosFiltradosn.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.establecerValoresFiltrosTablan();
  }

  establecerValoresFiltrosTablan() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltradosn.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  }

  limpiarFiltrosn() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrarn();
    document.getElementById('dvMessage')?.click();
    this.establecerValoresFiltrosTablan();
  }

  seleccionarAll(resultadosFiltrados: Array<any>): void {
    resultadosFiltrados.map((m) => {
      m.isChecked = this.seleccionarTodosChck ? true : false;
    });
  }

  seleccionarAllFiltro(columna: Column): void {
    columna.data.map((m) => {
      m.checked = columna.selectAll ? true : false;
    });
  }

  //sustituye a obtenerseleccionados()
  Seleccionados(Seleccionados: Array<any>) {
    return Seleccionados.filter((f) => f.isChecked);
  }

  descargarArchivo(file: any, fileName: string) {
    const downloadInstance = document.createElement('a');
    downloadInstance.href = URL.createObjectURL(file);
    downloadInstance.target = '_blank';
    downloadInstance.download = fileName;

    document.body.appendChild(downloadInstance);
    downloadInstance.click();
    document.body.removeChild(downloadInstance);
  }

  resetInputFile(input: ElementRef) {
    input.nativeElement.value = null;
  }

  seleccionarn(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
  }

  //SI SE OCUPA
  onFiltroCabecero(val: any, columna: Column) {
    let criterioBusqueda = val.target.value;
    columna.filteredData = columna.data;
    columna.filteredData = columna.filteredData?.filter(
      (f) =>
        f.value.toLowerCase().indexOf(criterioBusqueda.toLowerCase()) !== -1
    );
  }

  mostrarModalFiltro(opcion: string, columna: Column) {
    switch (opcion) {
      case this.filtradoEspecial.personalizado:
        columna.optionFilter = this.filtradoEspecial.equals;
        break;
      case this.filtradoEspecialNumeral.between:
        columna.optionFilter =
          columna.dataType == 'number'
            ? this.filtradoEspecialNumeral.greaterthanorequalto
            : this.filtradoEspecialFecha.beforeorequal;
        columna.secondOptionFilter =
          columna.dataType == 'number'
            ? this.filtradoEspecialNumeral.lessthanorequalto
            : this.filtradoEspecialFecha.afterorequal;
        break;
      default:
        columna.optionFilter = opcion;
        columna.secondOptionFilter = '';
        break;
    }
    this.columnaFiltroEspecial = columna;
    this.opcionesFiltrosModal = this.opcionesFiltros;
    columna.dataType == 'string'
      ? this.opcionesFiltrosModal.splice(this.opcionesFiltrosModal.length - 1)
      : this.opcionesFiltrosModal.splice(this.opcionesFiltrosModal.length - 2);
  }

  eliminarFiltro(columna: Column): string {
    let cadenaanterior = this.cadena.split('%');
    let repetidos = cadenaanterior.filter((x) => x.includes(columna.name));
    let indexx = cadenaanterior.indexOf(repetidos.toString());
    cadenaanterior.splice(indexx, 1);
    columna.filtered = false;
    this.existeEliminacionFiltro = true;
    return (this.cadena = cadenaanterior.toString().replaceAll('|', '%'));
  }

  deleteFilter(columnName: string): string {
    let index = this.columns.findIndex((f) => f.name == columnName);
    this.columns[index].filtered = false;

    let cadenaanterior = this.cadena.split('%');
    let repetidos = cadenaanterior.filter((x) => x.includes(columnName));
    let indexx = cadenaanterior.indexOf(repetidos.toString());
    cadenaanterior.splice(indexx, 1);

    return (this.cadena = cadenaanterior.toString().replaceAll('|', '%'));
  }

  validarExisteFiltrado(): boolean {
    return this.columns.filter((x) => x.filtered == true).length > 0
      ? true
      : false;
  }
}
