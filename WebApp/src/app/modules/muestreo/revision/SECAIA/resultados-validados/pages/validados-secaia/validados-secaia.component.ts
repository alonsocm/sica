
import { Component, OnInit, ViewChild } from '@angular/core';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Muestreo } from '../../../../../../../interfaces/Muestreo.interface';
import { MuestreoService } from '../../../../../liberacion/services/muestreo.service';
import { FileService } from '../../../../../../../shared/services/file.service';
import { TotalService } from '../../../../services/total.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-validados-secaia',
  templateUrl: './validados-secaia.component.html',
  styleUrls: ['./validados-secaia.component.css']
})
export class ValidadosSecaiaComponent extends BaseService implements OnInit {
  public observacionesCat: any[] = [];
  public pages: number = 1;
  registroParam: FormGroup;
  ismuestreoModalEdicion: boolean = false;
  muestreos: Array<Muestreo> = [];
  resultadosFiltrados: Array<Muestreo> = [];
  muestreosFiltrados: Array<any> = [];
  muestreoActualizar: Array<any> = [];
  encabezadosTablaEdicion: Array<string> = [
    "N°. ENTREGA A REVISAR",
    "NOMBRE SITIO",
    "CLAVE ÚNICA",
    "CLAVE PARÁMETRO",
    "RESULTADO", "OBSERVACIÓN SECAIA", "OTRO"]
  fechaLimiteRevision: string = '';
  @ViewChild('control') control: any;

  constructor(
    private fb: FormBuilder,
    private muestreoService: MuestreoService,
    private totalService: TotalService
  ) {
    super();
    this.registroParam = this.fb.group({
      dropObservaciones: [null, Validators.requiredTrue],
    });
  }

  ngOnInit(): void {
    this.columnas = [
      { nombre: 'noEntregaOCDL', etiqueta: 'N°. ENTREGA', orden: 1, filtro: new Filter() },
      { nombre: 'claveUnica', etiqueta: 'CLAVE ÚNICA', orden: 2, filtro: new Filter() },
      { nombre: 'claveSitio', etiqueta: 'CLAVE SITIO', orden: 3, filtro: new Filter() },
      { nombre: 'claveMonitoreo', etiqueta: 'CLAVE MONITOREO', orden: 4, filtro: new Filter() },
      { nombre: 'nombreSitio', etiqueta: 'NOMBRE SITIO', orden: 5, filtro: new Filter() },
      { nombre: 'claveParametro', etiqueta: 'CLAVE PARÁMETRO', orden: 6, filtro: new Filter() },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 7, filtro: new Filter() },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 8, filtro: new Filter() },
      { nombre: 'resultado', etiqueta: 'RESULTADO', orden: 9, filtro: new Filter() },
      { nombre: 'observacionSECAIA', etiqueta: 'OBSERVACIÓN SECAIA', orden: 10, filtro: new Filter() },
      { nombre: 'fechaRevision', etiqueta: 'FECHA DE REVISIÓN', orden: 12, filtro: new Filter() },
      { nombre: 'nombreUsuario', etiqueta: 'NOMBRE USUARIO QUE REVISÓ', orden: 13, filtro: new Filter() },
      { nombre: 'estatusResultado', etiqueta: 'ESTATUS DEL RESULTADO', orden: 14, filtro: new Filter() }
    ]
    this.consultarMonitoreos();
  }

  consultarMonitoreos(): void {
    this.loading = true;
    this.totalService.getResumenRevisionResultados(estatusMuestreo.Validado, false).subscribe({
      next: (response: any) => {
        //Para filtrado general es de diferente tipo que muetreos y muestreosFiltrados
        this.resultadosn = response.data;
        this.resultadosFiltradosn = this.resultadosn;

        this.muestreos = response.data;
        this.muestreosFiltrados = this.muestreos;

        if (this.resultadosn.length > 0) {
          this.totalService.getObseravciones().subscribe(result => {
            this.observacionesCat = result.data;
          }, error => console.error(error));
        }
        this.loading = false;
      },
      error: (error) => { this.loading = false; },
    });
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.obtenerSeleccionados();
    this.muestreoService.muestreosSeleccionados = muestreosSeleccionados;
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');
      return this.hacerScroll();
    }

    this.totalService.exportExcelValidadosSECAIA(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'ResultadosValidados.xlsx');
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

  enviarMonitoreosBloque(): void {
    if (this.obtenerSeleccionados().length > 0) { this.guardarenvios(); }
    else {
      this.mostrarMensaje(
        'Debes de seleccionar al menos un muestreo para el envio a etapa aprobacion final',
        'danger'
      );
      this.hacerScroll();
    }

  }

  guardarenvios(): void {
    this.muestreosFiltrados = this.obtenerSeleccionados();
    let muestreosModificar = [...new Set(this.muestreosFiltrados.map(m => m.muestreoId))];
    let muestreos = { estatusSECAIAId: estatusMuestreo.AprobacionFinal, estatusId: estatusMuestreo.AprobacionFinal, muestreoId: [...new Set(muestreosModificar)], IdUsuario: localStorage.getItem('idUsuario'), };
    this.loading = true;
    this.totalService.actualizarResultado(muestreos)
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.mostrarMensaje(
            'Se enviaron correctamente los monitoreos a la etapa de aprobación final',
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

  habilitarEdicion(): void {
    this.muestreoActualizar = [];
    this.muestreoActualizar = this.muestreosFiltrados.filter((f) => f.isChecked);
    this.muestreoActualizar.filter((f) => { f.observacionSECAIA = "", f.observacionSECAIAId = null });
    this.ismuestreoModalEdicion = (this.muestreoActualizar.length > 0) ? true : false;
    if (this.muestreoActualizar.length > 0) {
      this.ismuestreoModalEdicion = true;
    }
    else { this.ismuestreoModalEdicion = false; this.mostrarMensaje('Debe seleccionar  un monitoreo para realizar la edición de observaciones', 'warning'); return this.hacerScroll(); }
  }

  actualizarObservaciones() {
    this.muestreoActualizar = this.muestreoActualizar.map((m) => {
      m.isChecked = false;
      return m;
    });
    this.muestreoActualizar = this.muestreoActualizar.map((m) => {
      const input = document.getElementById(m.id) as HTMLInputElement | null;
      let valr = {
        Id: m.parametroId,
        ObservacionesSECAIAId: m.observacionSECAIAId,
        ObservacionesSECAIA: input?.value
      }
      m.lstParametros.push(valr);
      m.observacionSECAIA = input?.value
      return m;
    });
    this.loading = true;
    this.totalService.actualizacionParametros(this.muestreoActualizar)
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.mostrarMensaje(
            'Se actualizo correctamente las observaciones de los parametros',
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

  private obtenerSeleccionados(): Array<Muestreo> {
    return this.muestreosFiltrados.filter((f) => f.isChecked);
  }
}
