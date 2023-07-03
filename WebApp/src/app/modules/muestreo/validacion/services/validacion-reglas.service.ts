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

  exportarResultadosAcumuladosExcel(muestreos: Array<any> = []) { 
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelValidaciones', muestreos, { responseType: 'blob' } );
  }
  exportExcelResultadosaValidar(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelResultadosaValidar', muestreos, { responseType: 'blob' });
  }
  exportExcelResultadosValidados(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelResultadosValidados', muestreos, { responseType: 'blob' });
  }
  exportExcelResumenResultados(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelResumenResultados', muestreos, { responseType: 'blob' });
  }  
  getResultadosAcumuladosParametros(estatusId: number) {
    let params = new HttpParams({
      fromObject: { estatusId: estatusId},
    });
    return this.http.get(environment.apiUrl + '/Resultados/ResultadosAcumuladosParametros', { params });
  }
  getResultadosporMonitoreo(anios: Array<number>, numeroEntrega: Array<number>, estatusId: number) {
    let params = new HttpParams({
      fromObject: { anios: anios, numeroEntrega: numeroEntrega, estatusId: estatusId },
    });
    return this.http.get(environment.apiUrl + '/Resultados/ResultadosporMuestreo', { params });
  }

  enviarMuestreoaValidar(estatusId: number, muestreos: Array<number>) {  
    let datos = { estatusId: estatusId, muestreos: muestreos };
    return this.http.put(environment.apiUrl + '/Muestreos/cambioEstatusMuestreos', datos);
  }  
}


