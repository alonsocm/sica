import { Component, OnInit } from '@angular/core';
import { ConsultaEvidenciaService } from '../services/consulta-evidencia.service';
import { Filter } from 'src/app/interfaces/filtroValidados.interface';
import { FileService } from 'src/app/shared/services/file.service';
import { Evidencia } from 'src/app/interfaces/Muestreo.interface';
import { ConsultaEvidencia } from 'src/app/interfaces/evidencia';
import { BaseService } from 'src/app/shared/services/base.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';

@Component({
  selector: 'app-consulta-evidencia',
  templateUrl: './consulta-evidencia.component.html',
  styleUrls: ['./consulta-evidencia.component.css'],
})
export class ConsultaEvidenciaComponent extends BaseService implements OnInit {
  filtroConsulta: string = '';
  muestreos: Array<ConsultaEvidencia> = [];
  muestreosFiltrados: Array<ConsultaEvidencia> = [];

  constructor(private consultaEvidencias: ConsultaEvidenciaService) {
    super();
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarEvidencias();
    this.filtroConsulta = 'Seleccionar filtro';
  }

  private definirColumnas() {
    this.columnas = [
      {
        nombre: 'claveSitioOriginal',
        etiqueta: 'CLAVE SITIO ORIGINAL',
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
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'organismoCuenca',
        etiqueta: 'ORGANISMO CUENCA',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'direccionLocal',
        etiqueta: 'DIRECCIÓN LOCAL',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'cuerpoAgua',
        etiqueta: 'CUERPO DE AGUA',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoSitio',
        etiqueta: 'TIPO  SITIO',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO SUBROGADO',
        orden: 11,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaProgramada',
        etiqueta: 'FECHA PROGRAMACIÓN',
        orden: 12,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 13,
        filtro: new Filter(),
      },
      {
        nombre: 'programaAnual',
        etiqueta: 'AÑO',
        orden: 14,
        filtro: new Filter(),
      },
      {
        nombre: 'horaInicio',
        etiqueta: 'HORA INICIO MUESTREO',
        orden: 15,
        filtro: new Filter(),
      },
      {
        nombre: 'horaFin',
        etiqueta: 'HORA FIN MUESTREO',
        orden: 16,
        filtro: new Filter(),
      },
      {
        nombre: 'observaciones',
        etiqueta: 'OBSERVACIONES',
        orden: 17,
        filtro: new Filter(),
      },
      {
        nombre: 'horaCargaEvidencias',
        etiqueta: 'FECHA CARGA EVIDENCIAS',
        orden: 18,
        filtro: new Filter(),
      },
      {
        nombre: 'numeroCargaEvidenciasSICA',
        etiqueta: 'N° CARGA EVIDENCIA',
        orden: 19,
        filtro: new Filter(),
      },
    ];
  }

  existeEvidencia(evidencias: Array<Evidencia>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  consultarEvidencias(): void {
    this.consultaEvidencias.obtenerMuestreos().subscribe({
      next: (response: any) => {
        this.muestreos = response.data;
        this.muestreosFiltrados = this.muestreos;
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => { },
    });
  }

  descargarEvidencias() {
    let fecha = new Date();
    let fechaActual =
      fecha.getDate() +
      '-' +
      (fecha.getMonth() + 1) +
      '-' +
      fecha.getFullYear();
    let muestreosSeleccionados = this.obtenerSeleccionados().map((m) => {
      return m.muestreoId;
    });

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'No ha seleccionado ningún monitoreo',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.consultaEvidencias
      .descargarArchivosEvidencias(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          console.table(response);
          this.loading = !this.loading;
          this.seleccionarTodosChck = false;
          this.muestreosFiltrados.map((m) => (m.isChecked = false));
          FileService.download(response, 'Evidencias/' + fechaActual + '.zip');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.muestreosFiltrados.map((m) => (m.isChecked = false));
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.hacerScroll();
        },
      });
  }

  exportarEvidencias(): void {
    this.loading = !this.loading;
    let fecha = new Date();
    let fechaActual =
      fecha.getDate() +
      '-' +
      (fecha.getMonth() + 1) +
      '-' +
      fecha.getFullYear();
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    this.consultaEvidencias
      .exportarEvidenciasExcel(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(
            response,
            'Información de Evidencias Aprobadas-' + fechaActual + '.xlsx'
          );
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.hacerScroll();
        },
      });
  }

  descargarEvidencia(claveMuestreo: string, sufijo: string) {
    this.loading = !this.loading;
    let muestreo = this.muestreosFiltrados.find(
      (x) => x.claveMonitoreo == claveMuestreo
    );
    let nombreEvidencia = muestreo?.evidencias.find((x) => x.sufijo == sufijo);

    this.consultaEvidencias
      .descargarArchivo(nombreEvidencia?.nombreArchivo ?? '')
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          FileService.download(response, nombreEvidencia?.nombreArchivo ?? '');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.hacerScroll();
        },
      });
  }

  seleccionarTodos(): void {
    this.muestreosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    this.obtenerSeleccionados();
  }

  obtenerSeleccionados(): Array<ConsultaEvidencia> {
    return this.muestreosFiltrados.filter((f) => f.isChecked);
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  filtrar(): void {
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
}
