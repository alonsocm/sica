import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';

@Component({
  selector: 'app-validacion-reglas',
  templateUrl: './validacion-reglas.component.html',
  styleUrls: ['./validacion-reglas.component.css'],
})
export class ValidacionReglasComponent extends BaseService implements OnInit {
  constructor(private validacionService: ValidacionReglasService) {
    super();
  }

  anios: Array<number> = [];
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
    validacionPorReglas: string;
    fechaAplicacionReglas: string;
  }> = [];

  ngOnInit(): void {
    this.validacionService.obtenerMuestreos().subscribe({
      next: (response: any) => {
        this.anios = response.data;
      },
      error: (error) => {},
    });

    let anios = [2022];
    let numeroEntrega = [0];

    this.validacionService.obtenerResultadosValidadosPorReglas(anios, numeroEntrega).subscribe({
      next: (response: any) => {
        this.registros = response.data;
      },
      error: (error) => {},
    });
  }
}
