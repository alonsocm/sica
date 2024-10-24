import { MuestreoService } from 'src/app/modules/muestreo/liberacion/services/muestreo.service';
import { Component, OnInit } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { ICommonMethods } from 'src/app/shared/interfaces/ICommonMethods';
import { BaseService } from 'src/app/shared/services/base.service';
import { ResultadosValidadosService } from './services/resultados-validados.service';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';
import { Item } from 'src/app/interfaces/filter/item';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NotificationType } from 'src/app/shared/enums/notification-type';
import { FileService } from 'src/app/shared/services/file.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { Subscription } from 'rxjs';
import { ResultadoValidado } from './models/resultado-validado';
@Component({
  selector: 'app-validado',
  templateUrl: './validado.component.html',
  styleUrls: ['./validado.component.css'],
})
export class ValidadoComponent
  extends BaseService
  implements OnInit, ICommonMethods
{
  estatusEnviado: estatusMuestreo;
  filtroHistorialServiceSub: Subscription;
  resultados: Array<ResultadoValidado> = [];
  resultadosSeleccionados: Array<ResultadoValidado> = [];

  constructor(
    private filtroHistorialService: FiltroHistorialService,
    public muestreoService: MuestreoService,
    private resultadosService: ResultadosValidadosService,
    private notificationService: NotificationService
  ) {
    super();
    this.estatusEnviado = estatusMuestreo.ResumenValidaciónReglas;
    this.filtroHistorialServiceSub =
      this.filtroHistorialService.columnName.subscribe((columnName) => {
        this.deleteFilter(columnName);
        this.consultarResultados();
      });
  }

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarResultados();
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

  consultarResultados(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.resultadosService
      .getResultadosValidadosPorOCDL(page, pageSize, filter, this.orderBy)
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
          this.selectedPage = this.anyUnselected(this.resultados)
            ? false
            : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    resultados: Array<ResultadoValidado>,
    resultadosSeleccionados: Array<ResultadoValidado>
  ) {
    resultados.forEach((f) => {
      let resultadoSeleccionado = resultadosSeleccionados.find(
        (x) => f.resultadoId === x.resultadoId
      );

      if (resultadoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  onExportarResultadosClick(): void {
    if (this.resultadosSeleccionados.length === 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }

    this.loading = true;
    let resultadosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      resultadosSeleccionados = this.resultadosSeleccionados.map((s) => {
        return s.resultadoId;
      });
    }

    this.resultadosService
      .exportarResultadosValidados(resultadosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'RESULTADOS_VALIDADOS.xlsx');
          this.resetValues();
          this.unselectResultados();
          this.loading = false;
        },
        error: (response: any) => {
          this.loading = false;
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible descargar la información',
          });
        },
      });
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.resultadosService
      .getResultadosValidadosPorOCDL(this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.resultados = response.data;
        },
        error: (error) => {},
      });
  }

  onFilterClick(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarResultados();

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

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions();
    let filteredColumns = this.getFilteredColumns();
    this.muestreoService.filtrosSeleccionados = filteredColumns;
    this.filtros = filteredColumns;
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
      this.resultadosService
        .getDistinctOCDL(column.name, this.cadena)
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
    this.consultarResultados();
  }

  onPageClick(page: any) {
    this.consultarResultados(page, this.NoPage, this.cadena);
    this.page = page;
  }

  override onSelectPageClick(
    resultados: Array<ResultadoValidado>,
    resultadosSeleccionados: Array<ResultadoValidado>
  ) {
    resultados.map((m) => {
      m.selected = this.selectedPage;
      let index = resultadosSeleccionados.findIndex(
        (d) => d.resultadoId === m.resultadoId
      );

      if (index == -1) {
        resultadosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        resultadosSeleccionados.splice(index, 1);
      }
    });

    this.showOrHideSelectAllOption();
  }

  onSelectClick(resultado: ResultadoValidado) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    if (resultado.selected) {
      this.resultadosSeleccionados.push(resultado);
      this.selectedPage = this.anyUnselected(this.resultados) ? false : true;
    } else {
      let index = this.resultadosSeleccionados.findIndex(
        (m) => m.resultadoId === resultado.resultadoId
      );

      if (index > -1) {
        this.resultadosSeleccionados.splice(index, 1);
      }
    }
  }

  private resetValues() {
    this.resultadosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectResultados() {
    this.resultados.forEach((m) => (m.selected = false));
  }

  onEnviarResultadosValidadosPorOCDLClick(): void {
    if (this.resultadosSeleccionados.length === 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un resultado para enviar.',
      });
    }

    this.loading = true;
    let resultadosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      resultadosSeleccionados = this.resultadosSeleccionados.map((r) => {
        return r.resultadoId;
      });
    }

    this.resultadosService
      .enviarResultadosValidadosPorOCDL(resultadosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.consultarResultados();
          this.loading = false;
          this.resetValues();
          this.hacerScroll();
          this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Resultados enviados correctamente.',
          });
        },
        error: (response: any) => {
          this.hacerScroll();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible enviar los resultados.',
          });
        },
      });
  }

  onRegresarResultadosValidadosPorOCDLClick(): void {
    if (this.resultadosSeleccionados.length == 0 && !this.allSelected ) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un resultado para enviar.',
      });
    }

    this.loading = true;
    let resultadosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      resultadosSeleccionados = this.resultadosSeleccionados.map((r) => {
        return r.resultadoId;
      });
    }
    this.resultadosService
      .regresarResultadosValidadosPorOCDL(resultadosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Resultados enviados correctamente.',
          });
          this.resetValues();
          this.consultarResultados();
          this.loading = false;
        },
        error: (error: any) => {
          this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible enviar los resultados.',
          });
          this.loading = false;
          this.hacerScroll();
        },
      });
  }

  ngOnDestroy() {
    this.filtroHistorialServiceSub.unsubscribe();
  }
}
