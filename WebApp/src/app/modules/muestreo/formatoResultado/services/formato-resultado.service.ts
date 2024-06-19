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

  exportarResultadosExcel(muestreos: Array<any> = [], esAdmin: boolean) {
    var tipoExcel = 'resultado';
    return this.http.post(
      environment.apiUrl +
        '/Resultados/ExportarExcelResulatdosSECAIA?tipoExcel=' +
        tipoExcel +
        '&admin=' +
        esAdmin,
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
        estatus: estatusMuestreo.Cargado,
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
        estatus: estatusMuestreo.Cargado,
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

  getDistinctValuesParameterByFilter(
    column: string,
    filter: string
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        usuario: this.authService.getUser().usuarioId,
        parametro: column,
        estatus: estatusMuestreo.Cargado,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/resultados/GetDistinctValuesParametro',
      { params }
    );
  }
}
