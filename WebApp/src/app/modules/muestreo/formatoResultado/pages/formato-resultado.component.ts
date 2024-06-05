import { Component, OnInit, ViewChildren } from '@angular/core';
import { Filter } from '../../../../interfaces/filtro.interface';
import {
  Resultado,
  ResultadoDescarga,
} from '../../../../interfaces/Resultado.interface';
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

@Component({
  selector: 'app-formato-resultado',
  templateUrl: './formato-resultado.component.html',
  styleUrls: ['./formato-resultado.component.css'],
})
export class FormatoResultadoComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosSeleccionados: Array<Muestreo> = [];
  cuerpoAgua: Array<TipoHomologado> = [];
  perfil: string = '';
  parametrosTotales: any[] = [];
  parametros: Array<Columna> = [];
  tipoCuerpoAgua: number = -1;
  paramTotalOrdenados: Array<any> = [];
  camposDescarga: Array<ResultadoDescarga> = [];
  esAdmin: boolean = false;

  constructor(
    private formatoService: FormatoResultadoService,
    public muestreoService: MuestreoService,
    private usuario: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.perfil = this.usuario.getUser().nombrePerfil;
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();

    this.consultaCuerpoAgua();
    this.validarPerfil();
    this.consultarMonitoreos();

  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [

      {
        name: 'noEntregaOCDL', label: 'N° ENTREGA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'replica', label: 'TUVO REPLICA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveSitioOriginal', label: 'CLAVE SITIO ORIGINAL', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveSitio', label: 'CLAVE SITIO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveMonitoreo', label: 'CLAVE MONITOREO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaRealizacion', label: 'FECHA REALIZACIÓN', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorio', label: 'LABORATORIO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoCuerpoAgua', label: 'TIPO CUERPO AGUA ORIGINAL', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoHomologado', label: 'TIPO CUERPO AGUA', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoSitio', label: 'TIPO SITIO', order: 0, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
    ];
    this.columns = nombresColumnas;
    this.formatoService.getParametros().subscribe({
      next: (result: any) => {
        this.parametrosTotales = result.data;
        for (var i = 0; i < this.parametrosTotales.length; i++) {
          let columna: Column = {
            name: this.parametrosTotales[i].claveParametro.toLowerCase(),
            label: this.parametrosTotales[i].claveParametro,
            order: this.parametrosTotales[i].id,
            selectAll: true, filtered: false, asc: false, desc: false, data: [],
            filteredData: [],
            dataType: 'string',
            specialFilter: '', secondSpecialFilter: '', selectedData: '',
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
    tipoCuerpo: number = this.tipoCuerpoAgua,
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.loading = true;
    this.formatoService
      .getMuestreosParametrosPaginados(tipoCuerpo, page, this.pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          //this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected(this.muestreos) ? false : true;
          this.loading = false;
        },
        error: (error) => { },
      });
  }


  mostrarColumna(nombreColumna: string) {
    let mostrar: boolean = true;

    if (nombreColumna === 'N°. ENTREGA') {
      if (this.perfil === Perfil.ADMINISTRADOR) {
        mostrar = true;
      } else if (this.perfil === Perfil.SECAIA1) {
        mostrar = true;
      } else if (this.perfil === Perfil.SECAIA2) {
        mostrar = true;
      } else {
        mostrar = false;
      }
    }
    return mostrar;
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
          this.establecerValoresFiltrosTabla();
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
  establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.muestreos.map((m: any) => m[f.nombre])),
      ];
    });
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionadosDescarga();
    if (
      muestreosSeleccionados.length === 0 &&
      this.muestreosSeleccionados.length == 0
    ) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');
      return this.hacerScroll();
    }

    this.formatoService
      .exportarResultadosExcel(muestreosSeleccionados, this.esAdmin)
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
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
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
          noEntregaOCDL: selec[i].noEntregaOCDL,
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

  validarPerfil() {
    switch (this.perfil) {
      case Perfil.ADMINISTRADOR:
        this.esAdmin = true;
        break;
      case Perfil.SECAIA1:
        this.esAdmin = true;
        break;
      case Perfil.SECAIA2:
        this.esAdmin = true;
        break;
      default:
        this.esAdmin = false;
        break;
    }
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

  onPageClick(page: any) {
    this.page = page;
    this.consultarMuestreos(this.tipoCuerpoAgua, page);
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
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.muestreos = response.data;
        },
        error: (error) => { },
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
  pageClic(page: any) {
    this.consultarMonitoreos();
    this.page = page;
  }
}
