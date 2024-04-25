import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../login/services/auth.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';
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

  getCuerpoAgua() {
    return this.http.get<any>(
      environment.apiUrl + '/CuerpoDeAgua/TipoHomologado'
    );
  }
}
