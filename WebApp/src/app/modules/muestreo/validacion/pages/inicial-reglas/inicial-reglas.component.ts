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

@Component({
  selector: 'app-inicial-reglas',
  templateUrl: './inicial-reglas.component.html',
  styleUrls: ['./inicial-reglas.component.css'],
})
export class InicialReglasComponent extends BaseService implements OnInit {

  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef = {} as ElementRef;
  constructor(
    private validacionService: ValidacionReglasService,
    private notificationService: NotificationService,
    public muestreoService: MuestreoService
  ) {
    super();
  }
  resultadosMuestreo: Array<acumuladosMuestreo> = [];
  resultadosEnviados: Array<any> = [];
  notificacion: Notificacion = {
    title: 'Confirmar eliminación',
    text: '¿Está seguro de eliminar los resultados de los muestreos seleccionados?',
  };
  archivo: any;

  ngOnInit(): void {
    this.definirColumnas();
    this.cargaResultados();

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
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'fechaVisifechaProgramadata',
        label: 'FECHA PROGRAMADA',
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
        name: 'diferenciaDias',
        label: 'DIFERENCIA EN DÍAS',
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
        name: 'fechaEntregaTeorica',
        label: 'FECHA DE ENTREGA TEORICA',
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
        dataType: 'string',
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
        dataType: 'string',
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
        label: 'AUTORIZACIÓN DE REGLAS CUANDO NO CUMPLE FECHA DE ENTREGA (SI/NO)',
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

  cargaResultados(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.validacionService
      .getResultadosporMonitoreoPaginados(
        estatusMuestreo.InicialReglas,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.resultadosMuestreo = response.data;
          console.log(this.resultadosMuestreo);
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(
            this.resultadosMuestreo,
            this.resultadosFiltradosn
          );
          this.selectedPage = this.anyUnselected(this.resultadosMuestreo)
            ? false
            : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  onDownload(): void {
    if (this.resultadosFiltradosn.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }
    this.loading = true;
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn);
    console.log(this.resultadosEnviados);
    this.resultadosEnviados
      .map((s) => {
        s.correReglaValidacion = (s.correReglaValidacion) ? "SI" : "NO";
      });

    this.validacionService
      .exportExcelResultadosaValidar(this.resultadosEnviados)
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
    let datosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    let muestreosConResultados = datosSeleccionados.filter(m => m.numParametrosCargados != 0);

    if (datosSeleccionados.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debes de seleccionar al menos un muestreos para enviar a validar',
      });
    }
    else if (muestreosConResultados.length == 0) {

      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debes de seleccionar muestreos que cuenten con resultados para poder ser enviados a la etapa de "Módulo de reglas"',
      });
    }
    else {
      let lstMuestreos = muestreosConResultados.map(
        (m) =>
          <Muestreo>{
            estatusId: estatusMuestreo.SeleccionadoParaValidar,
            autorizacionIncompleto: m.autorizacionIncompleto,
            autorizacionFechaEntrega: m.autorizacionFechaEntrega,
            muestreoId: m.muestreoId
          }
      );

      this.muestreoService.actualizarMuestreos(lstMuestreos)
        .subscribe({
          next: (response: any) => {
            this.loading = true;
            if (response.succeded) {
              this.loading = false;
              this.cargaResultados();
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
  }

  limpiarFiltros() { }

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
          this.resultadosMuestreo = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.cargaResultados();
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

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.cargaResultados();

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
    this.cargaResultados(page, this.NoPage, this.cadena);
    this.page = page;
  }

  onSelectClick(muestreo: acumuladosMuestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.resultadosFiltradosn.push(muestreo);
      this.selectedPage = this.anyUnselected(this.resultadosMuestreo)
        ? false
        : true;
    } else {
      let index = this.resultadosFiltradosn.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.resultadosFiltradosn.splice(index, 1);
      }
    }
  }

  private resetValues() {
    this.resultadosFiltradosn = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  confirmarEliminacion() {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosMuestreo);
    if (!(muestreosSeleccionados.length > 0)) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe seleccionar al menos un muestreo para ser eliminados sus resultados corresondientes',
      });
    }
    document.getElementById('btnMdlConfirmacion')?.click();
  }

  eliminarResultados() {
    this.loading = true;
    if (this.allSelected) {
      this.validacionService.deleteResultadosByFilter(estatusMuestreo.InicialReglas, this.cadena).subscribe({
        next: (response) => {
          document.getElementById('btnCancelarModal')?.click();
          this.cargaResultados();
          this.loading = false;
          document.getElementById('inputExcelMonitoreos')?.click();
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

      let ids = this.resultadosFiltradosn.map((s) => s.muestreoId);

      this.validacionService.deleteResultadosByMuestreoId(ids).subscribe({
        next: (response) => {
          document.getElementById('btnCancelarModal')?.click();
          this.cargaResultados();
          this.loading = false;
          document.getElementById('inputExcelMonitoreos')?.click();
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
      this.muestreoService
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

  cargarArchivo(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    this.loading = true;
    if (this.archivo) {
      this.muestreoService.cargarArchivo(this.archivo[0], false, true, tipoCarga.Automatico).subscribe({
        next: (response: any) => {
          if (response.data.correcto) {
            this.loading = false;
            this.resetInputFile(this.inputExcelMonitoreos);
            this.cargaResultados();
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
          let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);
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
    if (this.resultadosFiltradosn.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No existe selección para la descarga de muestreos que no cumplen con la fecha de entrega',
      });

    }
    else {
      let filtrdosNoCumplen = this.resultadosFiltradosn.filter(x => x.cumpleFechaEntrega == "SI");
      if (filtrdosNoCumplen.length == this.resultadosFiltradosn.length) {
        this.hacerScroll();
        return this.notificationService.updateNotification({
          show: true,
          type: NotificationType.warning,
          text: 'Los muestreos seleccionados si cumplen con la fecha de entrega, no es necesaria la descarga ',
        });

      }
      else {
        this.loading = true;
        this.muestreoService
          .obtenerResultadosNoCumplenFechaEntrega(this.resultadosFiltradosn.map((s) => s.muestreoId))
          .subscribe({
            next: (response: any) => {
              this.loading = true;
              FileService.download(response, 'ParametrosNoCumplenFechaEntrega.xlsx');
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

 
}
