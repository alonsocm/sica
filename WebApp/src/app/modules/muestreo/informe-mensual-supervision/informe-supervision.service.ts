import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../login/services/auth.service';
import { InformeMensualSupervisionGeneral } from './models/informe-mensual-supervision-general';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InformeSupervisionService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  postInforme(informe: InformeMensualSupervisionGeneral) {
    let usuario = this.authService.getUser().usuarioId;
    var formData = new FormData();
    formData.append('archivo', informe.archivo, 'filename.pdf');
    formData.append('oficio', informe.oficio);
    formData.append('lugar', informe.lugar);
    formData.append('fecha', informe.fecha);
    formData.append('responsableId', String(informe.responsableId));
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
    formData.append('archivo', informe.archivo, 'filename.pdf');
    formData.append('oficio', informe.oficio);
    formData.append('lugar', informe.lugar);
    formData.append('fecha', informe.fecha);
    formData.append('responsableId', String(informe.responsableId));
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

  getDatosGeneralesInforme() {
    let params = new HttpParams().set('anioReporte', '2022').set('mes', '9');

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
}
