import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from 'src/app/interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo_1 } from 'src/app/shared/enums/estatusMuestreo_1';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { tipoCarga } from 'src/app/shared/enums/tipoCarga';
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { Notificacion } from '../../../../../shared/models/notification-model';
import { Item } from 'src/app/interfaces/filter/item';

@Component({
  selector: 'app-acumulacion-resultados',
  templateUrl: './acumulacion-resultados.component.html',
  styleUrls: ['./acumulacion-resultados.component.css'],
})
export class AcumulacionResultadosComponent
  extends BaseService
  implements OnInit
{
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  constructor(
    private validacionService: ValidacionReglasService,
    public muestreoService: MuestreoService,
    private notificationService: NotificationService
  ) {
    super();
  }
  datosAcumualdos: Array<acumuladosMuestreo> = [];
  resultadosFiltrados: Array<acumuladosMuestreo> = [];
  notificacion: Notificacion = {
    title: 'Confirmar eliminación',
    text: '¿Está seguro de eliminar los resultados seleccionados?',
    id: 'mdlConfirmacion',
  };

  notificacionConfirmacion: Notificacion = {
    title: 'Confirmar carga resultados',
    text: '¿Desea cargar los resultados eliminados?',
    id: 'mdlCargaResultados',
  };

  archivo: any;

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'validacionEvidencias',
        label: 'VALIDACIÓN POR SUPERVISIÓN',
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
        name: 'numeroEntrega',
        label: 'NÚMERO DE CARGA',
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
        name: 'claveUnica',
        label: 'CLAVE ÚNICA',
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
        name: 'claveMonitoreo',
        label: 'CLAVE MUESTREO',
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
        name: 'claveSitio',
        label: 'CLAVE CONALAB',
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
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
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
        name: 'fechaProgramada',
        label: 'FECHA PROGRAMADA VISITA',
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
        name: 'fechaRealizacion',
        label: 'FECHA REAL VISITA',
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
        name: 'horaInicio',
        label: 'HORA INICIO MUESTREO',
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
        name: 'horaFin',
        label: 'HORA FIN MUESTREO',
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
        name: 'tipoSitio',
        label: 'TIPO SITIO',
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
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
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
        name: 'subtipoCuerpoAgua',
        label: 'SUBTIPO CUERPO AGUA',
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
        name: 'laboratorio',
        label: 'LABORATORIO BASE DE DATOS',
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
        name: 'laboratorioRealizoMuestreo',
        label: 'LABORATORIO QUE REALIZO EL MUESTREO',
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
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO SUBROGADO',
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
        name: 'grupoParametro',
        label: 'GRUPO DE PARAMETROS',
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
        name: 'subGrupo',
        label: 'SUBGRUPO PARAMETRO',
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
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
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
        name: 'parametro',
        label: 'PARÁMETRO',
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
        name: 'unidadMedida',
        label: 'UNIDAD DE MEDIDA',
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
        name: 'resultado',
        label: 'RESULTADO',
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
        name: 'nuevoResultadoReplica',
        label: 'NUEVO RESULTADO',
        order: 23,
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
        order: 24,
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
        order: 25,
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
        order: 26,
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
        name: 'cambioResultado',
        label: 'CAMBIO DE RESULTADO',
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
        name: 'observaciones',
        label: 'OBSERVACIONES',
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
    ];
    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.validacionService
      .getResultadosAcumuladosParametrosPaginados(
        estatusMuestreo.AcumulacionResultados,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.datosAcumualdos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(
            this.datosAcumualdos,
            this.resultadosFiltrados
          );
          this.selectedPage = this.anyUnselected(this.datosAcumualdos)
            ? false
            : true;
        },
        error: (error) => {},
      });
  }

  //cambiar cuando selecciona todo
  onDownload(): void {
    if (this.resultadosFiltrados.length == 0 && !this.allSelected) {
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
      registrosSeleccionados = this.resultadosFiltrados.map((s) => {
        return s.muestreoId;
      });
    }

    this.validacionService
      .exportarResultadosAcumuladosExcel(registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.loading = true;
          FileService.download(response, 'AcumulacionResultados.xlsx');
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

  enviarMonitoreos(): void {
    if (this.resultadosFiltrados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para enviar',
      });
    }

    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.resultadosFiltrados.map((s) => {
        return s.muestreoId;
      });
    }

    this.loading = true;
    this.validacionService
      .enviarModuloInicialReglas(registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.hacerScroll();
          this.consultarMonitoreos();
          this.resetValues();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Se enviaron los muestreos a la etapa de "Módulo inicial reglas" correctamente',
          });
        },
        error: (response: any) => {
          this.loading = false;
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Error al enviar los muestreos a la etapa de "Módulo inicial reglas',
          });
        },
      });
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.validacionService
      .getResultadosAcumuladosParametrosPaginados(
        estatusMuestreo.AcumulacionResultados,
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
          this.datosAcumualdos = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarMonitoreos();
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
      this.resultadosFiltrados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.datosAcumualdos)
        ? false
        : true;
    } else {
      let index = this.resultadosFiltrados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.resultadosFiltrados.splice(index, 1);
      }
    }
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  eliminarResultados() {
    this.loading = true;
    if (this.allSelected) {
      this.validacionService
        .deleteResultadosByFilter(
          estatusMuestreo.AcumulacionResultados,
          this.cadena
        )
        .subscribe({
          next: (response) => {
            document.getElementById('btnCancelarModal')?.click();
            this.consultarMonitoreos();
            this.loading = false;
            document
              .getElementById('btnMdlConfirmacionCargaResultados')
              ?.click();
            this.resetValues();
            this.hacerScroll();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Resultados eliminados correctamente',
            });
          },
          error: (error) => {
            this.loading = false;
          },
        });
    } else {
      this.loading = false;
      let muestreosSeleccionados = this.Seleccionados(this.datosAcumualdos);
      let resultadosEliminar = muestreosSeleccionados.map(
        (s) => s.resultadoMuestreoId
      );
      this.validacionService
        .deleteResultadosById(resultadosEliminar)
        .subscribe({
          next: (response) => {
            document.getElementById('btnCancelarModal')?.click();
            this.consultarMonitoreos();
            this.loading = false;
            document
              .getElementById('btnMdlConfirmacionCargaResultados')
              ?.click();
            this.resetValues();
            this.hacerScroll();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Resultados eliminados correctamente',
            });
          },
          error: (error) => {
            this.loading = false;
          },
        });
    }
  }

  confirmacionCarga() {
    this.notificacion.title = 'Confirmación carga resultados eliminados';
    this.notificacion.text = '¿Desea cargar los resultados eliminados?';
    document.getElementById('btnMdlConfirmacion')?.click();
  }

  private resetValues() {
    this.resultadosFiltrados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  confirmarEliminacion() {
    let muestreosSeleccionados = this.Seleccionados(this.datosAcumualdos);
    if (!(muestreosSeleccionados.length > 0)) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un resultado para ser eliminado',
      });
    }
    document.getElementById('btnMdlConfirmacion')?.click();
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
        .getDistinctValuesFromColumn(
          column.name,
          this.cadena,
          estatusMuestreo.AcumulacionResultados
        )
        .subscribe({
          next: (response: any) => {
            this.loading = true;
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
            this.loading = false;
          },
          error: (error) => {},
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
}
