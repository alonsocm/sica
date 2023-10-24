import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { Filter } from '../../../../../../../interfaces/filtro.interface';
import { Resultado } from '../../../../../../../interfaces/Resultado.interface';
import { NumberService } from '../../../../../../../shared/services/number.service';
import { MuestreoService } from '../../../../../liberacion/services/muestreo.service';
import { FileService } from 'src/app/shared/services/file.service';
import { TotalService } from '../../../../services/total.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';

@Component({
  selector: 'app-total',
  templateUrl: './total.component.html',
  styleUrls: ['./total.component.css'],
})
export class TotalComponent extends BaseService implements OnInit {
  @ViewChild('inputExcelObservaciones') fileUpload: ElementRef =
    {} as ElementRef;
  @ViewChildren('filtrosn') filtrosn: any;
  public pages: number = 1;
  public observacionesCat: any[] = [];
  registroParam: FormGroup;
  archivo: any = null;
  muestreoSeleccionadoDatos: any = null;
  tipoaprobacion: number = 0;
  ismuestreoModal: boolean = false;
  muestreoActualizar: Array<any> = [];
  encabezadosDatosMuestreo: Array<string> = [
    'CLAVE SITIO',
    'CLAVE MONITOREO',
    'NOMBRE SITIO',
    'FECHA',
  ];
  encabezadosParametros: Array<string> = [
    'CLAVE PARÁMETRO',
    'NOMBRE PARÁMETRO',
    'RESULTADO',
    'OBSERVACIÓN OC/DL',
    'OTRO',
  ];
  fileName: string = '';
  nombredeArchivo: string = '';
  fechaLimiteRevision: string = '';

  constructor(
    public numberService: NumberService,
    private muestreoService: MuestreoService,
    private totalService: TotalService,
    private fb: FormBuilder
  ) {
    super();
    this.registroParam = this.fb.group({
      dropObservaciones: [null],
    });
  }

  ngOnInit(): void {
    this.columnas = [
      {
        nombre: 'noEntregaOCDL',
        etiqueta: 'N°. ENTREGA A REVISAR',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'organismoCuenca',
        etiqueta: 'OC/DL CORRESPONDIENTE',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA LÍMITE DE REALIZACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO DE CUERPO DE AGUA ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO DE CUERPO DE AGUA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'observaciones',
        etiqueta: 'OBSERVACIÓN LAB QUE HIZO DEL MUESTREO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaLimiteRevision',
        etiqueta: 'FECHA LÍMITE DE REVISIÓN',
        orden: 0,
        filtro: new Filter(),
      },
    ];
    this.resultadosn = [];
    this.resultadosFiltradosn = [];
    this.consultarMonitoreos();
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para descargar la información',
        'warning'
      );
      return this.hacerScroll();
    }
    this.loading = true;
    this.totalService
      .exportarResultadosExcel(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'TOTAL_RESULTADOS.xlsx');
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

  cargarArchivo(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.resetInputFile(this.fileUpload);
      this.fileName = file.name;
      this.loading = !this.loading;
      this.totalService.cargarArchivo(file).subscribe({
        next: (response: any) => {
          this.loading = false;
          this.mostrarMensaje('Archivo cargado correctamente.', 'success');
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
    }
  }

  consultarMonitoreos(): void {
    this.loading = true;
    this.totalService.getResultadosMuestreosParametros(true).subscribe({
      next: (response: any) => {
        this.resultadosn = response.data;
        this.resultadosFiltradosn = this.resultadosn;
        if (this.resultadosn.length > 0) {
          this.totalService.getObseravciones().subscribe(
            (result) => {
              this.observacionesCat = result.data;
            },
            (error) => console.error(error)
          );
        }
        this.establecerValoresFiltrosTablan();
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
      },
    });
  }

  enviarVencidos(lstVencidos: Array<Resultado>): void {
    for (var i = 0; i < lstVencidos.length; i++) {
      let valr = {
        estatusId: estatusMuestreo.PendienteDeEnvioAprobacionFinal,
        usuarioId: 0,
        tipoAprobId: estatusMuestreo.EnviadoConExtensionFecha,
        muestreoId: lstVencidos[i].muestreoId,
        lstparametros: lstVencidos[i].lstParametros,
        isOCDL: true,
      };
      this.muestreoActualizar.push(valr);
    }

    this.totalService
      .actualizacionMuestreosParametros(this.muestreoActualizar)
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.hacerScroll();
          this.seleccionarTodosChck = false;
        },
        error: (error) => {},
      });
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoService.resultadosSeleccionados = muestreosSeleccionados;
  }

  DescargarArch(valor: Resultado, tipoArchivo: number): void {
    let nomarch;
    if (valor.lstEvidencias != null) {
      let nombrearchivo = valor.lstEvidencias.filter(
        (x) => x.tipoEvidencia == tipoArchivo
      );
      nomarch = nombrearchivo[0].nombreArchivo;
      this.nombredeArchivo = nombrearchivo[0].nombreArchivo;
    }

    this.muestreoService.descargarArchivo(nomarch).subscribe({
      next: (response: any) => {
        FileService.download(response, this.nombredeArchivo);
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

  //metodo para actualizar
  enviarMonitoreos(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoSeleccionadoDatos = muestreosSeleccionados;
    this.ismuestreoModal =
      this.muestreoSeleccionadoDatos.length == 1 ? true : false;
    if (this.ismuestreoModal == false) {
      if (this.muestreoSeleccionadoDatos.length == 0)
        this.mostrarMensaje(
          'Debe seleccionar un monitoreo para realizar la validación manual',
          'warning'
        );
      else if (this.muestreoSeleccionadoDatos.length > 1) {
        this.mostrarMensaje(
          'Solo debes de  seleccionar un monitoreo para realizar la validación manual',
          'warning'
        );
      }
      return this.hacerScroll();
    }
  }

  //este es para enviar por bloque todo en si
  enviarMonitoreosBloque(): void {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.muestreoSeleccionadoDatos = muestreosSeleccionados;

    if (this.muestreoSeleccionadoDatos.length == 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para ser enviado a resultados validados por OC/DL',
        'warning'
      );
      return this.hacerScroll();
    } else {
      this.guardarEnvios(1);
    }
  }

  guardarEnvios(tipoaprobacion: number): void {
    for (var i = 0; i < this.muestreoSeleccionadoDatos.length; i++) {
      for (
        var j = 0;
        j < this.muestreoSeleccionadoDatos[0].lstParametros.length;
        j++
      ) {
        let input = document.getElementById(
          'tdcaja' + j
        ) as HTMLInputElement | null;
        this.muestreoSeleccionadoDatos[i].lstParametros[j].observacionesOCDL =
          input?.value;
      }

      let valr = {
        estatusId: 4,
        usuarioId: localStorage.getItem('idUsuario'),
        tipoAprobId: tipoaprobacion,
        muestreoId: this.muestreoSeleccionadoDatos[i].muestreoId,
        lstparametros: this.muestreoSeleccionadoDatos[i].lstParametros,
        isOCDL: true,
      };
      this.muestreoActualizar.push(valr);
    }

    this.loading = true;
    this.totalService
      .actualizacionMuestreosParametros(this.muestreoActualizar)
      .subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.fechaLimiteRevision = '';
          this.loading = false;
          this.mostrarMensaje(
            'Monitoreo(s) enviado(s) correctamente a resultados validados por OC/DL',
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
}
