import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

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
  entregas: Array<number> = [0];
  aniosSeleccionados: Array<number> = [];
  entregasSeleccionadas: Array<number> = [];
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
  }

  validarMuestreosPorReglas(){
    if (this.aniosSeleccionados.length == 0 || this.entregasSeleccionadas.length == 0 ) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un año y número de entrega.',
        TIPO_MENSAJE.alerta
      );
      return;
    }

    this.loading = !this.loading;
    this.validacionService.obtenerResultadosValidadosPorReglas(this.aniosSeleccionados, this.entregasSeleccionadas).subscribe({
      next: (response: any) => {
        this.registros = response.data;
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
      },
    });
  }

  exportarResultados(): void {
    if (this.aniosSeleccionados.length != 0 &&this.aniosSeleccionados.length != 0) {
      this.validacionService.exportarResumenResultadosValidadosPorReglas(this.aniosSeleccionados, this.entregasSeleccionadas).subscribe({
        next: (response: any) => {
          FileService.download(response, 'ResumenValidacionReglas.xlsx');
        },
        error: (response: any) => {
        this.mostrarMensaje(
          'No fue posible descargar la información',
          TIPO_MENSAJE.error
          );
          this.hacerScroll();
        },
      });

      return;
    }

    this.mostrarMensaje(
      'No existen resultados para exportar.',
      TIPO_MENSAJE.alerta
      );
  }
}
