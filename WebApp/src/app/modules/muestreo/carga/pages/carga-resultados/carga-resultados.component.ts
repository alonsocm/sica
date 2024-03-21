import { Component, ElementRef, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Filter, FilterFinal } from 'src/app/interfaces/filtro.interface';
import { Columna, ColumnaFinal } from 'src/app/interfaces/columna-inferface';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { from } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };






@Component({
  selector: 'app-carga-resultados',
  templateUrl: './carga-resultados.component.html',
  styleUrls: ['./carga-resultados.component.css'],
})
export class CargaResultadosComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  muestreosseleccionados: Array<Muestreo> = [];
  reemplazarResultados: boolean = false;
  resultadosEnviados: Array<number> = [];
  archivo: any;
  numeroEntrega: string = '';
  anioOperacion: string = '';
  esTemplate: boolean = true;
  initialValue: string = ''
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef = {} as ElementRef;
  registroParam: FormGroup;

  constructor(private muestreoService: MuestreoService, private fb: FormBuilder) {
    super();
    this.registroParam = this.fb.group({
      checkFiltro: [null],
      myFormCOntrolName: [null],
    });
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
    var a = document.getElementById("aprueba");
    console.log(a);
  
   
  
  }
  definirColumnas() {
    let nombresColumnas: Array<ColumnaFinal> = [
      { nombre: 'estatus', etiqueta: 'ESTATUS', orden: 1, filtro: new FilterFinal() },
      { nombre: 'evidencias', etiqueta: 'EVIDENCIAS COMPLETAS', orden: 2, filtro: new FilterFinal()  },
      { nombre: 'numeroEntrega', etiqueta: 'NÚMERO CARGA', orden: 3, filtro: new FilterFinal()  },
      { nombre: 'claveSitio', etiqueta: 'CLAVE NOSEC', orden: 4, filtro: new FilterFinal() },
      { nombre: '', etiqueta: 'CLAVE 5K', orden: 5, filtro: new FilterFinal()  },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 6, filtro: new FilterFinal()  },
      { nombre: 'tipoSitio', etiqueta: 'TIPO DE SITIO', orden: 7, filtro: new FilterFinal()  },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 8, filtro: new FilterFinal()  },
      { nombre: 'ocdl', etiqueta: 'OC/DL', orden: 9, filtro: new FilterFinal()  },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 10, filtro: new FilterFinal() },
      { nombre: 'subTipoCuerpoAgua', etiqueta: 'SUBTIPO CUERPO DE AGUA', orden: 11, filtro: new FilterFinal()  },
      { nombre: 'programaAnual', etiqueta: 'PROGRAMA ANUAL', orden: 12, filtro: new FilterFinal()  },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 13, filtro: new FilterFinal()  },
      { nombre: 'laboratorioSubrogado', etiqueta: 'LABORATORIO SUBROGADO', orden: 14, filtro: new FilterFinal() },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REALIZACIÓN', orden: 16, filtro: new FilterFinal()  },
      { nombre: 'fechaProgramada', etiqueta: 'FECHA PROGRAMACIÓN', orden: 15, filtro: new FilterFinal()  },
      { nombre: 'horaInicio', etiqueta: 'HORA INICIO MUESTREO', orden: 17, filtro: new FilterFinal() },
      { nombre: 'horaFin', etiqueta: 'HORA FIN MUESTREO', orden: 18, filtro: new FilterFinal()  },
      { nombre: 'fechaCarga', etiqueta: 'FECHA CARGA SICA', orden: 19, filtro: new FilterFinal()  },
      { nombre: 'fechaEntrega', etiqueta: 'FECHA ENTREGA', orden: 20, filtro: new FilterFinal()  },
    ];
    this.columnasF = nombresColumnas;
  }

  private consultarMonitoreos(): void {
    this.muestreoService.obtenerMuestreos(false).subscribe({
      next: (response: any) => {
        this.muestreos = response.data;        
        this.muestreosFiltrados = this.muestreos;
        console.log(this.muestreosFiltrados);
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => { },
    });
  }
  private establecerValoresFiltrosTabla() {
    this.columnasF.forEach((f) => {
      //f.filtro.values = [
      //  ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      //];

      f.filtro.values.push(
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      );

      //f.filtro.values.push("Seleccionar todos");
      //f.filtro.selectedValue = "Seleccionar todos";
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
          else {
            this.loading = false;
            this.numeroEntrega = response.data.numeroEntrega;
            this.anioOperacion = response.data.anio;
            document.getElementById('btnMdlConfirmacionActualizacion')?.click();
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
        }
        else {
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
    this.columnasF.forEach((columna) => {
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
    this.columnasF.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }
  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.getMuestreos();
  }

  seleccionarFiltro(): void {
    console.log("aquiii");

  }
  getMuestreos() {
    let muestreosSeleccionados = this.Seleccionados(this.muestreosFiltrados);
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
    this.muestreosseleccionados = muestreosSeleccionados;
  }
  exportarResultados(): void {
    if (this.muestreosseleccionados.length == 0 && this.muestreosFiltrados.length == 0) {
      this.mostrarMensaje('No hay información existente para descargar', 'warning');
      return this.hacerScroll();
    }

    this.loading = true;
    this.muestreosseleccionados = (this.muestreosseleccionados.length == 0) ? this.muestreosFiltrados : this.muestreosseleccionados;
    this.muestreoService.exportarCargaResultadosEbaseca(this.muestreosseleccionados)
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
    let valor = this.muestreosFiltrados.filter(x => x.estatus == "EvidenciasCargadas");

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

    this.muestreoService.enviarMuestreoaAcumulados(
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
              'Se enviaron ' + this.resultadosEnviados.length + ' muestreos a la etapa de "Acumulación resultados" correctamente',
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

  onFocused(event: any) {
    // do something
  }

}


