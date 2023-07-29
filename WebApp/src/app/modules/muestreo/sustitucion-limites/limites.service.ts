import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/modules/login/services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class LimitesService {

  constructor(private http: HttpClient, public authService: AuthService) { }

  sustituirLimites(parametrosSustitucion: any){
    let formData = new FormData();

    formData.append('archivo', parametrosSustitucion.archivo);
    formData.append('periodo', parametrosSustitucion.periodo);
    formData.append('origenLimites', parametrosSustitucion.origenLimites);

    return this.http.post(
      environment.apiUrl + '/Limites',
      formData
    );
  }

  getResultadosParametrosEstatus(idEstatus: number) {

    let params = new HttpParams({
      fromObject: { estatusId: idEstatus, userId: this.authService.getUser().usuarioId },
    });
    return this.http.get(environment.apiUrl + '/Resultados/ResultadosParametrosEstatus', { params });
  }




}
