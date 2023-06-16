import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Filter } from '../../../../../../../interfaces/filtro.interface';
import { Resultado, ResultadoDescarga } from '../../../../../../../interfaces/Resultado.interface';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { MuestreoService } from '../../../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { TotalService } from '../../../../services/total.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';


@Component({
  selector: 'app-total-secaia',
  templateUrl: './total-secaia.component.html',
  styleUrls: ['./total-secaia.component.css']
})
export class TotalSecaiaComponent extends BaseService implements OnInit {  
  public parametrosTotales: any[] = [];  
  muestreoSeleccionadoDatos: any = null;
  fileName: string = '';  
  fechaLimiteRevision?: string = '';
  isMuestreoModal: boolean = false;   
  camposDescarga: Array<ResultadoDescarga> = [];
  resultadosFiltradosnf: Array<any> = [];
  resultadosnf: Array<any> = [];
  paramTotalOrdenados: Array<any> = [];
  muestreoActualizar: Array<any> = [];
  @ViewChild('inputExcelObservaciones') inputExcelObservaciones: ElementRef = {} as ElementRef; 

  constructor(private totalService: TotalService, private muestreoService: MuestreoService) { super(); }
  ngOnInit(): void {    
   this.columnas = [
     { nombre: 'noEntregaOCDL', etiqueta: 'N°. ENTREGA A REVISAR', orden: 0, filtro: new Filter() },
     { nombre: 'fechaLimiteRevision', etiqueta: 'FECHA LÍMITE DE REVISIÓN', orden: 0, filtro: new Filter() },
     //{ nombre: 'estatusRevision', etiqueta: 'ESTATUS DE REVISIÓN', orden: 0, filtro: new Filter() },
     //{ nombre: 'fechaDescarga', etiqueta: 'FECHA DE DESCARGA', orden: 0, filtro: new Filter() },
     //{ nombre: 'observacionLaboratorio', etiqueta: 'OBSERVACIÓN LAB QUE HIZO DEL MUESTREO', orden: 0, filtro: new Filter() },
     { nombre: 'organismoCuenca', etiqueta: 'OC/DL CORRESPONDIENTE', orden: 0, filtro: new Filter() },
     { nombre: 'nombreSitio', etiqueta: 'SITIO', orden: 0, filtro: new Filter() },
     { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 0, filtro: new Filter() },
     { nombre: 'fechaRealizacion', etiqueta: 'FECHA DE REALIZACIÓN', orden: 0, filtro: new Filter() },
     { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 0, filtro: new Filter() },
     { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() }
   ];
   this.consultarMonitoreos();    
  }

  consultarMonitoreos(): void {
    this.loading = true;
    this.totalService.getResultadosMuestreosParametros(false).subscribe({
      next: (response: any) => {
        this.resultadosn = response.data;
        this.resultadosFiltradosn = response.data;
        if (this.resultadosn.length > 0) {                   
          this.definirColumnas();
          this.establecerValoresFiltrosTablan();
          this.loading = false;
        }
        else { this.loading = false; }
      },
      error: (error) => { this.loading = false; },
    });
  }

  definirColumnas() {
    this.totalService.getParametros().subscribe(result => {
      this.parametrosTotales = result.data;

      for (var i = 0; i < this.parametrosTotales.length; i++) {
        let columna: Columna = {
          //nombre: this.parametrosTotales[i].claveParametro.toLowerCase(),
          nombre: "param" + i,
          etiqueta: this.parametrosTotales[i].claveParametro,
          orden: this.parametrosTotales[i].id,
          filtro: new Filter()
        }    
        this.columnas.push(columna);
        this.paramTotalOrdenados.push(columna);       
      }

      this.resultadosFiltradosn = this.resultadosn;
      this.resultadosFiltradosn[0].lstParametrosTotalOrden = this.paramTotalOrdenados;
      if (this.resultadosFiltradosn.length > 0) {
        this.resultadosFiltradosn.forEach((f) => {
          this.paramTotalOrdenados.forEach((x) => {
            let paramOrdenado = f.lstParametros?.filter(y => y.id == x.orden);
            (paramOrdenado?.length == 1) ? (f.lstParametrosTotalResultado?.push(paramOrdenado[0].resulatdo)) : (f.lstParametrosTotalResultado?.push(""));
          });
        });
      }
    }, error => console.error(error));
  }

  cargarArchivo(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
      this.loading = !this.loading;
      this.totalService.cargarArchivoSECAIA(file).subscribe({
        next: (response: any) => {
          this.loading = false;
          this.mostrarMensaje(
            'Archivo cargado correctamente.',
            'success'
          );
          this.consultarMonitoreos();
        },
        error: (error: any) => {
          this.loading = false;
          this.fileName = '';
          const blob = new Blob(
            [error.error.Errors.toString().replaceAll(',', '\n')],
            {
              type: 'application/octet-stream',
            }
          );
          this.mostrarMensaje(
            'Se encontraron errores en el archivo procesado.',
            'warning'
          );
          this.hacerScroll();
          FileService.download(blob, 'errores.txt');
        },
      });
      this.resetInputFile(this.inputExcelObservaciones);
    }
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionadosDescarga();    
    if (muestreosSeleccionados.length == 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');
      return this.hacerScroll();
    }

