
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
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
    text: '',
    id:'',
  };
  @Output() changed = new EventEmitter<boolean>();

  constructor() { super(); }

  ngOnInit(): void {
  }
  btnClickAceptar() {     
    this.changed.emit(true);

  }
}
