import { Filter } from 'src/app/interfaces/filtro.interface';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FileService } from 'src/app/shared/services/file.service';
import { ReplicaTotalService } from '../../services/replica-total.service';
import { Replica } from 'src/app/interfaces/Replicas/replica';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-replica-total',
  templateUrl: './replica-total.component.html',
  styleUrls: ['./replica-total.component.css'],
})
export class ReplicaTotalComponent extends BaseService implements OnInit {
  archivo: any = null;
  resultadosFiltrados: Array<Replica> = [];
  resultados: Array<Replica> = [];
  nombredeArchivo: string = '';

  @ViewChild('inputExcelRevReplicas') inputExcelRevReplicas: ElementRef = {} as ElementRef; 
  @ViewChild('fileLNRUpload') fileLNRUpload: ElementRef = {} as ElementRef; 
  @ViewChild('evidenciasUpload') evidenciasUpload: ElementRef = {} as ElementRef; 

  constructor(private replicaTotalService: ReplicaTotalService) {
    super();
    this.filtroResumen = 'Seleccionar filtro';
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarReplicas();
  }

  private definirColumnas() {
    this.columnas = [
      {
        nombre: 'noEntrega',
        etiqueta: 'N° ENTREGA',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveUnica',
        etiqueta: 'CLAVE ÚNICA',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'nombre',
        etiqueta: 'NOMBRE SITIO',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'claveParametro',
        etiqueta: 'CLAVE PARÁMETRO',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'resultado',
        etiqueta: 'RESULTADO',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'esCorrectoOCDL',
        etiqueta: 'RESULTADO CORRECTO POR OC/DL',
        orden: 11,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionOCDL',
        etiqueta: 'OBSERVACIÓN OC/DL',
        orden: 12,
        filtro: new Filter(),
      },
      {
        nombre: 'esCorrectoSECAIA',
        etiqueta: 'RESULTADO CORRECTO POR SECAIA',
        orden: 13,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionSECAIA',
        etiqueta: 'OBSERVACIÓN SECAIA',
        orden: 14,
        filtro: new Filter(),
      },
      {
        nombre: 'clasificacionObservacion',
        etiqueta: 'CLASIFICACIÓN OBSERVACIÓN',
        orden: 15,
        filtro: new Filter(),
      },
      {
        nombre: 'causaRechazo',
        etiqueta: 'CAUSA RECHAZO',
        orden: 16,
        filtro: new Filter(),
      },
      {
        nombre: 'resultadoAceptado',
        etiqueta: 'SE ACEPTA RECHAZO (SÍ/NO)',
        orden: 17,
        filtro: new Filter(),
      },
      {
        nombre: 'resultadoReplica',
        etiqueta: 'RESULTADO RÉPLICA',
        orden: 18,
        filtro: new Filter(),
      },
      {
        nombre: 'esMismoResultado',
        etiqueta: 'MISMO RESULTADO',
        orden: 19,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionLaboratorio',
        etiqueta: 'OBSERVACIÓN LABORATORIO',
        orden: 20,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaReplicaLaboratorio',
        etiqueta: 'FECHA RÉPLICA LABORATORIO',
        orden: 21,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionSRNAMECA',
        etiqueta: 'OBSERVACIÓN SRENAMECA',
        orden: 22,
        filtro: new Filter(),
      },
      {
        nombre: 'comentarios',
        etiqueta: 'COMENTARIOS',
        orden: 23,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaObservacionRENAMECA',
        etiqueta: 'FECHA OBSERVACIÓN SRENAMECA',
        orden: 24,
        filtro: new Filter(),
      },
      {
        nombre: 'resultadoAprobadoDespuesReplica',
        etiqueta: 'RESULTADO APROBADO DESPUES DE RÉPLICA',
        orden: 25,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaEstatusFinal',
        etiqueta: 'FECHA ESTATUS FINAL',
        orden: 26,
        filtro: new Filter(),
      },
      {
        nombre: 'usuarioRevisor',
        etiqueta: 'NOMBRE USUARIO QUE REVISÓ',
        orden: 27,
        filtro: new Filter(),
      },
      {
        nombre: 'estatusResultado',
        etiqueta: 'ESTATUS DEL RESULTADO',
        orden: 28,
        filtro: new Filter(),
      },
      {
        nombre: '',
        etiqueta: 'EVIDENCIAS RÉPLICA',
        orden: 29,
        filtro: new Filter(),
      },
    ];
  }

