import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';
import { TipoCuerpoAgua, TipoHomologado } from '../../models/tipocuerpoagua';
import { TipoCuerpoAguaService } from '../../services/tipoCuerpoAgua.service';
import { FileService } from 'src/app/shared/services/file.service';
import * as XLSX from 'xlsx';
const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };
@Component({
  selector: 'app-tipo-cuerpo-agua',
  templateUrl: './tipo-cuerpo-agua.component.html',
  styleUrls: ['./tipo-cuerpo-agua.component.css'],
})
export class TipoCuerpoAguaComponent extends BaseService implements OnInit {
  registros: Array<TipoCuerpoAgua> = [];
  registrosSeleccionados: Array<TipoCuerpoAgua> = [];
  rtipohomologado: Array<TipoHomologado> = [];
  excelData: any[] = [];
  messageEventualidad: string = '';
  mostrarMensajeAlerta: boolean = false;

  public tipoCuerpoAgua: TipoCuerpoAgua = {
    id: 0,
    descripcion: '',
    tipoHomologadoId: 0,
    tipoHomologadoDescripcion: '',
    activo: true,
    frecuencia: '',
    evidenciasEsperadas: 0,
    tiempoMinimoMuestreo: 0,
    selected: false,
  };
  public tipoHomologado: TipoHomologado = {
    id: 0,
    descripcion: '',
    activo: true,
    selected: false,
  };

  editar: boolean = false;
  modalTitle: string = '';
  showModal: boolean = false;

  constructor(
    private tipoCuerpoAguaServices: TipoCuerpoAguaService,
    private fileService: FileService
  ) {
    super();
  }

  ngOnInit(): void {
    this.definirColumnas();
    this.getTipoCuerpoAgua();
    this.getTipoHomologado();
  }

