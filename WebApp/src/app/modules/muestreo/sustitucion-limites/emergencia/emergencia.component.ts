import { Component, OnInit } from '@angular/core';
import { LimitesService } from '../limites.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Columna } from 'src/app/interfaces/columna-inferface';

@Component({
  selector: 'app-emergencia',
  templateUrl: './emergencia.component.html',
  styleUrls: ['./emergencia.component.css']
})
export class EmergenciaComponent extends BaseService implements OnInit {
  registros: Array<any> = [];

  constructor(private limitesService: LimitesService) {
    super()
  }

  ngOnInit(): void {
    this.obtenerMuestreosSustituidos();
  }

  exportarResumen(){
  }

  obtenerMuestreosSustituidos() {
    this.limitesService.obtenerMuestreosSustituidos().subscribe({
      next: (response: any) => {
        this.registros = response.data;
        console.table(response.data);
      },
      error: (error) => {},
    });
  }

  definirColumnas() {
    let nombresColumnas: Array<Columna> = [
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE DEL SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO DE AGUA',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 5,
        filtro: new Filter(),
      },
      { nombre: 'anio', 
        etiqueta: 'AÑO', 
        orden: 6, 
        filtro: new Filter() },
    ]}
}