  cargarArchivo(): void {
    if (this.archivo == null) {
      return this.mostrarMensaje(
        'Debe seleccionar un archivo para cargar',
        TipoMensaje.Alerta
      );
    }
  }

  consultarReplicas(): void {
    this.replicaTotalService.obtenerRegistros().subscribe({
      next: (response: any) => {
        this.resultados = response.data;
        this.resultadosFiltrados = response.data;
        console.log(this.resultadosFiltrados)
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => {},
    });
  }

  seleccionarTodos(): void {
    this.resultadosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    this.obtenerSeleccionados();
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
  }

  obtenerSeleccionados(): Array<Replica> {
    return this.resultadosFiltrados.filter((f) => f.isChecked);
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  filtrar(): void {
    this.resultadosFiltrados = this.resultados;
    this.columnas.forEach((columna) => {
      this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
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

  descargarResultadosParaReplicas() {
    let resultadosReplicaExcel = this.obtenerSeleccionados().map(
      (m) =>
        <ResultadoReplicaExcel>{
          noEntrega: m.noEntrega,
          claveUnica: m.claveUnica,
          claveSitio: m.claveSitio,
          claveMonitoreo: m.claveMonitoreo,
          nombre: m.nombre,
          claveParametro: m.claveParametro,
          laboratorio: m.laboratorio,
          tipoCuerpoAgua: m.tipoCuerpoAgua,
          tipoCuerpoAguaOriginal: m.tipoCuerpoAguaOriginal,
          resultado: m.resultado,
          observacionSECAIA: m.observacionSECAIA,
          clasificacionObservacion: m.clasificacionObservacion,
          causaRechazo: m.causaRechazo,
        }
    );

    if (resultadosReplicaExcel.length == 0) {
      return this.mostrarMensaje(
        'No ha seleccionado ningún registro',
        TipoMensaje.Alerta
      );
    }

    this.loading = !this.loading;

    this.replicaTotalService
      .obtenerExcelResultadosParaReplica(resultadosReplicaExcel)
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'Resultados para Replicas.xlsx');
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

  cargarArchivoRevisionReplicas(event: any) {
    const archivo: File = event.target.files[0];

    if (archivo) {
      this.loading = !this.loading;

      this.replicaTotalService
        .cargarArchivoRevisionReplicas(archivo)
        .subscribe({
          next: (response: any) => {
            this.consultarReplicas();
            this.loading = false;
            this.mostrarMensaje(
              'Evidencias cargadas correctamente.',
              TipoMensaje.Correcto
            );
            this.consultarReplicas();
          },
          error: (error: any) => {
            this.loading = false;
            const blob = new Blob(
              [error.error.Errors.toString().replaceAll(',', '\n')],
              {
                type: 'application/octet-stream',
              }
            );
            this.mostrarMensaje(
              'Se encontraron errores en los archivos procesado.',
              TipoMensaje.Error
            );
            this.hacerScroll();
            FileService.download(blob, 'errores.txt');
          },
        });

        this.resetInputFile(this.inputExcelRevReplicas);
    }
  }

  descargarReplicasReplicasLNR(esLNR: boolean) {
    let resultadosReplicaLNRExcel = this.obtenerSeleccionados().map(
      (m) =>
        <RevisionReplicasLNR>{
          noEntrega: m.noEntrega,
          claveUnica: m.claveUnica,
          claveSitio: m.claveSitio,
          claveMonitoreo: m.claveMonitoreo,
          nombre: m.nombre,
          claveParametro: m.claveParametro,
          laboratorio: m.laboratorio,
          tipoCuerpoAgua: m.tipoCuerpoAgua,
          tipoCuerpoAguaOriginal: m.tipoCuerpoAguaOriginal,
          resultado: m.resultado,
          observacionSECAIA: m.observacionSECAIA,
          clasificacionObservacion: m.clasificacionObservacion,
          causaRechazo: m.causaRechazo,
          resultadoCorrectoOCDL: m.esCorrectoOCDL,
          observacionOCDL: m.observacionOCDL,
          resultadoCorrectoSECAIA: m.esCorrectoSECAIA,
          seAceptaRechazo: m.resultadoAceptado,
          resultadoReplica: m.resultadoReplica,
          esMismoResultado: m.esMismoResultado,
          observacionLaboratorio: m.observacionLaboratorio,
          fechaReplicaLaboratorio: m.fechaReplicaLaboratorio,
          observacionSRENAMECA: m.observacionSRNAMECA,
          comentarios: m.comentarios,
        }
    );

    if (resultadosReplicaLNRExcel.length == 0) {
      return this.mostrarMensaje(
        'No ha seleccionado ningún registro',
        TipoMensaje.Alerta
      );
    }

    this.loading = !this.loading;

    this.replicaTotalService
      .obtenerExcelReplicas(resultadosReplicaLNRExcel, esLNR)
      .subscribe({
        next: (response: any) => {
          this.consultarReplicas();
          this.loading = !this.loading;
          this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'Revisión LNR.xlsx');
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

  cargarArchivoRevisionLNR(event: any) {
    const archivo: File = event.target.files[0];

    if (archivo) {
      this.loading = !this.loading;

      this.replicaTotalService.cargarArchivoRevisionLNR(archivo).subscribe({
        next: (response: any) => {
          this.loading = false;
          this.mostrarMensaje(
            'Archivo procesado correctamente.',
            TipoMensaje.Correcto
          );
          this.consultarReplicas();
        },
        error: (error: any) => {
          this.loading = false;
          const blob = new Blob(
            [error.error.Errors.toString().replaceAll(',', '\n')],
            {
              type: 'application/octet-stream',
            }
          );
          this.mostrarMensaje(
            'Se encontraron errores en el archivo procesado.',
            TipoMensaje.Error
          );
          this.hacerScroll();
          FileService.download(blob, 'errores.txt');
        },
      });

      this.resetInputFile(this.fileLNRUpload);
    }
  }

  aprobarResultadosBloque(aprobar: boolean) {
    let resultados = this.obtenerSeleccionados();

    if (!(resultados.length > 0)) {
      this.mostrarMensaje(
        'No ha seleccionado ningún registro',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    if (aprobar) {
      let muestreosSinEvidencias = resultados.filter(
        (f) => f.resultadoAprobadoDespuesReplica !== 'SI'
      );

      if (muestreosSinEvidencias.length > 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido aprobados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    } else {
      let muestreosSinEvidencias = resultados.filter(
        (f) => f.resultadoAprobadoDespuesReplica !== 'NO'
      );

      if (muestreosSinEvidencias.length > 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido rechazados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    }

    this.loading = true;
    this.replicaTotalService
      .aprobarResultadosBloque(
        resultados.map((s) => {
          return s.claveUnica;
        }),
        aprobar
      )
      .subscribe({
        next: (response) => {
          this.consultarReplicas();
          this.loading = false;
          this.mostrarMensaje(
            'Resultados aprobados correctamente',
            TipoMensaje.Correcto
          );
          this.hacerScroll();
          this.seleccionarTodosChck = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  enviarResultados(aprobados: boolean) {
    let resultados = this.obtenerSeleccionados();

    if (!(resultados.length > 0)) {
      this.mostrarMensaje(
        'No ha seleccionado ningún registro',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    if (aprobados) {
      let muestreosSinEvidencias = resultados.filter(
        (f) => f.resultadoAprobadoDespuesReplica !== 'SI'
      );

      if (muestreosSinEvidencias.length > 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido aprobados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    } else {
      let muestreosSinEvidencias = resultados.filter(
        (f) => f.resultadoAprobadoDespuesReplica !== 'NO'
      );

      if (muestreosSinEvidencias.length > 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido rechazados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    }

    this.loading = true;
    this.replicaTotalService
      .enviarResultados(
        resultados.map((s) => {
          return s.claveUnica;
        }),
        aprobados
      )
      .subscribe({
        next: (response) => {
          this.consultarReplicas();
          this.loading = false;
          this.mostrarMensaje(
            'Resultados aprobados correctamente',
            TipoMensaje.Correcto
          );
          this.hacerScroll();
          this.seleccionarTodosChck = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  enviarResultadosDiferenteDato(){
    let resultados = this.obtenerSeleccionados();

    if (!(resultados.length > 0)) {
      this.mostrarMensaje(
        'No ha seleccionado ningún registro',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    let muestreosSinEvidencias = resultados.filter(
      (f) => f.esMismoResultado !== 'NO'
    );

    if (muestreosSinEvidencias.length > 0) {
      this.mostrarMensaje(
        'No todos los resultados seleccionados pueden ser enviados a replicas con diferente dato.',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.replicaTotalService
      .enviarResultadosDiferenteDato(
        resultados.map((s) => {
          return s.claveUnica;
        })
      )
      .subscribe({
        next: (response) => {
          this.consultarReplicas();
          this.loading = false;
          this.mostrarMensaje(
            'Resultados enviados correctamente',
            TipoMensaje.Correcto
          );
          this.hacerScroll();
          this.seleccionarTodosChck = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  cargarEvidenciasReplicas(event: any) {
    const archivos: File[] = event.target.files;

    if (archivos) {
      this.loading = !this.loading;

      this.replicaTotalService
        .cargarEvidenciasReplicas(archivos)
        .subscribe({
          next: (response: any) => {
            this.loading = false;
            this.mostrarMensaje(
              'Evidencias cargadas correctamente.',
              TipoMensaje.Correcto
            );
            this.consultarReplicas();
          },
          error: (error: any) => {
            this.loading = false;
            const blob = new Blob(
              [error.error.Errors.toString().replaceAll(',', '\n')],
              {
                type: 'application/octet-stream',
              }
            );
            this.mostrarMensaje(
              'Se encontraron errores en el archivo procesado.',
              TipoMensaje.Error
            );
            this.hacerScroll();
            FileService.download(blob, 'errores.txt');
          },
        });

        this.resetInputFile(this.evidenciasUpload);
    }
  }

  descargarEvidenciasReplica(claveUnica?: string){
    let clavesUnicas: Array<string> = [];

    if (claveUnica != null) {
      clavesUnicas.push(claveUnica);
    }
    else{
      let resultadosSeleccionados = this.obtenerSeleccionados();
  
      if (resultadosSeleccionados.length === 0) {
        return  this.mostrarMensaje('Debe seleccionar un registro', TipoMensaje.Alerta)
      }

      clavesUnicas = resultadosSeleccionados.map((m) => {
        return m.claveUnica
      })
    }

    this.loading = !this.loading;
    this.replicaTotalService.descargarEvidencias(clavesUnicas).subscribe({
      next: (response: any) => {
        this.loading = !this.loading;
        this.seleccionarTodosChck = false;
        FileService.download(response, 'evidencias.zip');
      },
      error: (response: any) => {
        this.loading = !this.loading;
        this.mostrarMensaje(
          'No fue posible descargar la información',
          TipoMensaje.Error
        );
        this.hacerScroll();
      }      
    });
  }
}

export interface ResultadoReplicaExcel {
  noEntrega: string,
  claveUnica: string,
  claveSitio: string,
  claveMonitoreo: string,
  nombre: string,
  claveParametro: string,
  laboratorio: string,
  tipoCuerpoAgua: string,
  tipoCuerpoAguaOriginal: string,
  resultado: string,
  observacionSECAIA: string,
  clasificacionObservacion: string,
  causaRechazo: string,
}

export interface RevisionReplicasLNR extends ResultadoReplicaExcel{
  resultadoCorrectoOCDL: string,
  observacionOCDL: string,
  resultadoCorrectoSECAIA: string,
  seAceptaRechazo: string,
  resultadoReplica: string,
  esMismoResultado: string,
  observacionLaboratorio: string,
  fechaReplicaLaboratorio: string,
  observacionSRENAMECA: string,
  comentarios: string,
}