    this.totalService.exportarResultadosExcelSECAIA(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          let fecha = new Date();
          let doc = "Revisión_SECAIA_" + fecha.getDate() + "_" + fecha.getMonth() + "_" + fecha.getFullYear();
          FileService.download(response, doc + '.xlsx');
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });  }

  obtenerSeleccionadosDescarga(): Array<any> {    
    this.camposDescarga = [];
    if (this.resultadosFiltradosn.filter((f) => f.isChecked).length > 0) {
      this.resultadosFiltradosn.filter((f) => f.isChecked).forEach((x) => {
        let campodes: ResultadoDescarga = {
          noEntregaOCDL: x.noEntregaOCDL,
          ocdl: x.direccionLocal,
          nombreSitio: x.nombreSitio,
          claveMonitoreo: x.claveMonitoreo,
          fechaRealizacion: x.fechaRealizacion,
          laboratorio: x.laboratorio,
          tipoCuerpoAgua: x.tipoCuerpoAgua,
          lstParametros: x.lstParametros,          
        }
        this.camposDescarga.push(campodes);
      });
    };
   
    if (this.camposDescarga.length > 0) {
      this.camposDescarga[0].lstParametrosOrden = this.paramTotalOrdenados;
    }
    return this.camposDescarga;
  }
  
  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoService.resultadosSeleccionados = muestreosSeleccionados;
  }

  descargarEvidencia(evidencias: Array<any>, sufijo: string) {
    this.loading = !this.loading;
    let nombrearchivo = evidencias.find((x) => x.sufijo == sufijo).nombreArchivo;
    this.muestreoService
      .descargarArchivo(nombrearchivo)
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          FileService.download(response, nombrearchivo ?? '');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }

  existeEvidencia(evidencias: Array<any>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  mostrarEvidencias(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoSeleccionadoDatos = muestreosSeleccionados;
    
    this.isMuestreoModal = (this.muestreoSeleccionadoDatos.length == 1) ? true : false;
    if (this.isMuestreoModal == false) {
      if (this.muestreoSeleccionadoDatos.length == 0)
        this.mostrarMensaje('Debe seleccionar un monitoreo para visualizar las evidencias correspondientes', 'warning');
      else if (this.muestreoSeleccionadoDatos.length > 1) { this.mostrarMensaje('Solo debes de  seleccionar un monitoreo para visualizar las evidencias correspondientes', 'warning'); }
      return this.hacerScroll();
    }
  }

  //este es para enviar por bloque todo en si
  enviarMonitoreosBloque(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoSeleccionadoDatos = muestreosSeleccionados;
    if (this.muestreoSeleccionadoDatos.length == 0) { this.mostrarMensaje("Al menos debes de selecionar un muestreo para enviar a revisión de resultados", "warning"); }
    else {
      this.guardarEnvios();
    }
    this.hacerScroll();
  }

  guardarEnvios(): void {
    for (var i = 0; i < this.muestreoSeleccionadoDatos.length; i++) {
      let valr = {
        estatusId: 4,
        usuarioId: localStorage.getItem('idUsuario'),
        tipoAprobId: 0,
        muestreoId: this.muestreoSeleccionadoDatos[i].muestreoId,
        lstparametros: this.muestreoSeleccionadoDatos[i].lstParametros,
        isOCDL: false
      }
      this.muestreoActualizar.push(valr);
    }
    this.loading = true;
    this.totalService.actualizacionMuestreosParametros(this.muestreoActualizar)
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.mostrarMensaje(
            'Se envio el monitoreo correctamente a revisión de resultados de SECAIA',
            'success'
          );
          this.hacerScroll();
          this.seleccionarTodosChck = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  filtrar() {
    this.resultadosFiltradosnf = this.resultadosnf;
    this.columnas.forEach((columna) => {
      this.resultadosFiltradosnf = this.resultadosFiltradosnf.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.establecerValoresFiltrosTablan();
  };

  establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltradosnf.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  };

  descargarEvidencias() {
    let fecha = new Date();
    let fechaActual =
      fecha.getDate() +
      '-' +
      (fecha.getMonth() + 1) +
      '-' +
      fecha.getFullYear();
    let muestreosSeleccionados= this.Seleccionados(this.resultadosFiltradosn).map((m) => {
      return m.muestreoId;
    });
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'No ha seleccionado ningún monitoreo',       
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }
    this.loading = true;
    this.totalService.descargarArchivosEvidencias(muestreosSeleccionados).subscribe({
      next: (response: any) => {     
        this.loading = !this.loading;
        this.seleccionarTodosChck = false;
        this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
          m.isChecked = false;
          return m;
        });
        //-----------------------------------------------------------------------
        FileService.download(response, 'Evidencias/' + fechaActual + '.zip');
      },
      error: (response: any) => {      
        this.loading = !this.loading;
        this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.mostrarMensaje(
          'No fue posible descargar la información',
          TipoMensaje.Error
        );
        this.hacerScroll();
      },
    });
  } 
}
