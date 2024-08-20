import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../login/services/auth.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class FormatoResultadoService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  exportarRegistrosExcel(muestreos: Array<number> = [], filter = '') {
    return this.http.post(
      environment.apiUrl +
        '/Resultados/ExportConsultaRegistroOriginal?usuario=' +
        this.authService.getUser().usuarioId +
        '&filter=' +
        filter,
      muestreos,
      { responseType: 'blob' }
    );
  }

  getParametros(muestreos: Array<any> = []) {
    return this.http.get<any>(environment.apiUrl + '/ParametrosGrupo');
  }

  getMuestreosParametros(tipoCuerpo: number, page: number, pageSize: number) {
    const params = new HttpParams({
      fromObject: {
        usuario: this.authService.getUser().usuarioId,
        tipoCuerpoAgua: tipoCuerpo,
        estatus: estatusMuestreo.CargaResultados,
        page,
        pageSize,
      },
    });
    return this.http.get<any>(
      environment.apiUrl + '/resultados/ParametrosMuestreo',
      { params }
    );
  }

  getMuestreosParametrosPaginados(
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        usuario: this.authService.getUser().usuarioId,
        page,
        pageSize,
        filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get<any>(
      environment.apiUrl + '/resultados/ParametrosMuestreo',
      { params }
    );
  }

  getCuerpoAgua() {
    return this.http.get<any>(
      environment.apiUrl + '/CuerpoDeAgua/TipoHomologado'
    );
  }

  getDistinctValuesFromColumn(
    column: string,
    filter: string
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        usuario: this.authService.getUser().usuarioId,
        column: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/resultados/GetDistinctValuesFromColumn',
      { params }
    );
  }

  getDistinctValuesParameterByFilter(
    column: string,
    filter: string
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        usuario: this.authService.getUser().usuarioId,
        parametro: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/resultados/GetDistinctValuesParametro',
      { params }
    );
  }
}
