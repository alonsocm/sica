import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from 'src/app/interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo_1 } from 'src/app/shared/enums/estatusMuestreo_1';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { Column } from '../../../../../interfaces/filter/column';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { Item } from 'src/app/interfaces/filter/item';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-resumen-reglas',
  templateUrl: './resumen-reglas.component.html',
  styleUrls: ['./resumen-reglas.component.css'],
})
export class ResumenReglasComponent extends BaseService implements OnInit {
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  filtroHistorialServiceSub: Subscription;
  constructor(
    private filtroHistorialService: FiltroHistorialService,
    public muestreoService: MuestreoService,
    private validacionService: ValidacionReglasService,
    private notificationService: NotificationService
  ) {
    super();
    this.filtroHistorialServiceSub =
      this.filtroHistorialService.columnName.subscribe((columnName) => {
        this.deleteFilter(columnName);
        this.consultarMonitoreos();
      });
  }
  registros: Array<acumuladosMuestreo> = []; //Contiene los registros consultados a la API*/
  registrosSeleccionados: Array<acumuladosMuestreo> = []; //Contiene los registros que se van seleccionando*/
  archivo: any;

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'numeroEntrega',
        label: 'NÚMERO DE CARGA',
        order: 1,
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
        name: 'claveUnica',
        label: 'CLAVE ÚNICA',
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
        name: 'claveMonitoreo',
        label: 'CLAVE MUESTREO',
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
        name: 'claveSitio',
        label: 'CLAVE CONALAB',
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
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
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
        name: 'fechaProgramada',
        label: 'FECHA PROGRAMADA VISITA',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'fechaRealizacion',
        label: 'FECHA REAL VISITA',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'horaInicio',
        label: 'HORA INICIO MUESTREO',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'horaFin',
        label: 'HORA FIN MUESTREO',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'zonaEstrategica',
        label: 'ZONA ESTRATEGICA',
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
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
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
        name: 'subtipoCuerpoAgua',
        label: 'SUBTIPO CUERPO AGUA',
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
        name: 'laboratorio',
        label: 'LABORATORIO BASE DE DATOS',
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
        name: 'laboratorioRealizoMuestreo',
        label: 'LABORATORIO QUE REALIZO EL MUESTREO',
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
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO SUBROGADO',
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
      },
      {
        name: 'grupoParametro',
        label: 'GRUPO DE PARAMETROS',
        order: 16,
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
        name: 'subGrupo',
        label: 'SUBGRUPO PARAMETRO',
        order: 17,
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
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
        order: 18,
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
        order: 19,
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
        name: 'unidadMedida',
        label: 'UNIDAD DE MEDIDA',
        order: 20,
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
        name: 'resultado',
        label: 'RESULTADO',
        order: 21,
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
        name: 'nuevoResultadoReplica',
        label: 'NUEVO RESULTADO',
        order: 22,
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
        name: 'programaAnual',
        label: 'AÑO DE OPERACIÓN',
        order: 23,
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
        name: 'idResultadoLaboratorio',
        label: 'ID RESULTADO',
        order: 24,
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
        name: 'fechaEntrega',
        label: 'FECHA ENTREGA',
        order: 25,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'replica',
        label: 'REPLICA',
        order: 26,
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
        name: 'cambioResultado',
        label: 'CAMBIO DE RESULTADO',
        order: 27,
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
        name: 'validoreglas',
        label: 'VALIDADO POR REGLAS',
        order: 28,
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
        name: 'observacionesReglas',
        label: 'OBSERVACIONES REGLAS',
        order: 29,
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
        name: 'costoparametro',
        label: 'COSTO DE PARÁMETRO',
        order: 30,
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
        name: 'validacionfinal',
        label: 'VALIDACIÓN FINAL',
        order: 31,
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
        name: 'observacionfinal',
        label: 'OBSERVACIÓN FINAL',
        order: 32,
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

  consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.validacionService
      .getResultadosAcumuladosParametrosPaginados(
        estatusMuestreo.ResumenValidaciónReglas,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.registros = response.data;     
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.registros, this.registrosSeleccionados);
          this.selectedPage = this.anyUnselected(this.registros) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    muestreos: Array<acumuladosMuestreo>,
    muestreosSeleccionados: Array<acumuladosMuestreo>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.resultadoMuestreoId === x.resultadoMuestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  onDownload(): void {
    if (this.registrosSeleccionados.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }

    this.loading = true;
    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.registrosSeleccionados.map((s) => {
        return s.resultadoMuestreoId;
      });
    }

    this.validacionService
      .exportExcelResumenResultados(
        estatusMuestreo.ResumenValidaciónReglas,
        registrosSeleccionados,
        this.cadena
      )
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'ResumenValidacionReglas.xlsx');
          this.loading = false;
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.loading = false;
          this.hacerScroll();
        },
      });
  }

  override onSelectPageClick(
    registros: Array<acumuladosMuestreo>,
    registrosSeleccionados: Array<acumuladosMuestreo>
  ) {
    registros.map((m) => {
      m.selected = this.selectedPage;

      //Buscamos el registro en los seleccionados
      let index = registrosSeleccionados.findIndex(
        (d) => d.resultadoMuestreoId === m.resultadoMuestreoId
      );

      if (index == -1) {
        //No existe en seleccionados, lo agremos
        registrosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        //Existe y el seleccionar página está deshabilitado, lo eliminamos, de los seleccionados
        registrosSeleccionados.splice(index, 1);
      }
    });

    this.showOrHideSelectAllOption();

    //this.getSummary();
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
      this.validacionService
        .getDistinctValuesFromColumn(column.name, this.cadena, estatusMuestreo.ResumenValidaciónReglas)
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

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.validacionService
      .getResultadosAcumuladosParametrosPaginados(
        estatusMuestreo.ResumenValidaciónReglas,
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
          this.registros = response.data;
        },
        error: (error) => {},
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

  onSelectClick(muestreo: acumuladosMuestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.registrosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.registros) ? false : true;
    } else {
      let index = this.registrosSeleccionados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.registrosSeleccionados.splice(index, 1);
      }
    }
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  enviarLiberacion() {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay registros seleccionados para liberar',
      });
    }

    this.loading = true;
    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.registrosSeleccionados.map((s) => {
        return s.muestreoId;
      });
    }

    this.validacionService
      .liberar(registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.consultarMonitoreos();
          this.loading = false;
          this.resetValues();
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Se realizó la liberación correctamente',
          });
        },
        error: (response: any) => {
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Ocurrió un error al realizar la liberación',
          });
        },
      });
  }

  private resetValues() {
    this.registrosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  cargarValidacionFinal(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    if (this.archivo) {
      this.loading = true;

      this.validacionService.cargarArchivo(this.archivo[0]).subscribe({
        next: (response: any) => {
          if (response.data) {
            this.loading = false;
            this.resetInputFile(this.inputExcelMonitoreos);
            this.consultarMonitoreos();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Archivo procesado correctamente.',
            });
          }
        },
        error: (error: any) => {
          this.loading = false;
          this.resetInputFile(this.inputExcelMonitoreos);
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Se encontraron errores en el archivo procesado.',
          });
        },
      });
    }
  }

  enviarIncidencia() {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay registros seleccionados para enviar a incidencias',
      });
    }

    this.loading = true;
    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.registrosSeleccionados.map((s) => {
        return s.resultadoMuestreoId;
      });
    }

    this.validacionService
      .enviarIncidencias(registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.resetValues();
          this.resetInputFile(this.inputExcelMonitoreos);
          this.consultarMonitoreos();
          this.hacerScroll();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Se enviaron los registros a incidencias',
          });
        },
        error: (response: any) => {
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: response.error.Message,
          });
        },
      });
  }

  ngOnDestroy() {
    this.filtroHistorialServiceSub.unsubscribe();
  }
}
