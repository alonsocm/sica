import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';


@Component({
  selector: 'app-acumulacion-resultados',
  templateUrl: './acumulacion-resultados.component.html',
  styleUrls: ['./acumulacion-resultados.component.css']
})
export class AcumulacionResultadosComponent extends BaseService implements OnInit {

  constructor(private validacionService: ValidacionReglasService) { super(); }

  ngOnInit(): void {

    this.columnas = [
      { nombre: 'claveUnica', etiqueta: 'CLAVE ÚNICA', orden: 0, filtro: new Filter() },
      { nombre: 'claveMuestreo', etiqueta: 'CLAVE MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'claveCONALAB', etiqueta: 'CLAVE CONALAB', orden: 0, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaVisita', etiqueta: 'FECHA PROGRAMADA VISITA', orden: 0, filtro: new Filter() },
      { nombre: 'fechaRealVisita', etiqueta: 'FECHA REAL VISITA', orden: 0, filtro: new Filter() },
      { nombre: 'horaInicioMuestreo', etiqueta: 'HORA INICIO MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'horaFincioMuestreo', etiqueta: 'HORA FIN MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'zonaEstrategica', etiqueta: 'ZONA ESTRATEGICA', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'subtipoCuerpoAgua', etiqueta: 'SUBTIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO BASE DE DATOS', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorioMuestreo', etiqueta: 'LABORATORIO QUE REALIZO EL MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorioSubrogado', etiqueta: 'LABORATORIO SUBROGADO', orden: 0, filtro: new Filter() },
      { nombre: 'grupoParametros', etiqueta: 'GRUPO DE PARAMETROS', orden: 0, filtro: new Filter() },
      { nombre: 'subgrupoParametros', etiqueta: 'SUBGRUPO PARAMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'claveParametro', etiqueta: 'CLAVE PARÁMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'parametro', etiqueta: 'PARÁMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'unidadMedida', etiqueta: 'UNIDAD DE MEDIDA', orden: 0, filtro: new Filter() },
      { nombre: 'resultado', etiqueta: 'RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'nuevoResultado', etiqueta: 'NUEVO RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'anioOperacion', etiqueta: 'AÑO DE OPERACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'idResultado', etiqueta: 'ID RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaEntrega', etiqueta: 'FECHA ENTREGA', orden: 0, filtro: new Filter() },
      { nombre: 'replica', etiqueta: 'REPLICA', orden: 0, filtro: new Filter() },
      { nombre: 'cambioResultado', etiqueta: 'CAMBIO DE RESULTADO', orden: 0, filtro: new Filter() }
    ];
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
          FileService.download(response, 'ACUMULACIÓN_RESULTADOS.xlsx');
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

}
