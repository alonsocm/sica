import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { BaseService } from 'src/app/shared/services/base.service';
import { SupervisionBusqueda } from './models/SupervisionBusqueda';

@Component({
  selector: 'app-supervision',
  templateUrl: './supervision.component.html',
  styleUrls: ['./supervision.component.css'],
})
export class SupervisionComponent extends BaseService implements OnInit {
  constructor() {
    super();
  }

  supervisionBusquedaForm = new FormGroup({
    ocdl: new FormControl(''),
    sitio: new FormControl(''),
    fechaMuestreo: new FormControl(''),
    puntaje: new FormControl(''),
    laboratorio: new FormControl(''),
    claveMuestreo: new FormControl(''),
    tipoCuerpoAgua: new FormControl(''),
  });

  ngOnInit(): void {
    this.definirColumnas();
  }

  definirColumnas() {
    this.columnas = [
      {
        nombre: 'ocdl',
        etiqueta: 'OC/DL realiza la supervisión',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'Laboratorio que realizó muestreo',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'sitio',
        etiqueta: 'Sitio',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMuestreo',
        etiqueta: 'Clave del muestreo',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaMuestreo',
        etiqueta: 'Fecha de muestreo',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'Tipo de cuerpo de agua',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'puntuaje',
        etiqueta: 'Puntaje obtenido',
        orden: 8,
        filtro: new Filter(),
      },
    ];
  }

  buscarSupervision() {
    console.log(this.supervisionBusquedaForm.value);
  }
}
