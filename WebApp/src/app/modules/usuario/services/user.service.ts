import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Data } from '@angular/router';
import { Observable } from 'rxjs';
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

  getAllusersPaginados(
    esLiberacion: boolean,
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        esLiberacion: esLiberacion,
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(environment.apiUrl + '/Usuarios/AllUsers', { params });
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

  getDistinctValuesFromColumn(
    column: string,
    filter: string
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        column: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/Usuarios/GetDistinctValuesFromColumn',
      { params }
    );
  }

}
