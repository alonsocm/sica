import { Component, OnInit, ViewChild } from '@angular/core';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { ResumenResultados } from '../../../../../../../interfaces/ResumenResultado.interface';
import { FileService } from '../../../../../../../shared/services/file.service';
import { TotalService } from '../../../../services/total.service';
import { NumberService } from 'src/app/shared/services/number.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Resultado } from '../../../../../../../interfaces/Resultado.interface';

@Component({
  selector: 'app-resumen',
  templateUrl: './resumen.component.html',
  styleUrls: ['./resumen.component.css'],
})
export class ResumenComponent extends BaseService implements OnInit {
  
  resultadosAprobados: number = 0;
  resultadosRechazados: number = 0;  

  Data: any[] = [];
  resultadosFiltro: any[] = [];
  resultadosParametros: any[] = [];
  filtradoCorrectoParam: any[] = [];
  filtradoRechazadosParam: any[] = [];
  datosResultados: Array<ResumenResultados> = [];  
  datosParametrosFiltrados: Array<ResumenResultados> = [];  
  selectedItemsList: Array<Resultado> = [];
  resumenParametros: Array<Parametro> = [];  

  constructor(public numberService: NumberService,private resumenSrv: TotalService) {super();}

  ngOnInit(): void {
    this.columnas = [
      { nombre: 'noEntregaOCDL', etiqueta: 'N° ENTREGA OC/DL', orden: 0, filtro: new Filter() },
      { nombre: 'claveUnica', etiqueta: 'CLAVE ÚNICA', orden: 0, filtro: new Filter() },
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 0, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'claveParametro', etiqueta: 'CLAVE PARÁMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAguaOriginal', etiqueta: 'TIPO CUERPO AGUA ORIGINAL', orden: 0, filtro: new Filter() },
      { nombre: 'resultado', etiqueta: 'RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'tipoAprobacion', etiqueta: 'TIPO DE APROBACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'esCorrectoResultado', etiqueta: 'RESULTADO CORRECTO', orden: 0, filtro: new Filter() },
      { nombre: 'observacionesOCDL', etiqueta: 'OBSERVACIÓN OC/DL', orden: 0, filtro: new Filter() },
      { nombre: 'fechaLimiteRevision', etiqueta: 'FECHA LÍMITE REVISIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'nombreUsuario', etiqueta: 'USUARIO QUE REVISÓ', orden: 0, filtro: new Filter() },
      { nombre: 'estatusResultado', etiqueta: 'ESTATUS DEL RESULTADO', orden: 0, filtro: new Filter() }      
    ];
    this.consultarMonitoreos();
  }  
  consultarMonitoreos(): void {
    this.loading = true;
    this.resumenSrv.getResumenRevisionResultados(5, true).subscribe({
      next: (response: any) => {
        this.Data = response.data;
        this.resultadosFiltradosn = this.Data;
        this.resultadosn = this.Data;
        this.establecerValoresFiltrosTablan();
        this.loading = false;
      },
      error: (error) => { this.loading = false; },
    });
  }  
  onDownload() {
    
    let fecha = new Date();
    var nombreDocumento = "Resumen_OCDL_" + fecha.getDate() + "_" + fecha.getMonth() + "_" + fecha.getFullYear();
    var seleccionados = this.Seleccionados(this.resultadosFiltradosn); 
    if (this.datosResultados != null && seleccionados.length > 0) {
      this.resumenSrv
        .getExcelResume(seleccionados)
        .subscribe({
          next: (response: any) => {
            //this.datosResultadosFiltrados = this.datosResultados.map((m) => {
            //  m.isChecked = true;
            //  return m;
            //});
            //this.showSpinner();
            FileService.download(
              response,
               nombreDocumento + '.xlsx'
            );
            this.mostrarMensaje('Se ha descargado la información', 'success');
          },
          error: (response: any) => {
            this.mostrarMensaje(
              'No fue posible generar el informe en formato excel',
              'danger'
            );
          },
        });
    }
    else { this.mostrarMensaje("Debe seleccionar al menos un registro", "warning"); this.hacerScroll();  }
  }   
  resumenDatos() {
    this.selectedItemsList = this.Seleccionados(this.resultadosFiltradosn);
    this.resultadosFiltro = this.obtenerResumenMonitoreosSeleccionados(this.selectedItemsList.map((m) => (m.esCorrectoResultado)));    
    this.resultadosAprobados = this.resultadosFiltro.filter(x => x.nombre == "SI")[0].valor;
    this.resultadosRechazados = this.resultadosFiltro.filter(x => x.nombre == "NO")[0].valor;

    this.obtenerResumenMonitoreosSeleccionados(this.selectedItemsList.map((m) => (m.claveParametro))).forEach((resultadoa) => {
        let parametross = this.selectedItemsList.filter(x => x.claveParametro == resultadoa.nombre).map((m) => (m.esCorrectoResultado));
        this.resultadosParametros = this.obtenerResumenMonitoreosSeleccionados(parametross);
        this.filtradoCorrectoParam = this.resultadosParametros.filter(x => x.nombre == "SI");
        this.filtradoRechazadosParam = this.resultadosParametros.filter(x => x.nombre == "NO");

           let nuevoParametro: Parametro = {
               nombre: resultadoa.nombre,
               correctos: (this.filtradoCorrectoParam.length > 0) ? this.filtradoCorrectoParam[0].valor : 0,
               incorrectos: (this.filtradoRechazadosParam.length > 0) ? this.filtradoRechazadosParam[0].valor : 0 };
           this.resumenParametros.push(nuevoParametro); });    
  }
  obtenerResumenMonitoreosSeleccionados(muestreos: any): { nombre: string; valor: unknown; }[] {    
    this.resultadosFiltro = muestreos.reduce(
      (prev: any, cur: any) => ((prev[cur] = prev[cur] + 1 || 1), prev),
      [] 
    );
    return Object.entries(this.resultadosFiltro).map(([nombre, valor]) => ({
      nombre,
      valor,
    }));
  }  
  changeSelection(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.resumenDatos();
  }
}

interface Parametro {
  nombre: string;
  correctos: number;
  incorrectos: number;
}
