import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Evidencia, Muestreo } from 'src/app/interfaces/Muestreo.interface';
import { Respuesta } from 'src/app/interfaces/respuesta.interface';
import { AuthService } from 'src/app/modules/login/services/auth.service';
import { Perfil } from 'src/app/shared/enums/perfil';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { BaseService } from 'src/app/shared/services/base.service';
import { FileService } from 'src/app/shared/services/file.service';
import { EvidenciasService } from '../../services/evidencias.service';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { Column } from 'src/app/interfaces/filter/column';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { NotificationType } from 'src/app/shared/enums/notification-type';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { Subscription } from 'rxjs';
import { FiltroHistorialService } from '../../../../../shared/services/filtro-historial.service';

@Component({
  selector: 'app-evidencias',
  templateUrl: './evidencias.component.html',
  styleUrls: ['./evidencias.component.css'],
})
export class EvidenciasComponent extends BaseService implements OnInit {
  muestreos: Array<Muestreo> = [];
  muestreosSeleccionados: Array<Muestreo> = [];
  perfil: string = '';

  registroParam: FormGroup;

  esfilrofoco: string = '';
  opctionFiltro: string = '';
  imgSrc: string = '';

  //Variables para manejar la funcionalidad de reemplazar evidencias, individualmente.
  reemplazar: boolean = false;
  nombreEvidencia: string = '';
  @ViewChild('fileUpload') inputFiles: ElementRef = {} as ElementRef;
  @ViewChild('fileReplace') inputFileReplace: ElementRef = {} as ElementRef;

  filtroHistorialServiceSub: Subscription;
  constructor(
    private filtroHistorialService: FiltroHistorialService,
    private evidenciasService: EvidenciasService,
    public muestreoService: MuestreoService,
    private usuario: AuthService,
    private fb: FormBuilder,
    private notificationService: NotificationService
  ) {
    super();
    this.filtroHistorialServiceSub =
      this.filtroHistorialService.columnName.subscribe((columnName) => {
        this.deleteFilter(columnName);
        this.consultarMonitoreos();
      });
    this.filtroHistorialService.columnaFiltroEspecial.subscribe(
      (dato: Column) => {
        if (dato.specialFilter != null) this.filtrar(dato, true);
      }
    );
    this.registroParam = this.fb.group({
      chkFiltro: new FormControl(),
      chckAllFiltro: new FormControl(),
      aOrdenarAZ: new FormControl(),
    });
  }

