import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MunicipiosService {

  constructor(private http: HttpClient) {}

  getAllMunicipio() {
    return this.http.get<any>(environment.apiUrl + '/Municipios/');
  }
  
}
