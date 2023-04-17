import { Injectable} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../../login/services/auth.service';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo'

@Injectable({
  providedIn: 'root'
})
export class ConsultaResultadoService {

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  
  getParametros() {  
    return  this.http.get<any>(environment.apiUrl + '/ParametrosGrupo');
  }
  getCuerpoAgua() {
    return this.http.get<any>(environment.apiUrl + '/CuerpoDeAgua/TipoHomologado');
  }
  getAniosConRegistro() {
    return this.http.get<any>(environment.apiUrl + '/Muestreos/AniosConRegistro');
  }

  getMuestreosParametros(tipoCuerpo : number, anio : number)  {
    const params = new HttpParams({
      fromObject: {  
        userId: this.authService.getUser().usuarioId,
        cuerpAId: tipoCuerpo,       
        estausId: estatusMuestreo.OriginalesAprobados,
        anio: anio,
      },
    });
    return this.http.get<any>(environment.apiUrl + '/resultados/ResultadosMuestreoParametrosTemp', {params} );   
  }
  
  exportarResultadosExcel(muestreos: Array<any> = [], esAdmin: boolean) {     
    const tipoExcel ='consulta';
    return this.http.post(environment.apiUrl + '/Resultados/ExportarExcelResulatdosSECAIA?tipoExcel='+tipoExcel+'&admin='+esAdmin, muestreos, { responseType: 'blob' });
  }

}
