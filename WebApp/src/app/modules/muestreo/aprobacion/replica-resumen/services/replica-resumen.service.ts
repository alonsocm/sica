import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ReplicaResumen } from 'src/app/interfaces/Replicas/replicaResumen';


@Injectable({
  providedIn: 'root',
})
export class ReplicaResumenService {
  private muestreosPrivate:  BehaviorSubject<ReplicaResumen[]> = new BehaviorSubject<ReplicaResumen[]>([]);
  constructor(private http: HttpClient) {}

  exportarResultadosExcel(resultados: Array<any> = []) {
    return this.http.post(
      environment.apiUrl + '/Replicas/ExportarExcelReplicaResumen',
      resultados,
      { responseType: 'blob' }
    );
  }

  obtenerReplicasResumen() {
    return this.http.get<ReplicaResumen>(
      environment.apiUrl +
        '/Replicas/Resumen'
    );
  }


}
