import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ParametrosService } from '../services/parametros.service';
import { Parametro } from '../models/parametro';
import { Column } from 'src/app/interfaces/filter/column';

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
    this.parametrosService.getParametros(this.page, this.pageSize).subscribe({
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

  onFilterIconClick(column: Column) {}

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.parametrosService.getParametros(this.page, this.pageSize).subscribe({
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
}
