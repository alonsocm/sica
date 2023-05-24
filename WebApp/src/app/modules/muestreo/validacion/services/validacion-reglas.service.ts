import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ValidacionReglasService {

  constructor(private http: HttpClient) { }

  obtenerMuestreos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos/AniosConRegistro');
  }

  obtenerResultadosValidadosPorReglas(anios: Array<number>, numeroEntrega:Array<number>){
    let params = new HttpParams({
      fromObject: { anios: anios, numeroEntrega:  numeroEntrega},
    });
    return this.http.get(environment.apiUrl + '/Resultados/ValidarResultadosPorReglas', { params });
  }

  exportarResumenResultadosValidadosPorReglas(anios: Array<number>, numeroEntrega:Array<number>){
    let params = new HttpParams({
      fromObject: { anios: anios, numeroEntrega:  numeroEntrega},
    });
    return this.http.get(environment.apiUrl + '/Resultados/ExportarResumenValidacion', { params, responseType: 'blob' });
  }
}
