import { Filter } from 'src/app/interfaces/filtro.interface';
import { Component, OnInit, ViewChildren } from '@angular/core';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { FileService } from 'src/app/shared/services/file.service';
import { ReplicaResumenService } from '../../services/replica-resumen.service';
import { ReplicaResumen } from 'src/app/interfaces/Replicas/replicaResumen';

@Component({
  selector: 'app-replica-resumen',
  templateUrl: './replica-resumen.component.html',
  styleUrls: ['./replica-resumen.component.css'],
})
export class ReplicaResumenComponent implements OnInit {
  public page: number | undefined;
  loading: boolean = false;
  muestreosFiltrados: Array<ReplicaResumen> = [];
  resultadosSeleccionados: Array<ReplicaResumen> = [];
  seleccionarTodosChck: boolean = false;
  filtroResumen: string = '';
  keyword: string = 'values';
  mostrarAlerta: boolean = false;
  mensajeAlerta: string = '';
  tipoAlerta: string = '';
  columnas: Array<Columna> = [];
  archivo: any = null;
  resultadosFiltrados: Array<ReplicaResumen> = [];
  nombredeArchivo: string = '';
  resultados: Array<ReplicaResumen> = [];

  @ViewChildren('filtros') filtros: any;

  constructor(
    private replicaResumenService: ReplicaResumenService,
  ) {
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
    this.filtroResumen = 'Seleccionar filtro';
  }

  private definirColumnas() {
    this.columnas = [
      {
        nombre: 'noEntrega',
        etiqueta: 'N° ENTREGA',
        orden: 1,
        filtro: new Filter()
      },
      {
        nombre: 'claveUnica',
        etiqueta: 'CLAVE ÚNICA',
        orden: 2,
        filtro: new Filter()
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 3,
        filtro: new Filter()
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 4,
        filtro: new Filter()
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 5,
        filtro: new Filter()
      },
      {
        nombre: 'claveParametro',
        etiqueta: 'CLAVE PARÁMETRO',
        orden: 6,
        filtro: new Filter()
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 7,
        filtro: new Filter()
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 8,
        filtro: new Filter()
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 9,
        filtro: new Filter()
      },
      {
        nombre: 'resultado',
        etiqueta: 'RESULTADO',
        orden: 10,
        filtro: new Filter()
      },
      {
        nombre: 'estatus',
        etiqueta: 'ESTATUS DEL RESULTADO',
        orden: 11,
        filtro: new Filter()
      },
    ];
  }

  mostrarMensaje(mensaje: string, tipo: string): void {
    this.mensajeAlerta = mensaje;
    this.tipoAlerta = tipo;
    this.mostrarAlerta = true;
  }

  hacerScroll() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }

  consultarMonitoreos(): void {
    this.replicaResumenService.obtenerReplicasResumen().subscribe({
      next: (response: any) => {
        this.resultados = response.data;
        this.resultadosFiltrados = this.resultados;
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => { },
    });
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    this.replicaResumenService.exportarResultadosExcel(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.seleccionarTodosChck = false;
        FileService.download(response, 'General.xlsx');
      },
      error: (response: any) => {
        this.mostrarMensaje(
          'No fue posible descargar la información',
          'danger'
        );
        this.hacerScroll();
      },
    });
  }

  seleccionarTodos(): void {
    this.resultadosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    this.resultadosSeleccionados = this.obtenerSeleccionados();
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.resultadosSeleccionados = this.obtenerSeleccionados();
  }

  obtenerSeleccionados(): Array<ReplicaResumen> {
    return this.resultadosFiltrados.filter((f) => f.isChecked);
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  filtrar(): void {
    this.resultadosFiltrados = this.resultados;
    this.columnas.forEach((columna) => {
      this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });

    this.establecerValoresFiltrosTabla();
  }

  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }
}
