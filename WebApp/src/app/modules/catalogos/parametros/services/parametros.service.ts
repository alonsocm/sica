import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ParametrosService {
  constructor(private http: HttpClient) {}

  getParametros(page: number, pageSize: number): Observable<Object> {
    return this.http.get(
      environment.apiUrl +
        '/ParametrosGrupo?page=' +
        page +
        '&pageSize=' +
        pageSize
    );
  }
}
