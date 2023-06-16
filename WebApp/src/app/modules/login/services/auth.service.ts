import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/interfaces/usuario.inteface';
const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private http: HttpClient, private router: Router) {
  }

  login(userName: string, password: string) {
   return this.http.post<any>(environment.apiUrl + '/usuarios/authenticate', { userName, password });        
  }

  public setSession(authResult: any) {
    localStorage.setItem('token', authResult.data.jwToken);
    localStorage.setItem('perfil', helper.decodeToken(authResult.data.jwToken).perfil);
    localStorage.setItem('idUsuario', authResult.data.id);
    localStorage.setItem('userName', authResult.data.userName);
  }

  public datosUsuario() {
    return this.http.get<any>(environment.apiUrl + '/usuarios/' + localStorage.getItem('idUsuario'))
  }

  public getUser(): Usuario{
    let usuario: Usuario = {
      usuarioId: Number(localStorage.getItem('idUsuario')),
      perfil: String(localStorage.getItem('perfil')),
      nombreUsuario: String(localStorage.getItem('userName')),
      nombrePerfil: String(localStorage.getItem('perfil')),
    };
    
    return usuario;
  }

  public logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("perfil");
    localStorage.removeItem("userName");
    localStorage.removeItem("perfil");
    localStorage.removeItem("idUsuario")
    this.router.navigate(['/login']);
  }

  public isLoggedIn() {
    const token = localStorage.getItem('token');
    if (token && !helper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }
}
