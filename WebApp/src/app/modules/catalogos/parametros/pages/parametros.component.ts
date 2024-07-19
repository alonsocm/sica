import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ParametrosService } from '../services/parametros.service';
import { Parametro } from '../models/parametro';
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';

@Component({
  selector: 'app-parametros',
  templateUrl: './parametros.component.html',
  styleUrls: ['./parametros.component.css'],
})
export class ParametrosComponent extends BaseService implements OnInit {
  registros: Array<Parametro> = [];
  registrosSeleccionados: Array<Parametro> = [];

  constructor(private parametrosService: ParametrosService) {
    super();
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.getParametros();
  }

  definirColumnas() {
    let columnas: Array<Column> = [
      {
        name: 'clave',
        label: 'CLAVE',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'descripcion',
        label: 'DESCRIPCIÓN',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'grupo',
        label: 'GRUPO',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'subgrupo',
        label: 'SUBGRUPO',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'unidadMedida',
        label: 'UNIDAD DE MEDIDA',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'parametroPadre',
        label: 'PARÁMETRO PADRE',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
    ];

    this.columns = columnas;
  }

  getParametros() {
    this.parametrosService
      .getParametros(this.page, this.pageSize, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.registros = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.registros, this.registrosSeleccionados);
          this.selectedPage = this.anyUnselected(this.registros) ? false : true;
          this.loading = false;
          console.log(this.registros);
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  onSelectClick(registro: Parametro) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (registro.selected) {
      this.registrosSeleccionados.push(registro);
      this.selectedPage = this.anyUnselected(this.registros) ? false : true;
    } else {
      let index = this.registrosSeleccionados.findIndex(
        (m) => m.id === registro.id
      );

      if (index > -1) {
        this.registrosSeleccionados.splice(index, 1);
      }
    }
  }

  getPreviousSelected(
    muestreos: Array<Parametro>,
    muestreosSeleccionados: Array<Parametro>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.id === x.id
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles

    let filteredColumns = this.getFilteredColumns(); //Obtenemos la lista de columnas que están filtradas
    //this.muestreoService.filtrosSeleccionados = filteredColumns; //Actualizamos la lista de filtros, para el componente de filtro
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
      this.parametrosService.getDistinct(column.name, this.cadena).subscribe({
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
        error: (error) => {},
      });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.parametrosService
      .getParametros(this.page, this.pageSize, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.registros = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    // this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    // this.consultarMonitoreos();
  }

  pageClic(page: any) {
    this.page = page;
    this.getParametros();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.getParametros();

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
    //this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }
}
