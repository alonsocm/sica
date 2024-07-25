import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TipoCuerpoAgua } from '../models/tipocuerpoagua';

@Injectable({
  providedIn: 'root'
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
      '/TipoCuerpoAgua?' +
      page +
      '&pageSize=' +
      pageSize +
      '&filter=' +
      filter
  );
}
getTipoHomologado(): Observable<Object> {
  return this.http.get(
    environment.apiUrl + '/CuerpoDeAgua/TipoHomologado'
  );
}
addTipoCuerpoAgua(tipoCuerpoAgua: TipoCuerpoAgua): Observable<TipoCuerpoAgua> {
  return this.http.post<TipoCuerpoAgua>(
    environment.apiUrl + '/TipoCuerpoAgua',
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

deleteTipoCuerpoAgua(id: number): Observable<void> {
  return this.http.delete<void>(environment.apiUrl + '/TipoCuerpoAgua/' + id);
}

getDistinct(column: string, filter: string): Observable<Object> {
  return this.http.get(
    environment.apiUrl +
      '/TipoCuerpoAgua?' +
      column +
      '&filter=' +
      filter
  );
}
}