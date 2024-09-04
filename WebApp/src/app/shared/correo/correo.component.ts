import { core } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CorreoModel } from '../models/correo-model';
import { correoService } from '../services/correo.service';

@Component({
  selector: 'app-correo',
  templateUrl: './correo.component.html',
  styleUrls: ['./correo.component.css']
})
export class CorreoComponent implements OnInit {
  @Input() correo: CorreoModel = {
    destinatarios: '',
    copias: '',
    asunto: '',
    cuerpo: '',
    archivos: []
  };
  @Output() changed = new EventEmitter<boolean>();
  extensiones: Array<string> = [];
  nombre: string = '';
  constructor(private correoService: correoService) { }

  ngOnInit(): void {
    console.log("modal correo");
    console.log(this.correo);

  }

  enviar() { }

  btnClickAceptar() {
    this.changed.emit(true);
    this.correoService.correoSeleccionados = this.correo;
    

  }


}
