import { HttpClient } from '@angular/common/http';
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
    filter: string
  ): Observable<Object> {
    return this.http.get(
      environment.apiUrl +
        '/TipoCuerpoAgua/TipoCuerpoAguaP?' +
        'page=' +
        page +
        '&pageSize=' +
        pageSize +
        '&filter=' +
        filter
    );
  }
  getDistinct(column: string, filter: string): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/TipoCuerpoAgua?/GetDistinctFromColumn' + column + '&filter=' + filter
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

  getTipoHomologado(): Observable<TipoHomologado[]> {
    return this.http.get<TipoHomologado[]>(
      environment.apiUrl + '/CuerpoDeAgua/TipoHomologado'
    );
  }

  
}
