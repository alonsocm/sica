import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { vwEstatusMuestreosAdministracion } from '../../../../../interfaces/vwEstatusMuestreosAdministracion.interface';
import { estatusMuestreo } from '../../../../../shared/enums/estatusMuestreo';
import { BaseService } from '../../../../../shared/services/base.service';
import { FileService } from '../../../../../shared/services/file.service';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { ValidacionReglasService } from '../../../validacion/services/validacion-reglas.service';


@Component({
  selector: 'app-administracion-muestreo',
  templateUrl: './administracion-muestreo.component.html',
  styleUrls: ['./administracion-muestreo.component.css']
})
export class AdministracionMuestreoComponent extends BaseService implements OnInit {

  @ViewChild('canvas', { static: true }) myCanvas!: ElementRef;
  @ViewChild('canvasFinal', { static: true }) myCanvasFinal!: ElementRef;
  

  ejemplo: string = "#ejemplo";
  etapaCarga: string = "Carga Ebaseca";
  etapaValRegOrig: string = "Validación de registro original";
  etapaAcumulacion: string = "Acumulación de resultados";
  etapaIniReg: string = "Inicial de reglas";
  etapaReglas: string = "Módulo de reglas";
  etapaResumen: string = "Resumen validación registro original";
  etapaLiberacion: string = "Liberación de resultados";
  etapaRev: string = "Revisión de resultados";
  etapaAprob: string = "Aprobación de resultados";
  etapaAprobRev: string = "Revisíón de resulados";

  etapaReplicaRes: string = "Réplica de resultados";
  etapaReplicaDifDato: string = "Réplica diferente dato";
  etapaResumenAprob: string = "Resumen aprobación de resultados";
  etapaResAprobados: string = "Resultados aprobados";

  ismuestreoModal: boolean = false;
  ismuestreoModalResultados: boolean = false;
  isrevisionResultados: boolean = false;

  etapa: string = '';
  registroParam: FormGroup;
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  muestreosseleccionados: Array<Muestreo> = [];
  resultadosFiltrados: Array<acumuladosMuestreo> = [];
  resultadosseleccionados: Array<acumuladosMuestreo> = [];

  resultadosAdministracion: Array<vwEstatusMuestreosAdministracion> = [];

  encabezadosPorMuestreo: Array<string> = [
    'CLAVE NOSEC',
    'CLAVE 5K',
    'CLAVE MONITOREO',
    'NOMBRE SITIO',
    'OC/DL',
    'TIPO CUERPO AGUA',
    'PROGRAMA ANUAL',
    'LABORATORIO',
    'LABORATORIO SUBROGADO',
    'FECHA PROGRAMACIÓN',
    'FECHA REALIZACIÓN',
    'NÚMERO CARGA',
    'ESTATUS'

  ];
  encabezadosPorResultado: Array<string> = [
    'CLAVE ÚNICA',
    'CLAVE MUESTREO',
    'CLAVE CONALAB',
    'NOMBRE SITIO',
    'FECHA REAL VISITA',
    'TIPO DE SITIO',
    'TIPO CUERPO AGUA',
    'SUBTIPO CUERPO AGUA',
    'GRUPO PARÁMETRO',
    'PARÁMETRO',
    'UNIDAD DE MEDIDA',
    'RESULTADO',
    'NUEVO RESULTADO',
    'RÉPLICA',
    'CAMBIO DE RESULTADO'

  ];
  constructor(private muestreoService: MuestreoService, private fb: FormBuilder, private validacionService: ValidacionReglasService) {
    super();
    this.registroParam = this.fb.group({});

  }

  ngOnInit(): void {
    this.obtenerTotales();

    const canvas: HTMLCanvasElement = this.myCanvas.nativeElement;
    const canvasFinal: HTMLCanvasElement = this.myCanvasFinal.nativeElement;

    //canvasFinal.addEventListener()

    const contextFinal = canvasFinal.getContext('2d');



    if (contextFinal) {
    
     contextFinal.fillStyle = 'rgba(71, 246, 15, 0.57)';
      this.#drawRectangle1(contextFinal);
      this.#drawRectangle2(contextFinal);
      //this.#drawRectangle(contextFinal);
      
      
    }


    const context = canvas.getContext('2d');
    if (context) {
      context.strokeStyle = 'red';
      context.fillStyle = 'rgba(17, 0, 255, 0.5)';
     this.#useGradients(context);
      this.#drawRectangle(context);
      this.#drawTriangle(context);
      this.#drawArc(context);
      this.#drawCurve(context);
      this.#drawUsingPath(context);
      this.#drawLine(context);
      this.#drawText(context);
    }

  }
  enviarMonitoreos(Etapamod: string, esLiberacion: boolean) {
    this.etapa = "";
    this.etapa = Etapamod;
    this.muestreosFiltrados = [];
    this.consultarMonitoreos(esLiberacion);
    this.ismuestreoModal = true;
  }
  enviarResultados(Etapamod: string, estatus: number) { this.etapa = ""; this.etapa = Etapamod; this.resultadosFiltrados = []; this.cargarDatos(estatus); this.ismuestreoModalResultados = true; }

