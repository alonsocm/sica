import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Revision } from '../../../../../interfaces/Replicas/RevisionResultado';
import { ResumenResultados } from '../../../../../interfaces/ResumenResultado.interface';
import { BehaviorSubject, Observable } from 'rxjs';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo'

@Injectable({
  providedIn: 'root',
})
export class RevisionResultadoService {
  private muestreosPrivate: BehaviorSubject<Revision[]> = new BehaviorSubject<Revision[]>([]);

  constructor(private http: HttpClient) {}

  set muestreosSeleccionados(muestreos: Revision[]) {
    this.muestreosPrivate.next(muestreos);
  }

  getResultadosRevision(usuario: number) {
    return this.http.get<ResumenResultados>(
      environment.apiUrl +'/Replicas/RevisionRepliFiltro?id=' +
      usuario
    );
  }

  aprovarRechazResultado(aprovacionMuestreo: Array<any> = []){
    return this.http.put(environment.apiUrl + '/Replicas/AutorizarRechaMuestreo', aprovacionMuestreo, { responseType: 'blob' });
  }

  exportarResultadosExcel(muestreos: Array<any> = []) {  
    return this.http.post(environment.apiUrl + '/Replicas/ExportarExcelReplica', muestreos, { responseType: 'blob' });
  }

  DescargaResultadoExcel(muestreos: Array<any> = []) {  
    return this.http.post(environment.apiUrl + '/Replicas/DescargarEstatusReplica', muestreos, { responseType: 'blob' });
  }

  Enviar(muestreos: Array<any> = []){    
    return this.http.put(environment.apiUrl + '/Replicas/EnviarAprobacion', muestreos);
  }

  cargarRevision(archivo: any, usuario: string): Observable<any> {
    const formData = new FormData();   
    formData.append('formFile', archivo);
    formData.append('usuarioId', usuario);
    return this.http.post(environment.apiUrl + '/Replicas/CargaRevision', formData, { responseType: 'blob' });
  }

  cambioEstatus(muestreoId:number){
    var params = new HttpParams({
      fromObject: { 
        muestreoId:muestreoId,
        estatus: estatusMuestreo.OriginalesAprobados 
      }});       
      //return this.http.post(environment.apiUrl +'/Muestreos/CambioEstatus?estatus='+estatusMuestreo.OriginalesAprobados + "&muestreoId="+ muestreoId,'');
      return this.http.get(environment.apiUrl +'/Muestreos/CambioEstatus',{ params}); 
    }
  }


