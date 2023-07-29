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
   


    this.limiteService.getResultadosParametrosEstatus(estatusMuestreo.AprobacionResultado).subscribe({

      next: (response: any) => {

        
        this.resultadosFiltradosn = response.data;
        console.log(this.resultadosFiltradosn);
        this.loading = false;
      },
      error: (error) => { this.loading = false; },
    }); 
  }
  seleccionar() { }

}
