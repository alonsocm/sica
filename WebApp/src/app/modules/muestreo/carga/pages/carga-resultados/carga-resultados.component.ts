import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Column } from 'src/app/interfaces/filter/column';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { Item } from 'src/app/interfaces/filter/item';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';

const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-carga-resultados',
  templateUrl: './carga-resultados.component.html',
  styleUrls: ['./carga-resultados.component.css'],
})
export class CargaResultadosComponent extends BaseService implements OnInit {
  filtrosComponente: string = '';
  //Variables para los muestros
  muestreos: Array<Muestreo> = []; //Contiene los registros consultados a la API*/
  muestreosSeleccionados: Array<Muestreo> = []; //Contiene los registros que se van seleccionando*/

  muestreosdataAll: Array<Muestreo> = [];


  filtrosModal: Array<Item> = [];

  resultadosEnviados: Array<number> = [];

  reemplazarResultados: boolean = false;
  esTemplate: boolean = true;
  mostrar: boolean = true;

  archivo: any;
  opcionColumnaFiltro: string = '';

  prueba = 'prueba';

  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  constructor(
    private filtroHistorialService: FiltroHistorialService,
    private muestreoService: MuestreoService
  ) {
    super();
    this.filtroHistorialService.columnName.subscribe((columnName) => {
      this.deleteFilter(columnName);
      //this.consultartotalCabeceros();
      this.consultarMonitoreos();
    });

    this.filtroHistorialService.columnaFiltroEspecial.subscribe((dato: Column) => {
      if (dato.specialFilter != null)
      this.filtrar(dato, true);      
    });
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'estatus', label: 'ESTATUS', order: 1, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'evidencias', label: 'EVIDENCIAS COMPLETAS', order: 2, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'numeroEntrega', label: 'NÚMERO CARGA', order: 3, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'number', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveSitio', label: 'CLAVE NOSEC', order: 4, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'clave5k', label: 'CLAVE 5K', order: 5, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveMonitoreo', label: 'CLAVE MONITOREO', order: 6, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoSitio', label: 'TIPO DE SITIO', order: 7, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'nombreSitio', label: 'SITIO', order: 8, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'ocdl', label: 'OC/DL', order: 9, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoCuerpoAgua', label: 'TIPO CUERPO AGUA', order: 10, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'subTipoCuerpoAgua', label: 'SUBTIPO CUERPO DE AGUA', order: 11, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'programaAnual', label: 'PROGRAMA ANUAL', order: 12, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorio', label: 'LABORATORIO', order: 13, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorioSubrogado', label: 'LABORATORIO SUBROGADO', order: 14, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaRealizacion', label: 'FECHA REALIZACIÓN', order: 16, selectAll: true, filtered: false,
        data: [], filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaProgramada', label: 'FECHA PROGRAMACIÓN', order: 15, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaInicio', label: 'HORA INICIO MUESTREO', order: 17, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaFin', label: 'HORA FIN MUESTREO', order: 18, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaCarga', label: 'FECHA CARGA SICA', order: 19, selectAll: true, filtered: false, asc: false, desc: false,
        data: [], filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaEntregaMuestreo', label: 'FECHA ENTREGA', order: 20, selectAll: true, filtered: false, desc: false, asc: false,
        data: [], filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
    ];

    this.columns = nombresColumnas;

    this.headers = this.columns.map((m) => ({
      label: m.label,
      name: m.name,
    }));

