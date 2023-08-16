/// <reference path="../limites.service.ts" />
import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from '../../../../interfaces/filtro.interface';
import { estatusMuestreo } from '../../../../shared/enums/estatusMuestreo';
import { LimitesService } from '../limites.service';



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
  esPrimeraVezSustitucion: boolean = false;

  ngOnInit(): void {

    this.columnas = [
      { nombre: 'noEntregaOCDL', etiqueta: 'N°. ENTREGA A REVISAR', orden: 0, filtro: new Filter() },
      { nombre: 'tipoSitio', etiqueta: 'TIPO DE SITIO', orden: 0, filtro: new Filter() },     
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA DE REALIZACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 0, filtro: new Filter() },
      { nombre: 'cuerpoAgua', etiqueta: 'CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAguaOriginal', etiqueta: 'TIPO CUERPO AGUA  ORIGINAL', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() }
    ];


    this.limiteService.obtenerAnios().subscribe({
      next: (response: any) => {      
        this.anios = response;},
      error: (error) => { },
    });

    this.limiteService.esPrimeraVezSustLaboratorio().subscribe({
      next: (response: any) => {
        this.esPrimeraVezSustitucion = response;   
      },
      error: (error) => { },
    });

    this.limiteService.getResultadosParametrosEstatus(estatusMuestreo.AprobacionResultado).subscribe({
      next: (response: any) => {        
        this.resultadosFiltradosn = response.data;          
        this.loading = false;
      },
      error: (error) => { this.loading = false; },
    }); 
  }
  seleccionar() { }
  exportarExcel() { }
  sustitucionLimite() {
    
    this.limiteService.actualizarLimitesLaboratorio(this.aniosseleccionados).subscribe({
      next: (response: any) => {             
      },
      error: (error) => {  },
    });

        
  }

}
