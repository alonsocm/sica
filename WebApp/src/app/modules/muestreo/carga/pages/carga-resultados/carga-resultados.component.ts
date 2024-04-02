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
import { Filter, FilterFinal } from 'src/app/interfaces/filtro.interface';
import { Columna, Column, FiltroBusqueda } from 'src/app/interfaces/columna-inferface';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { from } from 'rxjs';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { map } from 'leaflet';

const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-carga-resultados',
  templateUrl: './carga-resultados.component.html',
  styleUrls: ['./carga-resultados.component.css'],
})
export class CargaResultadosComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  muestreosFiltradosFiltrado: Array<Muestreo> = [];
  muestreosFiltradosFiltradoConcatenado: Array<Muestreo> = [];
  muestreosseleccionados: Array<Muestreo> = [];


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

  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  @ViewChild('thprueba') thprueba: ElementRef = {} as ElementRef;
  @ViewChild('auto') auto: ElementRef = {} as ElementRef;

  /* @ViewChild('btnAceptar') control: any;*/
  //@ViewChild('btnAceptar') btnAceptar: ElementRef = {} as ElementRef;

  registroParam: FormGroup;

  esfilrofoco: string = '';

  constructor(
    private muestreoService: MuestreoService,
    private fb: FormBuilder
  ) {
    super();
    this.registroParam = this.fb.group({
      chkFiltro: new FormControl(),
      chckAllFiltro: new FormControl(),
      //btnAceptar: [null]
    });
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
  }
  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      { nombre: 'estatus', etiqueta: 'ESTATUS', orden: 1, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'evidencias', etiqueta: 'EVIDENCIAS COMPLETAS', orden: 2, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'numeroEntrega', etiqueta: 'NÚMERO CARGA', orden: 3, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'claveSitio', etiqueta: 'CLAVE NOSEC', orden: 4, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: '', etiqueta: 'CLAVE 5K', orden: 5, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 6, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'tipoSitio', etiqueta: 'TIPO DE SITIO', orden: 7, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 8, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'ocdl', etiqueta: 'OC/DL', orden: 9, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 10, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'subTipoCuerpoAgua', etiqueta: 'SUBTIPO CUERPO DE AGUA', orden: 11, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'programaAnual', etiqueta: 'PROGRAMA ANUAL', orden: 12, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 13, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'laboratorioSubrogado', etiqueta: 'LABORATORIO SUBROGADO', orden: 14, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REALIZACIÓN', orden: 16, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'fechaProgramada', etiqueta: 'FECHA PROGRAMACIÓN', orden: 15, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'horaInicio', etiqueta: 'HORA INICIO MUESTREO', orden: 17, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'horaFin', etiqueta: 'HORA FIN MUESTREO', orden: 18, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'fechaCarga', etiqueta: 'FECHA CARGA SICA', orden: 19, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] },
      { nombre: 'fechaEntrega', etiqueta: 'FECHA ENTREGA', orden: 20, filtro: new FilterFinal(), esfiltrado: false, filtrobusqueda: [] }
    ];
    this.columnasF = nombresColumnas;
    this.filtrosCabeceroFoco = this.columnasF.map((m) => {
      return m.etiqueta;
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
          this.totalItems = response.totalRecords;
          this.muestreos = response.data;
          this.muestreosFiltrados = this.muestreos;
          this.establecerValoresFiltrosTabla();
        },
        error: (error) => { },
      });
  }
  private establecerValoresFiltrosTabla() {
    this.columnasF.forEach((f) => {
      f.filtro.values.push(
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre]))
      );
    });

    this.columnasF.forEach((x) => {
      this.filtrosValues = [];
      this.filtrosValues = x.filtro.values;
      x.filtro.values = [];

      this.filtrosValues.forEach((y) => {
        let valora = new FiltroBusqueda();
        valora.valor = y;
        x.filtro.values.push(valora);
      });
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
  };
  filtrar(columna: Column) {
    this.muestreosFiltradosFiltradoConcatenado = [];
    this.muestreosFiltrados = this.muestreos;

    let filtrosSeleccionados = columna.filtrobusqueda.filter(x => x.checked);

    for (var i = 0; i < filtrosSeleccionados.length; i++) {
      this.muestreosFiltradosFiltrado = [];
      this.muestreosFiltradosFiltrado = this.muestreosFiltrados.filter(
        (f: any) => {
          return f[columna.nombre] == filtrosSeleccionados[i].valor;
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
    this.columnasF
      .filter((x) => x.nombre == columna.nombre)
      .map((m) => {
        return (m.esfiltrado = true);
      });

    this.columnasF.forEach((f) => {
      f.filtro.values = [];
    });
    this.establecerValoresFiltrosTabla();
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
  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.getMuestreos();
  }
  seleccionarFiltro(): void {

  }
  getMuestreos() {
    let muestreosSeleccionados = this.Seleccionados(this.muestreosFiltrados);
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
    this.muestreosseleccionados = muestreosSeleccionados;
  }
  exportarResultados(): void {
    if (
      this.muestreosseleccionados.length == 0 &&
      this.muestreosFiltrados.length == 0
    ) {
      this.mostrarMensaje(
        'No hay información existente para descargar',
        'warning'
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.muestreosseleccionados =
      this.muestreosseleccionados.length == 0
        ? this.muestreosFiltrados
        : this.muestreosseleccionados;
    this.muestreoService
      .exportarCargaResultadosEbaseca(this.muestreosseleccionados)
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
  onFiltroCabecero(val: any, filtros: any, columna: any) {
    let criterioBusqueda = val.target.value;
    this.filtrosbusqueda = filtros;
    this.filtrosbusqueda = this.filtrosbusqueda.filter((f) => f.valor.toLowerCase().indexOf(criterioBusqueda.toLowerCase()) !== -1);
    columna.filtrobusqueda = this.filtrosbusqueda;

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
  eliminarFiltro(etiqueta: string) { }

  pageClic(page: any) {
    this.consultarMonitoreos(page);
    this.page = page;
  }
}
