import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { BaseService } from '../../../../shared/services/base.service';
import { validacionEvidencia } from 'src/app/interfaces/validacionEvidencias/validacionEvidencia.interface';
import { ValidacionService } from '../services/validacion.service';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { vwValidacionEvidencia } from 'src/app/interfaces/validacionEvidencias/vwValidacionEvidencia.interface';
import { Router } from '@angular/router';
import { FileService } from 'src/app/shared/services/file.service';
import { PuntosMuestreo, PuntosMuestreoNombre } from '../../../../shared/enums/puntosMuestreo';
import { tipoEvidencia } from '../../../../shared/enums/tipoEvidencia';
import { PuntosEvidenciaMuestreo } from '../../../../interfaces/puntosEvidenciaMuestreo.interface';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };


@Component({
  selector: 'app-validacion-evidencias',
  templateUrl: './validacion-evidencias.component.html',
  styleUrls: ['./validacion-evidencias.component.css']
})
export class ValidacionEvidenciasComponent extends BaseService implements OnInit {

  muestreosFiltrados: Array<vwValidacionEvidencia> = [];
  muestreosaValidr: Array<vwValidacionEvidencia> = [];
  columnasBitacoraMuestreo: Array<Columna> = [];
  columnasCriteriosFotoMuesreo: Array<Columna> = [];
  columnasCriteriosFotoMuestras: Array<Columna> = [];
  columnasCriteriosFotoAforo: Array<Columna> = [];
  columnasFormatoAForo: Array<Columna> = [];
  columnasTrackRuta: Array<Columna> = [];
  columnasCadenaCustodia: Array<Columna> = [];
  columnasTabla9: Array<Columna> = [];
  archivo: any;
  puntosMuestreo: Array<PuntosEvidenciaMuestreo> = [];



  @ViewChild('inputExcelMonitoreos') inputExcelMonitoreos: ElementRef = {} as ElementRef;
  constructor(private validacionService: ValidacionService,
    private evidenciaService: EvidenciasService,
    private router: Router) {
    super();
  }

  ngOnInit(): void {
    console.log(this.muestreosaValidr);
    this.definirColumnas();
    this.obtenerDatos();
  }

