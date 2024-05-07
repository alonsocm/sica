import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { Muestreo } from '../../../interfaces/Muestreo.interface';
import { BaseService } from '../../services/base.service';
import { MuestreoService } from '../../../modules/muestreo/liberacion/services/muestreo.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Column } from '../../../interfaces/filter/column';
import { GeneralService } from '../../services/general.service';

@Component({
  selector: 'app-cabeceros-historial',
  templateUrl: './cabeceros-historial.component.html',
  styleUrls: ['./cabeceros-historial.component.css'],
})
export class CabecerosHistorialComponent extends BaseService implements OnInit {
  //filtrosCabeceroFoco: Array<any> = []; //Listado de cabeceros utilizado en el drop para redirigir al usuario al cabecero seleccionado

  muestreos: Array<Muestreo> = []; //Contiene los registros consultados a la API*/
  muestreosSeleccionados: Array<Muestreo> = []; //Contiene los registros que se van seleccionando*/

  @Output() mostrandocabecero = new EventEmitter<boolean>();
  @Output() mostrandoesfiltrofoco = new EventEmitter<string>();
  @Output() mostrandoExisteFiltrado = new EventEmitter<boolean>();

  @Output() mostrandoCadena = new EventEmitter<string>();
  
  

  @Input() esHistorialValor: boolean = false;
  @Input() columnsValor: Array<Column> = [];
  

  constructor(private generalService: GeneralService, private muestreoService: MuestreoService) {
    super();    

    muestreoService.filtrosCabeceros.subscribe(
      (cabeceros) => {
        this.filtrosCabeceroFoco = cabeceros;
        
      }
    );

  }

  ngOnInit(): void { /*console.log(this.valor)*/; }

  public consultarMonitoreos(): void {
    console.log("entra a consulta monitoreos");
    this.mostrandoCadena.emit(this.cadena);
    return this.generalService.consultarMonitoreos();     
  }



  //SI SE OCUPA
  seleccionCabecero(val: string = '') {
    let header = document.getElementById(val) as HTMLElement;
    header.scrollIntoView({ behavior: 'smooth', block: 'center' });
    this.cabeceroSeleccionado = true;
    this.esfiltrofoco = val.toUpperCase(); 
    this.mostrandocabecero.emit(this.cabeceroSeleccionado);
    this.mostrandoesfiltrofoco.emit(this.esfiltrofoco);

  }

  //SI SE OCUPA
  onCabeceroFoco(val: string = '') {
    this.cabeceroSeleccionado = false;
    this.esfiltrofoco = val.toUpperCase();  

    this.mostrandocabecero.emit(this.cabeceroSeleccionado);
    this.mostrandoesfiltrofoco.emit(this.esfiltrofoco);


    //this.filtrosCabeceroFoco = this.filtrosCabeceroFoco.filter(
    //  (f) => f.toLowerCase.indexOf(val.toLowerCase()) !== -1
    //);
  }




}
