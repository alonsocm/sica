import { Component, OnInit } from '@angular/core';
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

  tipoSeleccionado: string = '';
  periodoSeleccionado: string = '';
  archivoLimites: File = {} as File;

  ngOnInit(): void {}
  
  mostrarValoresSeleccionados(){
    console.log(this.archivoLimites.name);
    
  }
}
