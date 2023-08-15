import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { LimitesService } from '../limites.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { FileService } from 'src/app/shared/services/file.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-emergencia',
  templateUrl: './emergencia.component.html',
  styleUrls: ['./emergencia.component.css'],
})
export class EmergenciaComponent extends BaseService implements OnInit {
  archivo: any;  
  registros: Array<any> = [];
  contratoSeleccionado : string = "Seleccionar";
  formOpcionesSustitucion = new FormGroup({
    origenLimites: new FormControl('', Validators.required),
    periodo: new FormControl('', Validators.required),
    excelLimites: new FormControl(),
    archivo: new FormControl(),
  });
  @ViewChild('inputExcelMonitoreosEmergencia') inputExcelMonitoreos: ElementRef = {} as ElementRef;

  constructor(private limitesService: LimitesService) {
    super();
  }

  ngOnInit(): void {
    // this.obtenerMuestreosSustituidos();
  }

  onSubmit(){}

  validarArchivo(){}

  onFileChange(event: any){}

  cargarArchivoEmergencias(event: Event) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    this.procesarArchivoEmergencias();
  }

  procesarArchivoEmergencias(reemplazar?: string){
    if (this.archivo) {
      this.loading = !this.loading;

      this.limitesService
        .cargaMuestreosEmergencia(this.archivo[0],  this.contratoSeleccionado, reemplazar)
        .subscribe({
          next: (response: any) => {
            if (response.data.correcto) {
              this.loading = false;
              this.mostrarMensaje(
                'Archivo procesado correctamente.',
                TIPO_MENSAJE.exito
              );
              this.resetInputFile(this.inputExcelMonitoreos);
            } else if(!response.Succeded) {
              this.loading = false;
              document.getElementById('btnMdlConfirmacionReemplazar')?.click();
              console.log(response);
            }
          },
          error: (error: any) => {
            this.loading = false;
            let archivoErrores = this.generarArchivoDeErrores(
              error.error.Errors
            );
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

  reemplazarResultadosPrevios(){
    this.formOpcionesSustitucion.reset();
    this.procesarArchivoEmergencias('true');
  }

  exportarResumen() {}

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

  cancelarEnvio(){
    this.formOpcionesSustitucion.reset()
    this.resetInputFile(this.inputExcelMonitoreos);
  }
}
