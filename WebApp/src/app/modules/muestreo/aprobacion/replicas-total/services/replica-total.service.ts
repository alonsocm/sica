import { JsonPipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/modules/login/services/auth.service';
import { environment } from 'src/environments/environment';
import {
  ResultadoReplicaExcel,
  RevisionReplicasLNR,
} from '../pages/replica-total/replica-total.component';

@Injectable({
  providedIn: 'root',
})
export class ReplicaTotalService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  obtenerRegistros() {
    return this.http.get(environment.apiUrl + '/Replicas');
  }

  obtenerExcelResultadosParaReplica(
    resultados: Array<ResultadoReplicaExcel> = []
  ): Observable<Blob> {
    return this.http.post(
      environment.apiUrl + '/Replicas/ReplicasExcel',
      resultados,
      { responseType: 'blob' }
    );
  }

  cargarArchivoRevisionReplicas(archivo: File): Observable<any> {
    let formData = new FormData();
    formData.append('archivoRevisionReplicas', archivo, archivo.name);
    return this.http.post(
      environment.apiUrl + '/Replicas/CargarRevisionReplicas',
      formData
    );
  }

  obtenerExcelReplicas(
    replicas: Array<RevisionReplicasLNR> = [],
    esLNR: boolean
  ): Observable<Blob> {
    let endpoint = esLNR ? 'RevisionReplicasLNRExcel' : 'ExportarExcel';
    return this.http.post(
      environment.apiUrl + '/Replicas/' + endpoint,
      replicas,
      { responseType: 'blob' }
    );
  }

  cargarArchivoRevisionLNR(archivo: File): Observable<any> {
    let idUsuario = this.authService.getUser().usuarioId.toString();
    let formData = new FormData();
    formData.append('archivo', archivo, archivo.name);
    formData.append('idUsuario', idUsuario);
    return this.http.post(
      environment.apiUrl + '/Replicas/CargarRevisionLNR',
      formData
    );
  }

  aprobarResultadosBloque(resultados: string[], aprobar: boolean) {
    let idUsuario = this.authService.getUser().usuarioId.toString();
    let aprobarResultados = {
      clavesUnicas: resultados,
      aprobado: aprobar,
      usuarioId: idUsuario,
    };
    return this.http.put(
      environment.apiUrl + '/Replicas/AprobarResultadosBloque',
      aprobarResultados
    );
  }

  enviarResultados(resultados: string[], aprobar: boolean) {
    let idUsuario = this.authService.getUser().usuarioId.toString();
    let aprobarResultados = {
      clavesUnicas: resultados,
      aprobado: aprobar,
      usuarioId: idUsuario,
    };
    return this.http.put(
      environment.apiUrl + '/Replicas/EnviarResultados',
      aprobarResultados
    );
  }

  enviarResultadosDiferenteDato(resultados: string[]) {
    let idUsuario = this.authService.getUser().usuarioId.toString();
    let aprobarResultados = { clavesUnicas: resultados, usuarioId: idUsuario };
    return this.http.put(
      environment.apiUrl + '/Replicas/EnviarResultadosDifDato',
      aprobarResultados
    );
  }

  cargarEvidenciasReplicas(archivos: Array<File>): Observable<any> {
    const formData = new FormData();
    Array.from(archivos).forEach((archivo) => {
      formData.append('archivos', archivo);
    });
    return this.http.post(
      environment.apiUrl + '/Replicas/CargarEvidenciasReplica',
      formData
    );
  }

  descargarEvidencias(claves: Array<string>){
    const params = new HttpParams({
      fromObject: { clavesUnicas: claves },
    });
    return this.http.get(environment.apiUrl + '/Replicas/ObtenerEvidencias', {
      params,
      responseType: 'blob',
    });
  }
}
