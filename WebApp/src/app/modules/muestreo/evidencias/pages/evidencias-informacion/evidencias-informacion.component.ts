import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from './informacion-evidencia';
import { EvidenciasService } from '../../services/evidencias.service';
import { SupervisionService } from '../../../supervision/supervision.service';
import { FileService } from 'src/app/shared/services/file.service';

@Component({
  selector: 'app-evidencias-informacion',
  templateUrl: './evidencias-informacion.component.html',
  styleUrls: ['./evidencias-informacion.component.css'],
})
export class EvidenciasInformacionComponent implements OnInit {
  imgSrc = '';
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];
  constructor(private evidenciasService: EvidenciasService) {}

  ngOnInit(): void {
    this.getInformacionEvidencias();
  }

  getInformacionEvidencias() {
    this.evidenciasService.getInformacionEvidencias(false).subscribe({
      next: (response: any) => {
        this.informacionEvidencias = response.data;
      },
      error: (response: any) => {},
    });
  }

  onPreviewDownloadArchivoClick(nombreArchivo: string, tipoArchivo: number) {
    this.evidenciasService
      .descargarArchivo(nombreArchivo)
      .subscribe((data: Blob) => {
        FileService.download(data, nombreArchivo);
      });
  }
}
