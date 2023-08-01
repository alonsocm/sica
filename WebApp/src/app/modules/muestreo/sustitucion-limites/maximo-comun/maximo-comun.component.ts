import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { LimitesService } from '../limites.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { FileService } from 'src/app/shared/services/file.service';

@Component({
  selector: 'app-maximo-comun',
  templateUrl: './maximo-comun.component.html',
  styleUrls: ['./maximo-comun.component.css'],
})
export class MaximoComunComponent extends BaseService implements OnInit {
  constructor(private limitesService: LimitesService) {
    super();
  }

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

  ngOnInit(): void {
    this.obtenerMuestreosSustituidos();
  }

  obtenerMuestreosSustituidos() {
    this.limitesService.obtenerMuestreosSustituidos().subscribe({
      next: (response: any) => {
        this.registros = response.data;
        console.table(response.data);
      },
      error: (error) => {},
    });
  }

  onSubmit() {
    let configSustitucion = {
      archivo: this.formOpcionesSustitucion.controls.archivo.value,
      origenLimites: this.formOpcionesSustitucion.controls.origenLimites.value,
      periodo: this.formOpcionesSustitucion.controls.periodo.value,
    };

    document.getElementById('btnMdlConfirmacion')?.click();
    this.loading = true;
    this.limitesService.sustituirLimites(configSustitucion).subscribe({      
      next: (response: any) => {        
        this.loading = false;
        this.mostrarMensaje('Se ejecutó correctamente la sustitución de los límites máximos', TipoMensaje.Correcto)
      },
      error: (response) => {
        this.loading = false;
        this.mostrarMensaje('Ocurrió un error en el proceso de sustitución' + ' ' + response.error.Errors[0], TipoMensaje.Error)
      },
    });
  }

  validarArchivo() {
    if (
      this.formOpcionesSustitucion.controls.origenLimites.value ==
      'tablaTemporal'
    ) {
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

  exportarResumen(){
    this.loading = true;    
    this.limitesService
    .exportarResumenExcel()
    .subscribe({
      next: (response: any) => {
        this.loading = false;
        FileService.download(response, 'resultados.xlsx');
      },
      error: (response: any) => {
        this.loading = false
      this.mostrarMensaje(
        'No fue posible descargar la información',
        TipoMensaje.Error
        );
      this.hacerScroll();
    },
  });
  }
}
