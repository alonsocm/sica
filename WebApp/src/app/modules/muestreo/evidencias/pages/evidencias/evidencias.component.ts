import { Component, OnInit, ViewChild } from '@angular/core';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { Evidencia, Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Respuesta } from 'src/app/interfaces/respuesta.interface';
import { AuthService } from 'src/app/modules/login/services/auth.service';
import { Perfil } from 'src/app/shared/enums/perfil';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { BaseService } from 'src/app/shared/services/base.service';
import { FileService } from 'src/app/shared/services/file.service';
import { EvidenciasService } from '../../services/evidencias.service';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';

@Component({
  selector: 'app-evidencias',
  templateUrl: './evidencias.component.html',
  styleUrls: ['./evidencias.component.css'],
})
export class EvidenciasComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosFiltrados: Array<Muestreo> = [];
  perfil: string = '';

  constructor(
    private evidenciasService: EvidenciasService,
    private muestreoService: MuestreoService,
    private usuario: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.perfil = this.usuario.getUser().nombrePerfil;
    this.definirColumnas();

    //anterior
    //this.evidenciasService.obtenerMuestreos().subscribe({
    //  next: (response: any) => {
    //    this.muestreos = response.data;
    //    this.muestreosFiltrados = this.muestreos;
    //    this.establecerValoresFiltrosTabla();
    //  },
    //  error: (error) => {},
    //});


    //Implementación de lo de Alonso
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, '')
      .subscribe({
        next: (response: any) => {
          console.log(response);
          //this.totalItems = response.totalRecords;
          this.muestreos = response.data;
          this.muestreosFiltrados = this.muestreos;
        },
        error: (error) => { },
      });


  }

  definirColumnas() {
    let nombresColumnas: Array<Columna> = [
      {
        nombre: 'noEntrega',
        etiqueta: 'N° ENTREGA',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitioOriginal',
        etiqueta: 'CLAVE SITIO ORIGINAL',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 5,
        filtro: new Filter(),
      },
      { nombre: 'ocdl', 
        etiqueta: 'OC/DL', 
        orden: 6, 
        filtro: new Filter() },
      {
        nombre: 'cuerpoAgua',
        etiqueta: 'CUERPO DE AGUA',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoSitio',
        etiqueta: 'TIPO SITIO',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 11,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorioSubrogado',
        etiqueta: 'LABORATORIO SUBROGADO',
        orden: 12,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaProgramada',
        etiqueta: 'FECHA PROGRAMACIÓN',
        orden: 13,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 14,
        filtro: new Filter(),
      },
      {
        nombre: 'programaAnual',
        etiqueta: 'AÑO',
        orden: 15,
        filtro: new Filter(),
      },
      {
        nombre: 'horaInicio',
        etiqueta: 'HORA INICIO MUESTREO',
        orden: 16,
        filtro: new Filter(),
      },
      {
        nombre: 'horaFin',
        etiqueta: 'HORA FIN MUESTREO',
        orden: 17,
        filtro: new Filter(),
      },
      {
        nombre: 'observaciones',
        etiqueta: 'OBSERVACIONES',
        orden: 18,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaCargaEvidencias',
        etiqueta: 'FECHA CARGA EVIDENCIAS A SICA',
        orden: 19,
        filtro: new Filter(),
      },
      {
        nombre: 'numeroCargaEvidencias',
        etiqueta: 'NÚMERO DE CARGA DE EVIDENCIAS',
        orden: 20,
        filtro: new Filter(),
      },
    ];

    this.columnas = nombresColumnas;
  }

  private establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.muestreosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  seleccionarTodos(): void {
    this.muestreosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    this.obtenerSeleccionados();
  }

  obtenerSeleccionados(): Array<Muestreo> {
    return this.muestreosFiltrados.filter((f) => f.isChecked);
  }

  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
  }

  filtrar() {
    this.muestreosFiltrados = this.muestreos;
    this.columnas.forEach((columna) => {
      this.muestreosFiltrados = this.muestreosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });

    this.establecerValoresFiltrosTabla();
  }

  existeEvidencia(evidencias: Array<Evidencia>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  descargarEvidencia(claveMuestreo: string, sufijo: string) {
    this.loading = !this.loading;
    let muestreo = this.muestreosFiltrados.find(
      (x) => x.claveMonitoreo == claveMuestreo
    );
    let nombreEvidencia = muestreo?.evidencias.find((x) => x.sufijo == sufijo);

    this.evidenciasService
      .descargarArchivo(nombreEvidencia?.nombreArchivo ?? '')
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          FileService.download(response, nombreEvidencia?.nombreArchivo ?? '');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }

  descargarEvidencias() {
    let muestreosSeleccionados = this.obtenerSeleccionados().map((m) => {
      return m.muestreoId;
    });

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'No ha seleccionado ningún monitoreo',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.evidenciasService.descargarArchivos(muestreosSeleccionados).subscribe({
      next: (response: any) => {
        console.table(response);
        this.loading = !this.loading;
        this.seleccionarTodosChck = false;
        this.muestreosFiltrados.map((m) => (m.isChecked = false));
        FileService.download(response, 'evidencias.zip');
      },
      error: (response: any) => {
        this.loading = !this.loading;
        this.muestreosFiltrados.map((m) => (m.isChecked = false));
        this.mostrarMensaje(
          'No fue posible descargar la información',
          TipoMensaje.Error
        );
        this.hacerScroll();
      },
    });
  }

  cargarEvidencias(event: Event) {
    let evidencias = (event.target as HTMLInputElement).files ?? new FileList();
    let errores = this.validarTamanoArchivos(evidencias);

    if (errores !== '') {
      return this.mostrarMensaje(
        'Se encontraron errores en las evidencias seleccionadas',
        TipoMensaje.Alerta
      );
    }

    this.loading = !this.loading;
    this.evidenciasService.cargarEvidencias(evidencias).subscribe({
      next: (response: Respuesta) => {
        this.mostrarMensaje(
          'Evidencias cargadas correctamente',
          TipoMensaje.Correcto
        );
        this.loading = !this.loading;
      },
      error: (response: any) => {
        this.loading = !this.loading;
        let archivo = this.generarArchivoDeErrores(
          response.error.Errors.toString()
        );
        FileService.download(archivo, 'errores.txt');
      },
    });
  }

  validarTamanoArchivos(archivos: FileList): string {
    let error: string = '';
    for (let index = 0; index < archivos.length; index++) {
      const element = archivos[index];
      if (element.size === 0) {
        error += 'El archivo ' + element.name + ' está vacío,';
      }
    }
    return error;
  }

  limpiarFiltros() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }

  mostrarColumna(nombreColumna: string): boolean {
    let mostrar: boolean = true;

    if (nombreColumna === 'No. Entrega') {
      let usuario = this.usuario.getUser();

      if (usuario.perfil === Perfil.ADMINISTRADOR) {
        mostrar = true;
      }
    }

    return mostrar;
  }

  mostrarCampo(): boolean {
    return (
      this.perfil ===
        (Perfil.ADMINISTRADOR || Perfil.SECAIA1 || Perfil.SECAIA2) ?? true
    );
  }
}