  private consultarMonitoreos(esLiberacion: boolean): void {
    this.muestreoService.obtenerMuestreos(esLiberacion).subscribe({
      next: (response: any) => {

        this.muestreos = response.data;
        this.muestreosFiltrados = this.muestreos;
      },

      error: (error) => { },
    });

  }
  cargarDatos(estatus: number) {

    this.isrevisionResultados = (estatus == estatusMuestreo.Aprobaciónderesultados) ? true : false; 

    this.validacionService.getResultadosAcumuladosParametros(estatus).subscribe({
      next: (response: any) => {
        this.resultadosFiltrados = response.data;
        this.loading = false;        
      },
      error: (error) => { this.loading = false; },
    });

  }
  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;

  }
  exportarExcelMuestreos(): void { 
    if (this.muestreosseleccionados.length == 0 && this.muestreosFiltrados.length == 0) {
      this.mostrarMensaje('No hay información existente para descargar', 'warning');
      return this.hacerScroll();
    }

    this.loading = true;
    this.muestreosseleccionados = (this.muestreosseleccionados.length == 0) ? this.muestreosFiltrados : this.muestreosseleccionados;
    this.muestreoService.exportarMuestreos(this.muestreosseleccionados)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, this.etapa + '.xlsx');
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
    this.muestreosFiltrados = [];
    this.muestreosseleccionados = [];
  }
  exportarExcelResultados(): void {
    if (this.resultadosseleccionados.length == 0 && this.resultadosFiltrados.length == 0) {
      this.mostrarMensaje('No hay información existente para descargar', 'warning');
      return this.hacerScroll();
    }
    this.loading = true;
    this.resultadosseleccionados = (this.resultadosseleccionados.length == 0) ? this.resultadosFiltrados : this.resultadosseleccionados;
    this.muestreoService.exportarResultados(this.resultadosseleccionados)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, this.etapa + '.xlsx');
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
    this.resultadosFiltrados = [];
    this.resultadosseleccionados = [];

  }
  obtenerTotales() {

    this.muestreoService.obtenerTotalesAdministracion().subscribe({
      next: (response: any) => {
        this.resultadosAdministracion = response.data;        
      },
      error: (error) => { },
    });

  }





  #drawRectangle(context: CanvasRenderingContext2D) {
    context.fillRect(20, 20, 100, 100);
  
    //context.clearRect(40, 40, 30, 30);
    //context.strokeRect(50, 50, 10, 10);
  }

  #drawRectangle1(context: CanvasRenderingContext2D) {
    context.fillRect(1180, 5, 300, 100);
    context.font = '20px Arial';
    context.shadowColor = 'rgba(71, 246, 15, 0.57)';
    context.fillStyle ="red"
    context.fillText('Hellooo', 1180, 35);
   
  }


  #drawRectangle2(context: CanvasRenderingContext2D) {
    context.fillRect(1180, 200, 300, 100);
    context.font = '20px Arial';
    context.shadowColor = 'rgba(71, 246, 15, 0.57)';
    context.fillStyle = "white"
    context.fillText('segundo', 1180, 230);
  }

  //#drawText(context: CanvasRenderingContext2D) {
  //  context.shadowOffsetX = 4;
  //  context.shadowOffsetY = 4;
  //  context.shadowBlur = 3;
  //  context.shadowColor = 'rgba(0, 0, 0, 0.5)';
  //  context.fillStyle = 'black';
  //  context.font = '48px Arial';
  //  context.fillText('Hello', 100, 500);
  //}




  #drawTriangle(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.moveTo(150, 70);
    context.lineTo(200, 20);
    context.lineTo(200, 120);
    context.fill();
     context.closePath();
     context.stroke();
  }
  #drawArc(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.arc(300, 100, 80, (Math.PI / 180) * 0, (Math.PI / 180) * 360);
    context.stroke();
     context.fill();
  }
  #drawCurve(context: CanvasRenderingContext2D) {
    context.beginPath();
    context.moveTo(500, 200);
    context.quadraticCurveTo(550, 0, 600, 200);
    context.stroke();
    context.beginPath();
    context.moveTo(700, 200);
    context.bezierCurveTo(750, 0, 750, 100, 800, 200);
    context.stroke();
  }

  #drawUsingPath(context: CanvasRenderingContext2D) {
    context.lineWidth = 20;
    context.lineJoin = 'bevel';
    const rectangle = new Path2D();
    rectangle.rect(20, 150, 100, 100);
    context.stroke(rectangle);
    const circle = new Path2D();
    circle.arc(300, 300, 80, (Math.PI / 180) * 0, (Math.PI / 180) * 360);
    context.fill(circle);
  }
  #drawLine(context: CanvasRenderingContext2D) {
    context.lineWidth = 10;
    context.lineCap = 'round';
    context.setLineDash([4, 4]);
    context.lineDashOffset = 0;
    context.beginPath();
    context.moveTo(100, 600);
    context.lineTo(200, 600);
    context.stroke();
  }
  #drawText(context: CanvasRenderingContext2D) {
    context.shadowOffsetX = 4;
    context.shadowOffsetY = 4;
    context.shadowBlur = 3;
    context.shadowColor = 'rgba(0, 0, 0, 0.5)';
    context.fillStyle = 'black';
    context.font = '48px Arial';
    context.fillText('Hello', 100, 500);
  }

  #useGradients(context: CanvasRenderingContext2D) {
    const lineargradient = context.createLinearGradient(20, 20, 120, 120);
    lineargradient.addColorStop(0, 'white');
    lineargradient.addColorStop(1, 'black');
    context.fillStyle = lineargradient;
    const radgrad = context.createRadialGradient(300, 300, 40, 300, 300, 80);
    radgrad.addColorStop(0, '#A7D30C');
    radgrad.addColorStop(0.9, '#019F62');
    radgrad.addColorStop(1, 'rgba(1, 159, 98, 0.5)');
    context.fillStyle = radgrad;
    const conicGrad = context.createConicGradient((Math.PI / 180) * 0, 150, 70);
    conicGrad.addColorStop(0, '#A7D30C');
    conicGrad.addColorStop(1, '#fff');
    context.fillStyle = conicGrad;
  }
  
}

