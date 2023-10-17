import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../login/services/auth.service';
import { InformeMensualSupervisionGeneral } from './models/informe-mensual-supervision-general';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';
import { InformeMensualSupervisionRegistro } from './models/informe-mensual-supervision-registro';

@Injectable({
  providedIn: 'root',
})
export class InformeSupervisionService {
  private informeIdDataSource = new BehaviorSubject(0);
  private mensajeDataSource = new BehaviorSubject({
    tipoMensaje: '',
    mensaje: '',
    mostrar: false,
  });

  public informeId = this.informeIdDataSource.asObservable();
  public mensaje = this.mensajeDataSource.asObservable();

  updateMensaje(value: {
    tipoMensaje: string;
    mensaje: string;
    mostrar: boolean;
  }) {
    this.mensajeDataSource.next(value);
  }

  updateInformeId(value: number) {
    this.informeIdDataSource.next(value);
  }

  constructor(private http: HttpClient, private authService: AuthService) {}

  postInforme(informe: InformeMensualSupervisionGeneral) {
    let usuario = this.authService.getUser().usuarioId;
    var formData = new FormData();
    formData.append('archivo', informe.archivo, 'filename.pdf');
    formData.append('oficio', informe.oficio);
    formData.append('lugar', informe.lugar);
    formData.append('fecha', informe.fecha);
    formData.append('responsableId', String(informe.responsableId));
    formData.append('anio', String(informe.anio));
    formData.append('mes', String(informe.mes));
    formData.append('personasInvolucradas', informe.personasInvolucradas);
    formData.append('usuario', String(usuario));

    informe.copias.forEach((obj, index) => {
      formData.append('copias[' + index + '][nombre]', obj.nombre);
      formData.append('copias[' + index + '][puesto]', obj.puesto);
    });
    return this.http.post(
      environment.apiUrl + '/ReporteSupervisionMuestreo',
      formData
    );
  }
  putInforme(informe: InformeMensualSupervisionGeneral, informeId: string) {
    let usuario = this.authService.getUser().usuarioId;
    var formData = new FormData();
    formData.append('archivo', informe.archivo, informe.archivo.name);
    formData.append('oficio', informe.oficio);
    formData.append('lugar', informe.lugar);
    formData.append('fecha', informe.fecha);
    formData.append('responsableId', String(informe.responsableId));
    formData.append('anio', String(informe.anio));
    formData.append('mes', String(informe.mes));
    formData.append('personasInvolucradas', informe.personasInvolucradas);
    formData.append('usuario', String(usuario));

    informe.copias.forEach((obj, index) => {
      formData.append('copias[' + index + '][nombre]', obj.nombre);
      formData.append('copias[' + index + '][puesto]', obj.puesto);
    });
    return this.http.put(
      environment.apiUrl + '/ReporteSupervisionMuestreo?informeId=' + informeId,
      formData
    );
  }

  getDatosGeneralesInforme(
    anioReporte: string,
    anioRegistro: string,
    mes: string,
    ocId: string
  ) {
    let params = new HttpParams()
      .set('anioReporte', anioReporte)
      .set('anioRegistro', anioRegistro)
      .set('mes', mes)
      .set('ocId', ocId);

    return this.http.get(
      environment.apiUrl +
        '/ReporteSupervisionMuestreo/InformeMensualResultados',
      { params }
    );
  }

  getDirectoresResponsables() {
    return this.http.get(
      environment.apiUrl +
        '/ReporteSupervisionMuestreo/DirectoresResponsables/?anio=' +
        2022
    );
  }

  getInformeSupervision(informe: string) {
    let params = new HttpParams().set('informe', informe);

    return this.http.get(environment.apiUrl + '/ReporteSupervisionMuestreo', {
      params,
    });
  }

  getInformes(criteriosBusqueda: InformeMensualSupervisionRegistro) {
    let params = new HttpParams()
      .set('oficio', criteriosBusqueda.oficio)
      .set('lugar', criteriosBusqueda.lugar)
      .set('direccionTecnica', criteriosBusqueda.direccionTecnica)
      .set('mesReporte', criteriosBusqueda.mesReporte)
      .set('contrato', criteriosBusqueda.contrato)
      .set('fechaRegistro', criteriosBusqueda.fechaRegistro)
      .set('fechaRegistroFin', criteriosBusqueda.fechaRegistroFin)
      .set('iniciales', '');

    return this.http.get(
      environment.apiUrl + '/ReporteSupervisionMuestreo/BusquedaInformeMensual',
      { params }
    );
  }

  putArchivoInforme(informe: string, archivoInforme: any) {
    let usuario = this.authService.getUser().usuarioId;
    var formData = new FormData();
    formData.append('archivoInforme', archivoInforme, archivoInforme.name);

    return this.http.put(
      environment.apiUrl +
        '/ReporteSupervisionMuestreo/InformeFirmado?' +
        'informe=' +
        informe +
        '&usuario=' +
        usuario,
      formData
    );
  }

  getLugaresInformeSupervision() {
    return this.http.get(
      environment.apiUrl + '/ReporteSupervisionMuestreo/LugaresInformeMensual'
    );
  }

  getMemorandosInformeSupervision() {
    return this.http.get(
      environment.apiUrl + '/ReporteSupervisionMuestreo/MemorandoInformeMensual'
    );
  }

  getArchivoInformeSupervision(informe: number, tipo: number) {
    return this.http.get(
      environment.apiUrl +
        '/ReporteSupervisionMuestreo/ArchivoInforme?informe=' +
        informe +
        '&tipo=' +
        tipo,
      { responseType: 'blob' }
    );
  }

  deleteInforme(informe: number) {
    let params = new HttpParams().set('informe', informe);

    return this.http.delete(
      environment.apiUrl + '/ReporteSupervisionMuestreo',
      { params }
    );
  }
}
