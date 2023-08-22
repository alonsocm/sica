import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { LimitesService } from '../limites.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { FileService } from 'src/app/shared/services/file.service';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { AuthService } from 'src/app/modules/login/services/auth.service';

@Component({
  selector: 'app-maximo-comun',
  templateUrl: './maximo-comun.component.html',
  styleUrls: ['./maximo-comun.component.css'],
})
export class MaximoComunComponent extends BaseService implements OnInit {
  constructor(
    private limitesService: LimitesService,
    private authService: AuthService
  ) {
    super();
  }

  muestreosFiltrados: Array<MuestreoSustitucion> = [];
  muestreos: Array<MuestreoSustitucion> = [];

  formOpcionesSustitucion = new FormGroup({
    origenLimites: new FormControl('', Validators.required),
    periodo: new FormControl('', Validators.required),
    excelLimites: new FormControl(),
    archivo: new FormControl(),
  });

  tipoSeleccionado: string = '';
  periodoSeleccionado: string = '';
  archivoLimites: File = {} as File;
  archivoRequerido: boolean = false;
  registros: Array<any> = [];
  existeSustitucion: boolean = false;

  ngOnInit(): void {
    this.definirColumnas();
    this.obtenerMuestreosSustituidos();
  }

  onSubmit() {
    document.getElementById('btnMdlConfirmacion')?.click();
    this.loading = true;
    this.limitesService
      .validarSustitucionPrevia(
        Number(this.formOpcionesSustitucion.controls.periodo.value)
      )
      .subscribe({
        next: (response: any) => {
          this.loading = false;
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
      periodo: Number(this.formOpcionesSustitucion.controls.periodo.value),
      usuario: this.authService.getUser().usuarioId,
    };

    this.loading = true;

    this.limitesService.sustituirLimites(configSustitucion).subscribe({
      next: (response: any) => {
        this.formOpcionesSustitucion.reset();
        this.loading = false;
        if (response.data === true) {
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

  obtenerMuestreosSustituidos(): void {
    this.loading = true;
    this.limitesService.obtenerMuestreosSustituidos().subscribe({
      next: (response: any) => {
        this.loading = false;
        this.muestreos = response.data;
        this.muestreosFiltrados = response.data;
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => {
        this.loading = false;
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

  exportarResumen() {
    this.loading = true;
    this.limitesService.exportarResumenExcel().subscribe({
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

  filtrar() {
    this.muestreosFiltrados = this.muestreos;
    this.columnas.forEach((columna) => {
      this.muestreosFiltrados = this.muestreosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });

    this.establecerValoresFiltrosTabla();
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  definirColumnas() {
    let nombresColumnas: Array<Columna> = [
      {
        nombre: 'noEntrega',
        etiqueta: 'No. Entrega',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoSitio',
        etiqueta: 'TIPO DE SITIO',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE DEL SITIO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'cuerpoAgua',
        etiqueta: 'CUERPO DE AGUA',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 10,
        filtro: new Filter(),
      },
    ];
    this.columnas = nombresColumnas;
  }
  
  definirColumna(ordenColuma: number){
    switch (ordenColuma) {
      case 1:
        return 'col-1'
        break;
        case 2:
          return 'col-2'
          break;
      default:
        break;
    }
    return 'primera-col'
  }
}

interface MuestreoSustitucion {
  noEntrega: string;
  tipoSitio: string;
  claveSitio: string;
  nombreSitio: string;
  claveMonitoreo: string;
  fechaRealizacion: string;
  laboratorio: string;
  cuerpoAgua: string;
  tipoCuerpoAguaOriginal: string;
  tipoCuerpoAgua: string;
  resultados: Array<any>;
}
