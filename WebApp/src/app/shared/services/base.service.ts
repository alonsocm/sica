import { ElementRef, Injectable, ViewChild, ViewChildren } from '@angular/core';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Column } from 'src/app/interfaces/filter/column';
import { acumuladosMuestreo } from '../../interfaces/acumuladosMuestreo.interface';
import { Item } from '../../interfaces/filter/item';
import {
  filtrosEspeciales,
  filtrosEspecialesFecha,
  filtrosEspecialesNumeral,
  mustreoExpression,
} from '../enums/filtrosEspeciales';

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  public page: number = 1;
  public NoPage: number = 30;
  public pageSize: number = 30;
  public totalItems: number = 0;

  //#region variables para la selección
  selectedPage: boolean = false;
  selectAllOption: boolean = false;
  allSelected: boolean = false;
  //#endregion

  noRegistro: string = 'No se encontraron registros';
  filtroResumen: string = 'Seleccionar filtro';
  keyword: string = 'label';
  tipoAlerta: string = '';
  mensajeAlerta: string = '';

  loading: boolean = false;
  mostrarAlerta: boolean = false;
  seleccionarTodosChck: boolean = false;

  columnas: Array<Columna> = [];
  columnasF: Array<Column> = [];

  resultadosFiltradosn: Array<any> = [];

  resultadosn: Array<any> = [];
  sufijos: Array<string> = ['A', 'C', 'D', 'E', 'M', 'O', 'R', 'S', 'V'];

  @ViewChild('mensajes') mensajes: any;
  @ViewChildren('filtros') filtros: any;

  filtradoEspecial: filtrosEspeciales = new filtrosEspeciales();
  filtradoEspecialNumeral: filtrosEspecialesNumeral =
    new filtrosEspecialesNumeral();
  filtradoEspecialFecha: filtrosEspecialesFecha = new filtrosEspecialesFecha();
  mustreoExpression: mustreoExpression = new mustreoExpression();
  columnaFiltroEspecial: Column = {
    name: 'estatus',
    label: 'ESTATUS',
    order: 1,
    selectAll: true,
    filtered: false,
    asc: false,
    desc: false,
    data: [],
    filteredData: [],
    dataType: 'string',
    optionFilter: '',
    secondOptionFilter: '',
    specialFilter: '',
    secondSpecialFilter: '',
    selectedData: '',
  };
  indicesopcionesFiltros: Array<number> = []; //Indices para indicar en que posicion se pone linea divisora
  opcionesFiltros: Array<string> = []; //Arreglo para submenu de filtro especial conforme al tipo de la columna string/number/date
  opcionesFiltrosModal: Array<string> = []; //Arreglo para combo en modal autofiltro personalizado conforme al tipo de la columna string/number/date
  columns: Array<Column> = [];
  cadena: string = '';
  filtroEspecializado: string = 'Filtro personalizado';
  filtroEspecialEquals: string = 'Es igual a';

  //variable para almacenar el ordenamiento
  orderBy: { column: string; type: string } = { column: '', type: '' };
  esHistorial: boolean = false;
  existeFiltrado: boolean = false;

  esfiltrofoco: string = '';
  cabeceroSeleccionado: boolean = false;
  headers: Array<any> = []; //Listado de cabeceros utilizado en el drop para redirigir al usuario al cabecero seleccionado

  opcionFiltrar: string = ''; //variable para guardar la opcion a filtrar en filtro especial
  leyendaFiltrosEspeciales: string = ''; //Leyenda para indicar si es filtro de texto/número/fecha
  numeroCarga: string = '';
  anioOperacion: string = '';
  initialValue: string = '';

  existeEliminacionFiltro: boolean = false;

  currentHeaderFocus = '';
  isAceptarNotificacion: boolean = false;
  showFilterSpinner: boolean = false;

  mostrarMensaje(mensaje: string, tipo: string): void {
    this.mensajeAlerta = mensaje;
    this.tipoAlerta = tipo;
    this.mostrarAlerta = true;
    setTimeout(() => {
      this.mostrarAlerta = false;
    }, 10000);
  }

  hacerScroll() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }

  generarArchivoDeErrores(errores: string) {
    const blob = new Blob([String(errores).replaceAll(',', '\n')], {
      type: 'application/octet-stream',
    });

    return blob;
  }

  filtrarn() {
    this.resultadosFiltradosn = this.resultadosn;
    this.columnas.forEach((columna) => {
      this.resultadosFiltradosn = this.resultadosFiltradosn.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.establecerValoresFiltrosTablan();
  }

  establecerValoresFiltrosTablan() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltradosn.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  }

  limpiarFiltrosn() {
    this.columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrarn();
    document.getElementById('dvMessage')?.click();
    this.establecerValoresFiltrosTablan();
  }

  seleccionarAll(resultadosFiltrados: Array<any>): void {
    resultadosFiltrados.map((m) => {
      m.selected = this.seleccionarTodosChck ? true : false;
    });
  }

  seleccionarAllFiltro(columna: Column): void {
    columna.data.map((m) => {
      m.checked = columna.selectAll ? true : false;
    });
  }

  //sustituye a obtenerseleccionados()
  Seleccionados(Seleccionados: Array<any>) {
    return Seleccionados.filter((f) => f.selected);
  }

  descargarArchivo(file: any, fileName: string) {
    const downloadInstance = document.createElement('a');
    downloadInstance.href = URL.createObjectURL(file);
    downloadInstance.target = '_blank';
    downloadInstance.download = fileName;

    document.body.appendChild(downloadInstance);
    downloadInstance.click();
    document.body.removeChild(downloadInstance);
  }

  resetInputFile(input: ElementRef) {
    input.nativeElement.value = null;
  }

  seleccionarn(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
  }

  //SI SE OCUPA
  onFiltroCabecero(val: any, columna: Column) {
    let criterioBusqueda = val.target.value;
    columna.filteredData = columna.data;
    columna.filteredData = columna.filteredData?.filter(
      (f) =>
        f.value.toLowerCase().indexOf(criterioBusqueda.toLowerCase()) !== -1
    );
  }

  mostrarModalFiltro(opcion: string, columna: Column) {
    switch (opcion) {
      case this.filtradoEspecial.personalizado:
        columna.optionFilter = this.filtradoEspecial.equals;
        break;
      case this.filtradoEspecialNumeral.between:
        columna.optionFilter =
          columna.dataType == 'number'
            ? this.filtradoEspecialNumeral.greaterthanorequalto
            : this.filtradoEspecialFecha.beforeorequal;
        columna.secondOptionFilter =
          columna.dataType == 'number'
            ? this.filtradoEspecialNumeral.lessthanorequalto
            : this.filtradoEspecialFecha.afterorequal;
        break;
      default:
        columna.optionFilter = opcion;
        columna.secondOptionFilter = '';
        break;
    }
    this.columnaFiltroEspecial = columna;
    let selectData = this.columnaFiltroEspecial.selectedData.split('_');

    if (selectData.length == 2) {
      this.columnaFiltroEspecial.secondOptionFilter =
        opcion === this.filtradoEspecial.personalizado
          ? this.filtradoEspecial.equals
          : '';
      this.columnaFiltroEspecial.specialFilter =
        opcion === this.filtradoEspecial.personalizado ? selectData[0] : '';
      this.columnaFiltroEspecial.secondSpecialFilter =
        opcion === this.filtradoEspecial.personalizado ? selectData[1] : '';
    } else if (selectData.length == 1) {
      this.columnaFiltroEspecial.specialFilter =
        this.columnaFiltroEspecial.selectedData;
    }

    this.opcionesFiltrosModal = this.opcionesFiltros;
    columna.dataType == 'string'
      ? this.opcionesFiltrosModal.splice(this.opcionesFiltrosModal.length - 1)
      : this.opcionesFiltrosModal.splice(this.opcionesFiltrosModal.length - 2);
  }

  deleteFilter(columnName: string): string {
    let index = this.columns.findIndex((f) => f.name == columnName);
    this.columns[index].filtered = false;
    this.columns[index].isLatestFilter = false;
    this.columns[index].selectedData = '';

    this.columns[index].optionFilter = '';
    this.columns[index].secondOptionFilter = '';
    this.columns[index].specialFilter = '';
    this.columns[index].secondSpecialFilter = '';

    this.existeEliminacionFiltro = true;

    let cadenaanterior = this.cadena.split('%');

    let repetidos = cadenaanterior.filter((x) => x.includes(columnName));
    let indexx = cadenaanterior.indexOf(repetidos.toString());
    cadenaanterior.splice(indexx, 1);
    let cadenaAnterior = '';

    if (cadenaanterior.length > 0) {
      cadenaanterior.forEach((x) => {
        cadenaAnterior += x.concat('%');
      });
      cadenaAnterior = cadenaAnterior.substring(
        0,
        cadenaAnterior.lastIndexOf('%')
      );
    }

    let nuevoUltimoFiltro = '';

    if (cadenaAnterior != '') {
      if (cadenaAnterior.indexOf('%') !== -1) {
        //Existe más de un filtro
        let ultimoFiltro =
          cadenaAnterior.split('%')[cadenaAnterior.split('%').length - 1]; // Con esto obtenemos el último elemento de la cadena

        if (ultimoFiltro.indexOf('[') !== -1) {
          //Existe un filtro de tipo parámetro
          nuevoUltimoFiltro = ultimoFiltro
            .replace('[', '')
            .replace(']', '')
            .split('*')[0];
        } else {
          //No existe filtro de parámetro
          nuevoUltimoFiltro = ultimoFiltro.split('_')[0];
        }
      } else {
        //Solo es un filtro
        if (cadenaAnterior.indexOf('[') !== -1) {
          //Existe un filtro de tipo parámetro
          nuevoUltimoFiltro = cadenaAnterior
            .replace('[', '')
            .replace(']', '')
            .split('*')[0];
        } else {
          //No existe filtro de parámetro
          nuevoUltimoFiltro = cadenaAnterior.split('_')[0];
        }
      }

      let indexNuevoUltimoFiltro = this.columns.findIndex(
        (f) => f.name == nuevoUltimoFiltro
      );

      //let nuevoUltimoFiltro = '';
      // nuevoUltimoFiltro =
      //   cadenaAnterior.indexOf('%') != -1
      //     ? cadenaAnterior.split('%')[cadenaAnterior.split('%').length - 1]
      //     : cadenaAnterior.split('_')[0];
      // let indexNuevoUltimoFiltro = this.columns.findIndex(
      //   (f) => f.name == nuevoUltimoFiltro.split('_')[0]
      // );
      this.columns[indexNuevoUltimoFiltro].isLatestFilter = true;
      this.existeFiltrado = this.validarExisteFiltrado();
    }
    return (this.cadena = cadenaAnterior.toString());
  }

  validarExisteFiltrado(): boolean {
    return this.columns.filter((x) => x.filtered == true).length > 0
      ? true
      : false;
  }

  obtenerLeyendaFiltroEspecial(dataType: string): void {
    switch (dataType) {
      case 'string':
        this.opcionesFiltros = Object.entries(this.filtradoEspecial).map(
          (i) => i[1]
        );
        this.leyendaFiltrosEspeciales = 'Filtros de texto';
        this.indicesopcionesFiltros = [1, 3, 5];
        break;
      case 'number':
        this.opcionesFiltros = Object.entries(this.filtradoEspecialNumeral).map(
          (i) => i[1]
        );
        this.leyendaFiltrosEspeciales = 'Filtros de número';
        this.indicesopcionesFiltros = [1, 6];
        break;
      case 'date':
        this.opcionesFiltros = Object.entries(this.filtradoEspecialFecha).map(
          (i) => i[1]
        );
        this.leyendaFiltrosEspeciales = 'Filtros de fecha';
        this.indicesopcionesFiltros = [0, 5];
        break;
      default:
        this.opcionesFiltros = Object.entries(this.filtradoEspecial).map(
          (i) => i[1]
        );
        this.leyendaFiltrosEspeciales = 'Filtros de texto';
        break;
    }
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

  getPreseleccionFiltradoColumna(column: Column, esFiltroEspecial: boolean) {
    if (column.isLatestFilter)
      column.filteredData.forEach((m) => {
        m.checked = esFiltroEspecial
          ? false
          : column.selectedData.includes(m.value)
            ? true
            : false;
      });
  }

  onSelectAllPagesClick() {
    this.allSelected = true;
 
  }

  onUnselectAllClick() {
    this.allSelected = false;
    
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

  obtenerFiltroEspecial(valor: string | undefined): string {
    switch (valor) {
      case this.filtradoEspecial.beginswith:
        this.opcionFiltrar = this.mustreoExpression.beginswith;
        break;
      case this.filtradoEspecial.endswith:
        this.opcionFiltrar = this.mustreoExpression.endswith;
        break;
      case this.filtradoEspecial.contains:
        this.opcionFiltrar = this.mustreoExpression.contains;
        break;
      case this.filtradoEspecial.equals:
        this.opcionFiltrar = this.mustreoExpression.equals;
        break;
      case this.filtradoEspecial.notcontains:
        this.opcionFiltrar = this.mustreoExpression.notcontains;
        break;
      case this.filtradoEspecial.notequals:
        this.opcionFiltrar = this.mustreoExpression.notequals;
        break;

      case this.filtradoEspecialNumeral.greaterthan:
        this.opcionFiltrar = this.mustreoExpression.greaterthan;
        break;
      case this.filtradoEspecialNumeral.greaterthanorequalto:
        this.opcionFiltrar = this.mustreoExpression.greaterthanorequalto;
        break;
      case this.filtradoEspecialNumeral.lessthan:
        this.opcionFiltrar = this.mustreoExpression.lessthan;
        break;
      case this.filtradoEspecialNumeral.lessthanorequalto:
        this.opcionFiltrar = this.mustreoExpression.lessthanorequalto;
        break;

      case this.filtradoEspecialFecha.before:
        this.opcionFiltrar = this.mustreoExpression.before;
        break;
      case this.filtradoEspecialFecha.after:
        this.opcionFiltrar = this.mustreoExpression.after;
        break;
      case this.filtradoEspecialFecha.beforeorequal:
        this.opcionFiltrar = this.mustreoExpression.beforeorequal;
        break;
      case this.filtradoEspecialFecha.afterorequal:
        this.opcionFiltrar = this.mustreoExpression.afterorequal;
        break;

      default:
        break;
    }
    return this.opcionFiltrar;
  }

  obtenerCadena(columna: Column, isFiltroEspecial: boolean): string {
    if (
      this.cadena.indexOf(
        !isFiltroEspecial ? columna.name : this.columnaFiltroEspecial.name
      ) != -1
    ) {
      this.cadena =
        this.cadena.indexOf('%') != -1
          ? this.deleteFilter(
            !isFiltroEspecial ? columna.name : this.columnaFiltroEspecial.name
          )
          : '';
    }

    if (!isFiltroEspecial) {
      let valoresSeleccionados = columna.filteredData?.filter((x) => x.checked);
      columna.selectedData = '';

      if (columna.parameter) {
        this.cadena = this.cadena != '' ? this.cadena.concat('%[') : '[';
        this.cadena += columna.name + '*';

        valoresSeleccionados.forEach((x) => {
          this.cadena += x.value;
          this.cadena += '*';
          columna.selectedData += x.value.concat('_');
        });

        this.cadena = this.cadena.substring(0, this.cadena.lastIndexOf('*'));
        this.cadena = this.cadena.concat(']');
      } else {
        valoresSeleccionados.forEach((x) => {
          columna.selectedData += x.value.concat('_');
        });

        columna.selectedData = columna.selectedData.substring(
          0,
          columna.selectedData.lastIndexOf('_')
        );

        this.cadena =
          this.cadena != ''
            ? this.cadena.concat(
              '%' + columna.name + '_' + columna.selectedData
            )
            : columna.name.concat('_' + columna.selectedData);
      }
    } else {
      this.opcionFiltrar = this.obtenerFiltroEspecial(
        this.columnaFiltroEspecial.optionFilter
      );
      let cadenaEspecial =
        this.columnaFiltroEspecial.name +
        '_*' +
        this.opcionFiltrar +
        '_' +
        this.columnaFiltroEspecial.specialFilter;
      this.cadena =
        this.cadena != ''
          ? this.cadena.concat('%' + cadenaEspecial)
          : cadenaEspecial;
      this.columnaFiltroEspecial.selectedData =
        this.columnaFiltroEspecial.selectedData?.concat(
          this.columnaFiltroEspecial.specialFilter + ','
        );

      if (this.columnaFiltroEspecial.secondSpecialFilter != '') {
        this.opcionFiltrar = this.obtenerFiltroEspecial(
          this.columnaFiltroEspecial.secondOptionFilter
        );
        let cadenaEspecial =
          this.columnaFiltroEspecial.name +
          '_*' +
          this.opcionFiltrar +
          '_' +
          this.columnaFiltroEspecial.secondSpecialFilter;
        this.cadena = this.cadena.concat('%' + cadenaEspecial);
        this.columnaFiltroEspecial.selectedData =
          this.columnaFiltroEspecial.selectedData?.concat(
            this.columnaFiltroEspecial.specialFilter + ','
          );
      }
    }
    return this.cadena;
  }

  //#region Funciones que manejan el mostrar/ocultar del dropdown de los filtros por columna
  onCancelarFiltroClick() {
    // this.hideColumnFilter();
  }

  public showColumnFilter(columnName: string) {
    let dropdown = document.getElementById('dd-' + columnName) as HTMLElement;
    dropdown.classList.add('show');
    let dropdownMenu = document.getElementById(
      'dd-menu-' + columnName
    ) as HTMLElement;
    dropdownMenu.classList.add('show');
  }

  public hideColumnFilter() {
    if (this.currentHeaderFocus != '') {
      let dropdown = document.getElementById(
        'dd-' + this.currentHeaderFocus
      ) as HTMLElement;
      dropdown.classList.remove('show');

      let dropdownMenu = document.getElementById(
        'dd-menu-' + this.currentHeaderFocus
      ) as HTMLElement;
      dropdownMenu.classList.remove('show');
    }
  }

  public setHeadersList(columns: Array<Column>) {
    this.headers = columns.map((m) => ({
      label: m.label,
      name: m.name,
    }));
  }

  onSelectPageClick(muestreos: Array<any>, muestreosSeleccionados: Array<any>) {
    console.log(muestreos);
    console.log(muestreosSeleccionados);

    muestreos.map((m) => {
      m.selected = this.selectedPage;

      //Buscamos el registro en los seleccionados
      let index = muestreosSeleccionados.findIndex(
        (d) => d.muestreoId === m.muestreoId
      );

      if (index == -1) {
        //No existe en seleccionados, lo agremos
        muestreosSeleccionados.push(m);
      } else if (!this.selectedPage) {
        //Existe y el seleccionar página está deshabilitado, lo eliminamos, de los seleccionados
        muestreosSeleccionados.splice(index, 1);
      }
    });

    this.showOrHideSelectAllOption();

    //this.getSummary();
  }

  public showOrHideSelectAllOption() {
    if (this.selectAllOption && !this.selectedPage) {
      this.selectAllOption = false;
      this.allSelected = false;
    } else if (!this.selectAllOption && this.selectedPage) {
      this.selectAllOption = true;
    }
  }

  anyUnselected(muestreos: Array<any>) {
    return muestreos.some((f) => !f.selected);
  }

  public getFilteredColumns() {
    let filtrosActuales = this.columns
      .filter((f) => f.filtered)
      .map((m) => ({
        name: m.name,
        label: m.label,
      }));

    return filtrosActuales;
  }

  public collapseFilterOptions() {
    let columsdesplegadas = document.getElementsByClassName('d-block');
    for (var i = 0; i < columsdesplegadas.length; i++) {
      columsdesplegadas[i].className = 'd-none';
    }
  }

  requiresToRefreshColumnValues(column: Column) {
    if (
      (!column.filtered && this.existeFiltrado) ||
      (column.filtered && !column.isLatestFilter) ||
      (!column.filtered && !this.existeFiltrado) ||
      (column.isLatestFilter && this.filtros.length == 1)
    ) {
      return true;
    }
    return false;
  }

  IsCustomFilter(column: Column) {
    return column.optionFilter === undefined ||
      column.optionFilter === this.filtroEspecialEquals
      ? false
      : true;
  }

  onLimpiarFiltrosClick() {
    window.location.reload();
  }

  validarCorreRegla(muestreo: acumuladosMuestreo): string {
    return muestreo.correReglaValidacion = (muestreo.cumpleReglasCondic == "NO") ? "NO" : (((muestreo.muestreoCompletoPorResultados == "SI" || muestreo.autorizacionIncompleto) && (muestreo.cumpleFechaEntrega == "SI" || muestreo.autorizacionFechaEntrega)) ? "SI" : "NO")
  }

  validarCaracteres(evento: any): void {
    const pattern: RegExp = /^([a-zA-ZÀ-ÿ_\u00f1\u00d1\s])$/;
    if (!pattern.test(evento.key)) {
      evento.preventDefault();
    }
  }
    //Función para validar caracteres  
  validarCaracteres2(evento: any): void {
    const pattern: RegExp = /^([a-zA-ZÀ-ÿ_\u00f1\u00d1\s0-9\.\,\-\/\(\)\:\;\'\"\?\&\%\$\€\+=\*\#\~\[\]\{\}\|\^\`\<\>\|\@])/; 
    if (!pattern.test(evento.key)) {
      evento.preventDefault();
    }
  }
}
