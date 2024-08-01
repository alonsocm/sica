import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Sitio } from '../../../muestreo/supervision/models/sitio';
import { environment } from 'src/environments/environment';
import { LimitesLaboratorios } from '../../../../interfaces/catalogos/limitesLaboratorio.interface';

@Injectable({
  providedIn: 'root',
})

export class LimiteLaboratorioService {
  private limiteLaboratorioPrivate: BehaviorSubject<LimitesLaboratorios[]> = new BehaviorSubject<
    LimitesLaboratorios[]
    >([]);
  constructor(private http: HttpClient) { }
  get limiteLaboratorios() {
    return this.limiteLaboratorioPrivate.asObservable();
  }
  set limiteLaboratoriosSeleccionados(limitesLab: LimitesLaboratorios[]) {
    this.limiteLaboratorioPrivate.next(limitesLab);
  }

  obtenerLimitesLaboratorioPaginados(
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
    return this.http.get(environment.apiUrl + '/LimiteParametroLaboratorio', { params });
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
      environment.apiUrl + '/LimiteParametroLaboratorio/GetDistinctValuesFromColumn',
      { params }
    );
  }

  addLimiteLaboratorios(registro: LimitesLaboratorios): Observable<Object> {
    return this.http.post(environment.apiUrl + '/LimiteParametroLaboratorio', registro);
  }

  updateLimiteLaboratorios(registro: LimitesLaboratorios): Observable<Object> {
    return this.http.put(environment.apiUrl + '/LimiteParametroLaboratorio', registro);
  }


  
}
