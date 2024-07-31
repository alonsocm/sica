import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Parametro } from '../models/parametro';

@Injectable({
  providedIn: 'root',
})
export class ParametrosService {
  uploadFile(archivo: File, actualizar: boolean) {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);
    return this.http.post(
      environment.apiUrl +
        '/ParametrosGrupo/cargamasiva?actualizar=' +
        actualizar,
      formData
    );
  }
  constructor(private http: HttpClient) {}

  getParametros(
    page: number,
    pageSize: number,
    filter: string
  ): Observable<Object> {
    return this.http.get(
      environment.apiUrl +
        '/ParametrosGrupo/ParametrosPaginados?page=' +
        page +
        '&pageSize=' +
        pageSize +
        '&filter=' +
        filter
    );
  }

  getDistinct(column: string, filter: string): Observable<Object> {
    return this.http.get(
      environment.apiUrl +
        '/ParametrosGrupo/GetDistinctFromColumn?column=' +
        column +
        '&filter=' +
        filter
    );
  }

  getGrupos(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/ParametrosGrupo/GetGruposParametros'
    );
  }

  getSubgrupos(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/ParametrosGrupo/GetSubGrupoAnalitico'
    );
  }

  getUnidadesMedida(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/ParametrosGrupo/GetUnidadesMedida'
    );
  }

  create(registro: Parametro): Observable<Object> {
    return this.http.post(environment.apiUrl + '/ParametrosGrupo', registro);
  }

  update(registro: Parametro): Observable<Object> {
    return this.http.put(environment.apiUrl + '/ParametrosGrupo', registro);
  }

  delete(registro: number): Observable<Object> {
    return this.http.delete(
      environment.apiUrl + '/ParametrosGrupo?parametroid=' + registro
    );
  }
}
