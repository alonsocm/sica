import { Component, OnInit } from '@angular/core';
import { LimitesLaboratorios } from '../../../../interfaces/catalogos/limitesLaboratorio.interface';
import { Column } from '../../../../interfaces/filter/column';
import { Item } from '../../../../interfaces/filter/item';
import { Notificacion } from '../../../../shared/models/notification-model';
import { BaseService } from '../../../../shared/services/base.service';
import { MuestreoService } from '../../../muestreo/liberacion/services/muestreo.service';
import { LimiteLaboratorioService } from '../services/limite-laboratorios.service';

@Component({
  selector: 'app-limite-laboratorios',
  templateUrl: './limite-laboratorios.component.html',
  styleUrls: ['./limite-laboratorios.component.css']
})
export class LimiteLaboratoriosComponent extends BaseService implements OnInit {
  limitesLaboratorio: Array<LimitesLaboratorios> = [];
  limitesLaboratorioSeleccionados: Array<LimitesLaboratorios> = [];
  modalTitle: string = '';
  editar: boolean = false;
  laboratorios: Array<any> = [];
  parametros: Array<any> = [];
  laboratorioMuestreo: Array<any> = [];
  laboratoriosSubrogado: Array<any> = [];
  public LaboratorioRegistro: LimitesLaboratorios = {
    id:null,
    claveParametro: '',
    nombreParametro: '',
    parametroId: null,
    laboratorio: '',
    laboratorioId: null,
    realizaLaboratorioMuestreoId: null,
    realizaLaboratorioMuestreo: '',
    laboratorioMuestreo: '',
    laboratorioMuestreoId: null,
    mes: '',
    periodoId: null,
    activo: false,
    loSubroga: '',
    loSubrogaId: null,
    ldMaCumplir: '',
    lpCaCumplir: '',
    loMuestra: null,
    accionLaboratorio: '',
    accionLaboratorioId: null,
    laboratorioSubrogado: '',
    laboratorioSubrogadoId: null,
    metodoAnalitico: '',
    ldm: '',
    lpc: '',
    AnioId: null,
    selected: false,
    anio:''
  }
  constructor(private muestreoService: MuestreoService, private limiteLaboratorioService: LimiteLaboratorioService) { super(); }
  notificacion: Notificacion = {
    title: 'Confirmar eliminación de límite de laboratorio',
    text: '¿Está seguro de querer eliminar el límite de laboratorio?',
    id: 'mdlConfirmacion'
  };
  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarLimitesLaboratorio();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'parametro',
        label: 'PARÁMETRO',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'realizaLaboratorioMuestreo',
        label: 'REALIZA LABORATORIO MUESTREO',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'laboratorioMuestreo',
        label: 'LABORATORIO MUESTREO',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'periodo',
        label: 'MES',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'activo',
        label: 'ACTIVO',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'LDMaCumplir',
        label: 'LDM A CUMPLIR',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'LPCaCumplir',
        label: 'LPC A CUMPLIR',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'loMuestra',
        label: 'LO MUESTRA',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'loSubroga',
        label: 'LO SUBROGA',
        order: 10,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO QUE SUBROGA',
        order: 11,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'metodoAnalitico',
        label: 'MÉTODO ANÁLITICO',
        order: 12,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'LDM',
        label: 'LDM',
        order: 13,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'LPC',
        label: 'LPC',
        order: 14,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'anio',
        label: 'AÑO',
        order: 15,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      }
    ];

    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  cargarArchivo(evento: Event) { }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles

    let filteredColumns = this.getFilteredColumns(); //Obtenemos la lista de columnas que están filtradas
    this.muestreoService.filtrosSeleccionados = filteredColumns; //Actualizamos la lista de filtros, para el componente de filtro
    this.filtros = filteredColumns;

    this.obtenerLeyendaFiltroEspecial(column.dataType); //Se define el arreglo opcionesFiltros dependiendo del tipo de dato de la columna para mostrar las opciones correspondientes de filtrado

    let esFiltroEspecial = this.IsCustomFilter(column);

    if (
      (!column.filtered && !this.existeFiltrado) ||
      (column.isLatestFilter && this.filtros.length == 1)
    ) {
      this.cadena = '';
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }

    if (this.requiresToRefreshColumnValues(column)) {
      this.limiteLaboratorioService
        .getDistinctValuesFromColumn(column.name, this.cadena)
        .subscribe({
          next: (response: any) => {
            column.data = response.data.map((register: any) => {
              let item: Item = {
                value: register,
                checked: true,
              };
              return item;
            });

            column.filteredData = column.data;
            this.ordenarAscedente(column.filteredData);
            this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
          },
          error: (error) => { },
        });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  public consultarLimitesLaboratorio(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.limiteLaboratorioService
      .obtenerLimitesLaboratorioPaginados(page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {        
          this.selectedPage = false;
          this.limitesLaboratorio = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.limitesLaboratorio, this.limitesLaboratorio);
          this.selectedPage = this.anyUnselected(this.limitesLaboratorio) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    sitios: Array<LimitesLaboratorios>,
    sitiosSeleccionados: Array<LimitesLaboratorios>
  ) {
    sitios.forEach((f) => {
      let muestreoSeleccionado = sitiosSeleccionados.find(
        (x) => f.id === x.id
      );

      if (sitiosSeleccionados != undefined) {
        f.selected = true;
      }
    });
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.limiteLaboratorioService
      .obtenerLimitesLaboratorioPaginados(this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.limitesLaboratorio = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarLimitesLaboratorio();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarLimitesLaboratorio();

    this.columns
      .filter((x) => x.isLatestFilter)
      .map((m) => {
        m.isLatestFilter = false;
      });

    if (!isFiltroEspecial) {
      columna.filtered = true;
      columna.isLatestFilter = true;
    } else {
      this.columns
        .filter((x) => x.name == this.columnaFiltroEspecial.name)
        .map((m) => {
          (m.filtered = true),
            (m.selectedData = this.columnaFiltroEspecial.selectedData),
            (m.isLatestFilter = true);
        });
    }

    this.esHistorial = true;
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }

  pageClic(page: any) {
    this.consultarLimitesLaboratorio(page, this.NoPage, this.cadena);
    this.page = page;
  }

  AddLimites() { }

  UpdateLimites() { }

  DeleteLimiteLaboratorio() { }
}
