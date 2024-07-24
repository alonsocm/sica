import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Sitio } from '../../../../interfaces/catalogos/sitio.interface';
import { Column } from '../../../../interfaces/filter/column';
import { Item } from '../../../../interfaces/filter/item';
import { MuestreoService } from '../../../muestreo/liberacion/services/muestreo.service';
import { SitioService } from '../services/sitio.service';

@Component({
  selector: 'app-sitios',
  templateUrl: './sitios.component.html',
  styleUrls: ['./sitios.component.css']
})
export class SitiosComponent extends BaseService implements OnInit {
  sitios: Array<Sitio> = []; 
  sitiosSeleccionados: Array<Sitio> = [];
  public sitioRegistro: Sitio = {
    id: 0,
    claveSitio: '',
    nombreSitio: '',
    cuenca: '',
    direccionLocal: '',
    estado: '',
    municipio: '',
    cuerpoAgua: '',
    tipoCuerpoAgua: '',
    subtipoCuerpoAgua: '',
    latitud: 0,
    longitud: 0,
    observaciones: '',
    selected: false,
    organismoCuencaId: null,
    direccionLocalId: null,
    estadoId: 0,
    municipioId: 0,
    cuerpoAguaId: 0,
    tipoCuerpoAguaId: 0,
    subtipoCuerpoAguaId: 0,
    acuifero: '',
    acuiferoId: null
  };
  modalTitle: string = '';
  editar: boolean = false;
  cuencaDireccionesLocales: Array<any> = [];
  organismosCuenca: Array<any> = [];
  direccionesLocales: Array<any> = [];
  estados: Array<any> = [];
  municipios: Array<any> = [];
  cuerposAgua: Array<any> = [];
  tiposCuerpoAgua: Array<any> = [];
  subtiposCuerpoAgua: Array<any> = [];
  constructor(public muestreoService: MuestreoService, public sitioService: SitioService) { super(); }

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarSitios();
    this.cargarCombos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'claveSitio',
        label: 'CLAVE SITIO',
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
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
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
        name: 'acuifero',
        label: 'ACUIFERO',
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
        name: 'cuenca',
        label: 'ORGANISMO DE CUENCA',
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
        name: 'direccionLocal',
        label: 'DIRECCIÓN LOCAL',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'estado',
        label: 'ESTADO',
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
        name: 'municipio',
        label: 'MUNICIPIO',
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
        name: 'cuerpoAgua',
        label: 'CUERPO DE AGUA',
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
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO DE AGUA',
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
        name: 'subtipoCuerpoAgua',
        label: 'SUBTIPO CUERPO DE AGUA',
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
        name: 'latitud',
        label: 'LATITUD',
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
        name: 'longitud',
        label: 'LONGITUD',
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
        name: 'observaciones',
        label: 'OBSERVACIONES',
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
    ];

    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  cargarCombos() {
    this.sitioService
      .getCuencasDireccionesLocales()
      .subscribe({
        next: (response: any) => {         
          this.cuencaDireccionesLocales = response;
          this.organismosCuenca = this.cuencaDireccionesLocales.filter(
            (thing, i, arr) => arr.findIndex(t => t.organismoCuenca === thing.organismoCuenca) === i
          );
        },
        error: (error) => {
         
        },
      });
  }

  cargaDireccionLocal() {
    this.direccionesLocales = this.cuencaDireccionesLocales.filter(x => x.organismoCuencaId == this.sitioRegistro.organismoCuencaId && x.dieccionLocalId != null);
  }

  public consultarSitios(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.sitioService
      .obtenerSitiosPaginados(page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.sitios = response.data;    
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.sitios, this.sitiosSeleccionados);
          this.selectedPage = this.anyUnselected(this.sitios) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    sitios: Array<Sitio>,
    sitiosSeleccionados: Array<Sitio>
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
  onSelectClick(muestreo: Sitio) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.sitiosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.sitios) ? false : true;
    } else {
      let index = this.sitiosSeleccionados.findIndex(
        (m) => m.id === muestreo.id
      );

      if (index > -1) {
        this.sitiosSeleccionados.splice(index, 1);
      }
    }

  
  }
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
      this.sitioService
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
    this.sitioService
      .obtenerSitiosPaginados(this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.sitios = response.data;
        },
        error: (error) => { },
      });
  }


  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarSitios();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarSitios();

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
    this.consultarSitios(page, this.NoPage, this.cadena);
    this.page = page;
  }

  cargarArchivo(event: Event) { }
  RegistrarSitio() { }

  AddSites() {  }
  UpdateSites() { }
}
