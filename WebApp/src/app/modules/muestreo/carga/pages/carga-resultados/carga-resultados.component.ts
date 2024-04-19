import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Column } from 'src/app/interfaces/filter/column';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Item } from 'src/app/interfaces/filter/item';

const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-carga-resultados',
  templateUrl: './carga-resultados.component.html',
  styleUrls: ['./carga-resultados.component.css'],
})
export class CargaResultadosComponent extends BaseService implements OnInit {
  //Variables para los muestros
  muestreos: Array<Muestreo> = []; //Contiene los registros consultados a la API
  muestreosSeleccionados: Array<Muestreo> = []; //Contiene los registros que se van seleccionando
  muestreosFiltradosFiltrado: Array<Muestreo> = [];
  muestreosFiltradosFiltradoConcatenado: Array<Muestreo> = [];

  filtrosbusqueda: Array<Item> = [];

  filtrosValues: Array<any> = [];
  filtrosfinal: Array<any> = [];

  filtrosCabeceroFoco: Array<any> = [];

  resultadosEnviados: Array<number> = [];

  opcionesFiltros: Array<string> = ["Es igual a", "No es igual a", "Es mayor que", "Es mayor o igual a", "Es menor que", "Es menor o igual a", "Comienza por", "No comienza por", "Termina con", "No termina con", "Contiene", "No contiene"];

  reemplazarResultados: boolean = false;
  esTemplate: boolean = true;
  mostrar: boolean = true;
  cabeceroSeleccionado: boolean = false;
  esHistorial: boolean = false;
  existeFiltrado: boolean = false;

  

  archivo: any;

  numeroEntrega: string = '';
  anioOperacion: string = '';
  initialValue: string = '';
  cadena: string = '';

  //Paginación
  totalItems = 0;

  opctionFiltro: string = '';
  segundaopctionFiltro: string = '';

  //Selección de registros
  selectAllOption: boolean = false;
  allSelected: boolean = false;
  selectedPage: boolean = false;

  //variable para almacenar el ordenamiento
  orderBy: { column: string; type: string } = { column: '', type: '' };

  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  @ViewChild('thprueba') thprueba: ElementRef = {} as ElementRef;
  @ViewChild('auto') auto: ElementRef = {} as ElementRef;
  @ViewChild('txtBuscador') txtBuscador: ElementRef = {} as ElementRef;

  registroParam: FormGroup;
  esfilrofoco: string = '';
  columns: Array<Column> = [];

  constructor(
    private muestreoService: MuestreoService,
    private fb: FormBuilder
  ) {
    super();
    this.registroParam = this.fb.group({
      chkFiltro: new FormControl(),
      chckAllFiltro: new FormControl(),
      aOrdenarAZ: new FormControl(),
    });
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'estatus',
        label: 'ESTATUS',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'evidencias',
        label: 'EVIDENCIAS COMPLETAS',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'numeroEntrega',
        label: 'NÚMERO CARGA',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'claveSitio',
        label: 'CLAVE NOSEC',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: '',
        label: 'CLAVE 5K',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'claveMonitoreo',
        label: 'CLAVE MONITOREO',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'tipoSitio',
        label: 'TIPO DE SITIO',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'nombreSitio',
        label: 'SITIO',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'ocdl',
        label: 'OC/DL',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
        order: 10,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'subTipoCuerpoAgua',
        label: 'SUBTIPO CUERPO DE AGUA',
        order: 11,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'programaAnual',
        label: 'PROGRAMA ANUAL',
        order: 12,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'laboratorio',
        label: 'LABORATORIO',
        order: 13,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO SUBROGADO',
        order: 14,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
        order: 16,
        selectAll: true,
        filtered: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'fechaProgramada',
        label: 'FECHA PROGRAMACIÓN',
        order: 15,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'horaInicio',
        label: 'HORA INICIO MUESTREO',
        order: 17,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'horaFin',
        label: 'HORA FIN MUESTREO',
        order: 18,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'fechaCarga',
        label: 'FECHA CARGA SICA',
        order: 19,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
      {
        name: 'fechaEntregaMuestreo',
        label: 'FECHA ENTREGA',
        order: 20,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        filteredDataFiltrado: [],
      },
    ];

    this.columns = nombresColumnas;

