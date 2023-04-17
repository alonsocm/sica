import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocalidadesService {

  constructor(private http: HttpClient) {}

  getAllLocalidad() {
    return this.http.get<any>(environment.apiUrl + '/Localidades/');
  }
}
