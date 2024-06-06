import { Component, OnInit } from '@angular/core';
import { Resultado, ResultadoDescarga } from '../../../../interfaces/Resultado.interface';
import { MuestreoService } from '../../liberacion/services/muestreo.service';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { FileService } from 'src/app/shared/services/file.service';
import { FormatoResultadoService } from '../services/formato-resultado.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { TipoHomologado } from 'src/app/interfaces/catalogos/tipo-homologado';
import { AuthService } from '../../../login/services/auth.service';
import { Perfil } from '../../../../shared/enums/perfil';
import { Muestreo } from '../models/muestreo';
import { Column } from '../../../../interfaces/filter/column';
import { FiltroHistorialService } from '../../../../shared/services/filtro-historial.service';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../../../shared/services/notification.service';
import { NotificationType } from '../../../../shared/enums/notification-type';

@Component({
  selector: 'app-formato-resultado',
  templateUrl: './formato-resultado.component.html',
  styleUrls: ['./formato-resultado.component.css'],
})
export class FormatoResultadoComponent extends BaseService implements OnInit {
  width: number = 150;
  muestreos: Array<Muestreo> = [];
  muestreosSeleccionados: Array<Muestreo> = [];
  cuerpoAgua: Array<TipoHomologado> = [];
  perfil: string = '';
  parametrosTotales: any[] = [];
  parametros: Array<Columna> = [];
  tipoCuerpoAgua: number = -1;
  paramTotalOrdenados: Array<any> = [];
  camposDescarga: Array<ResultadoDescarga> = [];
  esVisibleNumEntrega: boolean = false;
  numEntregaVisible: Array<Perfil> = [Perfil.ADMINISTRADOR, Perfil.SECAIA1, Perfil.SECAIA2];
  filtroHistorialServiceSub: Subscription;

