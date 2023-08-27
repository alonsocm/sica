import { Component, OnInit } from '@angular/core';
import { ClasificacionCriterio } from '../models/clasificacion-criterio';

@Component({
  selector: 'app-supervision-registro',
  templateUrl: './supervision-registro.component.html',
  styleUrls: ['./supervision-registro.component.css'],
})
export class SupervisionRegistroComponent implements OnInit {
  clasificaciones: Array<ClasificacionCriterio> = [];

  constructor() {}

  ngOnInit(): void {
    this.clasificaciones = [
      {
        numero: 1,
        descripcion: 'PREVIO A LA TOMA DE MUESTRAS',
        criterios: [
          {
            numero: 1,
            descripcion:
              'El sitio de muestreo es el correcto (verifican coordenadas y cumple con la base actualizada).*',
            puntaje: 2.5,
            cumple: false,
            noCumple: false,
            noAplica: false,
            observacion: 'observación uno',
          },
        ],
      },
    ];
  }
}