  definirColumnas() {
    let nombresColumnasTabla1: Array<Columna> = [
      { nombre: 'estatus', etiqueta: 'MUESTREO', orden: 1, filtro: new Filter(), },
      { nombre: 'sitio', etiqueta: 'SITIO', orden: 2, filtro: new Filter(), },
      { nombre: 'numeroEntrega', etiqueta: 'CLAVE CONALAB', orden: 3, filtro: new Filter(), },
      { nombre: 'tipoCuerpoAgua', etiqueta: 'TIPO CUERPO AGUA', orden: 4, filtro: new Filter(), },
      { nombre: 'laboratorio', etiqueta: 'LABORATORIO', orden: 5, filtro: new Filter(), },
      { nombre: 'laboratorioSubrogado', etiqueta: 'LABORATORIO REALIZO MUESTREO', orden: 6, filtro: new Filter(), },
      { nombre: 'eventualidad', etiqueta: 'CON EVENTUALIDADES', orden: 7, filtro: new Filter(), },
      { nombre: 'fechaProgramada', etiqueta: 'FECHA PROGRAMADA VISITA', orden: 8, filtro: new Filter(), },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REAL VISITA', orden: 9, filtro: new Filter(), },
      { nombre: 'brigada', etiqueta: 'BRIGADA', orden: 10, filtro: new Filter(), },
      { nombre: 'conqc', etiqueta: 'CON QC DE MUESTREO', orden: 11, filtro: new Filter(), },
      { nombre: 'tiposupervision', etiqueta: 'TIPO DE SUPERVISIÓN', orden: 12, filtro: new Filter(), },
      { nombre: 'observacionmuestreo', etiqueta: 'OBSERVACIÓN DEL MUESTREO', orden: 13, filtro: new Filter(), },
      { nombre: 'tipoEventualidad', etiqueta: 'TIPO DE EVENTUALIDAD', orden: 14, filtro: new Filter(), },
      { nombre: 'fechaProgramacion', etiqueta: 'FECHA DE REPROGRAMACIÓN', orden: 15, filtro: new Filter(), },
      { nombre: 'observaciones1', etiqueta: 'OBSERVACIONES EVIDENCIA CONAGUA', orden: 16, filtro: new Filter(), },
      { nombre: 'observaciones2', etiqueta: 'OBSERVACIONES EVIDENCIA CONAGUA 2', orden: 17, filtro: new Filter(), },
      { nombre: 'observaciones3', etiqueta: 'OBSERVACIONES EVIDENCIA CONAGUA 3', orden: 18, filtro: new Filter(), },
      { nombre: 'evidencias', etiqueta: 'EVIDENCIAS ESPERADAS', orden: 19, filtro: new Filter(), },
      { nombre: 'cumpleevidencias', etiqueta: 'CUMPLE NÚMERO EVIDENCIAS QUE SE ESPERABAN', orden: 20, filtro: new Filter(), },
    ];
    this.columnas = nombresColumnasTabla1;

    /*let criteriosBitacoraMuestreo: Array<Columna>*/
    this.columnasBitacoraMuestreo
      = [
      { nombre: 'folio', etiqueta: 'FOLIO', orden: 1, filtro: new Filter(), },
      { nombre: 'fechaRealizacion', etiqueta: 'FECHA REALIZACIÓN', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE CON FECHA REALIZACIÓN', orden: 3, filtro: new Filter(), },
      { nombre: 'horainio', etiqueta: 'HORA INICIO', orden: 4, filtro: new Filter(), },
      { nombre: 'horafin', etiqueta: 'HORA FIN', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'TIEMPO MÍNIMO DE MUESTREO', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'TIEMPO DE MUESTREO', orden: 7, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE TIEMPO DE MUESTREO', orden: 8, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CLAVE CONALAB', orden: 9, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE CLAVE', orden: 10, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CLAVE DE MUESTREO', orden: 11, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE CLAVE DE MUESTREO', orden: 12, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LÍDER DE LA BRIGADA', orden: 13, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LÍDER DE LA BRIGADA BASE', orden: 14, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE LÍDER DE LA BRIGADA', orden: 15, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CLAVE DE LA BRIGADA', orden: 16, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE CLAVE DE LA BRIGADA', orden: 17, filtro: new Filter(), },
      { nombre: '', etiqueta: 'PLACAS DEL VEHÍCULO DE MUESTREO', orden: 18, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LAT1', orden: 19, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LONG1', orden: 20, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LAT2', orden: 21, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LONG2', orden: 22, filtro: new Filter(), },
      { nombre: '', etiqueta: 'DISTANCIA ENTRE PUNTOS', orden: 23, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE GEOCERCA', orden: 24, filtro: new Filter(), },
      { nombre: '', etiqueta: 'MOSTRAR EN MAPA', orden: 25, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CALIBRACIÓN Y/O VERIFICACIÓN DE LOS EQUIPOS', orden: 26, filtro: new Filter(), },
      { nombre: '', etiqueta: 'REGISTRO DE RESULTADOS DE CAMPO', orden: 27, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FIRMADO Y CANCELADO', orden: 28, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FOTOGRAFÍA DEL GPS EN EL PUNTO DE MUESTREO', orden: 29, filtro: new Filter(), },
      { nombre: '', etiqueta: 'REGISTROS VISIBLES', orden: 30, filtro: new Filter(), },
    ];


    this.columnasCriteriosFotoMuesreo = [
      { nombre: '', etiqueta: 'METADATOS LAT', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'METADATOS LONG', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FECHA', orden: 3, filtro: new Filter(), },
      { nombre: '', etiqueta: 'HORA', orden: 4, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE METADATOS', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LÍDER DE BRIGADA Y CUERPO DE AGUA', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FOTO ÚNICA', orden: 7, filtro: new Filter(), },
    ];

    this.columnasCriteriosFotoMuestras = [
      { nombre: '', etiqueta: 'METADATOS LAT', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'METADATOS LONG', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FECHA', orden: 3, filtro: new Filter(), },
      { nombre: '', etiqueta: 'HORA', orden: 4, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE METADATOS', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'REGISTRO DE LOS RECIPIENTES', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'MUESTRAS PRSERVADAS CON HIELO', orden: 7, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LA FOTO ES ÚNICA', orden: 8, filtro: new Filter(), },
    ];

    this.columnasCriteriosFotoAforo = [
      { nombre: '', etiqueta: 'METADATOS LAT', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'METADATOS LONG', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FECHA', orden: 3, filtro: new Filter(), },
      { nombre: '', etiqueta: 'HORA', orden: 4, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE METADATOS', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'METODOLOGÍA', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FOTO ÚNICA', orden: 7, filtro: new Filter(), },
    ];

    this.columnasFormatoAForo = [
      { nombre: '', etiqueta: 'FORMATO LLENADO CORRECTO', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LAT3', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LONG3', orden: 3, filtro: new Filter(), },
      { nombre: '', etiqueta: 'DISTANCIA PUNTO DE AFORO', orden: 4, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE GEOCERCA', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CAUDAL EN m3/s', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'REGISTROS LEGIBLES', orden: 7, filtro: new Filter(), },
    ];

    this.columnasTrackRuta = [
      { nombre: '', etiqueta: 'PLACAS DE LA UNIDAD', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE PLACAS DE LA UNIDAD', orden: 2, filtro: new Filter(), },
      { nombre: '', etiqueta: 'LABORATORIO', orden: 3, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FECHA DE INICIO', orden: 4, filtro: new Filter(), },
      { nombre: '', etiqueta: 'HORA DE INICIO', orden: 5, filtro: new Filter(), },
      { nombre: '', etiqueta: 'FECHA FINAL', orden: 6, filtro: new Filter(), },
      { nombre: '', etiqueta: 'HORA FINAL', orden: 7, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CLAVE CONALAB', orden: 8, filtro: new Filter(), },
      { nombre: '', etiqueta: 'CUMPLE CLAVE CONALAB', orden: 9, filtro: new Filter(), },
      { nombre: '', etiqueta: 'POSICIÓN MÁS CERCANA AL PUNTO DE MUESTREO', orden: 10, filtro: new Filter(), },
      { nombre: '', etiqueta: 'MOSTRAR EN MAPA', orden: 11, filtro: new Filter(), },
    ];

    this.columnasCadenaCustodia = [
      { nombre: '', etiqueta: 'DEBIDAMENTE LLENADO ASEGURANDO LA CADENA INTERRUMPIDA', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'REGISTROS LEGIBLES', orden: 2, filtro: new Filter(), },
     
    ];

    this.columnasTabla9 = [
      { nombre: '', etiqueta: 'RECHAZO', orden: 1, filtro: new Filter(), },
      { nombre: '', etiqueta: 'OBSERVACIONES RECHAZO', orden: 2, filtro: new Filter(), },
    ];


  }

  seleccionar() { }

  filtrar() { }

  cargarArchivo(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();

    if (this.archivo) {
      this.loading = !this.loading;

      this.validacionService.cargarArchivo(this.archivo[0]).subscribe({
        next: (response: any) => {
          if (response.data.correcto) {
            this.loading = false;
            this.mostrarMensaje(
              'Archivo procesado correctamente.',
              TIPO_MENSAJE.exito
            );
            this.resetInputFile(this.inputExcelMonitoreos);
            //this.consultarMonitoreos();
          }
          else {
            this.loading = false;
            //this.numeroEntrega = response.data.numeroEntrega;
            //this.anioOperacion = response.data.anio;
            document.getElementById('btnMdlConfirmacionActualizacion')?.click();
          }
        },
        error: (error: any) => {
          this.loading = false;
          let archivoErrores = this.generarArchivoDeErrores(error.error.Errors);
          this.mostrarMensaje(
            'Se encontraron errores en el archivo procesado.',
            TIPO_MENSAJE.error
          );
          this.hacerScroll();
          FileService.download(archivoErrores, 'errores.txt');
          this.resetInputFile(this.inputExcelMonitoreos);
        },
      });
    }
  }
  validacion() { }
  limpiarFiltros() { }
  onVerMapaClick(muestreo: string, datos: any[]) {
    this.getPuntos_PR_PM(muestreo, datos);
  }

  getPuntos_PR_PM(claveMuestreo: string, datos: any[]) {
    localStorage.setItem('claveMuestreoCalculo', claveMuestreo);
    this.evidenciaService.getPuntosPB_PM(claveMuestreo).subscribe({
      next: (response: any) => {
        this.puntosMuestreo = response.data;
        //let datos = this.informacionEvidencias.filter(
        //  (x) =>
        //    x.muestreo == claveMuestreo &&
        //    this.tiposEvidenciasPuntos.includes(x.tipoEvidenciaMuestreo)
        //);

        if (datos.length > 0) {
          for (var i = 0; i < datos.length; i++) {
            let nombrepunto = '';
            let puntoMuestreo = '';
            switch (datos[i].tipoEvidenciaMuestreo) {
              case tipoEvidencia.FotoCaudal:
                nombrepunto = PuntosMuestreoNombre.FotodeAforo_FA;
                puntoMuestreo = PuntosMuestreo.FotodeAforo_FA;
                break;
              case tipoEvidencia.FotoMuestra:
                nombrepunto = PuntosMuestreoNombre.FotodeMuestras_FS;
                puntoMuestreo = PuntosMuestreo.FotodeMuestras_FS;
                break;
              case tipoEvidencia.Track:
                nombrepunto = PuntosMuestreoNombre.PuntoCercanoalTrack_TR;
                puntoMuestreo = PuntosMuestreo.PuntoCercanoalTrack_TR;
                break;
              case tipoEvidencia.FotoMuestreo:
                nombrepunto = PuntosMuestreoNombre.FotodeMuestreo_FM;
                puntoMuestreo = PuntosMuestreo.FotodeMuestreo_FM;
                break;
              default:
            }

            let punto: PuntosEvidenciaMuestreo = {
              claveMuestreo: claveMuestreo,
              latitud: Number(datos[i].latitud),
              longitud: Number(datos[i].longitud),
              nombrePunto: nombrepunto,
              punto: puntoMuestreo,
            };
            this.puntosMuestreo.push(punto);
            this.evidenciaService.updateCoordenadas(this.puntosMuestreo);
            this.router.navigate(['/map-muestreo']);
          }
        }
      },
      error: (response: any) => { },
    });
  }

  private obtenerDatos(): void {
    this.validacionService.obtenerDatosaValidar().subscribe({
      next: (response: any) => {

        this.muestreosFiltrados = response.data;
        console.log(this.muestreosFiltrados);
        
      },
      error: (error) => { },
    });
  }

  validar(muestreo: any) {
    console.log(muestreo);
    let usuarioId = localStorage.getItem('idUsuario');
    
   
    this.validacionService.validarMuestreo(muestreo, usuarioId).subscribe({
      next: (response: any) => {
        console.log(response.data);
      },
      error: (response: any) => { },
    });
    
  }
  
}
