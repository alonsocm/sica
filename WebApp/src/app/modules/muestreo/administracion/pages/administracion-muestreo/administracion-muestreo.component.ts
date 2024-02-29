import { Component, OnInit } from '@angular/core';
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

    this.isrevisionResultados = (estatus == estatusMuestreo.AprobacionResultado) ? true : false; 

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
}

