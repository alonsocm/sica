import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from './informacion-evidencia';
import { EvidenciasService } from '../../services/evidencias.service';

@Component({
  selector: 'app-evidencias-informacion',
  templateUrl: './evidencias-informacion.component.html',
  styleUrls: ['./evidencias-informacion.component.css'],
})
export class EvidenciasInformacionComponent implements OnInit {
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];
  constructor(private evidenciasService: EvidenciasService) {}

  ngOnInit(): void {
    this.getInformacionEvidencias();
  }

  getInformacionEvidencias() {
    this.evidenciasService.getInformacionEvidencias().subscribe({
      next: (response: any) => {
        this.informacionEvidencias = response.data;
      },
      error: (response: any) => {},
    });
  }
}
