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
  muestreosFiltrados: Array<Muestreo> = [];
  muestreosFiltradosFiltrado: Array<Muestreo> = [];
  muestreosFiltradosFiltradoConcatenado: Array<Muestreo> = [];

  filtrosValues: Array<any> = [];

  reemplazarResultados: boolean = false;
  esTemplate: boolean = true;
  mostrar: boolean = true;
  cabeceroSeleccionado: boolean = false;
  esHistorial: boolean = false;

  resultadosEnviados: Array<number> = [];

  filtrosfinal: Array<any> = [];
  filtrosbusqueda: Array<any> = [];
  filtrosCabeceroFoco: Array<any> = [];
  /*  columnasfiltross: Array<any> = [];*/

  archivo: any;

  numeroEntrega: string = '';
  anioOperacion: string = '';
  initialValue: string = '';

  //Paginación
  totalItems = 0;

  //Selección de registros
  selectAllOption: boolean = false;
  allSelected: boolean = false;
  selectedPage: boolean = false;

  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  @ViewChild('thprueba') thprueba: ElementRef = {} as ElementRef;
  @ViewChild('auto') auto: ElementRef = {} as ElementRef;

  /* @ViewChild('btnAceptar') control: any;*/
  //@ViewChild('btnAceptar') btnAceptar: ElementRef = {} as ElementRef;

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
      //iconoAZOrdenado: new FormControl(),
      //iconoZAOrdenado: new FormControl(),
      //btnAceptar: [null]
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
      },
      {
        name: 'nameSitio',
        label: 'SITIO',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
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
      },
      {
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
        order: 16,
        selectAll: true,
        filtered: false,
        data: [],
        filteredData: [],
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
      },
      {
        name: 'fechaEntrega',
        label: 'FECHA ENTREGA',
        order: 20,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
      },
    ];

    this.columns = nombresColumnas;

    this.filtrosCabeceroFoco = this.columns.map((m) => {
      return m.label;
    });
  }

  private consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage
  ): void {
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.totalItems = response.totalRecords;
          this.muestreos = response.data;
          this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected() ? false : true;
        },
        error: (error) => {},
      });
  }

  public establecerValoresFiltrosTabla(column: Column) {
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
      },
      error: (error) => {},
    });
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
    this.muestreosFiltradosFiltradoConcatenado = [];
    this.muestreosFiltrados = this.muestreos;
    let filtrosSeleccionados = columna.filteredData?.filter((x) => x.checked);

    for (var i = 0; i < filtrosSeleccionados.length; i++) {
      this.muestreosFiltradosFiltrado = [];
      this.muestreosFiltradosFiltrado = this.muestreosFiltrados.filter(
        (f: any) => {
          return f[columna.name] == filtrosSeleccionados[i].value;
        }
      );

      if (this.muestreosFiltradosFiltrado.length > 0) {
        this.muestreosFiltradosFiltradoConcatenado =
          this.muestreosFiltradosFiltradoConcatenado.concat(
            this.muestreosFiltradosFiltrado
          );
      }
    }

    this.muestreosFiltrados = this.muestreosFiltradosFiltradoConcatenado;
    this.columns
      .filter((x) => x.name == columna.name)
      .map((m) => {
        return (m.filtered = true);
      });

    this.columns.forEach((f) => {
      f.data = [];
    });

    //this.establecerValoresFiltrosTabla();
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
    let muestreo = this.muestreosFiltrados.find(
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

  seleccionarFiltro(columna: Column): void {
    if (columna.selectAll) {
      columna.selectAll = false;
    }
  }

  exportarResultados(): void {
    if (
      this.muestreosSeleccionados.length == 0 &&
      this.muestreosFiltrados.length == 0
    ) {
      this.mostrarMensaje(
        'No hay información existente para descargar',
        'warning'
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.muestreosSeleccionados =
      this.muestreosSeleccionados.length == 0
        ? this.muestreosFiltrados
        : this.muestreosSeleccionados;
    this.muestreoService
      .exportarCargaResultadosEbaseca(this.muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'CargaResultadosEbaseca.xlsx');
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

  confirmarEliminacion() {
    let muestreosSeleccionados = this.Seleccionados(this.muestreosFiltrados);
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
    let muestreosSeleccionados = this.Seleccionados(this.muestreosFiltrados);
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
    let valor = this.muestreosFiltrados.filter(
      (x) => x.estatus == 'EvidenciasCargadas'
    );
    //se hace pequeño cambio paraque pueda enviarlos aunque no este la carga de evidencias
    //this.resultadosEnviados = this.Seleccionados(valor).map(
    this.resultadosEnviados = this.Seleccionados(this.muestreosFiltrados).map(
      (m) => {
        return m.muestreoId;
      }
    );

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
    this.cabeceroSeleccionado = true;
    this.esfilrofoco = val.toUpperCase();
    this.thprueba.nativeElement.focus();
  }

  eliminarFiltro(etiqueta: string) {}

  pageClic(page: any) {
    this.consultarMonitoreos(page);
    this.page = page;
  }

  ordenarAZ(columna: Column, tipo: string) {
    this.muestreosFiltrados.sort(function (a: any, b: any) {
      if (a[columna.name] > b[columna.name]) {
        return tipo == 'asc' ? 1 : -1;
      }
      if (a[columna.name] < b[columna.name]) {
        return tipo == 'asc' ? -1 : 1;
      }
      return 0;
    });

    columna.asc = tipo == 'asc' ? true : false;
    columna.desc = tipo == 'desc' ? true : false;
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
    console.log(this.muestreosSeleccionados);
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
