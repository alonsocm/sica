import { Filter } from 'src/app/interfaces/filtro.interface';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NumberService } from '../../../../../../shared/services/number.service';
import { ReplicaDiferenteService } from 'src/app/modules/muestreo/aprobacion/replica-diferente/services/replica-diferente.service'
import { RepDiferente } from 'src/app/interfaces/Replicas/ReplicaDiferente'
import { FileService } from 'src/app/shared/services/file.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { AuthService } from 'src/app/modules/login/services/auth.service';
import { BaseService } from 'src/app/shared/services/base.service';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-replica-diferente',
  templateUrl: './replica-diferente.component.html',
  styleUrls: ['./replica-diferente.component.css']
})

export class ReplicaDiferenteComponent extends BaseService implements OnInit {
  muestreos: Array<RepDiferente> = [];
  muestreosFiltrados: Array<RepDiferente> = [];
  resultadosSeleccionados: Array<RepDiferente> = [];
  selectedItemsList: Array<any> = [];
  resultadosFiltro: any;
  NombArchivo: string = "";
  validadosFiltrados: Array<RepDiferente> = [];
  totalResultadosPorFiltro: number = 0;
  resultadosSeleccionadosDatos: any = null;
  archivo: any = null;

  filtronoEntrega = new Filter();
  filtroclaveUnica = new Filter();
  filtroclaveSitio = new Filter();
  filtroclaveMonitoreo = new Filter();
  filtronombreSitio = new Filter();
  filtroclaveParametro = new Filter();
  filtrolaboratorio = new Filter();
  filtrocuerpoAgua = new Filter();
  filtrocuerpoAguaO = new Filter();
  filtroresultadoAR = new Filter();
  filtroescorrectoROCDL = new Filter();
  filtroobservacionesOCDL = new Filter();
  filtroesCorectoresultado = new Filter();
  filtroobservacionSecaia = new Filter();
  filtroclasificaObserva = new Filter();
  filtroobservacionSRE = new Filter();
  filtrocomentarios = new Filter();
  filtrofechaObservaSRE = new Filter();
  filtroseaprueba = new Filter();
  filtrofechaEstatus = new Filter();
  filtronombreUsuario = new Filter();
  filtroestatus = new Filter();
  @ViewChild('inputExcelResultados') inputExcelResultados: ElementRef = {} as ElementRef;

  /* Se llaman en el método limpiarFiltros()*/
  @ViewChild('noEntrega') noEntrega: any;
  @ViewChild('claveUnica') claveUnica: any;
  @ViewChild('claveSitio') claveSitio: any;
  @ViewChild('claveMonitoreo') claveMonitoreo: any;
  @ViewChild('nombreSitio') nombreSitio: any;
  @ViewChild('claveParametro') claveParametro: any;
  @ViewChild('laboratorio') laboratorio: any;
  @ViewChild('cuerpoAgua') cuerpoAgua: any;
  @ViewChild('cuerpoAguaO') cuerpoAguaO: any;
  @ViewChild('resultadoAR') resultadoAR: any;
  @ViewChild('escorrectoROCDL') escorrectoROCDL: any;
  @ViewChild('observacionesOCDL') observacionesOCDL: any;
  @ViewChild('esCorectoresultado') esCorectoresultado: any;
  @ViewChild('observacionSecaia') observacionSecaia: any;
  @ViewChild('clasificaObserva') clasificaObserva: any;
  @ViewChild('observacionSRE') observacionSRE: any;
  @ViewChild('comentarios') comentarios: any;
  @ViewChild('fechaObservaSRE') fechaObservaSRE: any;
  @ViewChild('seaprueba') seaprueba: any;
  @ViewChild('fechaEstatus') fechaEstatus: any;
  @ViewChild('nombreUsuario') nombreUsuario: any;
  @ViewChild('estatus') estatus: any;

  constructor(
    private resultadosService: ReplicaDiferenteService,
    public numberService: NumberService,
    public authService: AuthService
  ) {
    super();
    this.filtroResumen = 'Seleccionar filtro';
  }

  ngOnInit(): void {
    this.consultarMonitoreos();
  }

