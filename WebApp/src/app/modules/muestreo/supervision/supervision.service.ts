import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
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

  getSupervisiones() {
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/DatosGeneralesSupervision'
    );
  }

  postSupervision(supervision: any): Observable<any> {
    return this.http.post(
      environment.apiUrl + '/supervisionmuestreo/',
      supervision
    );
  }

  getSupervision(id: number) {
    const params = new HttpParams({
      fromObject: { supervisionMuestreoId: id },
    });
    return this.http.get(
      environment.apiUrl +
        '/supervisionmuestreo/ObtenerSupervisionMuestreoPorId',
      { params }
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

  getClasificacionesCriterios() {
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/ClasificacionCriterios'
    );
  }

  getFormatoSupervision() {
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/FormatoSupervisionMuestreo',
      {
        responseType: 'blob',
      }
    );
  }

  deleteArchivo(supervisionId: number, nombreArchivo: string) {
    const params = new HttpParams()
      .set('supervisionId', supervisionId)
      .set('nombreArchivo', nombreArchivo);
    return this.http.delete(
      environment.apiUrl + '/supervisionmuestreo/archivo',
      { params }
    );
  }

  getArchivo(supervisionId: number, nombreArchivo: string) {
    const params = new HttpParams()
      .set('supervisionId', supervisionId)
      .set('nombreArchivo', nombreArchivo);
    return this.http.get(environment.apiUrl + '/supervisionmuestreo/archivo', {
      responseType: 'blob',
      params,
    });
  }

  postArchivosSupervision(
    supervision: number,
    archivoSupervision: any,
    evidencias: Array<any>
  ): Observable<any> {
    const formData = new FormData();
    formData.append('supervisionId', String(supervision));
    formData.append('archivos', archivoSupervision);
    Array.from(evidencias).forEach((archivo) => {
      formData.append('archivos', archivo);
    });

    return this.http.post(
      environment.apiUrl + '/supervisionmuestreo/ArchivosMuestreo',
      formData
    );
  }
}
