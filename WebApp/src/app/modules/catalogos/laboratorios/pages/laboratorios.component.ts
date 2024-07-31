import { Component, OnInit } from '@angular/core';
import { Laboratorio } from '../../../../interfaces/catalogos/laboratorio.interface';
import { Column } from '../../../../interfaces/filter/column';
import { Item } from '../../../../interfaces/filter/item';
import { Notificacion } from '../../../../shared/models/notification-model';
import { BaseService } from '../../../../shared/services/base.service';
import { MuestreoService } from '../../../muestreo/liberacion/services/muestreo.service';
import { LaboratorioService } from '../services/laboratorios.service';

@Component({
  selector: 'app-laboratorios',
  templateUrl: './laboratorios.component.html',
  styleUrls: ['./laboratorios.component.css']
})
export class LaboratoriosComponent extends BaseService implements OnInit {
  laboratorios: Array<Laboratorio> = [];
  laboratoriosSeleccionados: Array<Laboratorio> = [];
  modalTitle: string = '';
  editar: boolean = false;
  public LaboratorioRegistro: Laboratorio = {
    id:null,
    descripcion: '',
    nomenclatura: '',
    selected: false
  }
  constructor(public muestreoService: MuestreoService, private laboratorioService: LaboratorioService) { super(); }
  notificacion: Notificacion = {
    title: 'Confirmar eliminación de laboratorio',
    text: '¿Está seguro de querer eliminar el laboratorio?',
    id: 'mdlConfirmacion'
  };
  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarLaboratorios();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'descripcion',
        label: 'NOMBRE LABORATORIO',
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
        name: 'nomenclatura',
        label: 'NOMENCLATURA',
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
      }    
    ];

    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  //cambiar porque hay que mandar el filter en caso de que exista filtrado

  public consultarLaboratorios(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena): void {
    this.loading = true;
    this.laboratorioService
      .obtenerLaboratorios(page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;        
          this.laboratorios = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.laboratorios, this.laboratoriosSeleccionados);
          this.selectedPage = this.anyUnselected(this.laboratorios) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    laboratorios: Array<Laboratorio>,
    laboratoriosSeleccionados: Array<Laboratorio>
  ) {
    laboratorios.forEach((f) => {
      let muestreoSeleccionado = laboratoriosSeleccionados.find(
        (x) => f.id === x.id
      );

      if (laboratoriosSeleccionados != undefined) {
        f.selected = true;
      }
    });
  }

  AddLaboratorio() { }

  UpdateLaboratorio() { }

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
      this.laboratorioService
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

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.laboratorioService
      .obtenerLaboratorios(this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.laboratorios = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarLaboratorios();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarLaboratorios();

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
    this.consultarLaboratorios();
    this.page = page;
  }
  DeleteLaboratorio() { }
}
