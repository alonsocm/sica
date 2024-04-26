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
    private muestreoService: MuestreoService,
    private usuario: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.perfil = this.usuario.getUser().nombrePerfil;
    this.establecerColumnas();
    this.consultaCuerpoAgua();
    this.validarPerfil();
  }

  establecerColumnas() {
    this.columnas = [
      {
        nombre: 'noEntregaOCDL',
        etiqueta: 'N° ENTREGA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitioOriginal',
        etiqueta: 'CLAVE SITIO ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoHomologado',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoSitio',
        etiqueta: 'TIPO SITIO',
        orden: 0,
        filtro: new Filter(),
      },
    ];

    this.formatoService.getParametros().subscribe({
      next: (result: any) => {
        this.parametrosTotales = result.data;
        for (var i = 0; i < this.parametrosTotales.length; i++) {
          let columna: Columna = {
            nombre: this.parametrosTotales[i].claveParametro.toLowerCase(),
            etiqueta: this.parametrosTotales[i].claveParametro,
            orden: this.parametrosTotales[i].id,
            filtro: new Filter(),
          };
          this.columnas.push(columna);
          this.paramTotalOrdenados.push(columna);
        }
      },
      error: (error) => console.error(error),
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
        error: (error) => {},
      });
  }

  consultaCuerpoAgua() {
    this.formatoService.getCuerpoAgua().subscribe({
      next: (response: any) => {
        this.cuerpoAgua = response.data;
      },
      error: (error) => {},
    });
  }

  FiltrarCuerpoAgua() {
    this.resetValues();
    this.unselectMuestreos();

    if (this.tipoCuerpoAgua == -1) {
      this.muestreos = [];
    } else {
      this.consultarMuestreos(this.tipoCuerpoAgua, this.page);
    }
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

  filtrar(): void {
    // this.loading = !this.loading;
    // this.resultadosFiltrados = this.resultados;
    // this.columnas.forEach((columna) => {
    //   this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
    //     return columna.filtro.selectedValue == 'Seleccione'
    //       ? true
    //       : f[columna.nombre] == columna.filtro.selectedValue;
    //   });
    // });
    // this.loading = false;
    // this.establecerValoresFiltrosTabla();
  }

  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
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
      this.selectedPage = this.anyUnselected() ? false : true;
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

  onSelectPageClick() {
    this.muestreos.map((m) => {
      m.selected = this.selectedPage;

      //Buscamos el registro en los seleccionados
      let index = this.muestreosSeleccionados.findIndex(
        (d) => d.muestreoId === m.muestreoId
      );

      if (index == -1) {
        //No existe en seleccionados, lo agremos
        this.muestreosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        //Existe y el seleccionar página está deshabilitado, lo eliminamos, de los seleccionados
        this.muestreosSeleccionados.splice(index, 1);
      }
    });

    if (this.selectAllOption && !this.selectedPage) {
      this.selectAllOption = false;
      this.allSelected = false;
    } else if (!this.selectAllOption && this.selectedPage) {
      this.selectAllOption = true;
    }
  }

  anyUnselected() {
    return this.muestreos.some((f) => !f.selected);
  }

  onSelectAllPagesClick() {
    this.allSelected = true;
  }

  onUnselectAllClick() {
    this.allSelected = false;
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
}
