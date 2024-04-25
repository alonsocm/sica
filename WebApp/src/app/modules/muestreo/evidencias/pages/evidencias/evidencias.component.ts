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
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { filter } from 'rxjs';

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

  initialValue: string = '';
  esfilrofoco: string = '';
  opctionFiltro: string = '';
  existeFiltrado: boolean = false;
  cadena: string = '';
  esHistorial: boolean = false;
  columns: Array<Column> = [];

  cabeceroSeleccionado: boolean = false;

  //Selección de registros
  selectAllOption: boolean = false;
  allSelected: boolean = false;
  selectedPage: boolean = false;
  orderBy: { column: string; type: string } = { column: '', type: '' };

  constructor(
    private evidenciasService: EvidenciasService,
    private muestreoService: MuestreoService,
    private usuario: AuthService,
    private fb: FormBuilder
  ) {
    super();
    this.registroParam = this.fb.group({
      chkFiltro: new FormControl(),
      chckAllFiltro: new FormControl(),
      aOrdenarAZ: new FormControl(),
    });
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
          this.selectedPage = this.anyUnselected() ? false : true;
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
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
        filteredDataFiltrado: [],
        datype: '',
      },
    ];

    this.columns = nombresColumnas;
  }

  public establecerValoresFiltrosTabla(column: Column) {
    console.log(this.muestreos);

    if (!column.filtered && !this.existeFiltrado) {
      this.muestreoService.getDistinctValuesFromColumn(column.name).subscribe({
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
        },
        error: (error) => {},
      });
    } else if (!column.filtered && this.existeFiltrado) {
      column.data = this.muestreos.map((m: any) => {
        let item: Item = {
          value: m[column.name],
          checked: true,
        };
        return item;
      });
      column.filteredData = column.data;
      const distinctThings = column.filteredData.filter(
        (thing, i, arr) => arr.findIndex((t) => t.value === thing.value) === i
      );

      column.filteredData = distinctThings.sort();
      this.ordenarAscedente(column.filteredData);
    }

    //filtrados
    column.filteredDataFiltrado = this.muestreos.map((m: any) => {
      let item: Item = {
        value: m[column.name],
        checked: true,
      };
      return item;
    });
    column.filteredDataFiltrado = column.data;
    const distinctThings = column.filteredDataFiltrado.filter(
      (thing, i, arr) => arr.findIndex((t) => t.value === thing.value) === i
    );

    column.filteredDataFiltrado = distinctThings.sort();
    this.ordenarAscedente(column.filteredDataFiltrado);
  }

  ordenarAscedente(column: Array<Item>) {
    column.sort(function (a: any, b: any) {
      if (a.value > b.value) {
        return 1;
      }
      if (a.value < b.value) {
        return -1;
      }
      return 0;
    });
  }

  existeEvidencia(evidencias: Array<Evidencia>, sufijoEvidencia: string) {
    if (evidencias.length == 0) {
      return false;
    }
    return evidencias.find((f) => f.sufijo == sufijoEvidencia);
  }

  descargarEvidencia(claveMuestreo: string, sufijo: string) {
    this.loading = !this.loading;
    let muestreo = this.muestreos.find(
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
    if (this.muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'No ha seleccionado ningún monitoreo',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }

    this.loading = true;
    this.evidenciasService
      .descargarArchivos(this.muestreosSeleccionados.map((m) => m.muestreoId))
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          this.seleccionarTodosChck = false;
          this.muestreosSeleccionados.map((m) => (m.isChecked = false));
          FileService.download(response, 'evidencias.zip');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.muestreosSeleccionados.map((m) => (m.isChecked = false));
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.hacerScroll();
        },
      });

    this.resetValues();
    this.unselectMuestreos();
  }

  private resetValues() {
    this.muestreosSeleccionados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectMuestreos() {
    this.muestreos.forEach((m) => (m.isChecked = false));
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

  onFiltroCabecero(val: any, columna: Column) {
    let criterioBusqueda = val.target.value;
    columna.filteredData = columna.data;
    columna.filteredData = columna.filteredData?.filter(
      (f) =>
        f.value.toLowerCase().indexOf(criterioBusqueda.toLowerCase()) !== -1
    );
  }

  seleccionarFiltro(columna: Column): void {}

  filtrar(columna: Column) {
    this.existeFiltrado = true;
    let filtrosSeleccionados = columna.filteredData?.filter((x) => x.checked);
    let opciones = filtrosSeleccionados
      .map((x) => x.value)
      .toString()
      .replaceAll(',', '_');

    if (this.cadena.indexOf(columna.name) != -1) {
      this.cadena =
        this.cadena.indexOf('%') != -1 ? this.eliminarFiltro(columna) : '';
    }

    this.cadena =
      this.cadena != ''
        ? this.cadena + '%' + columna.name + '_' + opciones
        : columna.name + '_' + opciones;
    this.consultarMonitoreos();
    columna.filtered = true;
    //this.establecerValoresFiltrosTabla(columna);
    this.esHistorial = true;
  }

  validarExisteFiltrado(): boolean {
    return this.columns.filter((x) => x.filtered == true).length > 0
      ? true
      : false;
  }

  eliminarFiltro(columna: Column): string {
    let cadenaanterior = this.cadena.split('%');
    let repetidos = cadenaanterior.filter((x) => x.includes(columna.name));
    let indexx = cadenaanterior.indexOf(repetidos.toString());
    cadenaanterior.splice(indexx, 1);
    return (this.cadena = cadenaanterior.toString().replaceAll(',', '%'));
  }

  onSpecialFiltersClick($event: MouseEvent, columnName: string) {
    let dropDown = document.getElementById(
      'filters-' + columnName
    ) as HTMLElement;

    if (dropDown.className === 'd-none') {
      dropDown.className = 'd-block';
    } else {
      dropDown.className = 'd-none';
    }
    $event.stopPropagation();
  }

  onSelectPageClick() {
    this.muestreos.map((m) => {
      m.isChecked = this.selectedPage;

      //Buscamos el registro en los seleccionados
      let index = this.muestreosSeleccionados.findIndex(
        (d) => d.muestreoId === m.muestreoId
      );

      if (index == -1) {
        //No existe en seleccionados, lo agremos
        this.muestreosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        //Existe y el seleccionar página está deshabilitado, lo eliminamos, de los seleccionados
        this.muestreosSeleccionados.splice(index, 1);
      }
    });

    if (this.selectAllOption && !this.selectedPage) {
      this.selectAllOption = false;
      this.allSelected = false;
    } else if (!this.selectAllOption && this.selectedPage) {
      this.selectAllOption = true;
    }

    this.getSummary();
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

  anyUnselected() {
    return this.muestreos.some((f) => !f.isChecked);
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
        f.isChecked = true;
      }
    });
  }

  onSelectClick(muestreo: Muestreo) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (muestreo.isChecked) {
      this.muestreosSeleccionados.push(muestreo);
      this.selectedPage = this.anyUnselected() ? false : true;
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

  onSelectAllPagesClick() {
    this.allSelected = true;
  }

  onUnselectAllClick() {
    this.allSelected = false;
  }
}
