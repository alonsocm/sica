import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EvidenciasService {
  constructor(private http: HttpClient) {}

  cargarEvidencias(archivos: FileList): Observable<any> {
    const formData = new FormData();
    Array.from(archivos).forEach((archivo) => {
      formData.append('archivos', archivo);
    });

    return this.http.post(
      environment.apiUrl + '/EvidenciasMuestreos',
      formData
    );
  }

  obtenerMuestreos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos');
  }

  descargarArchivo(nombreArchivo: string) {
    const params = new HttpParams({
      fromObject: { nombreArchivo: nombreArchivo },
    });
    return this.http.get(environment.apiUrl + '/EvidenciasMuestreos', {
      params,
      responseType: 'blob',
    });
  }

  descargarArchivos(muestreos: Array<number>) {
    const params = new HttpParams({
      fromObject: { muestreos: muestreos },
    });
    return this.http.get(environment.apiUrl + '/EvidenciasMuestreos/Archivos', {
      params,
      responseType: 'blob',
    });
  }

  getInformacionEvidencias(isTrack: boolean): Observable<Object> {
    const params = new HttpParams({
      fromObject: { isTrack: isTrack },
    });
    return this.http.get(
      environment.apiUrl + '/EvidenciasMuestreos/InformacionEvidencias', { params }
    );
  }
}
