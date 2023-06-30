import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
@Component({
  selector: 'app-reglas-validar',
  templateUrl: './reglas-validar.component.html',
  styleUrls: ['./reglas-validar.component.css']
})
export class ReglasValidarComponent extends BaseService implements OnInit {

  constructor(private validacionService: ValidacionReglasService) { super(); }
  resultadosMuestreo: Array<acumuladosMuestreo> = [];
  resultadosSeleccionados: Array<acumuladosMuestreo> = [];

  aniosSeleccionados: Array<number> = [];
  entregasSeleccionadas: Array<number> = [];
  ngOnInit(): void {
    this.columnas = [
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REALIZACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'fechaProgramada', etiqueta: 'FECHA PROGRAMADA', orden: 0, filtro: new Filter() },      
      { nombre: 'laboratorioRealizoMuestreo', etiqueta: 'LABORATORIO BASE DE DATOS', orden: 0, filtro: new Filter() },
      { nombre: 'cuerpoAgua', etiqueta: 'CUERPO DE AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'subtipoCuerpoAgua', etiqueta: 'SUBTIPO CUERPO AGUA', orden: 0, filtro: new Filter() },      
      { nombre: 'muestreoCompletoPorResultados', etiqueta: 'MUESTREO COMPLETO POR RESULTADOS', orden: 0, filtro: new Filter() },

      { nombre: 'cumpleReglas', etiqueta: '¿CUMPLE CON LA REGLAS CONDICIONANTES?', orden: 0, filtro: new Filter() },      
      { nombre: 'cumpleFechaEntrega', etiqueta: 'CUMPLE CON LA FECHA DE ENTREGA', orden: 0, filtro: new Filter() },
      { nombre: 'reglaValicdacion', etiqueta: 'SE CORRE REGLA DE VALIDACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'autorizacionRegla', etiqueta: 'AUTORIZACIÓN DE REGLAS CUANDO ESTE INCOMPLETO (SI)', orden: 0, filtro: new Filter() },
      { nombre: 'cumpleTodosCriterios', etiqueta: 'CUMPLE CON TODOS LOS CRITERIOS PAR<A APLICAR REGLAS (SI/NO)', orden: 0, filtro: new Filter() },
      { nombre: 'resultado', etiqueta: 'RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'muestreoValidadoPor', etiqueta: 'MUESTREO VALIDADO POR', orden: 0, filtro: new Filter() },
      { nombre: 'porcentajePago', etiqueta: '% DE PAGO', orden: 0, filtro: new Filter() }];


    this.validacionService.getResultadosporMonitoreo(this.aniosSeleccionados, this.entregasSeleccionadas).subscribe({
      next: (response: any) => {
        this.loading = true;
        this.resultadosMuestreo = response.data;  
        this.resultadosFiltradosn = this.resultadosMuestreo;
        this.resultadosn = this.resultadosMuestreo;
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
    this.validacionService.exportExcelResultadosValidados(this.resultadosFiltradosn)
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
  aplicarReglas(): void { }

}