  consultarMonitoreos(): void {
    this.resultadosService.getReplicaDiferente().subscribe({
      next: (response: any) => {
        this.muestreos = response.data;       
        this.muestreosFiltrados = this.muestreos;
        //this.establecerValoresFiltrosTabla();

      },
      error: (error) => { },
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
    this.resultadosSeleccionados = this.obtenerSeleccionados();
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.resultadosSeleccionados = this.obtenerSeleccionados();
  }

  obtenerSeleccionados(): Array<any> {
    return this.muestreosFiltrados.filter((f) => f.isChecked);
  }


  



  

  filtrar(): void {
    this.muestreosFiltrados = this.muestreos.filter((muestreos) => {
      return (
        (this.filtronoEntrega.selectedValue == 'Seleccione'
          ? true
          : muestreos.noEntrega == this.filtronoEntrega.selectedValue) &&
        (this.filtroclaveUnica.selectedValue == 'Seleccione'
          ? true
          : muestreos.claveUnica == this.filtroclaveUnica.selectedValue) &&
        (this.filtroclaveSitio.selectedValue == 'Seleccione'
          ? true
          : muestreos.claveSitio == this.filtroclaveSitio.selectedValue) &&
        (this.filtroclaveMonitoreo.selectedValue == 'Seleccione'
          ? true
          : muestreos.claveMonitoreo == this.filtroclaveMonitoreo.selectedValue) &&
        (this.filtronombreSitio.selectedValue == 'Seleccione'
          ? true
          : muestreos.nombreSitio == this.filtronombreSitio.selectedValue) &&
        (this.filtroclaveParametro.selectedValue == 'Seleccione'
          ? true
          : muestreos.claveParametro == this.filtroclaveParametro.selectedValue) &&
        (this.filtrolaboratorio.selectedValue == 'Seleccione'
          ? true
          : muestreos.laboratorio == this.filtrolaboratorio.selectedValue) &&
        (this.filtrolaboratorio.selectedValue == 'Seleccione'
          ? true
          : muestreos.laboratorio == this.filtrolaboratorio.selectedValue) &&
        (this.filtrocuerpoAgua.selectedValue == 'Seleccione'
          ? true
          : muestreos.tipoCuerpoAgua == this.filtrocuerpoAgua.selectedValue) &&
        (this.filtrocuerpoAguaO.selectedValue == 'Seleccione'
          ? true
          : muestreos.tipoCuerpoAguaOriginal == this.filtrocuerpoAguaO.selectedValue) &&
        (this.filtroresultadoAR.selectedValue == 'Seleccione'
          ? true
          : muestreos.resultadoActualizadoporReplica == this.filtroresultadoAR.selectedValue) &&
        (this.filtroescorrectoROCDL.selectedValue == 'Seleccione'
          ? true
          : muestreos.esCorrectoOCDL == this.filtroescorrectoROCDL.selectedValue) &&
        (this.filtroobservacionesOCDL.selectedValue == 'Seleccione'
          ? true
          : muestreos.observacionOCDL == this.filtroobservacionesOCDL.selectedValue) &&
        (this.filtroesCorectoresultado.selectedValue == 'Seleccione'
          ? true
          : muestreos.esCorrectoSECAIA == this.filtroesCorectoresultado.selectedValue) &&
        (this.filtroobservacionSecaia.selectedValue == 'Seleccione'
          ? true
          : muestreos.observacionSECAIA == this.filtroobservacionSecaia.selectedValue) &&
        (this.filtroclasificaObserva.selectedValue == 'Seleccione'
          ? true
          : muestreos.clasificacionObservacion == this.filtroclasificaObserva.selectedValue) &&
        (this.filtroobservacionSRE.selectedValue == 'Seleccione'
          ? true
          : muestreos.observacionSRENAMECA == this.filtroobservacionSRE.selectedValue) &&
        (this.filtrocomentarios.selectedValue == 'Seleccione'
          ? true
          : muestreos.comentariosAprobacionResultados == this.filtrocomentarios.selectedValue) &&
        (this.filtrofechaObservaSRE.selectedValue == 'Seleccione'
          ? true
          : muestreos.fechaObservacionSRENAMECA == this.filtrofechaObservaSRE.selectedValue) &&
        (this.filtroseaprueba.selectedValue == 'Seleccione'
          ? true
          : muestreos.seApruebaResultadodespuesdelaReplica == this.filtroseaprueba.selectedValue) &&
        (this.filtrofechaEstatus.selectedValue == 'Seleccione'
          ? true
          : muestreos.fechaEstatusFinal == this.filtrofechaEstatus.selectedValue) &&
        (this.filtronombreUsuario.selectedValue == 'Seleccione'
          ? true
          : muestreos.usuarioRevision == this.filtronombreUsuario.selectedValue) &&
        (this.filtroestatus.selectedValue == 'Seleccione'
          ? true
          : muestreos.estatusResultado == this.filtroestatus.selectedValue)

      );
    });
  }

  limpiarFiltros() {
    this.noEntrega.clear();
    this.claveUnica.clear();
    this.claveSitio.clear();
    this.claveMonitoreo.clear();
    this.nombreSitio.clear();
    this.claveParametro.clear();
    this.laboratorio.clear();
    this.cuerpoAgua.clear();
    this.cuerpoAguaO.clear();
    this.resultadoAR.clear();
    this.escorrectoROCDL.clear();
    this.observacionesOCDL.clear();
    this.esCorectoresultado.clear();
    this.observacionSecaia.clear();
    this.clasificaObserva.clear();
    this.observacionSRE.clear();
    this.comentarios.clear();
    this.fechaObservaSRE.clear();
    this.seaprueba.clear();
    this.fechaEstatus.clear();
    this.nombreUsuario.clear();
    this.estatus.clear();

    document.getElementById('dvMessage')?.click();
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    this.resultadosService.exportarResultadosExcel(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.seleccionarTodosChck = false;
        FileService.download(response, 'Revision_LNR.xlsx');
      },
      error: (response: any) => {
        this.mostrarMensaje(
          'No fue posible descargar la información',
          'danger'
        );
        this.hacerScroll();
      },
    });
  }

