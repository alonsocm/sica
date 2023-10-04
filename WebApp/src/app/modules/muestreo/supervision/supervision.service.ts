import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SupervisionBusqueda } from './models/supervision-busqueda';
import { InformeMensualSupervisionGeneral } from '../informe-mensual-supervision/models/informe-mensual-supervision-general';
import { AuthService } from '../../login/services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class SupervisionService {
  private supervisionIdDataSource = new BehaviorSubject(0);
  private esConsultaDataSource = new BehaviorSubject(false);
  public supervisionId = this.supervisionIdDataSource.asObservable();
  public esConsulta = this.esConsultaDataSource.asObservable();
  private mensajeDataSource = new BehaviorSubject({
    tipoMensaje: '',
    mensaje: '',
    mostrar: false,
  });
  public mensaje = this.mensajeDataSource.asObservable();

  constructor(private http: HttpClient, private authService: AuthService) {}

  updateSupervisionId(value: number) {
    this.supervisionIdDataSource.next(value);
  }

  updateEsConsulta(value: boolean) {
    this.esConsultaDataSource.next(value);
  }

  updateMensaje(value: {
    tipoMensaje: string;
    mensaje: string;
    mostrar: boolean;
  }) {
    this.mensajeDataSource.next(value);
  }

  getSupervisiones(supervision: SupervisionBusqueda) {
    let params = new HttpParams()
      .set(
        'organismosDireccionesRealizaId',
        supervision.organismosDireccionesRealizaId == 0
          ? ''
          : supervision.organismosDireccionesRealizaId ?? 0
      )
      .set('sitioId', supervision.sitioId == 0 ? '' : supervision.sitioId ?? 0)
      .set('fechaMuestreo', supervision.fechaMuestreo ?? '')
      .set('fechaMuestreoFin', supervision.fechaMuestreoFin ?? '')
      .set(
        'puntajeObtenido',
        supervision.puntajeObtenido == 0 ? '' : supervision.puntajeObtenido ?? 0
      )
      .set(
        'laboratorioRealizaId',
        supervision.laboratorioRealizaId == 0
          ? ''
          : supervision.laboratorioRealizaId ?? 0
      )
      .set('claveMuestreo', supervision.claveMuestreo ?? '')
      .set(
        'tipoCuerpoAguaId',
        supervision.tipoCuerpoAguaId == 0
          ? ''
          : supervision.tipoCuerpoAguaId ?? 0
      );

    return this.http.get(environment.apiUrl + '/supervisionmuestreo/', {
      params,
    });
  }

  postSupervision(supervision: any): Observable<any> {
    let usuarioRegistroId = this.authService.getUser().usuarioId;
    return this.http.post(
      environment.apiUrl +
        '/supervisionmuestreo' +
        '?usuarioRegistroId=' +
        usuarioRegistroId,
      supervision
    );
  }

  deleteSupervision(supervisionId: number) {
    const params = new HttpParams().set('supervision', supervisionId);
    return this.http.delete(environment.apiUrl + '/supervisionmuestreo/', {
      params,
    });
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

  getSitios(organismoDireccion: number) {
    const params = new HttpParams({
      fromObject: { cuencaDireccionId: organismoDireccion },
    });
    return this.http.get(
      environment.apiUrl + '/supervisionmuestreo/SitiosPorCuencaDireccionId',
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
    return this.http.get(
      environment.apiUrl + '/Laboratorios/LaboratoriosMuestradores'
    );
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
    return this.http.get(environment.apiUrl + '/CuerpoDeAgua');
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
    evidencias: Array<any>,
    claveMuestreo: string
  ): Observable<any> {
    const formData = new FormData();
    formData.append('supervisionId', String(supervision));
    formData.append('claveMuestreo', claveMuestreo);
    formData.append('archivos', archivoSupervision);
    Array.from(evidencias).forEach((archivo) => {
      formData.append('archivos', archivo);
    });

    return this.http.post(
      environment.apiUrl + '/supervisionmuestreo/ArchivosMuestreo',
      formData
    );
  }

  getIntervalosPuntaje() {
    return this.http.get(environment.apiUrl + '/IntervalosPuntajeSupervision');
  }
}
