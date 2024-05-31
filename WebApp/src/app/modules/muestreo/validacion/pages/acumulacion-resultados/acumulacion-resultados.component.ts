import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { acumuladosMuestreo } from 'src/app/interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo'
import { Column } from '../../../../../interfaces/filter/column';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';

@Component({
  selector: 'app-acumulacion-resultados',
  templateUrl: './acumulacion-resultados.component.html',
  styleUrls: ['./acumulacion-resultados.component.css']
})
export class AcumulacionResultadosComponent extends BaseService implements OnInit {
  constructor(private validacionService: ValidacionReglasService, public muestreoService: MuestreoService) { super(); }
  datosAcumualdos: Array<acumuladosMuestreo> = [];
  resultadosFiltrados: Array<acumuladosMuestreo> = [];

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.definirColumnas();
    this.consultarMonitoreos();
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [

      {
        name: 'validacionEvidencias', label: 'VALIDACIÓN POR SUPERVISIÓN', order: 1, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },

      {
        name: 'numeroEntrega', label: 'N° ENTREGA', order: 2, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },

      {
        name: 'claveUnica', label: 'CLAVE ÚNICA', order: 3, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveMonitoreo', label: 'CLAVE MUESTREO', order: 4, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveSitio', label: 'CLAVE CONALAB', order: 5, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'nombreSitio', label: 'NOMBRE SITIO', order: 6, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaProgramada', label: 'FECHA PROGRAMADA VISITA', order: 7, selectAll: true, filtered: false, asc: false, desc: false, data: [], filteredData: [],
        dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaRealizacion', label: 'FECHA REAL VISITA', order: 8, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaInicio', label: 'HORA INICIO MUESTREO', order: 9, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'horaFin', label: 'HORA FIN MUESTREO', order: 10, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoSitio', label: 'TIPO SITIO', order: 11, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'tipoCuerpoAgua', label: 'TIPO CUERPO AGUA', order: 12, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'subtipoCuerpoAgua', label: 'SUBTIPO CUERPO AGUA', order: 13, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorio', label: 'LABORATORIO BASE DE DATOS', order: 14, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorioRealizoMuestreo', label: 'LABORATORIO QUE REALIZO EL MUESTREO', order: 15, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'laboratorioSubrogado', label: 'LABORATORIO SUBROGADO', order: 16, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'grupoParametro', label: 'GRUPO DE PARAMETROS', order: 17, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'subGrupo', label: 'SUBGRUPO PARAMETRO', order: 18, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'claveParametro', label: 'CLAVE PARÁMETRO', order: 19, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'parametro', label: 'PARÁMETRO', order: 20, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'unidadMedida', label: 'UNIDAD DE MEDIDA', order: 21, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'resultado', label: 'RESULTADO', order: 22, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'nuevoResultadoReplica', label: 'NUEVO RESULTADO', order: 23, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'programaAnual', label: 'AÑO DE OPERACIÓN', order: 24, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'number', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'idResultadoLaboratorio', label: 'ID RESULTADO', order: 25, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'fechaEntrega', label: 'FECHA ENTREGA', order: 26, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'date', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'replica', label: 'REPLICA', order: 27, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'cambioResultado', label: 'CAMBIO DE RESULTADO', order: 28, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
      {
        name: 'observaciones', label: 'OBSERVACIONES', order: 29, selectAll: true, filtered: false, asc: false, desc: false, data: [],
        filteredData: [], dataType: 'string', specialFilter: '', secondSpecialFilter: '', selectedData: '',
      },
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
    this.validacionService.getResultadosAcumuladosParametrosPaginados(estatusMuestreo.AcumulacionResultados, page, pageSize, filter, this.orderBy).subscribe({
      next: (response: any) => {

        this.selectedPage = false;
        this.datosAcumualdos = response.data;
        this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
        this.totalItems = response.totalRecords;
        this.getPreviousSelected(this.datosAcumualdos, this.resultadosFiltrados);
        this.selectedPage = this.anyUnselected(this.datosAcumualdos) ? false : true;

        this.resultadosFiltradosn = this.datosAcumualdos;
        this.resultadosn = this.datosAcumualdos;
        this.loading = false;

      },
      error: (error) => { this.loading = false; },
    });

  }

  onDownload(): void {
    if (this.resultadosFiltradosn.length == 0) {
      this.mostrarMensaje('No hay información existente para descargar', 'warning');
      return this.hacerScroll();
    }

    this.loading = true;
    this.validacionService.exportarResultadosAcumuladosExcel(this.resultadosFiltradosn)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'AcumulacionResultados.xlsx');
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

  enviarmonitoreos(): void {
    console.log(this.resultadosFiltradosn);
    let resuladosenviados = this.Seleccionados(this.resultadosFiltradosn).map(
      (m) => { return m.muestreoId; });
    let totalmuestreos = this.Seleccionados(this.resultadosFiltradosn).filter(x => x.claveSitio);
    console.log(totalmuestreos);

    if (resuladosenviados.length == 0) {
      this.hacerScroll();
      return this.mostrarMensaje(
        'Debes de seleccionar al menos un muestreo con evidencias cargadas para enviar a la etapa de "Acumulación resultados"',
        'danger'
      );
    }

    this.validacionService.enviarMuestreoaValidar(
      estatusMuestreo.InicialReglas,
      resuladosenviados
    )
      .subscribe({
        next: (response: any) => {
          console.log(resuladosenviados);
          this.loading = true;
          if (response.succeded) {
            this.loading = false;
            this.consultarMonitoreos();
            this.mostrarMensaje(
              'Se enviaron los muestreos a la etapa de "Módulo inicial reglas" correctamente',
              'success'
            );
            this.hacerScroll();
          }
        },
        error: (response: any) => {
          this.loading = false;
          this.mostrarMensaje(
            ' Error al enviar los muestreos a la etapa de "Módulo inicial reglas"',
            'danger'
          );
          this.hacerScroll();
        },
      });
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
          this.datosAcumualdos = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.setColumnsFiltered(this.muestreoService);
    this.consultarMonitoreos();
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

  onSelectClick(muestreo: acumuladosMuestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.isChecked) {
      this.resultadosFiltrados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.datosAcumualdos) ? false : true;
    } else {
      let index = this.resultadosFiltrados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.resultadosFiltrados.splice(index, 1);
      }
    }
 
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }
}