  definirColumnas() {
    let columnas: Array<Column> = [
      {
        name: 'descripcion',
        label: 'TIPO CUERPO DE AGUA',
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
        name: 'tipoHomologadoIdDescripcion',
        label: 'TIPO HOMOLOGADO',
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
        name: 'activo',
        label: 'ACTIVO',
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
    ];
    this.columns = columnas;
  }
  getTipoCuerpoAgua() {
    this.tipoCuerpoAguaServices
      .getTipoCuerpoAgua(this.page, this.pageSize, this.cadena)
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
  getTipoHomologado() {
    this.tipoCuerpoAguaServices.getTipoHomologado().subscribe({
      next: (response: any) => {
        this.rtipohomologado = response.data;
      },
      error: (error) => {},
    });
  }
  addTipoCuerpoAgua() {
    this.tipoCuerpoAguaServices
      .addTipoCuerpoAgua(this.tipoCuerpoAgua)
      .subscribe({
        next: (response: any) => {
          this.tipoCuerpoAgua = response.data;
          this.getTipoCuerpoAgua();
          this.tipoCuerpoAgua = {
            id: 0,
            descripcion: '',
            tipoHomologadoId: 0,
            tipoHomologadoDescripcion: '',
            activo: true,
            frecuencia: '',
            evidenciasEsperadas: 0,
            tiempoMinimoMuestreo: 0,
            selected: false,
          };
        },
        error: (error) => {
          console.error('Error al agregar tipo cuerpo de agua:', error);
        },
      });
  }

  onEditClick(id: number) {
    let index = this.registros.findIndex((x) => x.id == id);
    let registro = this.registros[index];
    this.tipoCuerpoAgua = registro;
    document.getElementById('btn-edit-modal')?.click();
  }

  updateTipoCuerpoAgua(id: number) {
    this.tipoCuerpoAguaServices
      .updateTipoCuerpoAgua(this.tipoCuerpoAgua.id, this.tipoCuerpoAgua)
      .subscribe({
        next: (data) => {
          console.log('Tipo cuerpo de agua actualizado:', data);
          this.tipoCuerpoAgua = {
            id: 0,
            descripcion: '',
            tipoHomologadoId: 0,
            tipoHomologadoDescripcion: '',
            activo: true,
            frecuencia: '',
            evidenciasEsperadas: 0,
            tiempoMinimoMuestreo: 0,
            selected: false,
          };
          this.getTipoCuerpoAgua();
        },
        error: (error) => {
          console.error('Error al actualizar tipo cuerpo de agua:', error);          
        },
      });
  }
  deleteTipoCuerpoAgua(id: number) {
    this.tipoCuerpoAguaServices
      .deleteTipoCuerpoAgua(this.tipoCuerpoAgua.id, this.tipoCuerpoAgua)
      .subscribe({
        next: () => {
          this.mostrarMensaje(
            'Tipo cuerpo de agua eliminado correctamente.',
            TIPO_MENSAJE.exito)
          this.tipoCuerpoAgua = {
            id: 0,
            descripcion: '',
            tipoHomologadoId: 0,
            tipoHomologadoDescripcion: '',
            activo: true,
            frecuencia: '',
            evidenciasEsperadas: 0,
            tiempoMinimoMuestreo: 0,
            selected: false,
          };
          this.getTipoCuerpoAgua();
          
        },
        error: (error) => {
          
          console.error('Error al eliminar tipo cuerpo de agua:', error);
          this.mostrarMensaje(
            'No se puede eliminar el tipo cuerpo de agua.',
            TIPO_MENSAJE.error
          );             
          this.tipoCuerpoAgua = {
            id: 0,
            descripcion: '',
            tipoHomologadoId: 0,
            tipoHomologadoDescripcion: '',
            activo: true,
            frecuencia: '',
            evidenciasEsperadas: 0,
            tiempoMinimoMuestreo: 0,
            selected: false,
          };
        },
      });
  }
  onDeleteClick(id: number) {
    let index = this.registros.findIndex((x) => x.id == id);
    let registro = this.registros[index];
    this.tipoCuerpoAgua = registro;
    document.getElementById('btn-delete-modal')?.click();
  }

  getPreviousSelected(
    muestreos: Array<TipoCuerpoAgua>,
    muestreosSeleccionados: Array<TipoCuerpoAgua>
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
    this.collapseFilterOptions();

    let filteredColumns = this.getFilteredColumns();
    this.filtros = filteredColumns;

    this.obtenerLeyendaFiltroEspecial(column.dataType);

    let esFiltroEspecial = this.IsCustomFilter(column);

    if (
      (!column.filtered && !this.existeFiltrado) ||
      (column.isLatestFilter && this.filtros.length == 1)
    ) {
      this.cadena = '';
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }

    if (this.requiresToRefreshColumnValues(column)) {
      this.tipoCuerpoAguaServices
        .getDistinct(column.name, this.cadena)
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
    this.tipoCuerpoAguaServices
      .getTipoCuerpoAgua(this.page, this.pageSize, this.cadena)
      .subscribe({
        next: (response: any) => {
          this.registros = response.data;
        },
        error: (error) => {},
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.getTipoCuerpoAgua();
  }

  pageClic(page: any) {
    this.page = page;
    this.getTipoCuerpoAgua();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;

    this.cadena = this.obtenerCadena(columna, isFiltroEspecial);

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
    this.getTipoCuerpoAgua();
    this.hideColumnFilter();
  }

importarExcel(event: any) {
  const file = event.target.files[0];
  const reader = new FileReader();
  reader.onload = (e: any) => {
    const workbook = XLSX.read(e.target.result, { type: 'binary' });
    const worksheet = workbook.Sheets[workbook.SheetNames[0]];
    const data = XLSX.utils.sheet_to_json(worksheet);
    console.log('datos',data);
    data.forEach((row: any) => {
      // Crear un nuevo objeto TipoCuerpoAgua
      const tipoCuerpoAgua: TipoCuerpoAgua = {
        id: 0, // Asignar un ID único si es necesario
        descripcion: row.descripcion,
        tipoHomologadoId: row.tipoHomologadoId, // Ajustar el nombre de la columna si es necesario
        tipoHomologadoDescripcion: row.tipoHomologadoDescripcion, // Ajustar el nombre de la columna si es necesario
        activo: row.activo, // Ajustar el nombre de la columna si es necesario
        frecuencia: row.frecuencia, // Ajustar el nombre de la columna si es necesario
        evidenciasEsperadas: row.evidenciasEsperadas, // Ajustar el nombre de la columna si es necesario
        tiempoMinimoMuestreo: row.tiempoMinimoMuestreo, // Ajustar el nombre de la columna si es necesario
        selected: false,        
      };   
         
      // Agregar el nuevo objeto a la lista de registros
      this.tipoCuerpoAguaServices.addTipoCuerpoAgua(tipoCuerpoAgua).subscribe({
        next: (response: any) => {
          this.registros.push(response.data);          
          // Actualizar la lista de registros
          this.getTipoCuerpoAgua();          
        },
        
        error: (error) => {
          console.error('Error al importar tipo cuerpo de agua:', error);
        },
        
      });
      
    });
    console.log('d',data)
  };

  reader.readAsBinaryString(file);
}
}
