import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Revision } from '../../../../../interfaces/Replicas/RevisionResultado';
import { RepDiferente } from 'src/app/interfaces/Replicas/ReplicaDiferente'
import { ResumenResultados } from '../../../../../interfaces/ResumenResultado.interface';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from 'src/app/modules/login/services/auth.service';



@Injectable({
  providedIn: 'root'
})
export class ReplicaDiferenteService {
  private muestreosPrivate: BehaviorSubject<RepDiferente[]> = new BehaviorSubject<RepDiferente[]>([]);

  constructor(private http: HttpClient, private authService: AuthService) { }

  getReplicaDiferente() {
    return this.http.get<RepDiferente>(
      environment.apiUrl + '/ReplicasDiferente/ReplicaDiferenteliFiltro?id=' +
      localStorage.getItem('idUsuario')
    );
  }

  set muestreosSeleccionados(muestreos: RepDiferente[]) {
    this.muestreosPrivate.next(muestreos);
  }

  exportarResultadosExcel(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/ReplicasDiferente/ExportarExcelReplicaDiferente', muestreos, { responseType: 'blob' });
  }

  exportarResultadosExcelGeneral(muestreos: Array<any> = []) {
    return this.http.post(environment.apiUrl + '/ReplicasDiferente/ExportarExcelReplicaDiferenteGeneral', muestreos, { responseType: 'blob' });
  }


  cargarArchivo(archivo: any): Observable<any> {
    const formData = new FormData();
    formData.append('formFile', archivo, archivo.name);
    return this.http.post(environment.apiUrl + '/ReplicasDiferente/CargarArchivoRepDiferente', formData);
  }


  aprobacionPorBloque(resultados: Array<any> = []) {
    return this.http.put(environment.apiUrl + '/ReplicasDiferente/Aprobacionporbloque', resultados, { responseType: 'blob' });
  }

  EnviarAprobado(muestreos: Array<any> = []) {
    return this.http.put(environment.apiUrl + '/ReplicasDiferente/EnvioAprobacion', muestreos);
  }
  


}
