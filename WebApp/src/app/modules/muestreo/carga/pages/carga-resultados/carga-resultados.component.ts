import { Component, ElementRef, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { BaseService } from 'src/app/shared/services/base.service';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-carga-resultados',
  templateUrl: './carga-resultados.component.html',
  styleUrls: ['./carga-resultados.component.css'],
})
export class CargaResultadosComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  reemplazarResultados: boolean = false;
  archivo: any;
  numeroEntrega: string = '';
  anioOperacion: string = '';
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef = {} as ElementRef;

  constructor(private muestreoService: MuestreoService) {
    super();
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Columna> = [
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE NOSEC',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE 5K',
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
      { nombre: 'ocdl', etiqueta: 'OC/DL', orden: 5, filtro: new Filter() },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'PROGRAMA ANUAL',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO SUBROGADO',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaProgramada',
        etiqueta: 'FECHA PROGRAMACIÓN',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 11,
        filtro: new Filter(),
      },
      {
        nombre: 'horaInicio',
        etiqueta: 'HORA INICIO MUESTREO',
        orden: 12,
        filtro: new Filter(),
      },
      {
        nombre: 'horaFin',
        etiqueta: 'HORA FIN MUESTREO',
        orden: 13,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaCarga',
        etiqueta: 'FECHA CAPTURA',
        orden: 14,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaCarga',
        etiqueta: 'FECHA CARGA SICA',
        orden: 15,
        filtro: new Filter(),
      },
      {
        nombre: 'numeroEntrega',
        etiqueta: 'NÚMERO CARGA',
        orden: 16,
        filtro: new Filter(),
      },
      {
        nombre: 'estatus',
        etiqueta: 'ESTATUS',
        orden: 17,
        filtro: new Filter(),
      },
    ];

    this.columnas = nombresColumnas;
  }

  private consultarMonitoreos(): void {
    this.muestreoService.obtenerMuestreos(false).subscribe({
      next: (response: any) => {
        this.muestreos = response.data;
        this.muestreosFiltrados = this.muestreos;
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
    });
  }

  cargarArchivo(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();

    if (this.archivo) {
      this.loading = !this.loading;

      this.muestreoService.cargarArchivo(this.archivo[0], false, this.reemplazarResultados).subscribe({
        next: (response: any) => {
          if (response.data.correcto) {
            this.loading = false;
            this.mostrarMensaje(
              'Archivo procesado correctamente.',
              TIPO_MENSAJE.exito
            );
            this.resetInputFile(this.inputExcelMonitoreos);
            this.consultarMonitoreos(); 
          }
          else{
            this.loading = false;
            this.numeroEntrega = response.data.numeroEntrega;
            this.anioOperacion = response.data.anio;
            document.getElementById('btnMdlConfirmacion')?.click();
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
  }

  sustituirResultados(){
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
        }
        else{
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
