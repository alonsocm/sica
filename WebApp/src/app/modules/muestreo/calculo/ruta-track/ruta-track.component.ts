
import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from '../../evidencias/pages/evidencias-informacion/informacion-evidencia';

@Component({
  selector: 'app-ruta-track',
  templateUrl: './ruta-track.component.html',
  styleUrls: ['./ruta-track.component.css']
})
export class RutaTrackComponent implements OnInit {
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];

  constructor() { }

  ngOnInit(): void {
  }


}
