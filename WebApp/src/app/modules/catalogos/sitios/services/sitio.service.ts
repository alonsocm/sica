import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Sitio } from '../../../muestreo/supervision/models/sitio';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})


export class SitioService {
  private sitiosPrivate: BehaviorSubject<Sitio[]> = new BehaviorSubject<
    Sitio[]
  >([]);
  constructor(private http: HttpClient) { }

  get sitios() {
    return this.sitiosPrivate.asObservable();
  }

  set sitiosSeleccionados(sitios: Sitio[]) {
    this.sitiosPrivate.next(sitios);
  }

  obtenerSitiosPaginados( 
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
    return this.http.get(environment.apiUrl + '/Sitios', { params });
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
      environment.apiUrl + '/Sitios/GetDistinctValuesFromColumn',
      { params }
    );
  }

  getCuencasDireccionesLocales(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/CuencasDireccionesLocales', {} 
    );
  }

  getEstados(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/Estados', {}
    );
  }


  getMunicipios(EstadoId: number): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        EstadoId: EstadoId      
      },
    });
    return this.http.get(
      environment.apiUrl + '/Municipios/MunicipiosByEstadoId', { params }
    );
  }

  getAcuiferos(): Observable<Object> {
    return this.http.get(
      environment.apiUrl + '/Acuiferos', {}
    );
  }


}


