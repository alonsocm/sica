import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';
import { MuestreoService } from 'src/app/modules/muestreo/liberacion/services/muestreo.service';
import { Component, OnInit, ViewChildren } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { BaseService } from 'src/app/shared/services/base.service';
import { TotalService } from '../../services/total.service';
import { estatusOcdlSecaia } from 'src/app/shared/enums/estatusOcdlSecaia';
import { ICommonMethods } from 'src/app/shared/interfaces/ICommonMethods';
import { Resultado } from 'src/app/interfaces/Resultado.interface';
import { Item } from 'src/app/interfaces/filter/item';
@Component({
  selector: 'app-validado',
  templateUrl: './validado.component.html',
  styleUrls: ['./validado.component.css'],
})
export class ValidadoComponent
  extends BaseService
  implements OnInit, ICommonMethods
{
  resultados: Array<Resultado> = [];
  resultadosSeleccionados: Array<Resultado> = [];
  muestreosSeleccionados: Array<Resultado> = [];
  muestreos: Array<Resultado> = [];
  firstColumnWidth: number = 75;

  constructor(
    private totalService: TotalService,
    private muestreoService: MuestreoService,
    private filtroHistorialService: FiltroHistorialService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.filtroHistorialService.columnName.subscribe((columnName) => {
      if (columnName !== '') {
        this.deleteFilter(columnName);
        this.consultarMuestreosSeleccionados();
      }
    });
    this.definirColumnas();
    this.consultarMuestreosSeleccionados();
  }
  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'noEntregaOCDL',
        label: 'N° ENTREGA OC/DL',
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
        name: 'claveUnica',
        label: 'CLAVE ÚNICA',
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
        name: 'claveSitio',
        label: 'CLAVE SITIO',
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
        name: 'claveMonitoreo',
        label: 'CLAVE MONITOREO',
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
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
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
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
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
      {
        name: 'laboratorio',
        label: 'LABORATORIO',
        order: 7,
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
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
        order: 8,
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
        name: 'tipoCuerpoAguaOriginal',
        label: 'TIPO CUERPO AGUA ORIGINAL',
        order: 9,
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
        name: 'resultado',
        label: 'RESULTADO',
        order: 10,
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
        name: 'tipoAprobacion',
        label: 'TIPO APROBACIÓN',
        order: 11,
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
        name: 'esCorrectoResultado',
        label: 'RESULTADO CORRECTO',
        order: 12,
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
        name: 'observaciones',
        label: 'OBSERVACIÓN OC/DL',
        order: 13,
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
        name: 'fechaLimiteRevision',
        label: 'FECHA LIMITE DE REVISIÓN',
        order: 14,
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
        name: 'nombreUsuario',
        label: 'USUARIO QUE REVISÓ',
        order: 15,
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
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
        order: 16,
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
        name: 'estatusResultado',
        label: 'ESTATUS DEL RESULTADO',
        order: 17,
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
    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  consultarMuestreosSeleccionados(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.totalService
      .getMuestreosPorParametro(
        estatusOcdlSecaia.Validado,
        true,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.resultados = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(
            this.resultados,
            this.resultadosSeleccionados
          );
          this.selectedPage = this.anyUnselected(this.resultados) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    resultados: Array<Resultado>,
    resultadosSeleccionados: Array<Resultado>
  ) {
    resultados.forEach((f) => {
      let resultadoSeleccionado = resultadosSeleccionados.find(
        (x) => f.claveUnica === x.claveUnica
      );

      if (resultadoSeleccionado != undefined) {
        f.isChecked = true;
      }
    });
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.totalService
      .getMuestreosPorParametro(
        estatusOcdlSecaia.Validado,
        true,
        this.page,
        this.NoPage,
        this.cadena,
        {
          column: column,
          type: type,
        }
      )
      .subscribe({
        next: (response: any) => {
          this.resultados = response.data;
        },
        error: (error) => {},
      });
  }
  onSelectClick(resultados: Resultado)
  {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    if (resultados.isChecked) {
      this.resultados.push(resultados);
      this.selectedPage = this.anyUnselected(this.resultados) ? false : true;
    } else {
      let index = this.resultados.findIndex(
        (m) => m.claveUnica === resultados.claveUnica
      );

      if (index > -1) {
        this.resultados.splice(index, 1);
      }
    }
  }

  onFilterClick(columna: Column, isFiltroEspecial: boolean) {
    this.loading = true;
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
      this.consultarMuestreosSeleccionados()

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
    this.filtroHistorialService.updateFilteredColumns(
      this.getFilteredColumns()
    );
    this.hideColumnFilter();
  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions();

    let filteredColumns = this.getFilteredColumns();
    this.filtros = filteredColumns;
    this.muestreoService.filtrosSeleccionados = filteredColumns;

    this.obtenerLeyendaFiltroEspecial(column.dataType);

    let esFiltroEspecial = this.IsCustomFilter(column);

    if (
      (!column.filtered && !this.existeFiltrado) ||
      (column.isLatestFilter && this.filtros.length == 1)
    ) {
      this.cadena = '';
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }

    if (this.requiresToRefreshColumnValues(column)) {
      this.totalService.getDistinct(column.name, this.cadena, estatusOcdlSecaia.Validado, true)
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
          error: (error) => {},
        });
    }
    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarMuestreosSeleccionados();
  }

  onPageClick(page: any) {
    this.page = page;
    this.consultarMuestreosSeleccionados(page);
  }


  exportarResultadosValidados() {}
  consultarMonitoreosmuestreo() {}
  enviarMonitoreos() {}
}