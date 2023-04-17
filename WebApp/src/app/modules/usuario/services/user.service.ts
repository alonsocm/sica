import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Data } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  adduser(request: any) {   
    return this.http.post<any>(environment.apiUrl + '/usuarios/register', request);
  }

  update(id: any, request: any) {   
    return this.http.put<any>(environment.apiUrl + '/usuarios/update/' + id ,request);
  }

  getAllusers() {
    return this.http.get<any>(environment.apiUrl + '/Usuarios/AllUsers');
  }

  getPerfiles() {
    return this.http.get<any>(environment.apiUrl + '/Perfiles');
  }

  getCuencas() {
    return this.http.get<any>(environment.apiUrl + '/OrganismosCuenca');
  }

  getDLocales() {

    return this.http.get<any>(environment.apiUrl + '/DireccionesLocales' );
  }
}
