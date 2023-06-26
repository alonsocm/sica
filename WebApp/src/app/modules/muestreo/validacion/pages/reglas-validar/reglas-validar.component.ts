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
  resultadosFiltrados: Array<acumuladosMuestreo> = [];
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
        this.resultadosMuestreo = response.data;
        this.resultadosFiltrados = this.resultadosMuestreo;
      },
      error: (error) => { },
    });
  }

  onDownload(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo para descargar la información', 'warning');
      return this.hacerScroll();
    }
    this.loading = true;
    //cambiar
    this.validacionService.exportarResultadosAcumuladosExcel(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {

          this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'RESUMEN_CARGARESULTADOS_A_VALIDAR.xlsx');
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

  filtrarColumnas() {
    //this.resultadosFiltrados = this.datosAcumualdos;
    console.log(this.resultadosFiltrados);
    this.columnas.forEach((columna) => {
      this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.establecerValores();
  };
  establecerValores() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltrados.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  };

  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrarn();
    document.getElementById('dvMessage')?.click();
    this.establecerValores();
  };

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.resultadosSeleccionados = this.Seleccionados(
      this.resultadosFiltrados
    );
  }
  aplicarReglas(): void { }

}
