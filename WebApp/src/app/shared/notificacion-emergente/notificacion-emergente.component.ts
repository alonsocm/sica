
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { MuestreoService } from '../../modules/muestreo/liberacion/services/muestreo.service';
import { Notificacion } from '../models/notification-model';
import { BaseService } from '../services/base.service';

@Component({
  selector: 'app-notificacion-emergente',
  templateUrl: './notificacion-emergente.component.html',
  styleUrls: ['./notificacion-emergente.component.css']
})
export class NotificacionEmergenteComponent extends BaseService implements OnInit {
  @Input() notificacion: Notificacion = {
    title: '',
    text: ''
  };
  @Output() isEjecutar = new EventEmitter<boolean>();
  @Input() datosSeleccionados: Array<any> = [];


  constructor(private muestreoService: MuestreoService) { super(); }

  ngOnInit(): void {
  }
  btnClickAceptar() {
   
    this.isAceptarNotificacion = true; this.isEjecutar.emit(this.isAceptarNotificacion);
    this.muestreoService.muestreosSeleccionados = this.datosSeleccionados;
  }
}
