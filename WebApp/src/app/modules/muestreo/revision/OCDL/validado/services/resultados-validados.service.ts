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
    return this.http.get(
      environment.apiUrl + '/resultados/ResultadosValidadosPorOCDL',
      { params }
    );
  }

  getDistinctOCDL(selector: string, filters: string = ''): Observable<any> {
    const params = new HttpParams({
      fromObject: {
        selector: selector,
        filters: filters,
      },
    });
    return this.http.get(
      environment.apiUrl + '/resultados/GetDistinctResultadosValidados',
      { params }
    );
  }

  exportarResultadosValidados(
    registros: Array<any> = [],
    filtro: string
  ) {
    return this.http.post(
      environment.apiUrl +
        '/Resultados/ExportarResultadosValidadosPorOCDL?' +
        '&filter=' +
        filtro,
      registros,
      { responseType: 'blob' }
    );
  }

  enviarResultadosValidadosPorOCDL(
    resultados: Array<any> = [],
    filtro: string
  ){
    return this.http.put(
      environment.apiUrl + '/resultados/EnviarResultadosValidadosPorOCDL?'+
      '&filter=' +
        filtro,
      resultados , { responseType: 'blob' }
    );
  }

  regresarResultadosValidadosPorOCDL(
    resultados: Array<any> = [],
    filtro: string
  ){
    return this.http.put(
      environment.apiUrl + '/resultados/regresarResultadosValidadosPorOCDL?'+
      '&filter=' +
        filtro,
      resultados , { responseType: 'blob' }
    );
  }
}
