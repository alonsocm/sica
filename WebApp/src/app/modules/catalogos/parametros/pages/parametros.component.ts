import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ParametrosService } from '../services/parametros.service';
import { Parametro } from '../models/parametro';
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NotificationType } from 'src/app/shared/enums/notification-type';

@Component({
  selector: 'app-parametros',
  templateUrl: './parametros.component.html',
  styleUrls: ['./parametros.component.css'],
})
export class ParametrosComponent extends BaseService implements OnInit {
  @ViewChild('inputExcelParametros') inputExcelMonitoreos: ElementRef =
    {} as ElementRef;

  registro: Parametro = {
    selected: false,
    id: 0,
    clave: '',
    descripcion: '',
    unidadMedida: '',
    unidadMedidaId: 0,
    grupo: '',
    grupoId: 0,
    subGrupo: '',
    subgrupoId: 0,
    parametroPadre: '',
    parametroPadreId: 0,
    orden: 0,
  };

  parametroForm = new FormGroup({
    clave: new FormControl(this.registro.clave, Validators.required),
    nombre: new FormControl(this.registro.descripcion, Validators.required),
    tipo: new FormControl(this.registro.grupoId ?? 0, [
      Validators.required,
      Validators.min(1),
    ]),
    subgrupo: new FormControl(this.registro.subgrupoId ?? 0, [
      Validators.required,
      Validators.min(1),
    ]),
    unidadMedida: new FormControl(this.registro.unidadMedidaId ?? 0, [
      Validators.required,
      Validators.min(1),
    ]),
  });

  registros: Array<Parametro> = [];
  registrosSeleccionados: Array<Parametro> = [];
  grupos: any;
  subgrupos: any;
  unidadesMedida: any;
  initialValueForm: any;
  fileList: any;
  operation: string = '';

  get form(): { [key: string]: AbstractControl } {
    return this.parametroForm.controls;
  }

  constructor(
    private parametrosService: ParametrosService,
    private notificationService: NotificationService
  ) {
    super();
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.getParametros();
    this.getUnidadesMedida();
    this.getGrupos();
    this.getSubgrupos();
    this.initialValueForm = this.parametroForm.value;
  }

  definirColumnas() {
    let columnas: Array<Column> = [
      {
        name: 'clave',
        label: 'CLAVE',
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
        name: 'descripcion',
        label: 'NOMBRE PARÁMETRO',
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
        name: 'grupo',
        label: 'TIPO DE PARÁMETRO',
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
        name: 'subgrupo',
        label: 'SUBGRUPO',
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
        name: 'unidadMedida',
        label: 'UNIDAD DE MEDIDA',
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
    ];

    this.columns = columnas;
  }

  getParametros() {
    this.parametrosService
      .getParametros(this.page, this.pageSize, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.registros = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.registros, this.registrosSeleccionados);
          this.selectedPage = this.anyUnselected(this.registros) ? false : true;
          this.loading = false;
          console.log(this.registros);
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }

  onSelectClick(registro: Parametro) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (registro.selected) {
      this.registrosSeleccionados.push(registro);
      this.selectedPage = this.anyUnselected(this.registros) ? false : true;
    } else {
      let index = this.registrosSeleccionados.findIndex(
        (m) => m.id === registro.id
      );

      if (index > -1) {
        this.registrosSeleccionados.splice(index, 1);
      }
    }
  }

