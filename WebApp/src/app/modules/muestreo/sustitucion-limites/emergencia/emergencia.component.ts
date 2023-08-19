import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { LimitesService } from '../limites.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { FileService } from 'src/app/shared/services/file.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/modules/login/services/auth.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';

@Component({
  selector: 'app-emergencia',
  templateUrl: './emergencia.component.html',
  styleUrls: ['./emergencia.component.css'],
})
export class EmergenciaComponent extends BaseService implements OnInit {
  existeSustitucion: boolean = true;
  archivo: any;
  registros: Array<any> = [];
  contratoSeleccionado: string = 'Seleccionar';
  emergenciasPrevias: Array<string> = [];
  @ViewChild('inputExcelMonitoreosEmergencia')
  inputExcelMonitoreos: ElementRef = {} as ElementRef;

  formOpcionesSustitucion = new FormGroup({
    origenLimites: new FormControl('', Validators.required),
    excelLimites: new FormControl(),
    archivo: new FormControl(),
  });

  constructor(
    private limitesService: LimitesService,
    private authService: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    // this.obtenerMuestreosSustituidos();
  }

  onSubmit() {
    this.loading = true;
    this.limitesService
      .validarSustitucionPreviaEmergencias(Number(this.contratoSeleccionado))
      .subscribe({
        next: (response: any) => {
          this.existeSustitucion = response.data;
          if (!this.existeSustitucion) {
            this.sustituirLimites();
          } else {
            document.getElementById('btnMdlConfirmacionSustitucion')?.click();
          }
        },
        error: (e) => console.error(e),
      });
  }

  sustituirLimites() {
    let configSustitucion = {
      archivo: this.formOpcionesSustitucion.controls.archivo.value,
      origenLimites: this.formOpcionesSustitucion.controls.origenLimites.value,
      periodo: Number(this.contratoSeleccionado),
      usuario: this.authService.getUser().usuarioId,
    };

    document.getElementById('btnMdlConfirmacion')?.click();
    this.loading = true;

    this.limitesService
      .sustituirLimitesEmergencias(configSustitucion)
      .subscribe({
        next: (response: any) => {
          this.formOpcionesSustitucion.reset();
          this.loading = false;
          if (response.succeded === true) {
            this.contratoSeleccionado = 'Seleccionar';
            this.mostrarMensaje(
              'Se ejecutó correctamente la sustitución de los límites máximos',
              TipoMensaje.Correcto
            );
          } else {
            this.mostrarMensaje(response.message, TipoMensaje.Alerta);
          }
        },
        error: (response) => {
          this.formOpcionesSustitucion.reset();
          this.loading = false;
          this.mostrarMensaje(
            'Ocurrió un error en el proceso de sustitución' +
              ' ' +
              response.error.Errors[0],
            TipoMensaje.Error
          );
        },
      });
  }

  validarArchivo() {
    if (this.formOpcionesSustitucion.controls.origenLimites.value == '2') {
      this.formOpcionesSustitucion.controls.excelLimites.setValue('');
      this.formOpcionesSustitucion.controls.excelLimites.setValidators(
        Validators.required
      );
    } else {
      this.formOpcionesSustitucion.controls.excelLimites.setValue('');
      this.formOpcionesSustitucion.controls.excelLimites.setValidators(null);
    }

    this.formOpcionesSustitucion.controls.excelLimites.updateValueAndValidity();
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formOpcionesSustitucion.patchValue({
        archivo: file,
      });
    }
  }

  cargarArchivoEmergencias(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    this.procesarArchivoEmergencias();
  }

  procesarArchivoEmergencias(reemplazar?: string) {
    if (this.archivo) {
      this.loading = !this.loading;

      this.limitesService
        .cargaMuestreosEmergencia(
          this.archivo[0],
          this.contratoSeleccionado,
          reemplazar
        )
        .subscribe({
          next: (response: any) => {
            if (response.succeded) {
              this.resetInputFile(this.inputExcelMonitoreos);
              this.loading = false;
              this.mostrarMensaje(
                'Archivo procesado correctamente.',
                TipoMensaje.Correcto
              );
            } else if (!response.succeded) {
              this.emergenciasPrevias = response.data;
              this.loading = false;
              document.getElementById('btnMdlConfirmacionReemplazar')?.click();
            }
          },
          error: (error: any) => {
            this.loading = false;
            let archivoErrores = this.generarArchivoDeErrores(
              error.error.Errors
            );
            this.mostrarMensaje(
              'Se encontraron errores en el archivo procesado.',
              TipoMensaje.Error
            );
            this.hacerScroll();
            FileService.download(archivoErrores, 'errores.txt');
            this.resetInputFile(this.inputExcelMonitoreos);
          },
        });
    }
  }

  reemplazarResultadosPrevios() {
    document.getElementById('btnMdlConfirmacionReemplazar')?.click();
    this.formOpcionesSustitucion.reset();
    this.procesarArchivoEmergencias('true');
  }

  exportarResumen() {}

  exportarEmergenciasPreviasExcel() {
    let emergencias = this.emergenciasPrevias;

    this.loading = true;
    this.limitesService.exportarEmergenciasPreviasExcel(emergencias).subscribe({
      next: (response: any) => {
        this.loading = false;
        FileService.download(response, 'resultados.xlsx');
      },
      error: (response: any) => {
        this.loading = false;
        this.mostrarMensaje(
          'No fue posible descargar la información',
          TipoMensaje.Error
        );
        this.hacerScroll();
      },
    });
  }

  obtenerMuestreosSustituidos() {
    this.limitesService.obtenerMuestreosSustituidos().subscribe({
      next: (response: any) => {
        this.registros = response.data;
      },
      error: (error) => {},
    });
  }

  definirColumnas() {
    let nombresColumnas: Array<Columna> = [
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE DEL SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO DE AGUA',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 5,
        filtro: new Filter(),
      },
      { nombre: 'anio', etiqueta: 'AÑO', orden: 6, filtro: new Filter() },
    ];
  }

  cancelarEnvio() {
    this.formOpcionesSustitucion.reset();
    this.resetInputFile(this.inputExcelMonitoreos);
  }
}