  constructor(
    private filtroHistorialService: FiltroHistorialService,
    private formatoService: FormatoResultadoService,
    public muestreoService: MuestreoService,
    private usuario: AuthService,
    private notificationService: NotificationService
  ) {
    super();
    this.filtroHistorialServiceSub =
      this.filtroHistorialService.columnName.subscribe((columnName) => {
        this.deleteFilter(columnName);
        this.consultarMonitoreos();
      });
  }

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.perfil = this.usuario.getUser().nombrePerfil;
    this.definirColumnas();
    this.esVisibleNumEntrega = (this.numEntregaVisible.filter(x => x.includes(this.perfil)).length > 0) ? true : false;
        this.consultarMonitoreos();
  
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'estatus', label: 'ESTATUS', order: 1, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },

      {
        name: 'noEntregaOCDL',
        label: 'N° ENTREGA',
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
        pinned: true,
      },
      {
        name: 'replica',
        label: 'TUVO REPLICA',
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
        pinned: true,
      },
      {
        name: 'claveSitioOriginal',
        label: 'CLAVE SITIO ORIGINAL',
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
        pinned: true,
      },
      {
        name: 'claveSitio',
        label: 'CLAVE SITIO',
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
        pinned: true,
      },
      {
        name: 'claveMonitoreo',
        label: 'CLAVE MONITOREO',
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
        pinned: true,
      },
      {
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
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
        pinned: true,
      },
      {
        name: 'laboratorio',
        label: 'LABORATORIO',
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
        pinned: true,
      },
      {
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA ORIGINAL',
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
        pinned: true,
      },
      {
        name: 'tipoHomologado',
        label: 'TIPO CUERPO AGUA',
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
        pinned: true,
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
        pinned: true,
      },
    ];
    this.columns = nombresColumnas;
    this.formatoService.getParametros().subscribe({
      next: (result: any) => {
        this.parametrosTotales = result.data;
        let orderParametro = 11;
        for (var i = 0; i < this.parametrosTotales.length; i++) {
          orderParametro++;
          let columna: Column = {
            name: this.parametrosTotales[i].claveParametro.toLowerCase(),
            label: this.parametrosTotales[i].claveParametro,
            /*order: this.parametrosTotales[i].id,*/
            order: orderParametro,
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
            pinned: false,
          };
          this.columns.push(columna);
          this.paramTotalOrdenados.push(columna);
          this.setHeadersList(this.columns);
        }
      },
      error: (error) => console.error(error),
    });
  }

  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.formatoService
      .getMuestreosParametrosPaginados(page, this.pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          console.log(this.muestreos);
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
        f.isChecked = true;
      }
    });
  }

  consultarMuestreos(tipoCuerpo: number, page: number) {
    this.loading = !this.loading;
    this.formatoService
      .getMuestreosParametros(tipoCuerpo, page, this.pageSize)
      .subscribe({
        next: (response: any) => {
          this.totalItems = response.totalRecords;
          this.loading = false;
          this.muestreos = response.data;          
        },
        error: (error) => { },
      });
  }

  consultaCuerpoAgua() {
    this.formatoService.getCuerpoAgua().subscribe({
      next: (response: any) => {
        this.cuerpoAgua = response.data;
      },
      error: (error) => { },
    });
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionadosDescarga();
    if (
      muestreosSeleccionados.length === 0 &&
      this.muestreosSeleccionados.length == 0
    ) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'Debe seleccionar al menos un monitoreo',
      });
    }

    this.formatoService
      .exportarResultadosExcel(muestreosSeleccionados, this.esVisibleNumEntrega)
      .subscribe({
        next: (response: any) => {
          this.muestreosSeleccionados = this.muestreosSeleccionados.map((m) => {
            m.selected = false;
            return m;
          });
          this.loading = false;
          this.resetValues();
          this.unselectMuestreos();

          FileService.download(response, 'FORMATO_REGISTRO_ORIGINAL.xlsx');
        },
        error: (response: any) => {
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

  obtenerSeleccionadosDescarga(): Array<any> {
    var selec = this.muestreosSeleccionados.filter((f) => f.selected);
    selec = selec.length == 0 ? this.muestreosSeleccionados : selec;
    this.camposDescarga = [];
    if (selec.length > 0) {
      for (var i = 0; i < selec.length; i++) {
        let campodes = {
          noEntregaOCDL: selec[i].numeroEntrega,
          claveSitioOriginal: selec[i].claveSitioOriginal,
          claveSitio: selec[i].claveSitio,
          claveMonitoreo: selec[i].claveMonitoreo,
          fechaRealizacion: selec[i].fechaRealizacion,
          laboratorio: selec[i].laboratorio,
          tipoCuerpoAgua: selec[i].tipoCuerpoAgua,
          tipoHomologado: selec[i].tipoHomologado,
          tipoSitio: selec[i].tipoSitio,
          lstParametros: selec[i].parametros,
          nombreSitio: '',
        };
        this.camposDescarga.push(campodes);
      }
      this.camposDescarga[0].lstParametrosOrden = this.paramTotalOrdenados;
    }
    return this.camposDescarga;
  }

  limpiarFiltros() {
    this.ngOnInit();
  }

  getValueParam(nombreParametro: string, parametros: any[]) {
    let index = parametros.findIndex(
      (f) => f.claveParametro == nombreParametro
    );
    if (index === -1) {
      return '';
    }

    return parametros[index].resultado;
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

  private resetValues() {
    this.muestreosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectMuestreos() {
    this.muestreos.forEach((m) => (m.selected = false));
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.formatoService
      .getMuestreosParametrosPaginados(this.page, this.NoPage, this.cadena, { column: column, type: type })
      .subscribe({
        next: (response: any) => {
          this.muestreos = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.setColumnsFiltered(this.muestreoService);
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
    this.setColumnsFiltered(this.muestreoService);
    this.hideColumnFilter();
  }

  onPageClick(page: any) {
    this.consultarMonitoreos();
    this.page = page;
  }
}
