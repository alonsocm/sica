import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { vwValidacionEvidencia } from '../../../../interfaces/validacionEvidencias/vwValidacionEvidencia.interface';

@Injectable({
  providedIn: 'root',
})
export class ValidacionService {

  constructor(private http: HttpClient) { }

  cargarArchivo(archivo: File): Observable<any> {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);       
    return this.http.post(environment.apiUrl + '/ValidacionEvidencias', formData);
  }

  obtenerDatosaValidar() {
    return this.http.get<any>(environment.apiUrl + '/ValidacionEvidencias');
  }

  validarMuestreo(muestreo: any, usuarioId: any): Observable<Object> {
    //const params = new HttpParams({
    //  fromObject: { muestreo: muestreo, usuarioId: usuarioId },
    //});
    return this.http.post(
      environment.apiUrl + '/ValidacionEvidencias/validarMuestreo?usuarioId=' + usuarioId, muestreo
    );
  }

  validarMuestreoLista(muestreo: Array<any>, usuarioId: any): Observable<Object> {
    //const params = new HttpParams({
    //  fromObject: { muestreo: muestreo, usuarioId: usuarioId },
    //});
    return this.http.post(
      environment.apiUrl + '/ValidacionEvidencias/validarMuestreoLista?usuarioId=' + usuarioId, muestreo
    );
  }

}
