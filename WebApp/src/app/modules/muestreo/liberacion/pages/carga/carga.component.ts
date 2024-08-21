import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { NumberService } from 'src/app/shared/services/number.service';
import { FileService } from 'src/app/shared/services/file.service';
import { MuestreoService } from '../../services/muestreo.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { DatePipe } from '@angular/common';
import { Column } from '../../../../../interfaces/filter/column';
import { Item } from '../../../../../interfaces/filter/item';
import { Notificacion } from '../../../../../shared/models/notification-model';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { tipoCarga } from 'src/app/shared/enums/tipoCarga';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { FiltroHistorialService } from 'src/app/shared/services/filtro-historial.service';

@Component({
  selector: 'app-carga',
  templateUrl: './carga.component.html',
  styleUrls: ['./carga.component.css'],
})
export class CargaComponent extends BaseService implements OnInit {
  archivo: any = null;
  archivos: any = null;
  muestreos: Array<Muestreo> = [];
  muestreosSeleccionados: Array<Muestreo> = [];
  fechaLimiteRevision: string = '';
  filtroSitio: string = '';
  fechaActual: string = '';
  @ViewChild('inputArchivoMuestreos') inputArchivoMuestreos: ElementRef =
    {} as ElementRef;
  @ViewChild('inputArchivoEvidencias') inputArchivoEvidencias: ElementRef =
    {} as ElementRef;
  notificacion: Notificacion = {
    title: 'Confirmar eliminación',
    text: '¿Está seguro de eliminar los monitoreos seleccionados y los resultados correspondientes?',
    id: 'mdlConfirmacion',
  };

