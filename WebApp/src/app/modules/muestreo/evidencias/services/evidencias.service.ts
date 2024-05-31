import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EvidenciasService {
  coordenada: Array<any> = [];
  private datocordenada = new BehaviorSubject(this.coordenada);
  public coordenadas = this.datocordenada.asObservable();
  constructor(private http: HttpClient) {}

  cargarEvidencias(archivos: FileList, reemplazar: boolean): Observable<any> {
    const formData = new FormData();
    Array.from(archivos).forEach((archivo) => {
      formData.append('archivos', archivo);
    });

    return this.http.post(
      environment.apiUrl + '/EvidenciasMuestreos?reemplazar=' + reemplazar,
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
      environment.apiUrl + '/EvidenciasMuestreos/InformacionEvidencias',
      { params }
    );
  }

  getPuntosPB_PM(claveMuestreo: string): Observable<Object> {
    const params = new HttpParams({
      fromObject: { claveMuestreo: claveMuestreo },
    });
    return this.http.get(
      environment.apiUrl + '/Muestreos/obtenerPuntosPorMuestreo',
      { params }
    );
  }
  updateCoordenadas(coordenadas: any) {
    this.datocordenada.next(coordenadas);
  }

  getPosition(): Promise<any> {
    return new Promise((resolve, reject) => {
      navigator.geolocation.getCurrentPosition(
        (resp) => {
          resolve({ lng: resp.coords.longitude, lat: resp.coords.latitude });
        },
        (err) => {
          reject(err);
        }
      );
    });
  }

  deleteEvidencias(muestreos: Array<string>): Observable<any> {
    const options = { body: muestreos };
    return this.http.delete(
      environment.apiUrl + '/evidenciasMuestreos',
      options
    );
  }

  deleteEvidenciasByFilter(filter: string): Observable<Object> {
    return this.http.delete(
      environment.apiUrl +
        '/evidenciasMuestreos/deletebyfilter?filter=' +
        filter
    );
  }
}
