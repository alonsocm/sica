import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from '../../../../../shared/enums/estatusMuestreo';
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { Notificacion } from '../../../../../shared/models/notification-model';

@Component({
  selector: 'app-reglas-validar',
  templateUrl: './reglas-validar.component.html',
  styleUrls: ['./reglas-validar.component.css'],
})
export class ReglasValidarComponent extends BaseService implements OnInit {
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
  };
  ngOnInit(): void {
    this.definirColumnas();
    this.cargaResultados();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'claveSitio',
        label: 'CLAVE SITIO',
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        label: 'CUMPLE CRITERIOS PARA APLICAR REGLAS',
        order: 0,
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
        order: 0,
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
        order: 0,
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
        order: 0,
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
        estatusMuestreo.SeleccionadoParaValidar, page, pageSize, filter, this.orderBy
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
      this.mostrarMensaje(
        'No hay información existente para descargar',
        'warning'
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn);
    this.validacionService
      .exportExcelResultadosValidados(
        this.resultadosEnviados.length > 0
          ? this.resultadosEnviados
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
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn).map(
      (m) => {
        return m.muestreoId;
      }
    );
    if (this.resultadosEnviados.length == 0) {
      this.mostrarMensaje(
        'Debes de seleccionar al menos un muestreo para aplicar las reglas',
        'warning'
      );
      return this.hacerScroll();
    }
    this.loading = true;
    this.validacionService
      .obtenerResultadosValidadosPorReglas(this.resultadosEnviados)
      .subscribe({
        next: (response: any) => {
          this.mostrarMensaje(
            'Se aplicaron las reglas de validación correctamente',
            'success'
          );
          this.loading = false;
          return this.hacerScroll();
        },
        error: (error) => {
          this.loading = false;
        },
      });
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

  eliminarResultados() {}

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
}
