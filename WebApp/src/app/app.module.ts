import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpInterceptorService } from './core/interceptors/http-interceptor.service';

//MODULOS
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
//COMPONENTES
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/login/pages/login.component';
import { NavRootComponent } from './shared/navigation/nav-root/nav-root.component';
import { UsuarioComponent } from './modules/usuario/pages/usuario.component';
import { HomeComponent } from './modules/home/pages/home.component';
import { LoadingComponent } from './shared/loading/loading.component';
import { GlobalerrorHandlerServiceService } from './core/services/globalerror-handler-service.service';
import { NotFoundComponent } from './core/pages/not-found/not-found.component';
import { NavChildComponent } from './shared/navigation/nav-child/nav-child.component';
import { CargaComponent } from './modules/muestreo/liberacion/pages/carga/carga.component';
import { ValidadosComponent } from './modules/muestreo/revision/OCDL/resultados-validados/pages/validados/validados.component';
import { TablaComponent } from './shared/tablaGral/tabla/tabla.component';
import { ResumenComponent } from './modules/muestreo/revision/OCDL/resultados-resumen/pages/resumen/resumen.component';
import { AlertComponent } from './shared/alert/alert.component';
import { TotalComponent } from './modules/muestreo/revision/OCDL/resultados-total/pages/total/total.component';
import { MuestreosTotalesComponent } from './modules/muestreo/liberacion/components/muestreos-totales/muestreos-totales.component';
import {EstadoComponent} from './modules/catalogos/estados/pages/estado.component';
import { MunicipiosComponent } from './modules/catalogos/municipios/pages/municipios.component';
import { LocalidadComponent } from './modules/catalogos/localidades/page/localidad.component';
import { CargaResultadosComponent } from './modules/muestreo/carga/pages/carga-resultados/carga-resultados.component';
import { ReplicaDiferenteComponent } from './modules/muestreo/aprobacion/replica-diferente/pages/replica-diferente/replica-diferente.component';
import { ReplicaResumenComponent } from './modules/muestreo/aprobacion/replica-resumen/pages/replica-resumen/replica-resumen.component';
import { ReplicaTotalComponent } from './modules/muestreo/aprobacion/replicas-total/pages/replica-total/replica-total.component';
import { RevisionResultadoComponent } from './modules/muestreo/aprobacion/revision-resultado/pages/revision-resultado.component';
import { TotalSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-total/pages/total-secaia/total-secaia.component';
import { ResumenSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-resumen/pages/resumen-secaia/resumen-secaia.component';
import { ValidadosSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-validados/pages/validados-secaia/validados-secaia.component';
import { FormatoResultadoComponent } from './modules/muestreo/formatoResultado/pages/formato-resultado.component';
import { EvidenciasComponent } from './modules/muestreo/evidencias/pages/evidencias/evidencias.component';
import { ConsultaEvidenciaComponent } from './modules/muestreo/originalesAprobados/consulta-evidencia/pages/consulta-evidencia.component';
import { ConsultaResultadoComponent } from './modules/muestreo/originalesAprobados/consulta-resultado/pages/consulta-resultado.component';
import { ValidacionReglasComponent } from './modules/muestreo/validacion/pages/validacion-reglas/validacion-reglas.component';
import { InicialReglasComponent } from './modules/muestreo/validacion/pages/inicial-reglas/inicial-reglas.component';
import { AcumulacionResultadosComponent } from './modules/muestreo/validacion/pages/acumulacion-resultados/acumulacion-resultados.component';
import { ReglasValidarComponent } from './modules/muestreo/validacion/pages/reglas-validar/reglas-validar.component';
import { ResumenReglasComponent } from './modules/muestreo/validacion/pages/resumen-reglas/resumen-reglas.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavRootComponent,
    UsuarioComponent,
    HomeComponent,
    LoadingComponent,
    NotFoundComponent,
    NavChildComponent,
    CargaComponent,
    ValidadosComponent,
    TablaComponent,
    ResumenComponent,
    AlertComponent,
    TotalComponent,
    MuestreosTotalesComponent,
    EstadoComponent,
    MunicipiosComponent,
    LocalidadComponent,
    CargaResultadosComponent,
    ReplicaDiferenteComponent,
    ReplicaResumenComponent,
    ReplicaTotalComponent,
    RevisionResultadoComponent,
    CargaResultadosComponent,
    TotalSecaiaComponent,
    ResumenSecaiaComponent,
    ValidadosSecaiaComponent,
    FormatoResultadoComponent,
    EvidenciasComponent,
    ConsultaEvidenciaComponent,
    ConsultaResultadoComponent,
    ValidacionReglasComponent,
    InicialReglasComponent,
    AcumulacionResultadosComponent,
    ReglasValidarComponent,
    ResumenReglasComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule, NgxPaginationModule,
    AutocompleteLibModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi:true},
              {provide: ErrorHandler, useClass: GlobalerrorHandlerServiceService}],
  bootstrap: [AppComponent]
})
export class AppModule { }
