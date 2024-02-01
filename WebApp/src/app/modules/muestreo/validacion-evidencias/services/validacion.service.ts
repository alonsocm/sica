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

  obtenerResultadosEvidencia() {
    return this.http.get<any>(environment.apiUrl + '/ValidacionEvidencias/obtenerResultadosEvidencia');
  }

  validarMuestreo(muestreo: any, usuarioId: any): Observable<Object> {
    return this.http.post(
      environment.apiUrl + '/ValidacionEvidencias/validarMuestreo?usuarioId=' + usuarioId, muestreo
    );
  }

  validarMuestreoLista(muestreo: Array<any>, usuarioId: any): Observable<Object> { 
    return this.http.post(
      environment.apiUrl + '/ValidacionEvidencias/validarMuestreoLista?usuarioId=' + usuarioId, muestreo
    );
  }

  obtenerMuestreosValidados(rechazo: boolean): Observable<Object> {
    const params = new HttpParams({
      fromObject: { rechazo: rechazo },
    });
    return this.http.get<any>(environment.apiUrl + '/ValidacionEvidencias/obtenerMuestreosAprobados', { params });
  }

  actualizarPorcentaje(muestreos: Array<any>=[]) {
    return this.http.put(environment.apiUrl + '/ValidacionEvidencias/actualizarPorcentaje', muestreos);
  }

  extraerEventualidades(muestreos: Array<any> = []) {

    return this.http.post(environment.apiUrl + '/ValidacionEvidencias/extraerEventualidades', muestreos, { responseType: 'blob' });
  }


  extraerMuestreosAprobados(muestreos: Array<any> = []) {

    return this.http.post(environment.apiUrl + '/ValidacionEvidencias/extraerMuestreosAprobados', muestreos, { responseType: 'blob' });
  }

  extraerMuestreosRechazados(muestreos: Array<any> = []) {

    return this.http.post(environment.apiUrl + '/ValidacionEvidencias/extraerMuestreosRechazados', muestreos, { responseType: 'blob' });
  }



}
