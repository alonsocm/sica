import { Component, OnInit } from '@angular/core';
import { GrupoParametro } from 'src/app/interfaces/grupoParametro.interface';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { MuestreoService } from '../../services/muestreo.service';
import { SummaryOptions } from '../../models/summaryOptions';

@Component({
  selector: 'app-muestreos-totales',
  templateUrl: './muestreos-totales.component.html',
  styleUrls: ['./muestreos-totales.component.css'],
})
export class MuestreosTotalesComponent implements OnInit {
  filtroResumen: string;
  resultadosFiltro: any;
  totalResultadosPorFiltro = 0;
  muestreosFiltrados: Muestreo[] = [];
  grupoParametros: Array<GrupoParametro> = [];
  totalResultadosParametros: number = 0;
  totalMuestreos: number = 0;
  resumenMuestreos: any;

  constructor(private muestreoService: MuestreoService) {
    this.filtroResumen = 'Seleccionar filtro';

    muestreoService.summaryOptions$.subscribe((summaryOptions) => {
      this.muestreosFiltrados = summaryOptions.muestreos;
      this.totalMuestreos = summaryOptions.selectAll
        ? summaryOptions.total
        : this.muestreosFiltrados.length;
      this.generarResumenMonitoreosGpoParametro(summaryOptions);
    });
  }

  ngOnInit(): void {}

  filtrarResumen(): void {
    this.resultadosFiltro = [];
    this.totalResultadosPorFiltro = 0;
    let index = -1;
    switch (this.filtroResumen) {
      case 'OC':
        index = this.resumenMuestreos.findIndex((x: any) => x.tipo === 'OC');
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      case 'DL':
        index = this.resumenMuestreos.findIndex((x: any) => x.tipo === 'DL');
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      case 'Estado':
        index = this.resumenMuestreos.findIndex(
          (x: any) => x.tipo === 'Estado'
        );
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      case 'Tipo cuerpo de agua':
        index = this.resumenMuestreos.findIndex(
          (x: any) => x.tipo === 'TipoCuerpoAgua'
        );
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      case 'Laboratorio':
        index = this.resumenMuestreos.findIndex(
          (x: any) => x.tipo === 'Laboratorio'
        );
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      case 'Fecha realización':
        index = this.resumenMuestreos.findIndex((x: any) => x.tipo === 'Fecha');
        this.resultadosFiltro = this.resumenMuestreos[index].criterios;
        break;
      default:
        break;
    }

    this.totalResultadosPorFiltro = this.calcularTotalMonitoreosResumen(
      this.resultadosFiltro
    );
  }

  calcularTotalMonitoreosResumen(monitoreos: any): any {
    return monitoreos.reduce((accumulator: any, obj: any) => {
      return accumulator + obj.cantidad;
    }, 0);
  }

  generarResumenMonitoreosGpoParametro(summaryOptions: SummaryOptions): void {
    if (summaryOptions.muestreos.length == 0 && !summaryOptions.selectAll) {
      this.grupoParametros = [];
      this.totalResultadosParametros = 0;
    } else {
      this.muestreoService
        .obtenerResumenPorGpoParametros(
          summaryOptions.muestreos.map((m) => m.muestreoId),
          summaryOptions.selectAll,
          summaryOptions.filter
        )
        .subscribe({
          next: (response: any) => {
            this.resumenMuestreos = response.data.resumenMuestreo;
            this.grupoParametros = response.data.resumenResultado;
            this.filtrarResumen();
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
