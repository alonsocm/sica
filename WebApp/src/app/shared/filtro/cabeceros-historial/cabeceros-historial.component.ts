import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Input() headerList: Array<{ name: string; label: string }> = [];

  constructor() {
    super();
  }

  ngOnInit(): void {}

  seleccionCabecero(val: { name: string; label: string }) {
    let header = document.getElementById(val.name) as HTMLElement;
    header.scrollIntoView({ behavior: 'smooth', block: 'center' });
    this.cabeceroSeleccionado = true;
    this.esfiltrofoco = val.name;
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
