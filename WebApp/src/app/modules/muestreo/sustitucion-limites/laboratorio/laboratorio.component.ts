/// <reference path="../limites.service.ts" />
import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from '../../../../interfaces/filtro.interface';
import { estatusMuestreo } from '../../../../shared/enums/estatusMuestreo';
import { LimitesService } from '../limites.service';
import { FileService } from 'src/app/shared/services/file.service';
import { MensajesSustitución, TipoMensaje } from '../../../../shared/enums/tipoMensaje';



@Component({
  selector: 'app-laboratorio',
  templateUrl: './laboratorio.component.html',
  styleUrls: ['./laboratorio.component.css']
})
export class LaboratorioComponent extends BaseService implements OnInit {

  constructor(private limiteService: LimitesService) { 
    super();
  }
  anios: Array<number> = [];
  aniosseleccionados: Array<number> = [2020, 2021, 2022];
  esprimeravez: boolean = false;
  parametrosSinLimite: Array<any> = [];
  muestreos: Array<any> = [];

  ngOnInit(): void {

    this.columnas = [
      { nombre: 'noEntregaOCDL', etiqueta: 'N°. ENTREGA A REVISAR', orden: 1, filtro: new Filter() },
      { nombre: 'tipoSitio', etiqueta: 'TIPO DE SITIO', orden: 2, filtro: new Filter() },     
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 3, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 4, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 5, filtro: new Filter() },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA DE REALIZACIÓN', orden: 6, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 7, filtro: new Filter() },
      { nombre: 'cuerpoAgua', etiqueta: 'CUERPO AGUA', orden: 8, filtro: new Filter() },
      { nombre: 'tipoCuerpoAguaOriginal', etiqueta: 'TIPO CUERPO AGUA  ORIGINAL', orden: 9, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 10, filtro: new Filter() }
    ];
    this.limiteService.obtenerAnios().subscribe({
      next: (response: any) => {      
        this.anios = response;},
      error: (error) => { },
    });
    this.limiteService.esPrimeraVezSustLaboratorio().subscribe({
      next: (response: any) => {       
        this.esprimeravez = response;
        if (this.esprimeravez == true) { this.mostrarMensaje(MensajesSustitución.PrimeraVez, TipoMensaje.Alerta); }
      },
      error: (error) => { },
    });

    if (!this.esprimeravez) { this.obtenerMuestreosSustituidos(); }
    //this.limiteService.getResultadosParametrosEstatus(estatusMuestreo.AprobacionResultado).subscribe({
    //  next: (response: any) => {       
    //    this.resultadosFiltradosn = response.data;          
    //    this.loading = false;
    //  },
    //  error: (error) => { this.loading = false; },
    //}); 
  }
  seleccionar() { }
  exportarExcel() { }
  sustitucionLimite() {
    
    this.limiteService.actualizarLimitesLaboratorio(this.aniosseleccionados).subscribe({
      next: (response: any) => {       
        if (response.message != "") {
          if (response.message == MensajesSustitución.NoExistenResultados) {
            this.mostrarMensaje(
              MensajesSustitución.NoExistenResultados, TipoMensaje.Alerta);
            this.hacerScroll();
          }
          else {
            let archivoErrores = this.generarArchivoDeErrores(response.message);
            this.mostrarMensaje(
              'La sustitución no ha sido realizada.', TipoMensaje.Error);
            this.hacerScroll();
            FileService.download(archivoErrores, 'Parámetros faltantes.txt');
          }
        }
        else {
          this.mostrarMensaje(
            'La sustitución fue realizada con éxito.', TipoMensaje.Correcto);
        }
      },
      error: (error) => {  },
    });

        
  }
  obtenerMuestreosSustituidos(): void {
    this.loading = true;
    this.limiteService.obtenerMuestreosSustituidos().subscribe({
      next: (response: any) => {
        this.loading = false;
        this.muestreos = response.data;
        this.resultadosFiltradosn = response.data;
        //this.establecerValoresFiltrosTabla();
      },
      error: (error) => {
        this.loading = false;
      },
    });
  }

}
