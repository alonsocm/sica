import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Laboratorio } from '../../../../interfaces/catalogos/laboratorio.interface';

@Injectable({
  providedIn: 'root',
})
export class LaboratorioService {
  private laboratorioPrivate: BehaviorSubject<Laboratorio[]> =
    new BehaviorSubject<Laboratorio[]>([]);

  constructor(private http: HttpClient) {}

  get laboratorios() {
    return this.laboratorioPrivate.asObservable();
  }

  set laboratoriosSeleccionados(laboratorios: Laboratorio[]) {
    this.laboratorioPrivate.next(laboratorios);
  }

  obtenerLaboratorios(
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
    return this.http.get(environment.apiUrl + '/Laboratorios/Laboratorios', {
      params,
    });
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
      environment.apiUrl + '/Laboratorios/GetDistinctValuesFromColumn',
      { params }
    );
  }

  create(registro: Laboratorio): Observable<Object> {
    return this.http.post(environment.apiUrl + '/Laboratorios', registro);
  }

  update(registro: Laboratorio): Observable<Object> {
    return this.http.put(environment.apiUrl + '/Laboratorios', registro);
  }
}
