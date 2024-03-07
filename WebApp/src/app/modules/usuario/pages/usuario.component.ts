import { Component, Input, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Usuario } from '../../../interfaces/registro.interface';
import { Respuesta } from '../../../interfaces/respuesta.interface';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';



@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css'] 
})
export class UsuarioComponent implements OnInit {
  
  public page: number | undefined ;
  registro: FormGroup; 
  errorMessage: any;
  public keyword = "nombre";
  public keywordP = "nombre";

  @Input() respuesta: Respuesta={
    succeded:false,
    message: '',
    errors:'',
    data:[]    
  }

  public usuario!: Usuario;
  public perfiles: any[] = [];
  public organismoCuenca: any[] = [];
  public direccionL: any[] = [];
  public Usuarios: any[] = [];
  public Usuarios2: any[] =[];
  public UsuarioId: number = 0;
  public active: boolean = false;

  constructor(private http: HttpClient, private router: Router,
    private fb: FormBuilder, private userService: UserService) {

    this.registro = this.fb.group({     
      usuario: ['', Validators.required],
      userRed: ['', Validators.required],     
      email: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]],
      perfil: [[null], Validators.required],
      organismoCuenca: [null],
      direccionL: [null],
    });   

  }
  //Propiedades Modal
  modalTitle: string = "";
  editar: boolean = false;
  
  ngOnInit() {
    this.setDefaults();


    this.userService.getAllusers().subscribe(result => {          
      this.Usuarios = result.data;
      this.Usuarios2 = result.data;    
    }, error => console.error(error));

    this.userService.getPerfiles().subscribe(result => {
      
      this.perfiles = result.data;
      console.log("perfil combo");
      console.log(this.perfiles);
    }, error => console.error(error));

    this.userService.getDLocales().subscribe(direcciones => {
        this.direccionL = direcciones.data;       
      }, error => console.log(error));

    this.userService.getCuencas().subscribe(result => {
      this.organismoCuenca = result.data;    
    }, error => console.error(error));
    
  }


  //Variables para los cambios de estado del buscador
  onChangeSearch(val: string='') {  
      this.Usuarios2 = this.Usuarios.filter(x => x.nombre.toLowerCase().indexOf(val.toLowerCase()) !==-1);            
  }
  selectEvent(item: any) {  
    this.Usuarios2 = this.Usuarios.filter(x => x.nombre == item.nombre);
  }
  onChangeSearch1(val: string = '') {
    this.Usuarios2 = this.Usuarios.filter(x => x.nombrePerfil.toLowerCase().indexOf(val.toLowerCase()) !== -1);
  }
  selectEvent1(item: any) {
    this.Usuarios2 = this.Usuarios.filter(x => x.nombrePerfil == item.nombre);
  }

 
  setDefaults() {
    this.registro.get("perfil")?.patchValue(null);
    this.registro.get("organismoCuenca")?.patchValue(null);
    this.registro.get("direccionL")?.patchValue(null);   
  }


  RegistrarUsuario() {   
    this.modalTitle = "Registrar Usuario";
    this.editar = false;
    
  }

  AddUser() { 
    const valor = this.registro.value;
    this.usuario = {
      Id:0,
      Activo: true,
      UserName: valor.userRed,
      Nombre: valor.usuario,
      ApellidoPaterno: valor.usuario,
      ApellidoMaterno: valor.usuario,
      Email: valor.email,
      PerfilId: valor.perfil ,
      CuencaId: valor.organismoCuenca,
      DireccionLocalId: valor.direccionL
    };   
    this.userService.adduser(this.usuario).subscribe({
      next: (response) => {
        var showAdd = document.getElementById('add-success');
        if (showAdd) {

          showAdd.style.display = "block";

          this.ngOnInit();
        }
        setTimeout(function () {
          if (showAdd) {
            showAdd.style.display = "none";
          }
        }, 2500);
      },
      error: (error) => {
        var showAdd = document.getElementById('error-danger');
        if (showAdd) {

          showAdd.style.display = "block";
          
        }
        setTimeout(function () {
          if (showAdd) {
            showAdd.style.display = "none";
          }
        }, 2500);

      }

    });
  }


  Update() {  
    const valor = this.registro.value;  
    this.usuario = {
      Id: this.UsuarioId,
      Activo: this.active,
      UserName: valor.userRed,
      Nombre: valor.usuario,
      ApellidoPaterno: valor.usuario,
      ApellidoMaterno: valor.usuario,
      Email: valor.email,
      PerfilId: valor.perfil,
      CuencaId: valor.perfil ==8 ? valor.organismoCuenca : null,
      DireccionLocalId: valor.perfil ==9 ? valor.direccionL : null
    };
    
    this.userService.update(this.usuario.Id, this.usuario).subscribe(
      
    response => {
        var showUpdate = document.getElementById('update-success');
        if (showUpdate) {

            showUpdate.style.display = "block";
            this.ngOnInit();
        }
        setTimeout(function () {
          if (showUpdate) {
            showUpdate.style.display = "none";
          }
        }, 2500);
      } 

    );
  }

  UpdateUser(users: any) {
    
    this.modalTitle = "Actualizar Usuario";
    this.editar = true;   
    this.registro = this.fb.group({
      usuario: users.nombre,
      userRed: users.userName,
      email: users.email,
      perfil: users.perfilId,
      organismoCuenca: users.cuencaId,
      direccionL: users.direccionLocalId
    });
    this.UsuarioId = users.id;
    this.active = users.activo;  
  }

  ChangeStatus(users: any) {
    this.modalTitle = "Cambiar Estatus";  
    this.usuario = {
      Id: users.id,
      Activo: (users.activo ==true)? false: true,
      UserName: users.userRed,
      Nombre: users.usuario,
      ApellidoPaterno: users.usuario,
      ApellidoMaterno: users.usuario,
      Email: users.email,
      PerfilId: users.perfilId,
      CuencaId: users.organismoCuenca,
      DireccionLocalId: users.direccionL
    };
  }

  Change() {

    this.userService.update(this.usuario.Id, this.usuario).subscribe(

      response => {
        var showUpdate = document.getElementById('change-success');
        if (showUpdate) {

          showUpdate.style.display = "block";
          this.ngOnInit();
        }
        setTimeout(function () {
          if (showUpdate) {
            showUpdate.style.display = "none";
          }
        }, 2500);
      }

    );
  }

  Cancelar() {
    this.registro.reset();
  }
  
}


