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
      environment.apiUrl + '/supervisionmuestreo/',
      supervision
    );
  }

  getOCDL() {
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/organismosdirecciones'
    );
  }

  getClavesSitios(organismoDireccion: number) {
    const params = new HttpParams({
      fromObject: { organismoDireccionId: organismoDireccion },
    });
    return this.http.get(
      environment.apiUrl +
        '/supervisionmuestreo/ClaveSitiosPorCuencaDireccionId',
      { params }
    );
  }

  getSitio(claveSitio: string) {
    const params = new HttpParams({
      fromObject: { claveSitio: claveSitio },
    });
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/obtenersitioporclave',
      { params }
    );
  }

  getCuencas() {
    return this.http.get(environment.apiUrl + '/OrganismosCuenca');
  }

  getLaboratorios() {
    return this.http.get(environment.apiUrl + '/Laboratorios/laboratorios');
  }

  getMuestreadoresLaboratorio(laboratorio: number) {
    const params = new HttpParams({
      fromObject: { laboratorioId: laboratorio },
    });
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/ResponsablesMuestreadores',
      { params }
    );
  }

  getTiposCuerpoAgua() {
    return this.http.get(environment.apiUrl + '/CuerpoDeAgua/TipoHomologado');
  }

  getFormatoSupervision() {
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/FormatoSupervisionMuestreo',
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
      environment.apiUrl + '/supervisionmuestreo/ArchivosMuestreo',
      formData
    );
  }
}