    this.filtrosCabeceroFoco = this.columns.map((m) => {
      return m.label;
    });
  }

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
        error: (error) => {},
      });
  }

  validarExisteFiltrado(): boolean {
    return this.columns.filter((x) => x.filtered == true).length > 0
      ? true
      : false;
  }

  ngAfterViewInit(): void {
    console.log('entra en after view');
  }

  public establecerValoresFiltrosTabla(column: Column) {
    console.log(this.muestreos);


    if (!column.filtered && !this.existeFiltrado) {
      this.muestreoService.getDistinctValuesFromColumn(column.name).subscribe({
        next: (response: any) => {
          column.data = response.data.map((register: any) => {
            let item: Item = {
              value: register,
              checked: true,
            };
            return item;
          });

          column.filteredData = column.data;
          //this.ordenarAscedente(column.filteredData);
        },
        error: (error) => {},
      });
    } else if (!column.filtered && this.existeFiltrado) {
      column.data = this.muestreos.map((m: any) => {
        let item: Item = {
          value: m[column.name],
          checked: true,
        };
        return item;
      });
      column.filteredData = column.data;
      const distinctThings = column.filteredData.filter(
        (thing, i, arr) => arr.findIndex((t) => t.value === thing.value) === i
      );

      column.filteredData = distinctThings.sort();
      //this.ordenarAscedente(column.filteredData);
    }

    //filtrados
    column.filteredDataFiltrado = this.muestreos.map((m: any) => {
      let item: Item = {
        value: m[column.name],
        checked: true,
      };
      return item;
    });
    column.filteredDataFiltrado = column.data;
    const distinctThings = column.filteredDataFiltrado.filter(
      (thing, i, arr) => arr.findIndex((t) => t.value === thing.value) === i
    );

    column.filteredDataFiltrado = distinctThings.sort();
    //this.ordenarAscedente(column.filteredDataFiltrado);
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

  filtrar(columna: Column) {
    this.existeFiltrado = true;
    let filtrosSeleccionados = columna.filteredData?.filter((x) => x.checked);
    let opciones = filtrosSeleccionados
      .map((x) => x.value)
      .toString()
      .replaceAll(',', '_');

    if (this.cadena.indexOf(columna.name) != -1) {
      this.cadena =
        this.cadena.indexOf('%') != -1 ? this.eliminarFiltro(columna) : '';
    }

    this.cadena =
      this.cadena != ''
        ? this.cadena + '%' + columna.name + '_' + opciones
        : columna.name + '_' + opciones;
    this.consultarMonitoreos();
    columna.filtered = true;
    //this.establecerValoresFiltrosTabla(columna);
    this.esHistorial = true;
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
    //this.muestreosFiltrados = this.muestreos;
    //this.esHistorial = false;
  }

  seleccionarFiltro(columna: Column): void {}

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
        this.hacerScroll();
        this.seleccionarTodosChck = false;
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

  onFiltroCabecero(val: any, columna: Column) {
    let criterioBusqueda = val.target.value;
    columna.filteredData = columna.data;
    columna.filteredData = columna.filteredData?.filter(
      (f) =>
        f.value.toLowerCase().indexOf(criterioBusqueda.toLowerCase()) !== -1
    );
  }

  onCabeceroFoco(val: string = '') {
    this.cabeceroSeleccionado = false;
    this.esfilrofoco = val.toUpperCase();
    this.filtrosCabeceroFoco = this.filtrosCabeceroFoco.filter(
      (f) => f.toLowerCase.indexOf(val.toLowerCase()) !== -1
    );
  }

  seleccionCabecero(val: string = '') {
    let header = document.getElementById(val) as HTMLElement;
    header.scrollIntoView({ behavior: 'smooth', block: 'center' });
    this.cabeceroSeleccionado = true;
    this.esfilrofoco = val.toUpperCase();
    this.thprueba.nativeElement.focus();
  }

  eliminarFiltro(columna: Column): string {
    let cadenaanterior = this.cadena.split('%');
    let repetidos = cadenaanterior.filter((x) => x.includes(columna.name));
    let indexx = cadenaanterior.indexOf(repetidos.toString());
    cadenaanterior.splice(indexx, 1);
    return (this.cadena = cadenaanterior.toString().replaceAll(',', '%'));
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
        error: (error) => {},
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

  getSelected() {
    return this.muestreos.filter((f) => f.isChecked);
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

  onSelectAllPagesClick() {
    this.allSelected = true;
  }

  onUnselectAllClick() {
    this.allSelected = false;
  }

  anyUnselected() {
    return this.muestreos.some((f) => !f.isChecked);
  }


}
