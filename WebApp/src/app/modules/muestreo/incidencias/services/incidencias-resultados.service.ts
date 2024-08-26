import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { ReplicasResultadosReglaVal } from "../../../../interfaces/ReplicasResultadosReglaVal.interface";

@Injectable({
  providedIn: 'root',
})
export class IncidenciasResultadosService {
  private resultadosReplicaPrivate: BehaviorSubject<ReplicasResultadosReglaVal[]> = new BehaviorSubject<
    ReplicasResultadosReglaVal[]
  >([]);
  constructor(private http: HttpClient) { }

  get resultadosReplicas() {
    return this.resultadosReplicaPrivate.asObservable();
  }

  set resultadosReplicasSeleccionados(muestreos: ReplicasResultadosReglaVal[]) {
    this.resultadosReplicaPrivate.next(muestreos);
  }

  getReplicasResultadosPaginados(
    estatusId: number,
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    let params = new HttpParams({
      fromObject: {
        estatusId: estatusId,
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(
      environment.apiUrl + '/ReplicasResultadosReglasValidacion',
      { params }
    );
  }

  getDistinctValuesFromColumn(
    column: string,
    filter: string    
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        column: column,
        filter: filter      
      },
    });
    return this.http.get(
      environment.apiUrl + '/ReplicasResultadosReglasValidacion/GetDistinctValuesFromColumn',
      { params }
    );
  }

  descargarInformacion(resultados: Array<any>): Observable<Blob> {
    const params = new HttpParams({
      fromObject: { resultados: resultados },
    });

    return this.http.post(environment.apiUrl + '/ReplicasResultadosReglasValidacion', resultados, {
      params,
      responseType: 'blob',
    });
  }

}
