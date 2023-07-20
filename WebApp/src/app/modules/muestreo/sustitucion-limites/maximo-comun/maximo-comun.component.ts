import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { LimitesService } from '../limites.service';

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
    archivo: new FormControl()    
  }); 

  tipoSeleccionado: string = '';
  periodoSeleccionado: string = '';
  archivoLimites: File = {} as File;
  archivoRequerido: boolean = false;

  ngOnInit(): void {}
  
  onSubmit(){
    let configSustitucion = {
      archivo: this.formOpcionesSustitucion.controls.archivo.value,
      origenLimites: this.formOpcionesSustitucion.controls.origenLimites.value,
      periodo: this.formOpcionesSustitucion.controls.periodo.value
    };

    console.table(configSustitucion); 

    this.limitesService.sustituirLimites(configSustitucion).subscribe({
      next: (response: any) => {        
      },
      error: (error) => {},
    });;
  }

  validarArchivo(){    
    if (this.formOpcionesSustitucion.controls.origenLimites.value == 'tablaTemporal') {
      this.formOpcionesSustitucion.controls.excelLimites.setValue('');
      this.formOpcionesSustitucion.controls.excelLimites.setValidators(Validators.required);
    } else {
      this.formOpcionesSustitucion.controls.excelLimites.setValue('');
      this.formOpcionesSustitucion.controls.excelLimites.setValidators(null);
    }

    this.formOpcionesSustitucion.controls.excelLimites.updateValueAndValidity();
  }
  
  onFileChange(event: any){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formOpcionesSustitucion.patchValue({
        archivo: file
      });
    }
  }
}
