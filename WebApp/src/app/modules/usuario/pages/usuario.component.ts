import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../../interfaces/registro.interface';
import { UserService } from '../services/user.service';
import { Column } from '../../../interfaces/filter/column';
import { BaseService } from '../../../shared/services/base.service';
import { MuestreoService } from '../../muestreo/liberacion/services/muestreo.service';
import { Item } from '../../../interfaces/filter/item';
import { Notificacion } from '../../../shared/models/notification-model';
import { NotificationService } from '../../../shared/services/notification.service';
import { NotificationType } from '../../../shared/enums/notification-type';
import { idPerfiles } from 'src/app/shared/enums/idPerfiles';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent extends BaseService implements OnInit {
  notificacion: Notificacion = {
    title: 'Cambiar estatus',
    text: '¿Está seguro de cambiar el estatus del usuario ?',
  };
  correctoFormatoEmail: boolean = false;
  public usuario: Usuario = {
    id: 0,
    nombre: '',
    activo: false,
    userName: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    email: '',
    perfilId: 0,
    cuencaId: null,
    direccionLocalId: null,
    nombrePerfil: ''
  };
  public perfiles: any[] = [];
  public organismoCuenca: any[] = [];
  public direccionL: any[] = [];
  public Usuarios2: Usuario[] = [];
  idPerfilOC: number = idPerfiles.OC;
  idPerfilDL: number = idPerfiles.DL;

  constructor(
    private userService: UserService,
    public muestreoService: MuestreoService,
    private notificationService: NotificationService) {
    super();
  }
  //Propiedades Modal
  modalTitle: string = "";
  editar: boolean = false;

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'numero',
        label: 'N°',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'NombreCompleto',
        label: 'NOMBRE',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'username',
        label: 'USUARIO',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'email',
        label: 'CORREO',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'NombrePerfil',
        label: 'PERFIL',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'Activo',
        label: 'ESTATUS',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
      {
        name: 'opcion',
        label: 'OPCIONES',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
      },
    ];
    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  ngOnInit() {
    this.definirColumnas();
    this.setDefaults();
    this.obtenerUsuarios();
    this.cargarCombos();
  }

  obtenerUsuarios() {
    this.userService.getAllusers().subscribe(result => {
      this.Usuarios2 = result.data;
    }, error => console.error(error));
  }

  cargarCombos() {
    this.userService.getPerfiles().subscribe(result => {
      this.perfiles = result.data;     
    }, error => console.error(error));

    this.userService.getDLocales().subscribe(direcciones => {
      this.direccionL = direcciones.data;
    }, error => console.log(error));

    this.userService.getCuencas().subscribe(result => {
      this.organismoCuenca = result.data;
    }, error => console.error(error));
  }

  setDefaults() {   
  }

  RegistrarUsuario() {
    this.modalTitle = "Registrar Usuario";
    this.editar = false;
  }

  validarObligatorios(): boolean {
    let obligatoriosRegistro: any[] = [this.usuario.nombre, this.usuario.perfilId, this.usuario.userName, this.usuario.email];
    if (this.usuario.perfilId == this.idPerfilOC) { obligatoriosRegistro.push(this.usuario.cuencaId); }
    else if (this.usuario.perfilId == this.idPerfilDL) { obligatoriosRegistro.push(this.usuario.direccionLocalId); }
    return (obligatoriosRegistro.includes("") || obligatoriosRegistro.includes(0)) ? false : true;
  }

  AddUser() {
    if (!this.validarObligatorios()) {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'Debe de llenar todos los campos obligatorios para su registro',
      });
    }
    else {
      this.userService.adduser(this.usuario).subscribe({
        next: (response: any) => {       
          this.loading = true;
          if (response.succeded) {
            this.obtenerUsuarios();
            this.loading = false;
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Usuario registrado exitosamente',
            });
          }
        }
      });
    }
  }

  Update() {
    this.userService.update(this.usuario.id, this.usuario).subscribe({
      next: (response: any) => {
        this.loading = true;
        if (response.succeded) {
          this.obtenerUsuarios();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Usuario actualizado exitosamente',
          });
        }
      }
    });

  }

  UpdateUser(users: Usuario) {
    this.usuario = users;
    this.modalTitle = "Actualizar Usuario";
    this.editar = true;
  }

  UpdateEstatusUser(users: Usuario) {
    this.usuario = users;
    this.usuario.activo = !users.activo;

  }

  Change() {
    this.userService.update(this.usuario.id, this.usuario).subscribe({
      next: (response: any) => {       
        this.loading = true;
        if (response.succeded) {
          this.obtenerUsuarios();
          this.loading = false;
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.success,
            text: 'Se a cambiado el estatus del usuario correctamente',
          });
        }
      }
    });
  }

  Cancelar() {
  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles

    let filteredColumns = this.getFilteredColumns(); //Obtenemos la lista de columnas que están filtradas
    this.muestreoService.filtrosSeleccionados = filteredColumns; //Actualizamos la lista de filtros, para el componente de filtro
    this.filtros = filteredColumns;

    this.obtenerLeyendaFiltroEspecial(column.dataType); //Se define el arreglo opcionesFiltros dependiendo del tipo de dato de la columna para mostrar las opciones correspondientes de filtrado

    let esFiltroEspecial = this.IsCustomFilter(column);

    if (
      (!column.filtered && !this.existeFiltrado) ||
      (column.isLatestFilter && this.filtros.length == 1)
    ) {
      this.cadena = '';
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }

    if (this.requiresToRefreshColumnValues(column)) {
      this.userService
        .getDistinctValuesFromColumn(column.name, this.cadena)
        .subscribe({
          next: (response: any) => {           
            column.data = response.data.map((register: any) => {              
              let item: Item = {           
                value: register,
                checked: true,
              };
              return item;
            });

            column.filteredData = column.data;          
            this.ordenarAscedente(column.filteredData);
            this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
          },
          error: (error) => { },
        });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };

    //this.muestreoService
    //  .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
    //    column: column,
    //    type: type,
    //  })
    //  .subscribe({
    //    next: (response: any) => {
    //      this.muestreos = response.data;
    //    },
    //    error: (error) => { },
    //  });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.obtenerUsuarios();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.obtenerUsuarios();

    this.columns
      .filter((x) => x.isLatestFilter)
      .map((m) => {
        m.isLatestFilter = false;
      });

    if (!isFiltroEspecial) {
      columna.filtered = true;
      columna.isLatestFilter = true;
    } else {
      this.columns
        .filter((x) => x.name == this.columnaFiltroEspecial.name)
        .map((m) => {
          (m.filtered = true),
            (m.selectedData = this.columnaFiltroEspecial.selectedData),
            (m.isLatestFilter = true);
        });
    }

    this.esHistorial = true;
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }

  pageClic(page: any) {
    /* this.consultarMonitoreos(page, this.NoPage, this.cadena);*/
    this.page = page;
  }

  validarCaracteres(evento: any): void {
    const pattern: RegExp = /^([a-zA-ZÀ-ÿ_\u00f1\u00d1\s])$/;
    if (!pattern.test(evento.key)) {
      evento.preventDefault();
    }
  }

  validarFormatoCorreo(evento: any): void {
    const pattern: RegExp = /^([a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3})$/;
    this.correctoFormatoEmail = (pattern.test(evento.target.value)) ? true : false;
  }
}

