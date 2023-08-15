import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/modules/login/services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class LimitesService {
  constructor(private http: HttpClient, public authService: AuthService) {}

  sustituirLimites(parametrosSustitucion: any) {
    let formData = new FormData();

    formData.append('usuario', parametrosSustitucion.usuario);
    formData.append('archivo', parametrosSustitucion.archivo);
    formData.append('periodo', parametrosSustitucion.periodo);
    formData.append('origenLimites', parametrosSustitucion.origenLimites);

    return this.http.post(environment.apiUrl + '/Limites', formData);
  }

  obtenerMuestreosSustituidos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Limites');
  }

  getResultadosParametrosEstatus(idEstatus: number) {
    let params = new HttpParams({
      fromObject: {
        estatusId: idEstatus,
        userId: this.authService.getUser().usuarioId,
      },
    });
    return this.http.get(
      environment.apiUrl + '/Resultados/ResultadosParametrosEstatus',
      { params }
    );
  }

  exportarResumenExcel(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Limites/ExportarExcel', {
      responseType: 'blob',
    });
  }

  obtenerMuestreos(): Observable<Object> {
    return this.http.get(environment.apiUrl + '/Muestreos/ProgramaAnios');
  }

  validarSustitucionPrevia(periodo: number): Observable<Object> {
    let params = new HttpParams({
      fromObject: { periodo: periodo },
    });
    return this.http.get(
      environment.apiUrl + '/Limites/ExisteSustitucionPrevia',
      { params }
    );
  }

  cargaMuestreosEmergencia(archivoMuestreos: File, anio: string, reemplazar?: string) {
    let formData = new FormData();
    formData.append('archivo', archivoMuestreos, archivoMuestreos.name);
    formData.append('anio', anio);
    formData.append('reemplazar', reemplazar??'');
    
    return this.http.post(
      environment.apiUrl + '/Muestreos/CargaEmergencias',
      formData
    );
  }
}
