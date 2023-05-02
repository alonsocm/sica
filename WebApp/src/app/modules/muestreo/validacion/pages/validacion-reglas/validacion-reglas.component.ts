import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-validacion-reglas',
  templateUrl: './validacion-reglas.component.html',
  styleUrls: ['./validacion-reglas.component.css'],
})
export class ValidacionReglasComponent extends BaseService implements OnInit {
  constructor() {
    super();
  }

  anios: Array<number> = [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023];
  entregas: Array<number> = [1, 2, 3];
  registros: Array<{
    anio: string;
    noEntrega: string;
    tipoSitio: string;
    claveUnica: string;
    claveSitio: string;
    claveMonitoreo: string;
    fechaRealizacion: string;
    laboratorio: string;
    claveParametro: string;
    resultado: string;
    validacionReglas: string;
    fechaReglas: string;
  }> = [{
    anio: '2012',
    noEntrega: '1',
    tipoSitio: 'LÉNTICO',
    claveSitio: 'DLAGU19',
    claveUnica: 'DLAGU19-CR_TOT',
    claveMonitoreo: 'DLAGU19-210822',
    fechaRealizacion: '19/08/2022',
    laboratorio: 'LABORATORIO',
    claveParametro: 'CR_TOT',
    resultado: '<0.005',
    validacionReglas: '',
    fechaReglas: '27/04/23'
  }];

  ngOnInit(): void {}
}
