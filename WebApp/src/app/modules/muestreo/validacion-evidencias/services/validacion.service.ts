import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';

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
    return this.http.get(environment.apiUrl + '/ObtenerDatosGenerales');
  }


}
