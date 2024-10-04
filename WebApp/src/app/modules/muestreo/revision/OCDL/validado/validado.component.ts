import { NotificationService } from 'src/app/shared/services/notification.service';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';
import { Component, OnInit, ViewChildren } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { BaseService } from 'src/app/shared/services/base.service';
import { TotalService } from '../../services/total.service';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { estatusOcdlSecaia } from 'src/app/shared/enums/estatusOcdlSecaia';
import { Item } from 'src/app/interfaces/filter/item';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { NotificationType } from 'src/app/shared/enums/notification-type';
import { FileService } from 'src/app/shared/services/file.service';



@Component({
  selector: 'app-validado',
  templateUrl: './validado.component.html',
  styleUrls: ['./validado.component.css']
})
export class ValidadoComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosSeleccionados: Array<Muestreo> = [];
  messageEventualidad: string = '';
  mostrarMensajeAlerta: boolean = false;
  ModalConfirmacion: boolean = false;

  constructor(
    private totalService: TotalService,
    private filtroHistorialService: FiltroHistorialService,
    private notificationService: NotificationService,
    private muestreoService: MuestreoService,

  )
  {
    super();
  }

  ngOnInit(): void {
    this.filtroHistorialService.columnName.subscribe((columnName) => {
      if (columnName !== '') {
        this.deleteFilter(columnName);
        this.consultarMonitoreos();
      }
    });
    this.definirColumnas();
    this.consultarMonitoreos()
  }
  definirColumnas(){
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
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
    ];
    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena):
    void {
    this.totalService
    .getMuestresxParametro(estatusOcdlSecaia.Validado, true, page, pageSize, filter, this.orderBy)
    .subscribe({
      next: (response: any) => {
        this.selectedPage = false;
        this.resultadosn = response.data;
        this.resultadosFiltradosn = this.resultadosn;
        this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
        this.totalItems = response.totalRecords;
        this.getPreviousSelected();
        this.selectedPage = this.anyUnselected(this.resultadosn) ? false : true;
      },
      error: (error) => {
        this.loading = false;
      },
    });
  }

  pageClic(page: any) {
    this.page = page;
    this.consultarMonitoreos();
  }
  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.totalService
      .getMuestresxParametro(estatusOcdlSecaia.Validado, true,this.page, this.pageSize, this.cadena,{
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.resultadosn = response.data;
        },
        error: (error) => {},
      });
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
      this.totalService
        .getDistinct(column.name, this.cadena)
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

  getPreviousSelected() {
    this.resultadosn.forEach((f) => {
      let resultadosFiltrados = this.resultadosFiltradosn.find(
        (x) => f.claveUnica === x.claveUnica
      );

      if (resultadosFiltrados != undefined) {
        f.selected = true;
      }
    });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarMonitoreos();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
      this.consultarMonitoreos();
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

   exportarResultados(): void {
    if (this.resultadosFiltradosn.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }
    this.loading = true;
    this.totalService
      .exportarResultadosValidados(this.resultadosFiltradosn)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'ReplicasResultadosValidados.xlsx');
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

  private resetValues() {
    this.resultadosFiltradosn = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectResultados() {
    this.resultadosFiltradosn.forEach((m) => (m.selected = false));
  }
  consultarMonitoreosmuestreo(){}
  enviarMonitoreos(){}
  cambiarEstatusMuestreo(){}

  onSelectClick(validado: Muestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    if (validado.selected) {
      this.resultadosFiltradosn.push(validado);
      this.selectedPage = this.anyUnselected(this.resultadosn) ? false : true;
    } else {
      let index = this.resultadosFiltradosn.findIndex(
        (m) => m.claveUnica === validado.claveMonitoreo
      );
      if (index > -1) {
        this.resultadosFiltradosn.splice(index, 1);
      }
    }
  }
}
