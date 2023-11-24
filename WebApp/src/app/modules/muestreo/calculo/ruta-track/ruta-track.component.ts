import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from '../../evidencias/pages/evidencias-informacion/informacion-evidencia';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { FileService } from 'src/app/shared/services/file.service';
import { tipoEvidencia } from 'src/app/shared/enums/tipoEvidencia';


@Component({
  selector: 'app-ruta-track',
  templateUrl: './ruta-track.component.html',
  styleUrls: ['./ruta-track.component.css']
})
export class RutaTrackComponent implements OnInit {
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];
  tiposEvidenciasPuntos: Array<number> = [tipoEvidencia.FotoCaudal, tipoEvidencia.FotoMuestra, tipoEvidencia.FormatoCaudal, tipoEvidencia.Track, tipoEvidencia.FotoMuestreo];

  constructor(private evidenciasService: EvidenciasService) { }

  ngOnInit(): void {
    this.getInformacionEvidencias();
  }
  getInformacionEvidencias() {
    this.evidenciasService.getInformacionEvidencias(true).subscribe({
      next: (response: any) => {
        this.informacionEvidencias = response.data;
      },
      error: (response: any) => { },
    });
  }

  onPreviewDownloadArchivoClick(nombreArchivo: string, tipoArchivo: number) {
    this.evidenciasService
      .descargarArchivo(nombreArchivo)
      .subscribe((data: Blob) => {
        FileService.download(data, nombreArchivo);
      });
  }

  onVerMapaClick(muestreo: string) {
    var datos = this.informacionEvidencias.filter(x => x.muestreo == muestreo && this.tiposEvidenciasPuntos.includes(x.tipoEvidenciaMuestreo));

    console.log(datos);
  }

}
