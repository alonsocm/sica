import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from '../../../../../shared/enums/estatusMuestreo';
import { tipoCarga } from '../../../../../shared/enums/tipoCarga';
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { Notificacion } from '../../../../../shared/models/notification-model';
import { Item } from 'src/app/interfaces/filter/item';

@Component({
  selector: 'app-reglas-validar',
  templateUrl: './reglas-validar.component.html',
  styleUrls: ['./reglas-validar.component.css'],
})
export class ReglasValidarComponent extends BaseService implements OnInit {
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef = {} as ElementRef;
  constructor(
    private validacionService: ValidacionReglasService,
    public muestreoService: MuestreoService,
    private notificationService: NotificationService
  ) {
    super();
  }
  resultadosMuestreo: Array<acumuladosMuestreo> = [];
  resultadosSeleccionados: Array<acumuladosMuestreo> = [];
  resultadosEnviados: Array<any> = [];
  notificacion: Notificacion = {
    title: 'Confirmar eliminación',
    text: '¿Está seguro de eliminar los resultados de los muestreos seleccionados?',
    id: 'mdlConfirmacion'
  };
  notificacionConfirmacion: Notificacion = {
    title: 'Confirmar carga resultados',
    text: '¿Desea cargar los resultados de los muestreos eliminados?',
    id: 'mdlCargaResultados'
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
        name: 'fechaProgramada',
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
        name: 'laboratorioRealizoMuestreo',
        label: 'LABORATORIO BASE DE DATOS',
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
        label: 'TIPO CUERPO AGUA',
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
        label: 'SUBTIPO CUERPO AGUA',
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
        name: 'muestreoCompletoPorResultados',
        label: 'MUESTREO COMPLETO POR RESULTADOS',
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
        name: 'cumpleReglasCondic',
        label: '¿CUMPLE CON LA REGLAS CONDICIONANTES?',
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
        name: 'cumpleFechaEntrega',
        label: 'CUMPLE CON LA FECHA DE ENTREGA',
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
        name: 'cumpleTodosCriterios',
        label: 'CUMPLE CON TODOS LOS CRITERIOS PARA APLICAR REGLAS (SI/NO)',
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
        name: 'autorizacionRegla',
        label: 'AUTORIZACIÓN DE REGLAS CUANDO ESTE INCOMPLETO (SI)',
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
        name: 'autorizacionRegla',
        label: 'AUTORIZACIÓN DE REGLAS CUANDO NO CUMPLE FECHA DE ENTREGA',
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
        name: 'autorizacionCondicionantes',
        label: 'AUTORIZACIÓN CUANDO NO CUMPLE REGLAS CONDICIONANTES',
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
        name: 'reglaValicdacion',
        label: 'SE CORRE REGLA DE VALIDACIÓN',
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
        name: 'muestreoValidadoPor',
        label: 'MUESTREO VALIDADO POR',
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
        name: 'porcentajePago',
        label: '% DE PAGO',
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
        estatusMuestreo.SeleccionadoParaValidar,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.resultadosMuestreo = response.data;      
           this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;

          this.getPreviousSelected(
            this.resultadosMuestreo,
            this.resultadosFiltradosn
          );
          this.selectedPage = this.anyUnselected(this.resultadosMuestreo)
            ? false
            : true;

          this.resultadosFiltradosn = this.resultadosMuestreo;
          this.resultadosn = this.resultadosMuestreo;
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
        (x) => f.muestreoId === x.muestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.isChecked = true;
      }
    });
  }

  onDownload(): void {   
    if (this.resultadosFiltradosn.length == 0) {  
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información existente para descargar',
      });
    }

    this.loading = true;
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn);
      this.validacionService
      .exportExcelResultadosValidados((this.resultadosEnviados.length > 0) ? this.resultadosEnviados
          : this.resultadosFiltradosn
    )
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'ResultadosaValidar.xlsx');
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

  aplicarReglas(): void {
    let datosSeleccionados = this.Seleccionados(this.resultadosSeleccionados);
    let muestreosConResultados = datosSeleccionados.filter(m => m.numParametrosCargados != 0);
        if (datosSeleccionados.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debes de seleccionar al menos un muestreos para aplicar reglas',
      });
    }
    else if (muestreosConResultados.length == 0) {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debes de seleccionar muestreos que cuenten con resultados para poder aplicar las reglas',
      });
    }
    
    else {

      ////Validacion para aplicar reglas debe de ser si en correr regla y haber sido validado por el area de supervisión
      
          let resultadosNoAplicaRegla = muestreosConResultados.filter(x => x.correReglaValidacion == false);
          let novalidadoMuestreo = muestreosConResultados.filter(x => x.usuarioValido == null);
      if (resultadosNoAplicaRegla.length > 0 || novalidadoMuestreo.length > 0) {
        return this.notificationService.updateNotification({
          show: true,
          type: NotificationType.warning,
          text: 'Debes de seleccionar muestreos que se encuentre validados por el área de Supervisión de muestreo y ser aprobado para correr la regla',
        });
      }

      this.resultadosEnviados = muestreosConResultados.map(
        (m) => { return m.muestreoId; });
      this.loading = true;
      this.validacionService
        .obtenerResultadosValidadosPorReglas(this.resultadosEnviados)
        .subscribe({
          next: (response: any) => {          
            this.loading = false;
            this.hacerScroll();
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Se aplicaron las reglas de validación correctamente',
            });
          },
          error: (error) => {
            this.loading = false;
          },
        });
    }
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    //cambiar
    this.validacionService
      .getResultadosporMonitoreoPaginados(
        estatusMuestreo.InicialReglas,
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
          this.resultadosMuestreo = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.cargaResultados();
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

  confirmarEliminacion() {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosMuestreo);
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

  onSelectClick(muestreo: acumuladosMuestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.resultadosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.resultadosMuestreo)
        ? false
        : true;
    } else {
      let index = this.resultadosSeleccionados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.resultadosSeleccionados.splice(index, 1);
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
          error: (error) => {},
        });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  eliminarResultados() {
    this.loading = true;
    if (this.allSelected) {      
      this.validacionService.deleteResultadosByFilter(estatusMuestreo.SeleccionadoParaValidar, this.cadena).subscribe({
        next: (response) => {
          document.getElementById('btnCancelarModal')?.click();
          this.cargaResultados();
          this.loading = false;
          document.getElementById('btnMdlConfirmacionCargaResultados')?.click();
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
          document.getElementById('btnMdlConfirmacionCargaResultados')?.click();
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

  private resetValues() {
    this.resultadosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
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

  validarCorreReglaAutorizaciones(muestreo: acumuladosMuestreo) {
    return muestreo.correReglaValidacion = ((muestreo.cumpleReglasCondic == "SI" || muestreo.autorizacionCondicionantes) && (muestreo.muestreoCompletoPorResultados == "SI" || muestreo.autorizacionIncompleto) && (muestreo.cumpleFechaEntrega == "SI" || muestreo.autorizacionFechaEntrega)) ? "SI" : "NO";
  }

}
