import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { acumuladosMuestreo } from 'src/app/interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo'


@Component({
  selector: 'app-resumen-reglas',
  templateUrl: './resumen-reglas.component.html',
  styleUrls: ['./resumen-reglas.component.css']
})
export class ResumenReglasComponent extends BaseService implements OnInit {

  constructor(private validacionService: ValidacionReglasService ) { super(); }
  datosAcumualdos: Array<acumuladosMuestreo> = [];

  ngOnInit(): void {
    this.columnas = [
      { nombre: 'numEntrega', etiqueta: 'NÚMERO DE ENTREGA', orden: 0, filtro: new Filter() },
      { nombre: 'claveUnica', etiqueta: 'CLAVE ÚNICA', orden: 0, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'claveSitio', etiqueta: 'CLAVE CONALAB', orden: 0, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaProgramada', etiqueta: 'FECHA PROGRAMADA VISITA', orden: 0, filtro: new Filter() },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REAL VISITA', orden: 0, filtro: new Filter() },
      { nombre: 'horaInicio', etiqueta: 'HORA INICIO MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'horaFin', etiqueta: 'HORA FIN MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'zonaEstrategica', etiqueta: 'ZONA ESTRATEGICA', orden: 0, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'subtipoCuerpoAgua', etiqueta: 'SUBTIPO CUERPO AGUA', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO BASE DE DATOS', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorioRealizoMuestreo', etiqueta: 'LABORATORIO QUE REALIZO EL MUESTREO', orden: 0, filtro: new Filter() },
      { nombre: 'laboratorioSubrogado', etiqueta: 'LABORATORIO SUBROGADO', orden: 0, filtro: new Filter() },
      { nombre: 'grupoParametro', etiqueta: 'GRUPO DE PARAMETROS', orden: 0, filtro: new Filter() },
      { nombre: 'subGrupo', etiqueta: 'SUBGRUPO PARAMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'claveParametro', etiqueta: 'CLAVE PARÁMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'parametro', etiqueta: 'PARÁMETRO', orden: 0, filtro: new Filter() },
      { nombre: 'unidadMedida', etiqueta: 'UNIDAD DE MEDIDA', orden: 0, filtro: new Filter() },
      { nombre: 'resultado', etiqueta: 'RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'nuevoResultadoReplica', etiqueta: 'NUEVO RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'programaAnual', etiqueta: 'AÑO DE OPERACIÓN', orden: 0, filtro: new Filter() },
      { nombre: 'idResultadoLaboratorio', etiqueta: 'ID RESULTADO', orden: 0, filtro: new Filter() },
      { nombre: 'fechaEntrega', etiqueta: 'FECHA ENTREGA', orden: 0, filtro: new Filter() },
      { nombre: 'replica', etiqueta: 'REPLICA', orden: 0, filtro: new Filter() },
      { nombre: 'cambioResultado', etiqueta: 'CAMBIO DE RESULTADO', orden: 0, filtro: new Filter() }
    ];

    //cAMBIAR ESTATUS
    this.validacionService.getResultadosAcumuladosParametros(estatusMuestreo.Cargado).subscribe({
      next: (response: any) => {
        this.datosAcumualdos = response.data;      
        this.resultadosFiltradosn = this.datosAcumualdos;
        this.resultadosn = this.datosAcumualdos;
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
          FileService.download(response, 'RESUMEN_VALIDACIONREGLAS.xlsx');
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
