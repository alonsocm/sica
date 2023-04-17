import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EstadosService {

  constructor(private http: HttpClient) {}

  getAllEstado() {
    return this.http.get<any>(environment.apiUrl + '/Estados/');
  }

  addEstado(request: any) {
    return this.http.post<any>(
      environment.apiUrl + '/Estados/Register',
      request
    );
  }

  updatEstado(id: any, request: any) {
    return this.http.put<any>(
      environment.apiUrl + '/Estados/Update/' + id,
      request
    );
  }

  deletEstado(id: any, request: any) {
    return this.http.delete<any>(
      environment.apiUrl + '/Estados/Delete/' + id,
      request
    );
  }
}
