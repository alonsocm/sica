import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

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
