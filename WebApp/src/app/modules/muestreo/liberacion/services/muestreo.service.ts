import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Resultado } from '../../../../interfaces/Resultado.interface';
import { environment } from 'src/environments/environment';
import { acumuladosMuestreo } from '../../../../interfaces/acumuladosMuestreo.interface';

@Injectable({
  providedIn: 'root',
})
export class MuestreoService {
  private muestreosPrivate: BehaviorSubject<Muestreo[]> = new BehaviorSubject<
    Muestreo[]
  >([]);
  private resultadosPrivate: BehaviorSubject<Resultado[]> = new BehaviorSubject<
    Resultado[]
  >([]);

  private filtrosPrivate: BehaviorSubject<any[]> = new BehaviorSubject<any[]>(
    []
  );
  private filtrosCabeceroFocoPrivate: BehaviorSubject<any[]> =
    new BehaviorSubject<any[]>([]);

  constructor(private http: HttpClient) {}

  get filtros() {
    return this.filtrosPrivate.asObservable();
  }

  set filtrosSeleccionados(filtros: any[]) {
    this.filtrosPrivate.next(filtros);
  }

  get muestreos() {
    return this.muestreosPrivate.asObservable();
  }
  get resultados() {
    return this.resultadosPrivate.asObservable();
  }

  get filtrosCabeceros() {
    return this.filtrosCabeceroFocoPrivate.asObservable();
  }

  set muestreosSeleccionados(muestreos: Muestreo[]) {
    this.muestreosPrivate.next(muestreos);
  }

  set resultadosSeleccionados(resultados: Resultado[]) {
    this.resultadosPrivate.next(resultados);
  }

  set filtrosCabeceroFoco(cabeceros: any[]) {
    this.filtrosCabeceroFocoPrivate.next(cabeceros);
  }

  cargarArchivo(
    archivo: File,
    validado: boolean,
    reemplazar: boolean = false,
    tipocarga: number
  ): Observable<any> {
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);
    formData.append('validado', validado.toString());
    formData.append('reemplazar', reemplazar.toString());
    formData.append('tipocarga', tipocarga.toString());
    return this.http.post(environment.apiUrl + '/muestreos', formData);
  }

  cargarEvidencias(archivos: FileList): Observable<any> {
    const formData = new FormData();
    Array.from(archivos).forEach((archivo) => {
      formData.append('archivos', archivo);
    });
    return this.http.post(
      environment.apiUrl + '/EvidenciasMuestreos',
      formData
    );
  }

  descargarArchivo(nombreArchivo: any) {
    const params = new HttpParams({
      fromObject: { nombreArchivo: nombreArchivo },
    });
    return this.http.get(environment.apiUrl + '/EvidenciasMuestreos', {
      params,
      responseType: 'blob',
    });
  }

  obtenerMuestreos(esLiberacion: boolean): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        esLiberacion: esLiberacion,
      },
    });
    return this.http.get(environment.apiUrl + '/Muestreos', { params });
  }

  obtenerMuestreosPaginados(
    esLiberacion: boolean,
    page: number,
    pageSize: number,
    filter: string,
    order?: { column: string; type: string }
  ): Observable<Object> {
    const params = new HttpParams({
      fromObject: {
        esLiberacion: esLiberacion,
        page: page,
        pageSize: pageSize,
        filter: filter,
        order: order != null ? order.column + '_' + order.type : '',
      },
    });
    return this.http.get(environment.apiUrl + '/Muestreos', { params });
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
      environment.apiUrl + '/Muestreos/GetDistinctValuesFromColumn',
      { params }
    );
  }

  enviarMuestreosRevision(muestreos: any): Observable<Object> {
    return this.http.put(environment.apiUrl + '/muestreos', muestreos);
  }

  obtenerResumenPorGpoParametros(muestreos: any): Observable<Object> {
    const params = new HttpParams({
      fromObject: { muestreos: muestreos },
    });

    return this.http.get(
      environment.apiUrl + '/Muestreos/ResumenResultadosPorMuestreo',
      { params }
    );
  }

  exportarResultadosExcel(muestreos: Array<Muestreo> = []): Observable<Blob> {
    return this.http.post(
      environment.apiUrl + '/Muestreos/ExportarExcel',
      muestreos,
      { responseType: 'blob' }
    );
  }

  eliminarMuestreos(muestreos: Array<number>): Observable<any> {
    const options = { body: muestreos };
    return this.http.delete(environment.apiUrl + '/muestreos', options);
  }

  deleteByFilter(filter: string): Observable<any> {
    return this.http.delete(
      environment.apiUrl + '/muestreos/deleteall?filter=' + filter
    );
  }

  exportarCargaResultadosEbaseca(
    muestreos: Array<Muestreo> = []
  ): Observable<Blob> {
    return this.http.post(
      environment.apiUrl + '/Muestreos/ExportarCargaResultadosEbaseca',
      muestreos,
      { responseType: 'blob' }
    );
  }

  exportAllEbaseca(
    esLiberacion: boolean,
    filter: string,
    muestreos: Array<number>
  ): Observable<Blob> {
    return this.http.post(
      environment.apiUrl +
        '/Muestreos/ExportarEbasecaExcel?esLiberacion=' +
        esLiberacion +
        (filter.length == 0 ? '' : '&filter=' + filter),
      muestreos,
      { responseType: 'blob' }
    );
  }

  enviarMuestreoaAcumulados(estatusId: number, muestreos: Array<number>) {
    let datos = { estatusId: estatusId, muestreos: muestreos };
    return this.http.put(
      environment.apiUrl + '/Muestreos/cambioEstatusMuestreos',
      datos
    );
  }

  enviarTodosMuestreosAcumulados(estatus: number, filter: string) {
    let datos = { Status: estatus, Filter: filter };
    return this.http.put(
      environment.apiUrl + '/Muestreos/ActualizaEstatusMuestreos',
      datos
    );
  }

  //Administracion de monitoreos
  exportarMuestreos(muestreos: Array<Muestreo> = []): Observable<Blob> {
    return this.http.post(
      environment.apiUrl + '/Muestreos/ExportarMuestreosAdministracion',
      muestreos,
      { responseType: 'blob' }
    );
  }

  //Administracion de monitoreos
  exportarResultados(
    muestreos: Array<acumuladosMuestreo> = []
  ): Observable<Blob> {
    return this.http.post(
      environment.apiUrl + '/Muestreos/ExportarResultadosAdministracion',
      muestreos,
      { responseType: 'blob' }
    );
  }

  //Administracion de monitoreos totales

  obtenerTotalesAdministracion() {
    return this.http.get<any>(
      environment.apiUrl + '/Muestreos/obtenerTotalesAdministracion'
    );
  }
}
