import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { tipoCarga } from 'src/app/shared/enums/tipoCarga';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { Notificacion } from '../../../../../shared/models/notification-model';
import { Item } from 'src/app/interfaces/filter/item';
import { Muestreo } from '../../../../../interfaces/Muestreo.interface';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';
import { ICommonMethods } from 'src/app/shared/interfaces/ICommonMethods';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-inicial-reglas',
  templateUrl: './inicial-reglas.component.html',
  styleUrls: ['./inicial-reglas.component.css'],
})
export class InicialReglasComponent
  extends BaseService
  implements OnInit, ICommonMethods
{
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  constructor(
    private validacionService: ValidacionReglasService,
    private notificationService: NotificationService,
    private filtroHistorialService: FiltroHistorialService,
    public muestreoService: MuestreoService
  ) {
    super();
    this.filtroHistorialServiceSub =
      this.filtroHistorialService.columnName.subscribe((columnName) => {
        if (columnName !== '') {
          this.loading = true;
          this.deleteFilter(columnName);
          this.consultarMonitoreos();
        }
      });
  }

  filtroHistorialServiceSub: Subscription;
  registros: Array<acumuladosMuestreo> = [];
  registrosSeleccionados: Array<acumuladosMuestreo> = [];
  notificacion: Notificacion = {
    title: 'Confirmar eliminación',
    text: '¿Está seguro de eliminar los resultados de los muestreos seleccionados?',
    id: 'mdlConfirmacion',
  };
  notificacionConfirmacion: Notificacion = {
    title: 'Confirmar carga resultados',
    text: '¿Desea cargar los resultados de los muestreos eliminados?',
    id: 'mdlCargaResultados',
  };
  archivo: any;

  ngOnInit(): void {
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
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'claveSitio',
        label: 'CLAVE SITIO',
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
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
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
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
        order: 5,
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
        name: 'fechaProgramada',
        label: 'FECHA PROGRAMADA',
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
        name: 'diferenciaDias',
        label: 'DIFERENCIA EN DÍAS',
        order: 7,
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
        name: 'fechaEntregaTeorica',
        label: 'FECHA DE ENTREGA TEORICA',
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
        name: 'laboratorioRealizoMuestreo',
        label: 'LABORATORIO BASE DE DATOS',
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
        name: 'cuerpoAgua',
        label: 'CUERPO DE AGUA',
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
        name: 'numParametrosEsperados',
        label: 'NÚMERO DE DATOS ESPERADOS',
        order: 13,
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
        name: 'numParametrosCargados',
        label: 'NÚMERO DE DATOS REPORTADOS',
        order: 14,
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
        name: 'muestreoCompletoPorResultados',
        label: 'MUESTREO COMPLETO POR RESULTADOS',
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
        name: 'cumpleReglasCondic',
        label: '¿CUMPLE CON LA REGLAS CONDICIONANTES?',
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
        name: 'Observaciones',
        label: 'OBSERVACIONES CONDICIONANTES',
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
        name: 'cumpleFechaEntrega',
        label: 'CUMPLE CON LA FECHA DE ENTREGA',
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
        name: 'cumpleTodosCriterios',
        label: 'CUMPLE CON TODOS LOS CRITERIOS PARA APLICAR REGLAS (SI/NO)',
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
        name: 'autorizacionRegla',
        label: 'AUTORIZACIÓN DE REGLAS CUANDO ESTE INCOMPLETO (SI/NO)',
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
        name: 'autorizacionRegla',
        label:
          'AUTORIZACIÓN DE REGLAS CUANDO NO CUMPLE FECHA DE ENTREGA (SI/NO)',
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
        name: 'reglaValicdacion',
        label: 'SE CORRE REGLA DE VALIDACIÓN',
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
      .getResultadosporMonitoreoPaginados(
        estatusMuestreo.MóduloInicialReglas,
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

  onDownload(): void {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }
    this.loading = true;
    this.registrosSeleccionados = this.Seleccionados(
      this.registrosSeleccionados
    );
    this.registrosSeleccionados.map((s) => {
      s.correReglaValidacion = s.correReglaValidacion ? 'SI' : 'NO';
    });

    this.validacionService
      .exportExcelResultadosaValidar(this.registrosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.loading = true;
          FileService.download(response, 'ResultadosaValidar.xlsx');
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

  enviaraValidacion(): void {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para enviar',
      });
    }

    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.registrosSeleccionados.map((s) => {
        return s.muestreoId;
      });
    }

    this.loading = true;

    this.muestreoService
      .actualizarMuestreos(registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.loading = true;
          if (response.succeded) {
            this.loading = false;
            this.consultarMonitoreos();
            this.hacerScroll();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Los muestreos fueron enviados a validar correctamente',
            });
          }
        },
        error: (response: any) => {
          this.loading = false;
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Error al enviar los muestreos a validar',
          });
        },
      });
  }

  limpiarFiltros() {}

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    //cambiar
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.registros = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.filtroHistorialService.updateFilteredColumns(
      this.getFilteredColumns()
    );
    this.consultarMonitoreos();
  }

  getPreviousSelected(
    muestreos: Array<acumuladosMuestreo>,
    muestreosSeleccionados: Array<acumuladosMuestreo>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.muestreoId === x.muestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.isChecked = true;
      }
    });
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

  private resetValues() {
    this.registrosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  confirmarEliminacion() {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un muestreo, para eliminar sus resultados correspondientes',
      });
    }

    document.getElementById('btnMdlConfirmacion')?.click();
  }

  eliminarResultados() {
    if (this.registrosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un resultado para ser eliminado',
      });
    }

    let registrosSeleccionados = new Array<number>();

    if (!this.allSelected) {
      registrosSeleccionados = this.registrosSeleccionados.map((s) => {
        return s.resultadoMuestreoId;
      });
    }

    this.loading = true;

    if (this.allSelected) {
      this.validacionService
        .deleteResultados(
          estatusMuestreo.MóduloInicialReglas,
          registrosSeleccionados,
          this.cadena
        )
        .subscribe({
          next: (response) => {
            document.getElementById('btnCancelarModal')?.click();
            document
              .getElementById('btnMdlConfirmacionCargaResultados')
              ?.click();
          },
          error: (error) => {
            this.loading = false;
          },
          complete: () => {
            this.resetValues();
            this.hacerScroll();
            this.consultarMonitoreos();
            this.loading = false;
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Resultados eliminados correctamente',
            });
          },
        });
    }
  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles
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
        .getDistinctValuesFromColumnporMuestreo(
          column.name,
          this.cadena,
          estatusMuestreo.MóduloInicialReglas
        )
        .subscribe({
          next: (response: any) => {
            column.data = response.data.map((register: any) => {
              let item: Item = {
                value: register,
                checked: true,
              };
              return item;
            });
          },
          error: (error) => {},
          complete: () => {
            column.filteredData = column.data;
            this.ordenarAscedente(column.filteredData);
            this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
          },
        });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  cargarArchivo(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    this.loading = true;
    if (this.archivo) {
      this.muestreoService
        .cargarArchivo(this.archivo[0], false, true, tipoCarga.Automatico)
        .subscribe({
          next: (response: any) => {
            if (response.data.correcto) {
              this.loading = false;
              this.resetInputFile(this.inputExcelMonitoreos);
              this.consultarMonitoreos();
              return this.notificationService.updateNotification({
                show: true,
                type: NotificationType.success,
                text: 'Se sustituyeron los datos correctamente.',
              });
            } else {
              this.loading = false;
            }
          },
          error: (error: any) => {
            this.loading = false;
            let archivoErrores = this.generarArchivoDeErrores(
              error.error.Errors
            );
            this.hacerScroll();
            FileService.download(archivoErrores, 'errores.txt');
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

  onDownloadNoCumpleFechaEntrega(): void {
    if (this.registrosSeleccionados.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No existe selección para la descarga de muestreos que no cumplen con la fecha de entrega',
      });
    } else {
      let filtrdosNoCumplen = this.registrosSeleccionados.filter(
        (x) => x.cumpleFechaEntrega == 'SI'
      );
      if (filtrdosNoCumplen.length == this.registrosSeleccionados.length) {
        this.hacerScroll();
        return this.notificationService.updateNotification({
          show: true,
          type: NotificationType.warning,
          text: 'Los muestreos seleccionados si cumplen con la fecha de entrega, no es necesaria la descarga ',
        });
      } else {
        this.loading = true;
        this.muestreoService
          .obtenerResultadosNoCumplenFechaEntrega(
            this.registrosSeleccionados.map((s) => s.muestreoId)
          )
          .subscribe({
            next: (response: any) => {
              this.loading = true;
              FileService.download(
                response,
                'ParametrosNoCumplenFechaEntrega.xlsx'
              );
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
    }
  }

  onFilterClick(columna: Column, isFiltroEspecial: boolean) {
    this.loading = true;
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
    this.filtroHistorialService.updateFilteredColumns(
      this.getFilteredColumns()
    );
    this.hideColumnFilter();
  }

  onPageClick(page: any): void {
    this.loading = true;
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  ngOnDestroy() {
    this.filtroHistorialService.updateFilteredColumns([]);
    this.filtroHistorialServiceSub.unsubscribe();
  }
}