  getPreviousSelected(
    muestreos: Array<Parametro>,
    muestreosSeleccionados: Array<Parametro>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.id === x.id
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles

    let filteredColumns = this.getFilteredColumns(); //Obtenemos la lista de columnas que están filtradas
    //this.muestreoService.filtrosSeleccionados = filteredColumns; //Actualizamos la lista de filtros, para el componente de filtro
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
      this.parametrosService.getDistinct(column.name, this.cadena).subscribe({
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
        error: (error) => {},
      });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.parametrosService
      .getParametros(this.page, this.pageSize, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.registros = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    // this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.getParametros();
  }

  pageClic(page: any) {
    this.page = page;
    this.getParametros();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.getParametros();

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
    //this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }

  onDeleteClick(registroId: number) {
    this.registro.id = registroId;
    document.getElementById('btn-confirm-modal')?.click();
  }

  onConfirmDeleteClick() {
    this.parametrosService.delete(this.registro.id).subscribe({
      next: (response: any) => {
        this.resetRegistro();
        this.getParametros();
        this.notificationService.updateNotification({
          type: NotificationType.success,
          text: 'Parámetro eliminado',
          show: true,
        });
      },
      error: (error) => {
        this.resetRegistro();
        this.notificationService.updateNotification({
          type: NotificationType.danger,
          text: 'El parámetro no pudo ser elminado. Se encontrarón resultados reportados para el parámetro',
          show: true,
        });
      },
    });
  }

  onAgregarClick() {
    this.operation = 'Registro parámetro';
    this.resetRegistro();
    this.parametroForm.reset(this.initialValueForm);
    document.getElementById('btn-edit-modal')?.click();
  }

  onEditClick(parametroId: number) {
    this.operation = 'Editar parámetro';
    let elemento = this.registros.findIndex((f) => f.id === parametroId);
    this.registro = this.registros[elemento];
    this.parametroForm.patchValue({
      clave: this.registro.clave,
      nombre: this.registro.descripcion,
      tipo: this.registro.grupoId,
      subgrupo: this.registro.subgrupoId,
      unidadMedida: this.registro.unidadMedidaId,
    });
    document.getElementById('btn-edit-modal')?.click();
  }

  getGrupos() {
    this.parametrosService.getGrupos().subscribe({
      next: (response: any) => {
        this.grupos = response.data;
      },
      error: (error) => {},
    });
  }

  getSubgrupos() {
    this.parametrosService.getSubgrupos().subscribe({
      next: (response: any) => {
        this.subgrupos = response.data;
      },
      error: (error) => {},
    });
  }

  getUnidadesMedida() {
    this.parametrosService.getUnidadesMedida().subscribe({
      next: (response: any) => {
        this.unidadesMedida = response.data;
      },
      error: (error) => {},
    });
  }

  onGuardarCambiosClick() {
    this.registro.clave = this.parametroForm.value.clave ?? '';
    this.registro.descripcion = this.parametroForm.value.nombre ?? '';
    this.registro.grupoId = this.parametroForm.value.tipo ?? 0;
    this.registro.subgrupoId = this.parametroForm.value.subgrupo ?? 0;
    this.registro.unidadMedidaId = this.parametroForm.value.unidadMedida ?? 0;

    if (this.parametroForm.valid && this.registro.id === 0) {
      this.parametrosService.create(this.registro).subscribe({
        next: (response: any) => {
          this.getParametros();
          document.getElementById('btn-close')?.click();
          this.notificationService.updateNotification({
            type: NotificationType.success,
            text: 'Parámetro agregado correctamente.',
            show: true,
          });
        },
        error: (error) => {},
      });
    } else if (this.parametroForm.valid && this.registro.id !== 0) {
      this.parametrosService.update(this.registro).subscribe({
        next: (response: any) => {
          document.getElementById('btn-close')?.click();
          this.notificationService.updateNotification({
            type: NotificationType.success,
            text: 'Cambios guardados correctamente.',
            show: true,
          });
        },
        error: (error) => {},
      });
    }
  }

  onCargarArchivoClick(event: Event) {
    this.fileList = (event.target as HTMLInputElement).files ?? new FileList();
    this.uploadFile(this.fileList, false);
  }

  onSustituirParametrosClick() {
    this.uploadFile(this.fileList, true);
  }

  private uploadFile(archivo: FileList, sustituir: boolean) {
    if (archivo) {
      this.loading = true;
      this.parametrosService.uploadFile(archivo[0], sustituir).subscribe({
        next: (response: any) => {
          if (response.succeded) {
            this.getParametros();
            this.loading = false;
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Archivo procesado correctamente.',
            });
          } else {
            this.loading = false;
            document.getElementById('btnMdlConfirmacionActualizacion')?.click();
          }
        },
        error: (error: any) => {
          this.loading = false;
          let errores = '';
          if (error.error.Errors === null) {
            errores = error.error.Message;
          } else {
            errores = error.error.Errors;
          }
          let archivoErrores = this.generarArchivoDeErrores(errores);
          this.hacerScroll();
          // FileService.download(archivoErrores, 'errores.txt');
          this.resetInputFile(this.inputExcelMonitoreos);
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Se encontraron errores en el archivo procesado.',
          });
        },
      });
    }
  }

  resetRegistro() {
    this.registro = {
      selected: false,
      id: 0,
      clave: '',
      descripcion: '',
      unidadMedida: '',
      unidadMedidaId: 0,
      grupo: '',
      grupoId: 0,
      subGrupo: '',
      subgrupoId: 0,
      parametroPadre: '',
      parametroPadreId: 0,
      orden: 0,
    };
  }
}