  private definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'ocdl',
        label: 'OC/DL',
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
        label: 'CLAVE MONITOREO',
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
        name: 'estado',
        label: 'ESTADO',
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
        name: 'tipoSitio',
        label: 'TIPO SITIO',
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
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
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
      //Revisar si es correcto el valor
      {
        name: 'laboratorio',
        label: 'LABORATORIO QUE REALIZÓ EL MUESTREO',
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
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
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
        name: 'fechaLimiteRevision',
        label: 'FECHA LÍMITE REVISIÓN',
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
        name: 'numeroEntrega',
        label: 'N° ENTREGA',
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
        name: 'estatus',
        label: 'ESTATUS',
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
        name: 'tipoCargaResultados',
        label: 'TIPO CARGA RESULTADOS',
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
        name: 'replica',
        label: 'REPLICA',
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
  constructor(
    public numberService: NumberService,
    private muestreoService: MuestreoService,
    private notificationService: NotificationService,
    private filtroHistorialService: FiltroHistorialService
  ) {
    super();
  }

  ngOnInit(): void {
    this.filtroHistorialService.columnName.subscribe((columnName) => {
      if (columnName !== '') {
        this.deleteFilter(columnName);
        this.consultarMonitoreos();
      }
    });
    this.fechaActual =
      new DatePipe('en-US').transform(Date.now(), 'yyyy-MM-dd') ?? '';
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  private consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.muestreoService
      .obtenerMuestreosPaginados(true, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected(this.muestreos) ? false : true;
          this.loading = false;
        },
        error: (error) => {},
      });
  }

  getPreviousSelected(
    muestreos: Array<Muestreo>,
    muestreosSeleccionados: Array<Muestreo>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.muestreoId === x.muestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  private obtenerSeleccionados(): Array<Muestreo> {
    return this.muestreos.filter((f) => f.selected);
  }

  seleccionarArchivo(event: any) {
    this.archivo = event.target.files[0];
  }

  validarTamanoArchivos(archivos: FileList) {
    let error: any = '';
    for (let index = 0; index < archivos.length; index++) {
      const element = archivos[index];
      if (element.size === 0) {
        error += 'El archivo ' + element.name + ' está vacío,';
      }
    }
    return error;
  }

  cargarEvidencias(event: Event) {
    let archivos = (event.target as HTMLInputElement).files ?? new FileList();
    let errores = this.validarTamanoArchivos(archivos);

    if (errores !== '') {
      let archivoErrores = this.generarArchivoDeErrores(errores);
      FileService.download(archivoErrores, 'errores.txt');
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Se encontraron errores en las evidencias seleccionadas',
      });
    }

    this.loading = !this.loading;
    this.muestreoService.cargarEvidencias(archivos).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.consultarMonitoreos();
        return this.notificationService.updateNotification({
          show: true,
          type: NotificationType.success,
          text: 'Archivo procesado correctamente',
        });
      },
      error: (error: any) => {
        this.loading = false;
        let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);
        FileService.download(archivoErrores, 'errores.txt');
        this.hacerScroll();
        return this.notificationService.updateNotification({
          show: true,
          type: NotificationType.danger,
          text: 'Se encontraron errores en el archivo procesado.',
        });
      },
    });
    this.resetInputFile(this.inputArchivoEvidencias);
  }

  cargarArchivo(event: Event): void {
    let archivo = (event.target as HTMLInputElement).files ?? new FileList();

    this.loading = !this.loading;
    this.muestreoService
      .cargarArchivo(archivo[0], true, undefined, tipoCarga.Manual)
      .subscribe({
        next: (response: any) => {
          this.loading = false;
          this.consultarMonitoreos();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Archivo procesado correctamente.',
          });
        },
        error: (error: any) => {
          this.loading = false;
          let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);
          this.hacerScroll();
          FileService.download(archivoErrores, 'errores.txt');
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Se encontraron errores en el archivo procesado.',
          });
        },
      });
    this.resetInputFile(this.inputArchivoMuestreos);
  }

  seleccionarTodos(): void {
    this.muestreosSeleccionados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.selected ? true : (m.selected = true);
      } else {
        m.selected ? (m.selected = false) : true;
      }
    });
    let muestreosSeleccionados = this.obtenerSeleccionados();
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
  }

  onSelectClick(muestreo: Muestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.muestreosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.muestreos) ? false : true;
    } else {
      let index = this.muestreosSeleccionados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.muestreosSeleccionados.splice(index, 1);
      }
    }
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
    this.filtroHistorialService.updateFilteredColumns(
      this.getFilteredColumns()
    );
    this.hideColumnFilter();
  }

  enviarMonitoreos(): void {
    let muestreosSeleccionados = this.muestreosSeleccionados;

    if (!(muestreosSeleccionados.length > 0)) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un monitoreo para enviar',
      });
    } else if (
      muestreosSeleccionados.filter((x) => x.fechaLimiteRevision == '').length >
      0
    ) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Los monitoreos enviados a revisión deben de contar con fecha límite de revisión',
      });
    } else {
      this.loading = true;
      this.muestreoService
        .enviarMuestreoaSiguienteEtapa(
          estatusMuestreo.RevisiónOCDLSECAIA,
          muestreosSeleccionados.map((m) => {
            return m.muestreoId;
          })
        )
        .subscribe({
          next: (response) => {
            this.consultarMonitoreos();
            this.fechaLimiteRevision = '';
            this.loading = false;
            this.seleccionarTodosChck = false;
            this.hacerScroll();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Monitoreos enviados correctamente a revisión',
            });
          },
          error: (error) => {
            this.loading = false;
          },
        });
    }
  }

  exportarResultados(): void {
    if (this.muestreosSeleccionados.length == 0 && !this.allSelected) {
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
      registrosSeleccionados = this.muestreosSeleccionados.map((s) => {
        return s.muestreoId;
      });
    }

    this.muestreoService
      .exportarResultadosExcel(true, registrosSeleccionados, this.cadena)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'resultados.xlsx');
          this.resetValues();
          this.unselectMuestreos();
          this.loading = false;
        },
        error: (response: any) => {
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible descargar la información',
          });
        },
      });
  }

  private unselectMuestreos() {
    this.muestreos.forEach((m) => (m.selected = false));
  }

  private resetValues() {
    this.muestreosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
    //this.getSummary();
  }

  confirmarEliminacion() {
    let muestreosSeleccionados = this.obtenerSeleccionados();
    if (!(muestreosSeleccionados.length > 0)) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un monitoreo para eliminar',
      });
    }
    document.getElementById('btnMdlConfirmacion')?.click();
  }

  eliminarMuestreos() {
    let muestreosSeleccionados = this.obtenerSeleccionados();

    if (!(muestreosSeleccionados.length > 0)) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un monitoreo para eliminar',
      });
    }

    let muestreosAutomaticos = muestreosSeleccionados.filter(
      (x) => x.tipoCargaResultados == 'Automático'
    );
    if (muestreosAutomaticos.length > 0) {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar monitoreos cargados "Manualmente"',
      });
    } else {
      this.loading = true;
      let muestreosEliminar = muestreosSeleccionados.map((s) => s.muestreoId);
      this.muestreoService.eliminarMuestreos(muestreosEliminar).subscribe({
        next: (response) => {
          document.getElementById('btnCancelarModal')?.click();
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.hacerScroll();
          this.seleccionarTodosChck = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Monitoreos eliminados correctamente',
          });
        },
        error: (error) => {
          this.loading = false;
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
      this.muestreoService
        .getDistinctValuesFromColumn(column.name, this.cadena, true)
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
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.muestreos = response.data;
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

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  asignarFechaLimite() {
    if (this.muestreosSeleccionados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un monitoreo para asignar la fecha límite',
      });
    }

    if (this.fechaLimiteRevision == '') {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'La fecha límite se encuentra vacía',
      });
    }

    this.loading = true;
    let registrosSeleccionados: Array<number> = [];

    if (!this.allSelected) {
      registrosSeleccionados = this.muestreosSeleccionados.map((s) => {
        return s.muestreoId;
      });
    }

    this.muestreoService
      .asignarFechaLimite(
        registrosSeleccionados,
        true,
        this.fechaLimiteRevision,
        this.cadena
      )
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.resetValues();
          this.hacerScroll();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Se asigno la fecha límite correctamente',
          });
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }
}
