import { Component, OnInit } from '@angular/core';
import { LimitesLaboratorios } from '../../../../interfaces/catalogos/limitesLaboratorio.interface';
import { Column } from '../../../../interfaces/filter/column';
import { Item } from '../../../../interfaces/filter/item';
import { NotificationType } from '../../../../shared/enums/notification-type';
import { Notificacion } from '../../../../shared/models/notification-model';
import { BaseService } from '../../../../shared/services/base.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { MuestreoService } from '../../../muestreo/liberacion/services/muestreo.service';
import { LaboratorioService } from '../../laboratorios/services/laboratorios.service';
import { ParametrosService } from '../../parametros/services/parametros.service';
import { LimiteLaboratorioService } from '../services/limite-laboratorios.service';

@Component({
  selector: 'app-limite-laboratorios',
  templateUrl: './limite-laboratorios.component.html',
  styleUrls: ['./limite-laboratorios.component.css']
})
export class LimiteLaboratoriosComponent extends BaseService implements OnInit {
  limitesLaboratorio: Array<LimitesLaboratorios> = [];
  limitesLaboratorioSeleccionados: Array<LimitesLaboratorios> = [];
  modalTitle: string = '';
  editar: boolean = false;
  laboratorios: Array<any> = [];
  parametros: Array<any> = [];
  laboratorioMuestreo: Array<any> = [];
  laboratoriosSubrogado: Array<any> = [];
  meses: Array<any> = [];
  anios: Array<any> = [];
  realizaLaboratorioMuestreo: Array<any> = [];
  loSubroga: Array<any> = [];
  public LaboratorioRegistro: LimitesLaboratorios = {
    id: null,
    claveParametro: '',
    nombreParametro: '',
    parametroId: null,
    laboratorio: '',
    laboratorioId: null,
    realizaLaboratorioMuestreoId: null,
    realizaLaboratorioMuestreo: '',
    laboratorioMuestreo: '',
    laboratorioMuestreoId: null,
    mes: '',
    periodoId: null,
    activo: false,
    loSubroga: '',
    loSubrogaId: null,
    ldMaCumplir: '',
    lpCaCumplir: '',
    loMuestra: null,
    accionLaboratorio: '',
    accionLaboratorioId: null,
    laboratorioSubrogado: '',
    laboratorioSubrogadoId: null,
    metodoAnalitico: '',
    ldm: '',
    lpc: '',
    AnioId: null,
    selected: false,
    anio: ''
  }
  esValido: boolean = false;
  constructor(private muestreoService: MuestreoService,
    private limiteLaboratorioService: LimiteLaboratorioService,
    private parametrosService: ParametrosService,
    private laboratoriosService: LaboratorioService,
    private notificationService: NotificationService) { super(); }
  notificacion: Notificacion = {
    title: 'Confirmar eliminación de límite de laboratorio',
    text: '¿Está seguro de querer eliminar el límite de laboratorio?',
    id: 'mdlConfirmacion'
  };
  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarLimitesLaboratorio();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
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
        name: 'nombreParametro',
        label: 'PARÁMETRO',
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
        name: 'realizaLaboratorioMuestreo',
        label: 'REALIZA LABORATORIO MUESTREO',
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
        name: 'laboratorioMuestreo',
        label: 'LABORATORIO MUESTREO',
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
        name: 'mes',
        label: 'MES',
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
        name: 'activo',
        label: 'ACTIVO',
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
        name: 'ldMaCumplir',
        label: 'LDM A CUMPLIR',
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
        name: 'lpCaCumplir',
        label: 'LPC A CUMPLIR',
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
        name: 'loMuestra',
        label: 'LO MUESTRA',
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
        name: 'loSubroga',
        label: 'LO SUBROGA',
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
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO QUE SUBROGA',
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
        name: 'metodoAnalitico',
        label: 'MÉTODO ANÁLITICO',
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
        name: 'ldm',
        label: 'LDM',
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
        name: 'lpc',
        label: 'LPC',
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
        name: 'anio',
        label: 'AÑO',
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
      }
    ];

    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  cargarArchivo(evento: Event) { }

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
      this.limiteLaboratorioService
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

  public consultarLimitesLaboratorio(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.limiteLaboratorioService
      .obtenerLimitesLaboratorioPaginados(page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.limitesLaboratorio = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.limitesLaboratorio, this.limitesLaboratorio);
          this.selectedPage = this.anyUnselected(this.limitesLaboratorio) ? false : true;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  getPreviousSelected(
    sitios: Array<LimitesLaboratorios>,
    sitiosSeleccionados: Array<LimitesLaboratorios>
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

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.limiteLaboratorioService
      .obtenerLimitesLaboratorioPaginados(this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.limitesLaboratorio = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarLimitesLaboratorio();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarLimitesLaboratorio();

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
    this.consultarLimitesLaboratorio(page, this.NoPage, this.cadena);
    this.page = page;
  }

  validarObligatorios(): boolean {
    let obligatoriosRegistro: any[] = [this.LaboratorioRegistro.parametroId, this.LaboratorioRegistro.laboratorioId, this.LaboratorioRegistro.AnioId];
    return (obligatoriosRegistro.includes("") || obligatoriosRegistro.includes(null)) ? false : true;
  }

  AddLimites() {

    if (!this.validarObligatorios()) {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe de llenar los campos obligatorios para su registro',
      });
    }
    else {
      this.limiteLaboratorioService.addLimiteLaboratorios(this.LaboratorioRegistro).subscribe({
        next: (response: any) => {
          this.loading = true;
          if (response.succeded) {
            this.consultarLimitesLaboratorio();
            this.loading = false;
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Se registro el límite laboratorio exitosamente',
            });
          }
        }
      });
  }
}

  MostrarModalRegistro() {
    this.resetLimiteLaboratorio();
    this.modalTitle = 'Registro límite laboratorio';
    this.cargarCombos();
  }

  UpdateLimites(limites: LimitesLaboratorios) {
    this.LaboratorioRegistro = limites; 
    this.cargarCombos();
    this.modalTitle = "Edición de límites de laboratorios";
    this.editar = true;
  }

  Update() {
    this.limiteLaboratorioService.updateLimiteLaboratorios(this.LaboratorioRegistro).subscribe({
      next: (response: any) => {
        this.loading = true;
        if (response.succeded) {
          this.consultarLimitesLaboratorio();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Límite laboratorio actualizado exitosamente',
          });
        }
      }
    });
  }

  DeleteLimiteLaboratorio() { }

  cargarCombos() {
    this.parametrosService
      .getAllParametros()
      .subscribe({
        next: (response: any) => {
          this.parametros = response.data;
        },
        error: (error) => {
        },
      });

    this.laboratoriosService
      .obtenerLaboratorios(0, 0, '', this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.laboratorios = response.data;
          this.laboratorioMuestreo = response.data;
          this.laboratoriosSubrogado = response.data;
        },
        error: (error) => {
        },
      });

    this.limiteLaboratorioService
      .getMes()
      .subscribe({
        next: (response: any) => {
          this.meses = response.data;
        },
        error: (error) => {
        },
      });

    this.limiteLaboratorioService
      .getAccionesLaboratorio()
      .subscribe({
        next: (response: any) => {
          this.realizaLaboratorioMuestreo = response;
          this.loSubroga = response;
        },
        error: (error) => {
        },
      });
    this.limiteLaboratorioService
      .getAnios()
      .subscribe({
        next: (response: any) => {
          this.anios = response.data;        
        },
        error: (error) => {
        },
      });

    
  }

  obtenerNombre() { this.LaboratorioRegistro.laboratorio = (this.laboratorios.filter(x => x.id == this.LaboratorioRegistro.laboratorioId))[0].descripcion; }

  obtenerNombreParametro() { this.LaboratorioRegistro.nombreParametro = (this.parametros.filter(x => x.id == this.LaboratorioRegistro.parametroId))[0].descripcion; }

  obtenerLaboratorioMuestreo() { this.LaboratorioRegistro.laboratorioMuestreo = (this.laboratorioMuestreo.filter(x => x.id == this.LaboratorioRegistro.laboratorioMuestreoId))[0].descripcion; }

  obtenerLaboratorioSubrogado() { this.LaboratorioRegistro.laboratorioSubrogado = (this.laboratoriosSubrogado.filter(x => x.id == this.LaboratorioRegistro.laboratorioSubrogadoId))[0].descripcion; }

  resetLimiteLaboratorio() {
    this.LaboratorioRegistro.id = null;
    this.LaboratorioRegistro.claveParametro = '';
    this.LaboratorioRegistro.nombreParametro = '';
    this.LaboratorioRegistro.parametroId = null;
    this.LaboratorioRegistro.laboratorio = '';
    this.LaboratorioRegistro.laboratorioId = null;
    this.LaboratorioRegistro.realizaLaboratorioMuestreoId = null;
    this.LaboratorioRegistro.realizaLaboratorioMuestreo = '';
    this.LaboratorioRegistro.laboratorioMuestreo = '';
    this.LaboratorioRegistro.laboratorioMuestreoId = null;
    this.LaboratorioRegistro.mes = '';
    this.LaboratorioRegistro.periodoId = null;
    this.LaboratorioRegistro.activo = false;
    this.LaboratorioRegistro.loSubroga = '';
    this.LaboratorioRegistro.loSubrogaId = null;
    this.LaboratorioRegistro.ldMaCumplir = '';
    this.LaboratorioRegistro.lpCaCumplir = '';
    this.LaboratorioRegistro.loMuestra = null;
    this.LaboratorioRegistro.accionLaboratorio = '';
    this.LaboratorioRegistro.accionLaboratorioId = null;
    this.LaboratorioRegistro.laboratorioSubrogado = '';
    this.LaboratorioRegistro.laboratorioSubrogadoId = null;
    this.LaboratorioRegistro.metodoAnalitico = '';
    this.LaboratorioRegistro.ldm = '';
    this.LaboratorioRegistro.lpc = '';
    this.LaboratorioRegistro.AnioId = null;
    this.LaboratorioRegistro.selected = false;
    this.LaboratorioRegistro.anio = ''

  }
}
