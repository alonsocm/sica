import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TipoCuerpoAgua, TipoHomologado } from '../models/tipocuerpoagua';


@Injectable({
  providedIn: 'root',
})
export class TipoCuerpoAguaService {
  constructor(private http: HttpClient) {}

  getTipoCuerpoAgua(
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {     
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(environment.apiUrl + '/TipoCuerpoAgua/TipoCuerpoAguaP', { params });
  }
  getTipoHomologado(
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: { 
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });   
      return this.http.get(environment.apiUrl + '/CuerpoDeAgua/TipoHomologado', { params });         
  }

  getDistinct(column: string, filter: string): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        column: column,
        filter: filter,
      },
    });
    return this.http.get(
      environment.apiUrl + '/TipoCuerpoAgua/GetDistinctFromColumn',
      { params }
    );
  }

  addTipoCuerpoAgua(
    tipoCuerpoAgua: TipoCuerpoAgua
  ): Observable<TipoCuerpoAgua> {
    return this.http.post<TipoCuerpoAgua>(
      environment.apiUrl + '/TipoCuerpoAgua?',
      tipoCuerpoAgua
    );
  }

  updateTipoCuerpoAgua(
    id: number,
    tipoCuerpoAgua: TipoCuerpoAgua
  ): Observable<TipoCuerpoAgua> {
    return this.http.put<TipoCuerpoAgua>(
      environment.apiUrl + '/TipoCuerpoAgua/' + id,
      tipoCuerpoAgua
    );
  }

  deleteTipoCuerpoAgua(
    id: number,
    tipoCuerpoAgua: TipoCuerpoAgua
  ): Observable<TipoCuerpoAgua> {
    return this.http.delete<TipoCuerpoAgua>(
      environment.apiUrl + '/TipoCuerpoAgua/' + id
    );
  }

  getTipoCuerpoAguaById(id: number): Observable<TipoCuerpoAgua> {
    return this.http.get<TipoCuerpoAgua>(environment.apiUrl + '/TipoCuerpoAgua/' + id);
  }

  
  uploadFile(archivo: File, actualizar: boolean) {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);
    return this.http.post(
      environment.apiUrl + '/TipoCuerpoAgua/cargamasiva?actualizar=' + actualizar,
      formData
    );
  }

  
}
