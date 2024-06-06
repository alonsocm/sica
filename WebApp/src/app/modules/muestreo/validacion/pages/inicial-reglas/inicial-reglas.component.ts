import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';

@Component({
  selector: 'app-inicial-reglas',
  templateUrl: './inicial-reglas.component.html',
  styleUrls: ['./inicial-reglas.component.css'],
})
export class InicialReglasComponent extends BaseService implements OnInit {
  constructor(private validacionService: ValidacionReglasService,
    private notificationService: NotificationService, public muestreoService: MuestreoService,) {
    super();
  }

  resultadosMuestreo: Array<acumuladosMuestreo> = [];
  aniosSeleccionados: Array<number> = [];
  entregasSeleccionadas: Array<number> = [];
  resultadosEnviados: Array<number> = [];
  anios: Array<number> = [];
  entregas: Array<number> = [];

  ngOnInit(): void {
 
    this.definirColumnas();

    this.validacionService.obtenerMuestreos().subscribe({
      next: (response: any) => {
        this.anios = response.data;
      },
      error: (error) => { },
    });
    this.validacionService.obtenerNumerosEntrega().subscribe({
      next: (response: any) => {
        this.entregas = response.data;
      },
      error: (error) => { },
    });

  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      //{
      //  name: 'estatus', label: 'ESTATUS', order: 1, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
      //  dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      //},

      {
        name: 'claveSitio', label: 'CLAVE SITIO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'claveMonitoreo', label: 'CLAVE MUESTREO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'nombreSitio', label: 'NOMBRE SITIO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'fechaRealizacion', label: 'FECHA REALIZACIÓN', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'fechaVisifechaProgramadata', label: 'FECHA PROGRAMADA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'diferenciaDias', label: 'DIFERENCIA EN DÍAS', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'fechaEntregaTeorica', label: 'FECHA DE ENTREGA TEORICA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'laboratorioRealizoMuestreo', label: 'LABORATORIO BASE DE DATOS', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'cuerpoAgua', label: 'CUERPO DE AGUA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'tipoCuerpoAgua', label: 'TIPO CUERPO AGUA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'subtipoCuerpoAgua', label: 'SUBTIPO CUERPO AGUA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'numParametrosEsperados', label: 'NÚMERO DE DATOS ESPERADOS', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'numParametrosCargados', label: 'NÚMERO DE DATOS REPORTADOS', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'muestreoCompletoPorResultados', label: 'MUESTREO COMPLETO POR RESULTADOS', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''

      },
      {
        name: 'cumpleReglasCondic', label: '¿CUMPLE CON LA REGLAS CONDICIONANTES?', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'Observaciones', label: 'OBSERVACIONES CONDICIONANTES', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'cumpleFechaEntrega', label: 'CUMPLE CON LA FECHA DE ENTREGA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'reglaValicdacion', label: 'SE CORRE REGLA DE VALIDACIÓN', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'autorizacionRegla', label: 'AUTORIZACIÓN DE REGLAS CUANDO ESTE INCOMPLETO (SI)', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
      {
        name: 'cumpleTodosCriterios', label: 'CUMPLE CRITERIOS PARA APLICAR REGLAS (SI/NO)', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: ''
      },
    ];
    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  cargaResultados() {
    if (
      this.entregasSeleccionadas.length == 0 &&
      this.aniosSeleccionados.length == 0
    ) {   
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'Debes de seleccionar al menos un número de entrega',
      });
    }
    this.loading = true;
    this.validacionService
      .getResultadosporMonitoreo(
        this.aniosSeleccionados,
        this.entregasSeleccionadas,
        estatusMuestreo.InicialReglas
      )
      .subscribe({
        next: (response: any) => {
          this.resultadosMuestreo = response.data;
          this.resultadosFiltradosn = this.resultadosMuestreo;
          this.resultadosn = this.resultadosMuestreo;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }
  onDownload(): void {
    if (this.resultadosFiltradosn.length == 0) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'No hay información existente para descargar',
      });
    }
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn);
    this.validacionService
      .exportExcelResultadosaValidar(
        this.resultadosEnviados.length > 0
          ? this.resultadosEnviados
          : this.resultadosFiltradosn
      )
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
            text:
              'No fue posible descargar la información',
          });
        },
      });
  }
  enviaraValidacion(): void {
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn).map(
      (m) => {
        return m.muestreoId;
      }
    );

    if (this.resultadosEnviados.length == 0) {
      this.hacerScroll();    
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'Debes de seleccionar al menos un muestreos para enviar a validar',
      });
    }

    this.validacionService
      .enviarMuestreoaValidar(
        estatusMuestreo.SeleccionadoParaValidar,
        this.resultadosEnviados
      )
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
              text:
                'Los muestreos fueron enviados a validar correctamente',
            });
          }
        },
        error: (response: any) => {
          this.loading = false;     
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text:
              'Error al enviar los muestreos a validar',
          });
        },
      });
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
    this.setColumnsFiltered(this.muestreoService);
    this.consultarMonitoreos();
  }
  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    //cambiar
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.resultadosMuestreo = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.resultadosMuestreo, this.resultadosFiltradosn);
          this.selectedPage = this.anyUnselected(this.resultadosMuestreo) ? false : true;
          this.loading = false;
        },
        error: (error) => { },
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
    this.setColumnsFiltered(this.muestreoService);
    this.hideColumnFilter();
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }
}