  ngOnInit(): void {
    this.muestreoService.filtrosSeleccionados = [];
    this.perfil = this.usuario.getUser().nombrePerfil;
    this.definirColumnas();

    //Implementación de lo de Alonso
    this.consultarMonitoreos();
  }

  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ) {
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected(this.muestreos) ? false : true;
        },
        error: (error) => {},
      });
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'noEntrega',
        label: 'N° ENTREGA',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'claveSitioOriginal',
        label: 'CLAVE SITIO ORIGINAL',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'claveSitio',
        label: 'CLAVE SITIO',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'claveMonitoreo',
        label: 'CLAVE MONITOREO',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'nombreSitio',
        label: 'NOMBRE SITIO',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'ocdl',
        label: 'OC/DL',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'cuerpoAgua',
        label: 'CUERPO DE AGUA',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'tipoCuerpoAguaOriginal',
        label: 'TIPO CUERPO AGUA ORIGINAL',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO AGUA',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'tipoSitio',
        label: 'TIPO SITIO',
        order: 10,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'laboratorio',
        label: 'LABORATORIO',
        order: 11,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'laboratorioSubrogado',
        label: 'LABORATORIO SUBROGADO',
        order: 12,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'fechaProgramada',
        label: 'FECHA PROGRAMACIÓN',
        order: 13,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'fechaRealizacion',
        label: 'FECHA REALIZACIÓN',
        order: 14,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'programaAnual',
        label: 'AÑO',
        order: 15,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'horaInicio',
        label: 'HORA INICIO MUESTREO',
        order: 16,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'horaFin',
        label: 'HORA FIN MUESTREO',
        order: 17,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'observaciones',
        label: 'OBSERVACIONES',
        order: 18,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'fechaCargaEvidencias',
        label: 'FECHA CARGA EVIDENCIAS A SICA',
        order: 19,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: '',
        selectedData: '',
      },
      {
        name: 'numeroCargaEvidencias',
        label: 'NÚMERO DE CARGA DE EVIDENCIAS',
        order: 20,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        selectedData: '',
      },
    ];

    this.columns = nombresColumnas;
    this.setHeadersList(this.columns);
  }

  existeEvidencia(evidencias: Array<Evidencia>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  onPreviewOrDownloadFileClick(claveMuestreo: string, sufijo: string) {
    this.loading = !this.loading;
    let muestreo = this.muestreos.find(
      (x) => x.claveMonitoreo == claveMuestreo
    );
    let evidencia = muestreo?.evidencias.find((x) => x.sufijo == sufijo);

    this.evidenciasService
      .descargarArchivo(evidencia?.nombreArchivo ?? '')
      .subscribe({
        next: (response: Blob) => {
          this.loading = !this.loading;
          if (['A', 'M', 'S'].findIndex((x) => x === sufijo) != -1) {
            const reader = new FileReader();
            reader.onloadend = () => {
              this.imgSrc = reader.result as string;
            };
            reader.readAsDataURL(response);

            let titutloEvidencia = document.getElementById(
              'tituloEvidencia'
            ) as HTMLHtmlElement;
            titutloEvidencia.textContent = evidencia?.nombreArchivo ?? '';

            document.getElementById('btn-img-modal')?.click();
          } else if (['D', 'E'].findIndex((x) => x === sufijo) != -1) {
            const url = URL.createObjectURL(response);
            window.open(url);
          } else {
            FileService.download(response, evidencia?.nombreArchivo ?? '');
          }
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
    if (this.muestreosSeleccionados.length === 0) {
      this.notificationService.updateNotification({
        show: true,
        text: 'No ha seleccionado ningún monitoreo',
        type: NotificationType.warning,
      });
      return this.hacerScroll();
    }

    this.loading = true;
    this.evidenciasService
      .descargarArchivos(this.muestreosSeleccionados.map((m) => m.muestreoId))
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          this.seleccionarTodosChck = false;
          this.muestreosSeleccionados.map((m) => (m.selected = false));
          FileService.download(response, 'evidencias.zip');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.muestreosSeleccionados.map((m) => (m.selected = false));
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.hacerScroll();
        },
      });

    this.resetSelection();
    this.unselectMuestreos();
  }

  private resetSelection() {
    this.muestreosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectMuestreos() {
    this.muestreos.forEach((m) => (m.selected = false));
  }

  cargarEvidencias(
    event: Event,
    reemplazar: boolean = false,
    nombreEvidencia: string = ''
  ) {
    let evidencias = (event.target as HTMLInputElement).files ?? new FileList();
    let errores = this.validarTamanoArchivos(evidencias);

    if (
      reemplazar === true &&
      nombreEvidencia.toLowerCase() != evidencias[0].name.toLocaleLowerCase()
    ) {
      errores =
        'El nombre o tipo de archivo, no coincide con el de la evidencia a reemplazar.';
    }

    if (errores !== '') {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'Se encontraron errores en las evidencias seleccionadas: ' + errores,
      });
    }

    this.loading = !this.loading;
    this.evidenciasService.cargarEvidencias(evidencias, reemplazar).subscribe({
      next: (response: Respuesta) => {
        this.resetInputFile(this.inputFileReplace);
        this.resetInputFile(this.inputFiles);
        this.notificationService.updateNotification({
          show: true,
          type: NotificationType.success,
          text: 'Evidencias cargadas correctamente',
        });
        this.loading = !this.loading;
      },
      error: (response: any) => {
        this.resetInputFile(this.inputFileReplace);
        this.resetInputFile(this.inputFiles);
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
    // this.columnas.forEach((f) => {
    //   f.filtro.selectedValue = 'Seleccione';
    // });
    // this.filtrar();
    // this.filtros.forEach((element: any) => {
    //   element.clear();
    // });
    // document.getElementById('dvMessage')?.click();
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

  seleccionarFiltro(columna: Column): void {}

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarMonitoreos();

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
    this.columnaFiltroEspecial.optionFilter = '';
    this.columnaFiltroEspecial.specialFilter = '';
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.muestreoService
      .obtenerMuestreosPaginados(false, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.muestreos = response.data;
        },
        error: (error) => {},
      });
  }

  getSummary() {
    this.muestreoService.muestreosSeleccionados = this.muestreosSeleccionados;
  }

  getPreviousSelected(
    muestreos: Array<Muestreo>,
    muestreosSeleccionados: Array<Muestreo>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.muestreoId === x.muestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  onSelectClick(muestreo: Muestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.selected) {
      this.muestreosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected(this.muestreos) ? false : true;
    } else {
      let index = this.muestreosSeleccionados.findIndex(
        (m) => m.muestreoId === muestreo.muestreoId
      );

      if (index > -1) {
        this.muestreosSeleccionados.splice(index, 1);
      }
    }

    this.getSummary();
  }

  pageClic(page: any) {
    this.consultarMonitoreos(page, this.NoPage, this.cadena);
    this.page = page;
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarMonitoreos();
  }

  onReplaceEvidenciaClick(claveMuestreo: string, sufijo: string) {
    let muestreo = this.muestreos.find(
      (x) => x.claveMonitoreo == claveMuestreo
    );

    let evidencia =
      muestreo?.evidencias.find((x) => x.sufijo == sufijo)?.nombreArchivo ?? '';

    this.reemplazar = true;
    this.nombreEvidencia = evidencia;
    document.getElementById('fileReplace')?.click();
  }

  onDeleteEvidenciasClick() {
    if (!this.allSelected && this.muestreosSeleccionados.length === 0) {
      this.notificationService.updateNotification({
        show: true,
        text: 'Debe seleccionar al menos un monitoreo para eliminar',
        type: NotificationType.warning,
      });
      return this.hacerScroll();
    }

    document.getElementById('btnMdlConfirmacion')?.click();
  }

  onConfirmDeleteEvidenciasClick() {
    this.loading = true;
    if (this.allSelected) {
      this.evidenciasService.deleteEvidenciasByFilter(this.cadena).subscribe({
        next: (response) => {
          this.resetValues();
        },
        error: (error) => {
          this.loading = false;
        },
      });
    } else {
      if (this.muestreosSeleccionados.length === 0) {
        this.notificationService.updateNotification({
          show: true,
          text: 'Debe seleccionar al menos un monitoreo',
          type: NotificationType.warning,
        });
        return this.hacerScroll();
      }

      let muestreosEliminar = this.muestreosSeleccionados.map(
        (s) => s.claveMonitoreo
      );
      this.evidenciasService.deleteEvidencias(muestreosEliminar).subscribe({
        next: (response) => {
          this.resetValues();
        },
        error: (error) => {
          this.loading = false;
        },
      });
    }
  }

  private resetValues() {
    this.consultarMonitoreos();
    this.notificationService.updateNotification({
      show: true,
      text: 'Evidencias eliminadas correctamente',
      type: NotificationType.success,
    });
    this.resetSelection();
    this.hacerScroll();
    this.loading = false;
  }
}
