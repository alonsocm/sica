import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../../../../environments/environment";
import { ReplicasResultadosReglaVal } from "../../../../interfaces/ReplicasResultadosReglaVal.interface";
import { CorreoModel } from "../../../../shared/models/correo-model";

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
    estatusId: Array<number>,
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
    return this.http.post(
      environment.apiUrl + '/ReplicasResultadosReglasValidacion/GetResultadosReplicas', estatusId,
      { params }
    );



  }

  getDistinctValuesFromColumn(
    column: string,
    estatusId: Array<number>,
    filter: string,
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        column: column,
        estatusId: estatusId,
        filter: filter,
      },
    });
    return this.http.post(
      environment.apiUrl + '/ReplicasResultadosReglasValidacion/GetDistinctValuesFromColumn', estatusId,
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

  sendEmailFile(resultados: Array<any>, tipoArchivo: number, correo: CorreoModel): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        resultados: resultados, tipoArchivo: tipoArchivo,
        destinatario: correo.destinatarios,
        asunto: correo.asunto,
        body: correo.cuerpo,
        cc: correo.copias
      },
    });
    return this.http.post(environment.apiUrl + '/ReplicasResultadosReglasValidacion/SendEmailCreateFile', resultados,
      { params }
    );
  }

  cargarArchivo(
    archivo: File,
    tipoArchivo: number
  ): Observable<any> {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);

    const params = new HttpParams({
      fromObject: {
        tipoArchivo: tipoArchivo
      },
    });
    return this.http.post(environment.apiUrl + '/ReplicasResultadosReglasValidacion/uploadfileReplicas', formData, { params });
  }


}
