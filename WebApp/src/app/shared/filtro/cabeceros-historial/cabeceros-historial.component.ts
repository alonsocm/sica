import { Component, EventEmitter, Input, OnInit, Output, } from '@angular/core';
import { BaseService } from '../../services/base.service';
import { MuestreoService } from '../../../modules/muestreo/liberacion/services/muestreo.service';


@Component({
  selector: 'app-cabeceros-historial',
  templateUrl: './cabeceros-historial.component.html',
  styleUrls: ['./cabeceros-historial.component.css'],
})
export class CabecerosHistorialComponent extends BaseService implements OnInit {
  
  @Output() mostrandocabecero = new EventEmitter<boolean>();
  @Output() mostrandoesfiltrofoco = new EventEmitter<string>();
  @Input() esHistorialValor: boolean = false;  

  constructor(private muestreoService: MuestreoService) {
    super();    

    this.muestreoService.filtrosCabeceros.subscribe(
      (cabeceros) => {
        this.filtrosCabeceroFoco = cabeceros;        
      }
    );

  }

  ngOnInit(): void {}
 
  seleccionCabecero(val: string = '') {
    let header = document.getElementById(val) as HTMLElement;
    header.scrollIntoView({ behavior: 'smooth', block: 'center' });
    this.cabeceroSeleccionado = true;
    this.esfiltrofoco = val.toUpperCase(); 
    this.mostrandocabecero.emit(this.cabeceroSeleccionado);
    this.mostrandoesfiltrofoco.emit(this.esfiltrofoco);
  }

  onCabeceroFoco(val: string = '') {
    this.cabeceroSeleccionado = false;
    this.esfiltrofoco = val.toUpperCase();
    this.mostrandocabecero.emit(this.cabeceroSeleccionado);
    this.mostrandoesfiltrofoco.emit(this.esfiltrofoco);
  }

}
