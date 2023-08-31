import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SupervisionService {
  private dataSource = new BehaviorSubject(0);
  public data = this.dataSource.asObservable();

  constructor(private http: HttpClient) {}

  updateData(value: number) {
    this.dataSource.next(value);
  }

  postSupervision(supervision: any): Observable<any> {
    return this.http.post(
      environment.apiUrl + '/creacionsupervisionmuestreo/supervisionmuestreo',
      supervision
    );
  }

  getOCDL() {
    return this.http.get(
      environment.apiUrl + '/creacionsupervisionmuestreo/organismosdirecciones'
    );
  }

  getCuencas() {
    return this.http.get(environment.apiUrl + '/OrganismosCuenca');
  }

  getLaboratorios() {
    return this.http.get(environment.apiUrl + '/Laboratorios');
  }

  getMuestreadoresLaboratorio(laboratorio: number) {
    const params = new HttpParams({
      fromObject: { laboratorioId: laboratorio },
    });
    return this.http.get(
      environment.apiUrl +
        '/creacionsupervisionmuestreo/ResponsablesMuestreadores',
      { params }
    );
  }

  getTiposCuerpoAgua() {
    return this.http.get(environment.apiUrl + '/CuerpoDeAgua/TipoHomologado');
  }

  getFormatoSupervision() {
    return this.http.get(
      environment.apiUrl +
        '/creacionsupervisionmuestreo/FormatoSupervisionMuestreo',
      {
        responseType: 'blob',
      }
    );
  }

  postArchivosSupervision(
    archivoSupervision: any,
    evidencias: Array<any>
  ): Observable<any> {
    const formData = new FormData();
    formData.append('supervisionId', '1');
    formData.append('archivoSupervision', archivoSupervision);
    Array.from(evidencias).forEach((archivo) => {
      formData.append('evidencias', archivo);
    });

    return this.http.post(
      environment.apiUrl + '/creacionsupervisionmuestreo/ArchivosMuestreo',
      formData
    );
  }
}
