import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Parametro } from '../models/parametro';

@Injectable({
  providedIn: 'root',
})
export class ParametrosService {
  constructor(private http: HttpClient) {}

  getParametros(
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        page,
        pageSize,
        filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(
      environment.apiUrl + '/ParametrosGrupo/ParametrosPaginados',
      { params }
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

  getAllParametros() {
    return this.http.get(environment.apiUrl + '/ParametrosGrupo');
  }

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
}
