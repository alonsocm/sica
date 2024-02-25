import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { NumberService } from 'src/app/shared/services/number.service';
import { FileService } from 'src/app/shared/services/file.service';
import { MuestreoService } from '../../services/muestreo.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { DatePipe } from '@angular/common';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-carga',
  templateUrl: './carga.component.html',
  styleUrls: ['./carga.component.css'],
})
export class CargaComponent extends BaseService implements OnInit {
  archivo: any = null;
  archivos: any = null;
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  fechaLimiteRevision: string = '';
  filtroSitio: string = '';
  fechaActual: string = '';
  @ViewChild('inputArchivoMuestreos') inputArchivoMuestreos:ElementRef = {} as ElementRef;
  @ViewChild('inputArchivoEvidencias') inputArchivoEvidencias:ElementRef = {} as ElementRef;

  private definirColumnas() {
    this.columnas = [
      {
        nombre: 'ocdl',
        etiqueta: 'OC/DL',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'estado',
        etiqueta: 'ESTADO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaLimiteRevision',
        etiqueta: 'FECHA LÍMITE REVISIÓN',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'numeroEntrega',
        etiqueta: 'N° ENTREGA',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'estatus',
        etiqueta: 'ESTATUS',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCarga',
        etiqueta: 'TIPO CARGA RESULTADOS',
        orden: 11,
        filtro: new Filter(),
      },
    ];
  }

  constructor(
    public numberService: NumberService,
    private muestreoService: MuestreoService
  ) {
    super();
  }

  ngOnInit(): void {
    this.fechaActual = new DatePipe("en-US").transform(Date.now(), 'yyyy-MM-dd')??'';
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  private consultarMonitoreos(): void {
    this.muestreoService.obtenerMuestreos(true).subscribe({
      next: (response: any) => {
        this.muestreos = response.data;
        this.muestreosFiltrados = this.muestreos;
        this.muestreoService.muestreosSeleccionados =
          this.obtenerSeleccionados();
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => {},
    });
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  }

  private obtenerSeleccionados(): Array<Muestreo> {
    return this.muestreos.filter((f) => f.isChecked);
  }

  seleccionarArchivo(event: any) {
    this.archivo = event.target.files[0];
  }

  validarTamanoArchivos(archivos: FileList) {
    let error: any = '';
    for (let index = 0; index < archivos.length; index++) {
      const element = archivos[index];
      if (element.size === 0) {
        error += 'El archivo ' + element.name + ' está vacío,';
      }
    }
    return error;
  }

  cargarEvidencias(event: Event) {
    let archivos = (event.target as HTMLInputElement).files ?? new FileList();
    let errores = this.validarTamanoArchivos(archivos);

    if (errores !== '') {
      this.mostrarMensaje(
        'Se encontraron errores en las evidencias seleccionadas',
        TIPO_MENSAJE.alerta
      );

      let archivoErrores = this.generarArchivoDeErrores(errores);
      return FileService.download(archivoErrores, 'errores.txt');
    }

    this.loading = !this.loading;
    this.muestreoService.cargarEvidencias(archivos).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.consultarMonitoreos();
        this.mostrarMensaje(
          'Archivo procesado correctamente.',
          TIPO_MENSAJE.exito
        );
      },
      error: (error: any) => {
        this.loading = false;
        let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);

        this.mostrarMensaje(
          'Se encontraron errores en el archivo procesado.',
          TIPO_MENSAJE.error
        );

        FileService.download(archivoErrores, 'errores.txt');
        this.hacerScroll();
      },
    });
    this.resetInputFile(this.inputArchivoEvidencias);
  }

  cargarArchivo(event: Event): void {
    let archivo = (event.target as HTMLInputElement).files ?? new FileList();

    this.loading = !this.loading;
    this.muestreoService.cargarArchivo(archivo[0], true).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.mostrarMensaje(
          'Archivo procesado correctamente.',
          TIPO_MENSAJE.exito
        );
        this.consultarMonitoreos();
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
      },
    }
    );
    this.resetInputFile(this.inputArchivoMuestreos);
  }

  seleccionarTodos(): void {
    this.muestreosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    let muestreosSeleccionados = this.obtenerSeleccionados();
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.obtenerSeleccionados();
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
  }

  filtrar() {
    this.muestreosFiltrados = this.muestreos;
    this.columnas.forEach((columna) => {
      this.muestreosFiltrados = this.muestreosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });

    this.establecerValoresFiltrosTabla();
  }

  enviarMonitoreos(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (!(muestreosSeleccionados.length > 0)) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para enviar',
        TIPO_MENSAJE.alerta
      );
      return this.hacerScroll();
    }

    //Se comenta ya que la etapa de evidencias cargadas es desde ebaseca
    //let muestreosSinEvidencias = muestreosSeleccionados.filter(
    //  (f) => f.estatus !== 'Evidencias cargadas'
    //);

    //if (muestreosSinEvidencias.length > 0) {
    //  this.mostrarMensaje(
    //    'Solo se pueden enviar a revisión los muestreos con evidencias cargadas.',
    //    TIPO_MENSAJE.alerta
    //  );
    //  return this.hacerScroll();
    //}

    if (this.fechaLimiteRevision == '') {
      this.mostrarMensaje(
        'Debe elegir una fecha de revisión',
        TIPO_MENSAJE.alerta
      );
      return this.hacerScroll();
    }
    this.loading = true;
    this.muestreoService
      .enviarMuestreosRevision(
        muestreosSeleccionados.map((s) => ({
          muestreoId: s.muestreoId,
          claveMonitoreo: s.claveMonitoreo,
          fechaRevision: this.fechaLimiteRevision,
        }))
      )
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.mostrarMensaje(
            'Monitoreos enviados correctamente a revisión',
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

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo',
        TIPO_MENSAJE.alerta
      );
      return this.hacerScroll();
    }

    this.muestreoService
      .exportarResultadosExcel(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.muestreoService.muestreosSeleccionados = this.obtenerSeleccionados();
          this.seleccionarTodosChck = false;
          FileService.download(response, 'resultados.xlsx');
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TIPO_MENSAJE.error
          );
          this.hacerScroll();
        },
      });
  }

  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }

  confirmarEliminacion() {
    let muestreosSeleccionados = this.obtenerSeleccionados();
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
    let muestreosSeleccionados = this.obtenerSeleccionados();

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
        this.fechaLimiteRevision = '';
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
}
