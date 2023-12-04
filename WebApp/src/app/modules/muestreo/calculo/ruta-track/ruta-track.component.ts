import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { InformacionEvidencia } from '../../evidencias/pages/evidencias-informacion/informacion-evidencia';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { FileService } from 'src/app/shared/services/file.service';
import { tipoEvidencia } from 'src/app/shared/enums/tipoEvidencia';
import { PuntosEvidenciaMuestreo } from 'src/app/interfaces/puntosEvidenciaMuestreo.interface';
import {
  PuntosMuestreo,
  PuntosMuestreoNombre,
} from 'src/app/shared/enums/puntosMuestreo';
import { AuthService } from '../../../login/services/auth.service';

@Component({
  selector: 'app-ruta-track',
  templateUrl: './ruta-track.component.html',
  styleUrls: ['./ruta-track.component.css'],
})
export class RutaTrackComponent implements OnInit {
  page: number = 1;
  informacionEvidencias: Array<InformacionEvidencia> = [];
  informacionEvidenciasTrack: Array<InformacionEvidencia> = [];
  tiposEvidenciasPuntos: Array<number> = [
    tipoEvidencia.FotoCaudal,
    tipoEvidencia.FotoMuestra,
    tipoEvidencia.Track,
    tipoEvidencia.FotoMuestreo,
  ];
  puntosMuestreo: Array<PuntosEvidenciaMuestreo> = [];

  constructor(
    private evidenciasService: EvidenciasService,
    private authService: AuthService,
    private router: Router
  ) { localStorage.setItem('claveMuestreoCalculo', ''); }
  ngOnInit(): void {
    this.getInformacionEvidencias();
  }
  getInformacionEvidencias() {
    this.evidenciasService.getInformacionEvidencias(true).subscribe({
      next: (response: any) => {
        this.informacionEvidencias = response.data;
        this.informacionEvidenciasTrack = this.informacionEvidencias.filter(x => x.tipoEvidenciaMuestreo == tipoEvidencia.Track);
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
  onVerMapaClick(muestreo: string) {
    this.getPuntos_PR_PM(muestreo);
  }
  getPuntos_PR_PM(claveMuestreo: string) {
    localStorage.setItem('claveMuestreoCalculo', claveMuestreo );
    this.evidenciasService.getPuntosPB_PM(claveMuestreo).subscribe({
      next: (response: any) => {
        this.puntosMuestreo = response.data;
        let datos = this.informacionEvidencias.filter(
          (x) =>
            x.muestreo == claveMuestreo &&
            this.tiposEvidenciasPuntos.includes(x.tipoEvidenciaMuestreo)
        );
        for (var i = 0; i < datos.length; i++) {
          let nombrepunto = '';
          let puntoMuestreo = '';
          switch (datos[i].tipoEvidenciaMuestreo) {
            case tipoEvidencia.FotoCaudal:
              nombrepunto = PuntosMuestreoNombre.FotodeAforo_FA;
              puntoMuestreo = PuntosMuestreo.FotodeAforo_FA;
              break;
            case tipoEvidencia.FotoMuestra:
              nombrepunto = PuntosMuestreoNombre.FotodeMuestras_FS;
              puntoMuestreo = PuntosMuestreo.FotodeMuestras_FS;
              break;
            case tipoEvidencia.Track:
              nombrepunto = PuntosMuestreoNombre.PuntoCercanoalTrack_TR;
              puntoMuestreo = PuntosMuestreo.PuntoCercanoalTrack_TR;
              break;
            case tipoEvidencia.FotoMuestreo:
              nombrepunto = PuntosMuestreoNombre.FotodeMuestreo_FM;
              puntoMuestreo = PuntosMuestreo.FotodeMuestreo_FM;
              break;
            default:
          }

          let punto: PuntosEvidenciaMuestreo = {
            claveMuestreo: claveMuestreo,
            latitud: Number(datos[i].latitud),
            longitud: Number(datos[i].longitud),
            nombrePunto: nombrepunto,
            punto: puntoMuestreo,
          };
          this.puntosMuestreo.push(punto);
          this.evidenciasService.updateCoordenadas(this.puntosMuestreo);
          this.router.navigate(['/map-muestreo']);
        }
      },
      error: (response: any) => {},
    });
  }
}
