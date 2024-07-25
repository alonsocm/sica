import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Column } from 'src/app/interfaces/filter/column';
import { Item } from 'src/app/interfaces/filter/item';
import { TipoCuerpoAgua } from '../../models/tipocuerpoagua';
import { TipoCuerpoAguaService } from '../../services/tipoCuerpoAgua.service';
import { TipoHomologado } from 'src/app/interfaces/catalogos/tipo-homologado';



@Component({
  selector: 'app-tipo-cuerpo-agua',
  templateUrl: './tipo-cuerpo-agua.component.html',
  styleUrls: ['./tipo-cuerpo-agua.component.css']
})

export class TipoCuerpoAguaComponent extends BaseService implements OnInit {
[x: string]: any;
  
  registros: Array<TipoCuerpoAgua> = [];
  registrosSeleccionados: Array<TipoCuerpoAgua> = [];
  tipoHomologado:Array<TipoHomologado> = [];

  public tipoHomologadoRegistro: TipoHomologado = {
    id: 0,
    descripcion: '',
    activo: new Blob(),
  }
  public TipoCuerpoAgua: TipoCuerpoAgua = {
    id: 0,
    descripcion: '',
    tipoHomologadoId: 0,
    tipoHomologadoDescripcion: '',
    activo: true,
    frecuencia: '',
    evidenciasEsperadas: 0,
    tiempoMinimoMuestreo: 0,
    selected: false
  };
  editar: boolean = false;
  modalTitle: string = '';
  constructor(private tipocuerpoagua: TipoCuerpoAguaService) {
  super();
}

ngOnInit(): void {
  this.definirColumnas();
  this.getTipoCuerpoAguaQuery();
}
definirColumnas() {
  let columnas: Array<Column> = [
    
    {
      name: 'descripcion',
      label: 'DESCRIPCIÓN',
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
      order:2,
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
      order:3,
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
      name: 'frecuencia',
      label: 'FRECUENCIA',
      order:4,
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
      name: 'evidenciasEsperadas',
      label: 'NÚMERO DE EVIDENCIAS ESPERADAS',
      order:5,
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
      name: 'tiempoMinimoMuestreo',
      label: 'TIEMPO MÍNIMO DE MUESTREO',
      order:6,
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

addTipoCuerpoAgua() {
  this.tipocuerpoagua.addTipoCuerpoAgua(this.TipoCuerpoAgua).subscribe({
    next: (data) => {
      console.log('Tipo cuerpo de agua creado:', data);
      this.TipoCuerpoAgua = {
        id: 0,
        descripcion: '',
        tipoHomologadoId: 0,
        tipoHomologadoDescripcion: '',
        activo: true,
        frecuencia: '',
        evidenciasEsperadas: 0,
        tiempoMinimoMuestreo: 0,
        selected: false
      };
      this.getTipoCuerpoAguaQuery();
    },
    error: (error) => {
      console.error('Error al crear tipo cuerpo de agua:', error);    
    }
  });
}
onEditClick(id: number){
  let index = this.registros.findIndex(x => x.id == id);
  let registro= this.registros[index];
  this.TipoCuerpoAgua=registro
  document.getElementById('btn-edit-modal')?.click();
}
updateTipoCuerpoAgua(id: number) {
  this.tipocuerpoagua.updateTipoCuerpoAgua(this.TipoCuerpoAgua.id, this.TipoCuerpoAgua).subscribe({
    next: (data) => {
      console.log('Tipo cuerpo de agua actualizado:', data);
      this.TipoCuerpoAgua = {
        id: 0,
        descripcion: '',
        tipoHomologadoId: 0,
        tipoHomologadoDescripcion: '',
        activo: true,
        frecuencia: '',
        evidenciasEsperadas: 0,
        tiempoMinimoMuestreo: 0,
        selected: false
      };
      this.getTipoCuerpoAguaQuery();
    },
    error: (error) => {
      console.error('Error al actualizar tipo cuerpo de agua:', error);
    }
  });
}

deleteTipoCuerpoAgua(id: number) {
  this.tipocuerpoagua.deleteTipoCuerpoAgua(id).subscribe({
    next: () => {
      console.log('Tipo cuerpo de agua eliminado:', id);
      this.TipoCuerpoAgua = {
        id: 0,
        descripcion: '',
        tipoHomologadoId: 0,
        tipoHomologadoDescripcion: '',
        activo: true,
        frecuencia: '',
        evidenciasEsperadas: 0,
        tiempoMinimoMuestreo: 0,
        selected: false
      };
      this.getTipoCuerpoAguaQuery();
    },
    error: (error) => {
      console.error('Error al eliminar tipo cuerpo de agua:', error);
    }
  });
}
getTipoHomologado() {
  this.tipoHomologado = this.tipoHomologado.filter(x => x.id == this.TipoCuerpoAgua.tipoHomologadoId);
}
getTipoCuerpoAguaQuery() {
  this.tipocuerpoagua
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
  onSelectClick(registro: TipoCuerpoAgua) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;
  
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
      this.tipocuerpoagua.getDistinct(column.name, this.cadena).subscribe({
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
    this.tipocuerpoagua
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
        this.getTipoCuerpoAguaQuery();
  }

  pageClic(page: any) {
    this.page = page;
    this.getTipoCuerpoAguaQuery();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.getTipoCuerpoAguaQuery();

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
    
    this.hideColumnFilter();
  }
 
}

