import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { Filter } from '../../../../../../../interfaces/filtro.interface';
import { Muestreo } from '../../../../../../../interfaces/Muestreo.interface';
import { ResumenResultados } from '../../../../../../../interfaces/ResumenResultado.interface';
import { FileService } from '../../../../../../../shared/services/file.service';
import { TotalService } from '../../../../services/total.service';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-resumen-secaia',
  templateUrl: './resumen-secaia.component.html',
  styleUrls: ['./resumen-secaia.component.css']
})
export class ResumenSecaiaComponent extends BaseService implements OnInit {
  public pages: number = 1;

  archivo: any = null;
  archivos: any = null;
  datosResultados: Array<ResumenResultados> = [];
  datosResultadosFiltrados: Array<any> = [];
  resultadosAprobados: number = 0;
  resultadosRechazados: number = 0;
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<any> = [];
  fechaLimiteRevision: string = '';
  filtroSitio: string = '';
  selectedItemsList: Array<ResumenResultados> = [];
  datosParametrosFiltrados: Array<ResumenResultados> = [];
  inputSelectAll: boolean = false;
  resumenParametros: Array<Parametro> = [];

  private definirColumnas() {
    this.columnas = [
      { nombre: 'noEntregaOCDL',etiqueta: 'N° ENTREGA', orden: 1, filtro: new Filter()},
      { nombre: 'claveUnica', etiqueta: 'CLAVE ÚNICA', orden: 2, filtro: new Filter()},
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 3, filtro: new Filter()},
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 4, filtro: new Filter()},
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 5, filtro: new Filter()},
      { nombre: 'claveParametro', etiqueta: 'CLAVE PARÁMETRO',orden: 6, filtro: new Filter()},
      {nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 7, filtro: new Filter()},
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 8, filtro: new Filter()},
      { nombre: 'resultado',etiqueta: 'RESULTADO', orden: 9, filtro: new Filter()},
      { nombre: 'esCorrectoResultado', etiqueta: 'RESULTADO CORRECTO', orden: 10, filtro: new Filter()},
      { nombre: 'observacionSECAIA', etiqueta: 'OBSERVACIÓN SECAIA', orden: 11, filtro: new Filter()},      
      { nombre: 'fechaRevision', etiqueta: 'FECHA DE REVISIÓN', orden: 12, filtro: new Filter()},
      { nombre: 'nombreUsuario', etiqueta: 'NOMBRE USUARIO QUE REVISÓ', orden: 13, filtro: new Filter()},
      { nombre: 'estatusResultado', etiqueta: 'ESTATUS DEL RESULTADO', orden: 14, filtro: new Filter()}
    ]
  }
  constructor(private totalService: TotalService) { super(); }

  ngOnInit(): void {
    this.definirColumnas();    
    this.obtenerDatosResumenResultados();
  }
  
  private obtenerSeleccionados(): Array<ResumenResultados> {
    return this.datosResultadosFiltrados.filter((f) => f.isChecked);
  }
  
  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrarn();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');
      return this.hacerScroll();
    }

    this.totalService.exportExcelResumenSECAIA(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.muestreosFiltrados = this.muestreosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'DescargaObservaciones.xlsx');
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

  //Generamos tabla ResultadosTotales (Aprobados/Rechazados)
  genTablaTotales(datosResultados: Array<ResumenResultados>) {
    this.resultadosAprobados = 0;
    this.resultadosRechazados = 0;
    datosResultados.map((t) => {
      if (t.esCorrectoResultado == 'SI') {
        this.resultadosAprobados = this.resultadosAprobados + 1;
      } else {
        this.resultadosRechazados = this.resultadosRechazados + 1;
      }
    });
  }

  //Generamos tabla ResultadosPorParametrosEspecificos
  genTotalesParamEsp(datosResultados: Array<ResumenResultados>) {
    let resultados = datosResultados.map((m) => ({
      claveParametro: m.claveParametro,
      esCorrrecto: m.esCorrectoResultado,
      correctos: 0,
      incorrectos: 0,
    }));
    this.resumenParametros = [];
    resultados.forEach((resultado) => {
      let parametro = this.resumenParametros.find(
        (f) => f.nombre == resultado.claveParametro
      );

      if (!parametro) {
        let nuevoParametro: Parametro = {
          nombre: resultado.claveParametro,
          correctos: resultado.esCorrrecto == 'SI' ? 1 : 0,
          incorrectos: resultado.esCorrrecto == 'NO' ? 1 : 0,
        };
        this.resumenParametros.push(nuevoParametro);
      } else {
        parametro.correctos =
          resultado.esCorrrecto == 'SI'
            ? parametro.correctos + 1
            : parametro.correctos;
        parametro.incorrectos =
          resultado.esCorrrecto == 'NO'
            ? parametro.incorrectos + 1
            : parametro.incorrectos;
      }
    });
  }

  obtenerDatosResumenResultados() {
    this.totalService.getResumenRevisionResultados(5, false).subscribe({
      next: (response: any) => {
        this.datosResultados = response.data;
        this.datosResultadosFiltrados = this.datosResultados;        
      },
      error: (error) => { },
    });
  }

  seleccionarTodos(): void {
    this.datosResultadosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    this.selectedItemsList = this.obtenerSeleccionados();
    this.fetchSelectedItems();
  }

  fetchSelectedItems() {
    this.selectedItemsList = this.datosResultados
      .filter((value, index) => {
        return value.isChecked;
      })
      .map((s) => ({
        id: s.id,
        muestreoId: s.muestreoId,
        parametroId: s.parametroId,
        resultado: s.resultado,
        observaciones: s.observaciones,
        noEntregaOCDL: s.noEntregaOCDL,
        claveUnica: s.claveUnica,
        claveSitio: s.claveSitio,
        claveMonitoreo: s.claveMonitoreo,
        nombreSitio: s.nombreSitio,
        claveParametro: s.claveParametro,
        laboratorio: s.laboratorio,
        tipoCuerpoAgua: s.tipoCuerpoAgua,
        tipoCuerpoAguaOriginal: s.tipoCuerpoAguaOriginal,
        tipoAprobacion: s.tipoAprobacion,
        esCorrectoResultado: s.esCorrectoResultado,
        fechaRevision: s.fechaRevision,
        nombreUsuario: s.nombreUsuario,
        estatusResultado: s.estatusResultado,
        isChecked: s.isChecked,
      }));
    this.genTablaTotales(this.selectedItemsList);
    this.datosParametrosFiltrados = [...new Set(this.selectedItemsList)];
    this.genTotalesParamEsp(this.selectedItemsList);
  }

  changeSelection(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.fetchSelectedItems();
  }

  obtenerRegistrosSeleccionados(): Array<ResumenResultados> {
    return this.datosResultadosFiltrados.filter((f) => f.isChecked);
  }
}

interface Parametro {
  nombre: string;
  correctos: number;
  incorrectos: number;
}
