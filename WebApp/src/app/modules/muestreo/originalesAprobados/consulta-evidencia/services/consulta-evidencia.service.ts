import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ConsultaEvidencia } from 'src/app/interfaces/evidencia';

@Injectable({
  providedIn: 'root',
})
export class ConsultaEvidenciaService {
  constructor(private http: HttpClient) {}

  obtenerMuestreos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos/Aprobados');
  }

  exportarEvidenciasExcel(evidencias: Array<ConsultaEvidencia> = []) {
    return this.http.post(
      environment.apiUrl +
        '/EvidenciasMuestreos/ExportarExcelConsultaEvidencia',
      evidencias,
      { responseType: 'blob' }
    );
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

  descargarArchivo(nombreArchivo: string) {
    const params = new HttpParams({
      fromObject: { nombreArchivo: nombreArchivo },
    });
    return this.http.get(environment.apiUrl + '/EvidenciasMuestreos', {
      params,
      responseType: 'blob',
    });
  }
}
