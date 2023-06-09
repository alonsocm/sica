import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/login/pages/login.component';
import { UsuarioComponent } from './modules/usuario/pages/usuario.component';
import { HomeComponent } from './modules/home/pages/home.component'
import { AuthGuardService } from './core/guards/auth-guard.service';
import { NavRootComponent } from './shared/navigation/nav-root/nav-root.component';
import { NotFoundComponent } from './core/pages/not-found/not-found.component';
import { CargaComponent } from './modules/muestreo/liberacion/pages/carga/carga.component';
import { ResumenComponent } from './modules/muestreo/revision/OCDL/resultados-resumen/pages/resumen/resumen.component';
import { ValidadosComponent } from './modules/muestreo/revision/OCDL/resultados-validados/pages/validados/validados.component';
import { TotalComponent } from './modules/muestreo/revision/OCDL/resultados-total/pages/total/total.component';
import { EstadoComponent } from './modules/catalogos/estados/pages/estado.component';
import { MunicipiosComponent }from './modules/catalogos/municipios/pages/municipios.component';
import { LocalidadComponent } from './modules/catalogos/localidades/page/localidad.component';
import { CargaResultadosComponent } from './modules/muestreo/carga/pages/carga-resultados/carga-resultados.component';
import { RevisionResultadoComponent } from './modules/muestreo/aprobacion/revision-resultado/pages/revision-resultado.component';
import { ReplicaDiferenteComponent } from './modules/muestreo/aprobacion/replica-diferente/pages/replica-diferente/replica-diferente.component';
import { ReplicaResumenComponent } from './modules/muestreo/aprobacion/replica-resumen/pages/replica-resumen/replica-resumen.component';
import { ReplicaTotalComponent } from './modules/muestreo/aprobacion/replicas-total/pages/replica-total/replica-total.component';
import { TotalSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-total/pages/total-secaia/total-secaia.component';
import { ValidadosSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-validados/pages/validados-secaia/validados-secaia.component';
import { ResumenSecaiaComponent } from './modules/muestreo/revision/SECAIA/resultados-resumen/pages/resumen-secaia/resumen-secaia.component';
import { FormatoResultadoComponent } from './modules/muestreo/formatoResultado/pages/formato-resultado.component';
import { EvidenciasComponent } from './modules/muestreo/evidencias/pages/evidencias/evidencias.component';
import { ConsultaResultadoComponent } from './modules/muestreo/originalesAprobados/consulta-resultado/pages/consulta-resultado.component';
import { ConsultaEvidenciaComponent } from './modules/muestreo/originalesAprobados/consulta-evidencia/pages/consulta-evidencia.component';
import { ValidacionReglasComponent } from './modules/muestreo/validacion/pages/validacion-reglas/validacion-reglas.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch:'full' },
  { path: 'login', component: LoginComponent },
  { path: 'usuario', component: UsuarioComponent, children: [{path:'', outlet:'menu', component: NavRootComponent}], canActivate: [AuthGuardService] },
  { path: 'home', component: HomeComponent, children: [{path:'', outlet:'menu', component: NavRootComponent}], canActivate: [AuthGuardService] },
  { path: 'liberacion-muestreo', component: CargaComponent, children: [{path:'', outlet:'menu', component: NavRootComponent}], canActivate: [AuthGuardService]},
  { path: 'resumen', component: ResumenComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'validados-muestreo', component: ValidadosComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }] },
  { path: 'revision-totales', component: TotalComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService]},
  { path: 'estados', component: EstadoComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'municipios', component: MunicipiosComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'localidades', component: LocalidadComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }] , canActivate: [AuthGuardService]},
  { path: 'carga-resultados', component: CargaResultadosComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'revision-resultado', component: RevisionResultadoComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'replica-total', component: ReplicaTotalComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },  
  { path: 'revision-totalesSecaia', component: TotalSecaiaComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'revision-validadosSecaia', component: ValidadosSecaiaComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'revision-resumenSecaia', component: ResumenSecaiaComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'replica-diferente', component: ReplicaDiferenteComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'replica-resumen', component: ReplicaResumenComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'formato-resultado', component: FormatoResultadoComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'evidencias', component: EvidenciasComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'consulta-resultado', component: ConsultaResultadoComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'consulta-evidencia', component: ConsultaEvidenciaComponent, children: [{ path: '', outlet: 'menu', component: NavRootComponent }], canActivate: [AuthGuardService] },
  { path: 'validacion-reglas', component: ValidacionReglasComponent, children: [{path: '', outlet: 'menu', component: NavRootComponent}]},
  { path: '**', component: NotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