  cargarArchivo(event: any): void {
    this.archivo = event.target.files[0];

    if (this.archivo == null) {
      return this.mostrarMensaje(
        'Debe seleccionar un archivo para cargar',
        TIPO_MENSAJE.alerta
      );
    }
    this.loading = !this.loading;
    this.resultadosService.cargarArchivo(this.archivo).subscribe({
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
        const blob = new Blob(
          [error.error.Errors.toString().replaceAll(',', '\n')],
          {
            type: 'application/octet-stream',
          }
        );
        this.mostrarMensaje(
          'Se encontraron errores en el archivo procesado.',
          TIPO_MENSAJE.error
        );
        this.hacerScroll();
        FileService.download(blob, 'errores.txt');
      },
    });

    this.resetInputFile(this.inputExcelResultados);
  }

  AprobacionResultadoBloque(aprobar:boolean): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();
    let resultados = [];
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    if (aprobar) {
       resultados = muestreosSeleccionados.filter(
        (f) => f.seApruebaResultadodespuesdelaReplica === 'SI'
        
      );
      

      if (resultados.length === 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido aprobados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    }
    else {
       resultados = muestreosSeleccionados.filter(
        (f) => f.seApruebaResultadodespuesdelaReplica === 'NO'
      );

      if (resultados.length === 0) {
        this.mostrarMensaje(
          'No todos los resultados seleccionados han sido rechazados después de replica.',
          TipoMensaje.Alerta
        );
        return this.hacerScroll();
      }
    }
    
    for (let index = 0; index < resultados.length; index++) {
      resultados[index].usuarioRevisionId =
        this.authService.getUser().usuarioId;
      resultados[index].seApruebaResultadodespuesdelaReplica = 'NO';
    }    
    this.resultadosService.aprobacionPorBloque(
     resultados
    ).subscribe({
      next: (response: any) => {
        this.consultarMonitoreos();
        let mensaje = aprobar ? 'Se aprobo el resultado por bloque, correctamente' : 'Se rechazo el resultado por bloque, correctamente';
        this.mostrarMensaje(mensaje, 'success');
        this.hacerScroll();
        this.seleccionarTodosChck = false;
      },
      error: (response: any) => {
        this.mostrarMensaje('No fue posible aprobar el resultado', 'danger');
      },
    });
  }

  exportarResultadosGeneral(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    this.resultadosService.exportarResultadosExcelGeneral(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.seleccionarTodosChck = false;
        FileService.download(response, 'General.xlsx');
      },
      error: (response: any) => {
        this.mostrarMensaje(
          'No fue posible descargar la información',
          'danger'
        );
        this.hacerScroll();
      },
    });
  }

  seleccionarArchivo(event: any) {
    this.archivo = event.target.files[0];
  }

  EnviosAprobados(aprobarrechazar:number): void {
    
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    for (let index = 0; index < muestreosSeleccionados.length; index++) {
      muestreosSeleccionados[index].estatusResultadoId = aprobarrechazar;
    }
  
    this.EnvioAprobado(muestreosSeleccionados);
  }

  EnvioAprobado(filtro: Array<any> = []) {
    this.resultadosService.EnviarAprobado(filtro).subscribe({
      next: (response: any) => {
        this.consultarMonitoreos();
        this.mostrarMensaje('Resultados enviados correctamente', 'success');
        this.hacerScroll();
        this.seleccionarTodosChck = false;
      },
      error: (response: any) => {
        this.consultarMonitoreos();
        this.mostrarMensaje('No fue posible enviar el resultado', 'danger');
      },
    });
  }
}



