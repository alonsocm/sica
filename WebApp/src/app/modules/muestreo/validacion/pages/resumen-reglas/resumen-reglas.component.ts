import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from 'src/app/interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo'
import { Column } from '../../../../../interfaces/filter/column';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';


@Component({
  selector: 'app-resumen-reglas',
  templateUrl: './resumen-reglas.component.html',
  styleUrls: ['./resumen-reglas.component.css']
})
export class ResumenReglasComponent extends BaseService implements OnInit {
  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;
  constructor(private validacionService: ValidacionReglasService,
    private notificationService: NotificationService) { super(); }
  datosAcumualdos: Array<acumuladosMuestreo> = [];

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarMonitoreos();
  

  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'numeroEntrega', label: 'NÚMERO DE CARGA', order: 1, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'number', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveUnica', label: 'CLAVE ÚNICA', order: 2, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveMonitoreo', label: 'CLAVE MUESTREO', order: 3, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveSitio', label: 'CLAVE CONALAB', order: 4, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'nombreSitio', label: 'NOMBRE SITIO', order: 5, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaProgramada', label: 'FECHA PROGRAMADA VISITA', order: 6, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaRealizacion', label: 'FECHA REAL VISITA', order: 7, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaInicio', label: 'HORA INICIO MUESTREO', order: 8, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaFin', label: 'HORA FIN MUESTREO', order: 9, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'zonaEstrategica', label: 'ZONA ESTRATEGICA', order: 10, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoCuerpoAgua', label: 'TIPO CUERPO AGUA', order: 11, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'subtipoCuerpoAgua', label: 'SUBTIPO CUERPO AGUA', order: 12, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorio', label: 'LABORATORIO BASE DE DATOS', order: 13, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorioRealizoMuestreo', label: 'LABORATORIO QUE REALIZO EL MUESTREO', order: 14, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorioSubrogado', label: 'LABORATORIO SUBROGADO', order: 15, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'grupoParametro', label: 'GRUPO DE PARAMETROS', order: 16, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'subGrupo', label: 'SUBGRUPO PARAMETRO', order: 17, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveParametro', label: 'CLAVE PARÁMETRO', order: 18, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'parametro', label: 'PARÁMETRO', order: 19, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'unidadMedida', label: 'UNIDAD DE MEDIDA', order: 20, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'resultado', label: 'RESULTADO', order: 21, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'nuevoResultadoReplica', label: 'NUEVO RESULTADO', order: 22, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'programaAnual', label: 'AÑO DE OPERACIÓN', order: 23, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'number', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'idResultadoLaboratorio', label: 'ID RESULTADO', order: 24, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaEntrega', label: 'FECHA ENTREGA', order: 25, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'replica', label: 'REPLICA', order: 26, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'cambioResultado', label: 'CAMBIO DE RESULTADO', order: 27, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'validoreglas', label: 'VALIDADO POR REGLAS', order: 28, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'observacionesReglas', label: 'OBSERVACIONES REGLAS', order: 29, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'costoparametro', label: 'COSTO DE PARÁMETRO', order: 30, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'number', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'validacionfinal', label: 'VALIDACIÓN FINAL', order: 31, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'observacionfinal', label: 'OBSERVACIÓN FINAL', order: 32, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      }
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
    this.validacionService.getResultadosAcumuladosParametrosPaginados(estatusMuestreo.ValidadoPorReglas, page, pageSize, filter, this.orderBy).subscribe({
      next: (response: any) => {

        this.selectedPage = false;
        this.datosAcumualdos = response.data;

        this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
        this.totalItems = response.totalRecords;
        this.getPreviousSelected(this.datosAcumualdos, this.resultadosFiltradosn);
        this.selectedPage = this.anyUnselected(this.datosAcumualdos) ? false : true;
        this.loading = false;

      },
      error: (error) => { this.loading = false; },
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
    if (this.datosAcumualdos.length == 0) {
      this.mostrarMensaje('No hay información existente para descargar', 'warning');
      return this.hacerScroll();
    }

    this.loading = true;
    this.validacionService.exportExcelResumenResultados(this.datosAcumualdos)
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

  onFilterIconClick(column: Column) { }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.validacionService
      .getResultadosAcumuladosParametrosPaginados(estatusMuestreo.ValidadoPorReglas, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.datosAcumualdos = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columna: string) { }

  filtrar(columna: Column, valor: boolean) { }

  onSelectClick(muestreo: acumuladosMuestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.resultadosFiltradosn.push(muestreo);
      this.selectedPage = this.anyUnselected(this.datosAcumualdos) ? false : true;
    } else {
      let index = this.resultadosFiltradosn.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.resultadosFiltradosn.splice(index, 1);
      }
    }
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  enviarLiberacion() {

    //return this.notificationService.updateNotification({
    //  show: true,
    //  type: NotificationType.warning,
    //  text: 'Debe seleccionar resultados con "Validación final" en "ok"',
    //});

    return this.notificationService.updateNotification({
      show: true,
      type: NotificationType.success,
      text: 'Se realizó la liberación de monitoreos exitosamente',
    });
  }

  cargarValidacion(event: Event) { }

  enviarIncidencia() {
    //return this.notificationService.updateNotification({
    //  show: true,
    //  type: NotificationType.warning,
    //  text: 'Para ser enviados a incidencia, la "Validación final" de los resultados debe de ser diferente de "OK"',
    //});

    return this.notificationService.updateNotification({
      show: true,
      type: NotificationType.success,
      text: 'Se realizó el envío a incidencias exitosamente',
    });

  }


}
