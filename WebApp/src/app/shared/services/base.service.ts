import { ElementRef, Injectable, ViewChild, ViewChildren } from '@angular/core';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Column } from 'src/app/interfaces/filter/column';
import { Resultado } from 'src/app/interfaces/Resultado.interface';
import { FilterFinal } from '../../interfaces/filtro.interface';

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  public page: number = 1;
  public NoPage: number = 30;
  public pageSize: number = 30;
  public totalItems: number = 0;

  noRegistro: string = 'No se encontraron registros';
  filtroResumen: string = 'Seleccionar filtro';
  keyword: string = 'values';
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

  constructor() {}

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
}
