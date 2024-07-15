import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { acumuladosMuestreo } from '../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';

@Injectable({
  providedIn: 'root',
})
export class ValidacionReglasService {
  constructor(private http: HttpClient) {}

  obtenerMuestreos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos/AniosConRegistro');
  }
  obtenerNumerosEntrega(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos/NumerosEntrega');
  }

  obtenerResultadosValidadosPorReglas(Muestreos: Array<number>) {
    let params = new HttpParams({
      fromObject: { Muestreos: Muestreos },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ValidarResultadosPorReglas',
      { params }
    );
  }

  exportarResumenResultadosValidadosPorReglas(
    anios: Array<number>,
    numeroEntrega: Array<number>
  ) {
    let params = new HttpParams({
      fromObject: { anios: anios, numeroEntrega: numeroEntrega },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ExportarResumenValidacion',
      { params, responseType: 'blob' }
    );
  }

  exportarResultadosAcumuladosExcel(muestreos: Array<any> = []) {
    return this.http.post(
      environment.apiUrl + '/Resultados/exportExcelValidaciones',
      muestreos,
      { responseType: 'blob' }
    );
  }

  exportExcelResultadosaValidar(muestreos: Array<any> = []) {
    return this.http.post(
      environment.apiUrl + '/Resultados/exportExcelResultadosaValidar',
      muestreos,
      { responseType: 'blob' }
    );
  }

  exportExcelResultadosValidados(muestreos: Array<any> = []) {
    return this.http.post(
      environment.apiUrl + '/Resultados/exportExcelResultadosValidados',
      muestreos,
      { responseType: 'blob' }
    );
  }

  exportExcelResumenResultados(
    estatus: number,
    registros: Array<any> = [],
    filtro: string
  ) {
    return this.http.post(
      environment.apiUrl +
        '/Resultados/exportExcelResumenResultados?' +
        'estatus=' +
        estatus +
        '&filter=' +
        filtro,
      registros,
      { responseType: 'blob' }
    );
  }

  getResultadosAcumuladosParametros(estatusId: number) {
    let params = new HttpParams({
      fromObject: { estatusId: estatusId },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ResultadosAcumuladosParametros',
      { params }
    );
  }

  getResultadosAcumuladosParametrosPaginados(
    estatusId: number,
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    let params = new HttpParams({
      fromObject: {
        estatusId: estatusId,
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ResultadosAcumuladosParametros',
      { params }
    );
  }

  getResultadosporMonitoreoPaginados(
    estatusId: number,
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    let params = new HttpParams({
      fromObject: {
        estatusId: estatusId,
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ResultadosporMuestreo',
      { params }
    );
  }

  enviarMuestreoaValidar(estatusId: number, muestreos: Array<number>) {
    let datos = { estatusId: estatusId, muestreos: muestreos };
    return this.http.put(
      environment.apiUrl + '/Muestreos/cambioEstatusMuestreos',
      datos
    );
  }

  deleteResultadosByFilter(estatusId: number, filter: string): Observable<any> {
    let params = new HttpParams({
      fromObject: {
        estatusId: estatusId,
        Filters: filter,
      },
    });
    return this.http.delete(
      environment.apiUrl + '/Resultados/DeleteAllResultados',
      { params }
    );
  }

  deleteResultadosById(resultados: Array<number>): Observable<any> {
    const options = { body: resultados };
    return this.http.delete(environment.apiUrl + '/Resultados', options);
  }

  deleteResultadosByMuestreoId(lstMuestreosId: Array<number>): Observable<any> {
    const options = { body: lstMuestreosId };
    return this.http.delete(
      environment.apiUrl + '/Resultados/DeleteByMuestreoId',
      options
    );
  }

  getDistinctValuesFromColumn(
    column: string,
    filter: string
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        estatusId: estatusMuestreo.ValidadoPorReglas,
        column: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl +
        '/resultados/GetColumnValuesResultadosAcumuladosParametros',
      { params }
    );
  }

  cargarArchivo(archivo: File): Observable<any> {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);

    return this.http.post(
      environment.apiUrl +
        '/Resultados/CargaObservacionesResumenValidacionReglas',
      formData
    );
  }

  liberar(muestreos: Array<number> = [], filter = '') {
    return this.http.post(
      environment.apiUrl + '/Resultados/Liberar?' + 'filter=' + filter,
      muestreos
    );
  }

  enviarIncidencias(muestreos: Array<number> = [], filter = '') {
    return this.http.post(
      environment.apiUrl +
        '/Resultados/EnviarIncidencias?' +
        'filter=' +
        filter,
      muestreos
    );
  }
}
