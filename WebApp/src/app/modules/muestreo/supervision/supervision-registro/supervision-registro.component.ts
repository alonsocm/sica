import { ANALYZE_FOR_ENTRY_COMPONENTS, Component, OnInit } from '@angular/core';
import { ClasificacionCriterio } from '../models/clasificacion-criterio';
import { Criterio } from '../models/criterio';
import { FormControl, FormGroup } from '@angular/forms';

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
            critico: false,
            numero: 1,
            descripcion:
              'El sitio de muestreo es el correcto (verifican coordenadas y cumple con la base actualizada).*',
            puntaje: 2.5,
            cumplimiento: '',
            observacion: 'observación uno',
          },
        ],
      },
      {
        numero: 2,
        descripcion:
          'VERIFICACIÓN DE EQUIPO Y MATERIAL PARA MEDICIONES DE CAMPO Y MUESTREO',
        criterios: [
          {
            critico: false,
            numero: 1,
            descripcion:
              'Cuenta con termómetro o termopar para la medición de temperatura del agua con resolución de  0.1°C . *',
            puntaje: 2.5,
            cumplimiento: '',
            observacion: '',
          },
        ],
      },
    ];
  }
}
