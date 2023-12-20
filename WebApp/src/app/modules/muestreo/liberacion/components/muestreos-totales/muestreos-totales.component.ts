import { Component, OnInit } from '@angular/core';
import { GrupoParametro } from 'src/app/interfaces/grupoParametro.interface';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { MuestreoService } from '../../services/muestreo.service';

@Component({
  selector: 'app-muestreos-totales',
  templateUrl: './muestreos-totales.component.html',
  styleUrls: ['./muestreos-totales.component.css']
})
export class MuestreosTotalesComponent implements OnInit {
  filtroResumen: string;
  resultadosFiltro: any;
  totalResultadosPorFiltro = 0;
  muestreosFiltrados: Muestreo[] = [];
  grupoParametros: Array<GrupoParametro> = [];
  totalResultadosParametros: number = 0;
  totalMuestreos: number = 0;


  constructor(private muestreoService: MuestreoService) {
    this.filtroResumen = 'Seleccionar filtro';
    
    muestreoService.muestreos.subscribe(
      (muestreos) => {
        this.muestreosFiltrados = muestreos;
        this.totalMuestreos = this.muestreosFiltrados.length;
        this.filtrarResumen();
        this.generarResumenMonitoreosGpoParametro(this.muestreosFiltrados);
      }
    );
  }

  ngOnInit(): void {
  }

  filtrarResumen(): void {
    this.resultadosFiltro = [];
    this.totalResultadosPorFiltro = 0;
    switch (this.filtroResumen) {
      case 'OC':
        let oc = this.muestreosFiltrados
          .filter((f) => f.isChecked && f.ocdl.startsWith('OC'))
          .map((m) => m.ocdl);
        this.resultadosFiltro = this.obtenerResumenMonitoreosSeleccionados(oc);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      case 'DL':
        let direccionesLocales = this.muestreosFiltrados
          .filter((f) => f.isChecked && f.ocdl.startsWith('DL'))
          .map((m) => m.ocdl);
        this.resultadosFiltro =
          this.obtenerResumenMonitoreosSeleccionados(direccionesLocales);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      case 'Estado':
        let estados = this.muestreosFiltrados
          .filter((f) => f.isChecked)
          .map((m) => m.estado);
        this.resultadosFiltro =
          this.obtenerResumenMonitoreosSeleccionados(estados);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      case 'Tipo cuerpo de agua':
        let cuerpoAgua = this.muestreosFiltrados
          .filter((f) => f.isChecked)
          .map((m) => m.tipoCuerpoAgua);
        this.resultadosFiltro =
          this.obtenerResumenMonitoreosSeleccionados(cuerpoAgua);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      case 'Laboratorio':
        let laboratorio = this.muestreosFiltrados
          .filter((f) => f.isChecked)
          .map((m) => m.laboratorio);
        this.resultadosFiltro =
          this.obtenerResumenMonitoreosSeleccionados(laboratorio);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      case 'Fecha realización':
        let fechaRealizacion = this.muestreosFiltrados
          .filter((f) => f.isChecked)
          .map((m) => m.fechaRealizacion);
        this.resultadosFiltro =
          this.obtenerResumenMonitoreosSeleccionados(fechaRealizacion);
        this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
          this.resultadosFiltro
        );
        break;
      default:
        break;
    }
  }

  obtenerResumenMonitoreosSeleccionados(
    muestreos: any
  ): { nombre: string; valor: unknown }[] {
    this.resultadosFiltro = muestreos.reduce(
      (prev: any, cur: any) => ((prev[cur] = prev[cur] + 1 || 1), prev),
      []
    );
    return Object.entries(this.resultadosFiltro).map(([nombre, valor]) => ({
      nombre,
      valor,
    }));
  }

  calcularTotalMonitoreosResumen(monitoreos: any): any {
    return monitoreos.reduce((accumulator: any, obj: any) => {
      return accumulator + obj.valor;
    }, 0);
  }

  generarResumenMonitoreosGpoParametro(
    muestreosSelecionados: Array<Muestreo>
  ): void {
    if (muestreosSelecionados.length == 0) {
      this.grupoParametros = [];
      this.totalResultadosParametros = 0;
    } else {
      this.muestreoService
        .obtenerResumenPorGpoParametros(
          muestreosSelecionados.map((m) => m.muestreoId)
        )
        .subscribe({
          next: (response: any) => {
            this.grupoParametros = response.data;
            this.totalResultadosParametros = this.grupoParametros.reduce(
              (accumulator, obj: any) => {
                return accumulator + obj.cantidad;
              },
              0
            );
          },
        });
    }
  }

}
