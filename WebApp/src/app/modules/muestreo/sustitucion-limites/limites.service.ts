import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LimitesService {

  constructor(private http: HttpClient) { }

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
}