    // this.muestreoService.filtrosCabeceroFoco = this.headers;
  }


  //public consultartotalCabeceros(
  //  page: number = 0,
  //  pageSize: number = this.pageSize,
  //  filter: string = this.cadena
  //): void {   
  //  this.muestreoService
  //    .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
  //    .subscribe({
  //      next: (response: any) => {          
  //        this.muestreosdataAll = response.data;      
  //      },
  //      error: (error) => { },
  //    });
  //}


  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {    
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected() ? false : true;
        },
        error: (error) => { },
      });
  }

  public establecerValoresFiltrosTabla(column: Column) {  
  
    this.setColumnsFiltered(); 

    this.muestreoService.filtros.subscribe((filtro) => { this.filtros = filtro });  

    //Se define el arreglo opcionesFiltros dependiendo del tipo de dato de la columna para mostrar las opciones correspondientes de filtrado
    this.obtenerLeyendaFiltroEspecial(column.dataType);

    if (!column.filtered && !this.existeFiltrado || (column.isLatestFilter && this.filtros.length == 1)) {
      this.cadena = '';
      this.muestreoService.getDistinctValuesFromColumn(column.name, this.cadena).subscribe({
        next: (response: any) => {
          column.data = response.data.map((register: any) => {
            let item: Item = {
              value: register,
              checked: true,
            };
            return item;
          });

          column.filteredData = column.data;
          this.ordenarAscedente(column.filteredData);
          this.getPreseleccionFiltradoColumna(column);
        },
        error: (error) => { },
      });
    } else if (
      (!column.filtered && this.existeFiltrado) ||
      (column.filtered && !column.isLatestFilter)
    ) {  
      this.muestreoService.getDistinctValuesFromColumn(column.name, this.cadena).subscribe({
        next: (response: any) => {
          column.data = response.data.map((register: any) => {
            let item: Item = {
              value: register,
              checked: true,
            };
            return item;
          });

          column.filteredData = column.data;
          this.ordenarAscedente(column.filteredData);
          this.getPreseleccionFiltradoColumna(column);
        },
        error: (error) => { },
      });


    }
  }

  //Método que permite, mediante el z-index, mostrar correctamente el dropdown que contiene las opciones de filtro, por cada columna
  private setZindexToHeader(columnLabel: string) {
    if (this.currentHeaderFocus != '') {
      let previousHeader = document.getElementById(
        this.currentHeaderFocus
      ) as HTMLHeadElement;
      previousHeader.style.zIndex = '0';
    }

    this.currentHeaderFocus = columnLabel;
    let header = document.getElementById(columnLabel) as HTMLHeadElement;
    header.style.zIndex = '1';
  }



  cargarArchivo(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    if (this.archivo) {
      this.loading = !this.loading;

      this.muestreoService
        .cargarArchivo(this.archivo[0], false, this.reemplazarResultados)
        .subscribe({
          next: (response: any) => {
            if (response.data.correcto) {
              this.loading = false;
              this.mostrarMensaje(
                'Archivo procesado correctamente.',
                TIPO_MENSAJE.exito
              );
              this.resetInputFile(this.inputExcelMonitoreos);
              this.consultarMonitoreos();
            } else {
              this.loading = false;
              this.numeroEntrega = response.data.numeroEntrega;
              this.anioOperacion = response.data.anio;
              document
                .getElementById('btnMdlConfirmacionActualizacion')
                ?.click();
            }
          },
          error: (error: any) => {
            this.loading = false;
            let archivoErrores = this.generarArchivoDeErrores(
              error.error.Errors
            );
            this.mostrarMensaje(
              'Se encontraron errores en el archivo procesado.',
              TIPO_MENSAJE.error
            );
            this.hacerScroll();
            FileService.download(archivoErrores, 'errores.txt');
            this.resetInputFile(this.inputExcelMonitoreos);
          },
        });
    }
  }

  sustituirResultados() {
    this.loading = true;
    this.muestreoService.cargarArchivo(this.archivo[0], false, true).subscribe({
      next: (response: any) => {
        if (response.data.correcto) {
          this.loading = false;
          this.mostrarMensaje(
            'Archivo procesado correctamente.',
            TIPO_MENSAJE.exito
          );
          this.resetInputFile(this.inputExcelMonitoreos);
          this.consultarMonitoreos();
        } else {
          this.loading = false;
        }
      },
      error: (error: any) => {
        this.loading = false;
        let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);
        this.mostrarMensaje(
          'Se encontraron errores en el archivo procesado.',
          TIPO_MENSAJE.error
        );
        this.hacerScroll();
        FileService.download(archivoErrores, 'errores.txt');
        this.resetInputFile(this.inputExcelMonitoreos);
      },
    });
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarMonitoreos();

    this.columns
      .filter((x) => x.isLatestFilter)
      .map((m) => {
        m.isLatestFilter = false;
      });

    if (!isFiltroEspecial) {
      columna.filtered = true;
      columna.isLatestFilter = true;
    } else {
      this.columns
        .filter((x) => x.name == this.columnaFiltroEspecial.name)
        .map((m) => {
          (m.filtered = true),
            (m.selectedData = this.columnaFiltroEspecial.selectedData),
            (m.isLatestFilter = true);
        });
    }

    this.esHistorial = true;
    this.columnaFiltroEspecial.optionFilter = '';
    this.columnaFiltroEspecial.specialFilter = '';
    this.setColumnsFiltered();
    this.hideColumnFilter();
  }

  private setColumnsFiltered() {
    let filtrosActuales = this.columns
      .filter((f) => f.filtered)
      .map((m) => ({
        name: m.name,
        label: m.label,
      }));

    this.muestreoService.filtrosSeleccionados = filtrosActuales;
 
  }


  existeEvidencia(evidencias: Array<any>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  descargarEvidencia(claveMuestreo: string, sufijo: string) {
    this.loading = !this.loading;
    let muestreo = this.muestreosSeleccionados.find(
      (x) => x.claveMonitoreo == claveMuestreo
    );
    let nombreEvidencia = muestreo?.evidencias.find((x) => x.sufijo == sufijo);

    this.muestreoService
      .descargarArchivo(nombreEvidencia?.nombreArchivo)
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          FileService.download(response, nombreEvidencia?.nombreArchivo ?? '');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }

  limpiarFiltros() {
    this.ngOnInit();
  }

  exportarResultados(): void {
    if (this.muestreosSeleccionados.length == 0 && !this.allSelected) {
      this.mostrarMensaje(
        'No hay información seleccionada para descargar',
        'warning'
      );
      return this.hacerScroll();
    }

    this.loading = true;

    if (this.allSelected) {
      this.muestreoService.exportAllEbaseca(false, this.cadena).subscribe({
        next: (response: any) => {
          FileService.download(response, 'CargaResultadosEbaseca.xlsx');
          this.resetValues();
          this.unselectMuestreos();
          this.loading = false;
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.loading = false;
          this.hacerScroll();
        },
      });
    } else {
      this.muestreoService
        .exportarCargaResultadosEbaseca(this.muestreosSeleccionados)
        .subscribe({
          next: (response: any) => {
            FileService.download(response, 'CargaResultadosEbaseca.xlsx');
            this.resetValues();
            this.unselectMuestreos();
            this.loading = false;
          },
          error: (response: any) => {
            this.mostrarMensaje(
              'No fue posible descargar la información',
              'danger'
            );
            this.loading = false;
            this.hacerScroll();
          },
        });
    }
  }

  private unselectMuestreos() {
    this.muestreos.forEach((m) => (m.isChecked = false));
  }

  confirmarEliminacion() {
    let muestreosSeleccionados = this.Seleccionados(
      this.muestreosSeleccionados
    );
    if (!(muestreosSeleccionados.length > 0)) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para eliminar',
        TIPO_MENSAJE.alerta
      );
      return this.hacerScroll();
    }
    document.getElementById('btnMdlConfirmacion')?.click();
  }

  eliminarMuestreos() {
    let muestreosSeleccionados = this.Seleccionados(
      this.muestreosSeleccionados
    );
    if (!(muestreosSeleccionados.length > 0)) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para eliminar',
        TIPO_MENSAJE.alerta
      );
      return this.hacerScroll();
    }
    this.loading = true;
    let muestreosEliminar = muestreosSeleccionados.map((s) => s.muestreoId);
    this.muestreoService.eliminarMuestreos(muestreosEliminar).subscribe({
      next: (response) => {
        document.getElementById('btnCancelarModal')?.click();
        this.consultarMonitoreos();
        this.loading = false;
        this.mostrarMensaje(
          'Monitoreos eliminados correctamente',
          TIPO_MENSAJE.exito
        );
        this.resetValues();
        this.hacerScroll();
      },
      error: (error) => {
        this.loading = false;
      },
    });
  }

  enviarMonitoreos(): void {
    //Si todos los registros están seleccionados, vamos a utlizar otra función, donde pasamos el filtro actual
    if (this.allSelected) {
      this.muestreoService
        .enviarTodosMuestreosAcumulados(
          estatusMuestreo.AcumulacionResultados,
          this.cadena
        )
        .subscribe({
          next: (response: any) => {
            this.loading = true;
            if (response.succeded) {
              this.resetValues();
              this.loading = false;
              this.consultarMonitoreos();
              this.mostrarMensaje(
                'Se enviaron ' +
                this.totalItems +
                ' muestreos a la etapa de "Acumulación resultados" correctamente',
                'success'
              );
              this.hacerScroll();
            }
          },
          error: (response: any) => {
            this.loading = false;
            this.mostrarMensaje(
              ' Error al enviar los muestreos a la etapa de "Acumulación resultados"',
              'danger'
            );
            this.hacerScroll();
          },
        });
    } else {
      //se hace pequeño cambio paraque pueda enviarlos aunque no este la carga de evidencias
      this.resultadosEnviados = this.muestreosSeleccionados.map((m) => {
        return m.muestreoId;
      });

      if (this.resultadosEnviados.length == 0) {
        this.hacerScroll();
        return this.mostrarMensaje(
          'Debes de seleccionar al menos un muestreo con evidencias cargadas para enviar a la etapa de "Acumulación resultados"',
          'danger'
        );
      }

      this.muestreoService
        .enviarMuestreoaAcumulados(
          estatusMuestreo.AcumulacionResultados,
          this.resultadosEnviados
        )
        .subscribe({
          next: (response: any) => {
            this.loading = true;
            if (response.succeded) {
              this.resetValues();
              this.loading = false;
              this.consultarMonitoreos();
              this.mostrarMensaje(
                'Se enviaron ' +
                this.resultadosEnviados.length +
                ' muestreos a la etapa de "Acumulación resultados" correctamente',
                'success'
              );
              this.hacerScroll();
            }
          },
          error: (response: any) => {
            this.loading = false;
            this.mostrarMensaje(
              ' Error al enviar los muestreos a la etapa de "Acumulación resultados"',
              'danger'
            );
            this.hacerScroll();
          },
        });
    }
  }

  private resetValues() {
    this.muestreosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
    this.getSummary();
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.muestreos = response.data;
        },
        error: (error) => { },
      });
  }

  onSelectPageClick() {
    this.muestreos.map((m) => {
      m.isChecked = this.selectedPage;

      //Buscamos el registro en los seleccionados
      let index = this.muestreosSeleccionados.findIndex(
        (d) => d.muestreoId === m.muestreoId
      );

      if (index == -1) {
        //No existe en seleccionados, lo agremos
        this.muestreosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        //Existe y el seleccionar página está deshabilitado, lo eliminamos, de los seleccionados
        this.muestreosSeleccionados.splice(index, 1);
      }
    });

    if (this.selectAllOption && !this.selectedPage) {
      this.selectAllOption = false;
      this.allSelected = false;
    } else if (!this.selectAllOption && this.selectedPage) {
      this.selectAllOption = true;
    }

    this.getSummary();
  }

  onSelectClick(muestreo: Muestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.isChecked) {
      this.muestreosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected() ? false : true;
    } else {
      let index = this.muestreosSeleccionados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.muestreosSeleccionados.splice(index, 1);
      }
    }

    this.getSummary();
  }

  getSummary() {
    this.muestreoService.muestreosSeleccionados = this.muestreosSeleccionados;
  }

  getPreviousSelected(
    muestreos: Array<Muestreo>,
    muestreosSeleccionados: Array<Muestreo>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.muestreoId === x.muestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.isChecked = true;
      }
    });
  }

  anyUnselected() {
    return this.muestreos.some((f) => !f.isChecked);
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);   
    this.setColumnsFiltered();
    //this.consultartotalCabeceros();
    this.consultarMonitoreos(); 
  }
}
