import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../modules/login/services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './nav-root.component.html',
  styleUrls: ['./nav-root.component.css'],
})
export class NavRootComponent implements OnInit {
  public paginas: any[] = [];
  public paginasRoot: any[] = [];
  public usuario: any;
  public paginaAdminUsuario: any;
 
  constructor(private http: HttpClient, private authService: AuthService) {}
  
  mostrar(idPagina: number) {
    let hijo = document.getElementById('elemento-hijo' + idPagina);
    if (hijo != null) {
      hijo.classList.add('show');
      hijo.classList.add('primer-nivel');
      hijo.classList.remove('hide');
    }
  }
  ocultar(idPagina: number) {
    let hijo = document.getElementById('elemento-hijo' + idPagina);
    if (hijo != null) {
      hijo.classList.remove('show');
      hijo.classList.add('hide');
    }
  }

  cerrarSesion() {
    this.authService.logout();
  }

  ngOnInit(): void {
    this.usuario = this.authService.getUser();
    let idPerfil = localStorage.getItem('perfil');
    let perfilId = localStorage.getItem('perfilId');
    this.http
    .get<any>(environment.apiUrl + '/paginas/' + idPerfil)
    .subscribe((response) => {
      this.paginas = response.data;
      const idPaginaAdministradorUsuarios: number = 28;
      this.paginaAdminUsuario = this.paginas.filter((f) => f.id == idPaginaAdministradorUsuarios);
      this.paginasRoot = this.paginas.filter((f) => f.idPaginaPadre == null && f.id != idPaginaAdministradorUsuarios);
      });
  }
}
