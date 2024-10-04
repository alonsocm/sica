import { HttpClient, HttpParams} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class TotalService {

  constructor(private http: HttpClient) { }
  getResultadosMuestreosParametros(isOCDL: boolean) {
    return this.http.get<any>(environment.apiUrl + '/resultados/ResultadosMuestreoParametros?id=' + localStorage.getItem('idUsuario') + '&isOCDL=' + isOCDL);
  }
  getResumenRevisionResultados(idEstatus: number, isOCDL: boolean) {
    return this.http.get(environment.apiUrl + '/resultados/MuestreosxFiltro?estatusId=' + idEstatus
      + '&userId=' + localStorage.getItem('idUsuario') + '&isOCDL=' + isOCDL);

  }
  exportarResultadosExcel(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/ExportarExcelResultados', muestreos, { responseType: 'blob' });
  }
  exportarResultadosValidados(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/ExportarResultadosValidados', muestreos, { responseType: 'blob' });
  }
  exportarResultadosExcelSECAIA(muestreos: Array<any> = []) {
    const tipoExcel = 'secaia';
    return this.http.post(environment.apiUrl + '/Resultados/ExportarExcelResulatdosSECAIA?tipoExcel=' + tipoExcel, muestreos, { responseType: 'blob' });
  }
  exportExcelResumenSECAIA(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelResumenSECAIA', muestreos, { responseType: 'blob' });
  }

  exportExcelValidadosSECAIA(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/Resultados/exportExcelValidadosSECAIA', muestreos, { responseType: 'blob' });
  }


  getExcelResume(resultados: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/resultados/exporttoexceltwosheets/', resultados, { responseType: 'blob' });
  }
  actualizacionMuestreosParametros(request: Array<any> = []) {
    return this.http.put(environment.apiUrl + '/Resultados/updateMuestreoParametros', request);
  }
  actualizacionParametros(request: Array<any> = []) {
    return this.http.put(environment.apiUrl + '/Resultados/updateParametros', request);
  }
  actualizarResultado(resultados: any) {
    return this.http.put(environment.apiUrl + '/resultados', resultados);
  }
  getObseravciones() {
    return this.http.get<any>(environment.apiUrl + '/Observaciones');
  }
  getParametros() {
    return this.http.get<any>(environment.apiUrl + '/ParametrosGrupo');
  }
  cargarArchivo(archivo: any): Observable<any> {
    const formData = new FormData();
    formData.append('formFile', archivo, archivo.name);
    return this.http.post(environment.apiUrl + '/Resultados', formData);
  }
  cargarArchivoSECAIA(archivo: any): Observable<any> {
    const formData = new FormData();
    formData.append('formFile', archivo, archivo.name);
    return this.http.post(environment.apiUrl + '/Resultados/CargarExcelObservacionesSECAIA?UserId=' + localStorage.getItem('idUsuario'), formData);
  }

  descargarArchivosEvidencias(muestreos: Array<number>) {
    const params = new HttpParams({
      fromObject: { muestreos: muestreos },
    });
    return this.http.get(environment.apiUrl + '/EvidenciasMuestreos/Archivos', {
      params,
      responseType: 'blob',
    });
  }
}
