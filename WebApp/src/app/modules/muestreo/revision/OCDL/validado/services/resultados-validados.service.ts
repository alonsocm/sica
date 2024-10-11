import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ResultadosValidadosService {
  constructor(private http: HttpClient) {}

  getResultadosValidadosPorOCDL(
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<any> {
    const userId = localStorage.getItem('idUsuario') || '';
    let params = new HttpParams({
      fromObject: {
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(environment.apiUrl+'/resultados/ResultadosValidadosPorOCDL', { params });
  }

  getDistinctOCDL(
    column: string,
    filter: string,
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        column: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/resultados/GetDistinctValuesFromColumnOCDL',
      { params }
    );
  }
  exportarResultadosValidados(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/ExportarResultadosValidados', muestreos, { responseType: 'blob' });
  }
}
