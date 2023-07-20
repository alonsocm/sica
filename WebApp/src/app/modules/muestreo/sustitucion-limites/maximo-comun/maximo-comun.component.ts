import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-maximo-comun',
  templateUrl: './maximo-comun.component.html',
  styleUrls: ['./maximo-comun.component.css'],
})
export class MaximoComunComponent extends BaseService implements OnInit {
  constructor() {
    super();
  }

  form = new FormGroup({
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
    console.table(this.form.value); 
  }

  validarArchivo(){    
    if (this.form.controls.origenLimites.value == 'tablaTemporal') {
      this.form.controls.excelLimites.setValue('');
      this.form.controls.excelLimites.setValidators(Validators.required);
    } else {
      this.form.controls.excelLimites.setValue('');
      this.form.controls.excelLimites.setValidators(null);
    }

    this.form.controls.excelLimites.updateValueAndValidity();
  }
  
  onFileChange(event: any){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.form.patchValue({
        archivo: file
      });
    }
  }
}
