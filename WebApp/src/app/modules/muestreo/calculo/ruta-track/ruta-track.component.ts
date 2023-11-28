import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from '../../evidencias/pages/evidencias-informacion/informacion-evidencia';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { FileService } from 'src/app/shared/services/file.service';
import { tipoEvidencia } from 'src/app/shared/enums/tipoEvidencia';
import { PuntosEvidenciaMuestreo } from 'src/app/interfaces/puntosEvidenciaMuestreo.interface';
import { puntosMuestreo, puntosMuestreoNombre } from 'src/app/shared/enums/puntosMuestreo';
import { AuthService } from '../../../login/services/auth.service';

@Component({
  selector: 'app-ruta-track',
  templateUrl: './ruta-track.component.html',
  styleUrls: ['./ruta-track.component.css']
})
export class RutaTrackComponent implements OnInit {
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];
  tiposEvidenciasPuntos: Array<number> = [tipoEvidencia.FotoCaudal, tipoEvidencia.FotoMuestra, tipoEvidencia.Track, tipoEvidencia.FotoMuestreo];
  puntosMuestreo: Array<PuntosEvidenciaMuestreo> = [];

  constructor(
    private evidenciasService: EvidenciasService,
    private authService: AuthService,
    private router: Router) { }
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
    this.getPuntos_PR_PM(muestreo);
  }
  getPuntos_PR_PM(claveMuestreo: string) {
    this.evidenciasService.getPuntosPB_PM(claveMuestreo).subscribe({
      next: (response: any) => {
        this.puntosMuestreo = response.data;
        let datos = this.informacionEvidencias.filter(x => x.muestreo == claveMuestreo && this.tiposEvidenciasPuntos.includes(x.tipoEvidenciaMuestreo));
        for (var i = 0; i < datos.length; i++) {

          let nombrepunto = '';
          let puntoMuestreo = '';
          switch (datos[i].tipoEvidenciaMuestreo) {

            case tipoEvidencia.FotoCaudal:
              nombrepunto = puntosMuestreoNombre.FotodeAforo_FA;
              puntoMuestreo = puntosMuestreo.FotodeAforo_FA;
              break;
            case tipoEvidencia.FotoMuestra:
              nombrepunto = puntosMuestreoNombre.FotodeMuestras_FS;
              puntoMuestreo = puntosMuestreo.FotodeMuestras_FS;
              break;
            case tipoEvidencia.Track:
              nombrepunto = puntosMuestreoNombre.PuntoCercanoalTrack_TR;
              puntoMuestreo = puntosMuestreo.PuntoCercanoalTrack_TR;
              break;
            case tipoEvidencia.FotoMuestreo:
              nombrepunto = puntosMuestreoNombre.FotodeMuestreo_FM;
              puntoMuestreo = puntosMuestreo.FotodeMuestreo_FM;
              break;
            default:
          }

          let punto: PuntosEvidenciaMuestreo = {
            claveMuestreo: claveMuestreo,
            latitud: Number(datos[i].latitud),
            longitud: Number(datos[i].longitud),
            nombrePunto: nombrepunto,
            punto: puntoMuestreo
          };
          this.puntosMuestreo.push(punto);
          this.evidenciasService.updateCoordenadas(this.puntosMuestreo);
          this.router.navigate(['/map-muestreo']);
        }
      },
      error: (response: any) => { },
    });
  }
}
