import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
//import{Excelservice} 'src/app/services/excel.service';
//import { NotificationService } from 'src/app/services/notification.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';
import { TipoCuerpoAgua, TipoHomologado } from '../../models/tipocuerpoagua';
import { TipoCuerpoAguaService } from '../../services/tipoCuerpoAgua.service';
import { FileService } from 'src/app/shared/services/file.service'; 
//import * as XLSX from 'xlsx';


@Component({
  selector: 'app-tipo-cuerpo-agua',
  templateUrl: './tipo-cuerpo-agua.component.html',
  styleUrls: ['./tipo-cuerpo-agua.component.css'],
})
export class TipoCuerpoAguaComponent extends BaseService implements OnInit {
  registros: Array<TipoCuerpoAgua> = [];
  registrosSeleccionados: Array<TipoCuerpoAgua> = [];
  rtipohomologado: Array<TipoHomologado> = [];
  //@ViewChild('inputExcel') inputExcel: ElementRef= {} as ElementRef;;

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
  constructor(private tipoCuerpoAguaServices: TipoCuerpoAguaService,private fileService: FileService ) {super();}

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
          console.log('Tipo cuerpo de agua eliminado:', id);
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
        error: (error) => {},
      });
  }
  onDeleteClick(id: number) {
    let index = this.registros.findIndex((x) => x.id == id);
    let registro = this.registros[index];
    this.tipoCuerpoAgua = registro;
    document.getElementById('btn-delete-modal')?.click();
  }
  //openFileExplorer(event: Event) {this.inputExcel.nativeElement.click();}
 

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
  
}
