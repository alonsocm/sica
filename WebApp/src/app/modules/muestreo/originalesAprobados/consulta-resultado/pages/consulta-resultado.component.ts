import { Component, OnInit,ViewChild } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Resultado,ResultadoDescarga } from '../../../../../interfaces/Resultado.interface';
import { ConsultaResultadoService } from '../services/consulta-resultado.service';
import { AuthService } from '../../../../login/services/auth.service';
import { TipoHomologado } from 'src/app/interfaces/catalogos/tipo-homologado';
import { Filter } from '../../../../../interfaces/filtro.interface';
import { Perfil } from '../../../../../shared/enums/perfil';
import { FileService } from 'src/app/shared/services/file.service';

@Component({
  selector: 'app-consulta-resultado',
  templateUrl: './consulta-resultado.component.html',
  styleUrls: ['./consulta-resultado.component.css'],
})
export class ConsultaResultadoComponent extends BaseService implements OnInit {
  resultados: Array<Resultado> = [];
  resultadosFiltrados: Array<Resultado> = [];
  cuerpoAgua: Array<TipoHomologado> = [];
  perfil: string = '';
  parametrosTotales: any[] = [];
  parametros: Array<Columna> = [];
  opctionCuerpo: number = 0;
  paramTotalOrdenados: Array<any> = [];
  camposDescarga: Array<any> = [];
  esAdmin: boolean = false;
  anio: number = 0;
  anios: Array<number> = [];

  @ViewChild('entrega') entrega: any;
  @ViewChild('ocdl') ocdl: any;
  @ViewChild('clavesitio') calvesitio: any;
  @ViewChild('clavemonitoreo') clavemonitoreo: any;
  @ViewChild('nombresitio') nombresitio: any;
  @ViewChild('fecharealizacion') fechaRealizacion: any;
  @ViewChild('laboratorio') laboratorio: any;
  @ViewChild('tipocuerpoagua') tipocuerpoagua: any;
  @ViewChild('observacionesrev') observacionesrev: any;
  @ViewChild('fechaLimiteRev') fechaLimiteRev: any;
  @ViewChild('control') control: any;

  constructor(
    private consultaRService: ConsultaResultadoService,
    private usuario: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.perfil=this.usuario.getUser().perfil;
    this.establecerColumnas();
    this.consultaCuerpoAgua();
    this.validarPerfil();
    this.aniosConRegistro();
  }

  establecerColumnas() {
    this.columnas = [
      {
        nombre: 'noEntregaOCDL',
        etiqueta: 'N° ENTREGA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitioOriginal',
        etiqueta: 'CLAVE SITIO ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoHomologado',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoSitio',
        etiqueta: 'TIPO SITIO',
        orden: 0,
        filtro: new Filter(),
      },
    ];

    this.consultaRService.getParametros().subscribe(
      (result) => {
        this.parametrosTotales = result.data;

        for (var i = 0; i < this.parametrosTotales.length; i++) {
          let columna: Columna = {
            nombre: this.parametrosTotales[i].claveParametro.toLowerCase(),
            etiqueta: this.parametrosTotales[i].claveParametro,
            orden: this.parametrosTotales[i].id,
            filtro: new Filter(),
          };
          this.columnas.push(columna);
          this.paramTotalOrdenados.push(columna);
        }
      },
      (error) => console.error(error)
    );
  }

  consultaCuerpoAgua() {
    this.consultaRService.getCuerpoAgua().subscribe({
      next: (response: any) => {
        this.cuerpoAgua = response.data;
      },
      error: (error) => {},
    });
  }

  aniosConRegistro() {
    this.consultaRService.getAniosConRegistro().subscribe({
      next: (response: any) => {
        this.anios = response.data;        
      },
      error: (error) => { },
    });
  }

  FiltrarCuerpoAgua() {
 
      this.consultarMonitoreos(this.opctionCuerpo?? 0, this.anio??0);
    
  }

  consultarMonitoreos(tipoCuerpo: number, anio: number) {
    this.loading = !this.loading;
    this.consultaRService.getMuestreosParametros(tipoCuerpo, anio).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.resultados = response.data;
        this.resultadosFiltrados = this.resultados;
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => {},
    });
  }

  limpiarFiltros() {
    this.columnas.forEach((f) => {    
      f.filtro.selectedValue = 'Seleccione';
   
    });
    this.filtrar();
    document.getElementById('dvMessage')?.click();
    this.establecerValoresFiltrosTabla();
  }

  mostrarColumna(nombreColumna: string) {
    let mostrar: boolean = true;

    if (nombreColumna === 'N°. ENTREGA') {
      if (this.perfil === Perfil.ADMINISTRADOR) {
        mostrar = true;
      } else if (this.perfil === Perfil.SECAIA1) {
        mostrar = true;
      } else if (this.perfil === Perfil.SECAIA2) {
        mostrar = true;
      } else {
        mostrar = false;
      }
    }
    return mostrar;
  }

  validarPerfil() {
    switch (this.perfil) {
      case Perfil.ADMINISTRADOR:
          this.esAdmin = true;
        break;
      case Perfil.SECAIA1:
          this.esAdmin = true;
        break;
      case Perfil.SECAIA2:
          this.esAdmin = true;
        break;
      default:
          this.esAdmin = false;
        break;
    }
  }

  mostrarCampo(): boolean {
    let mostrar: boolean = false;
    if (Perfil.ADMINISTRADOR || Perfil.SECAIA1 || Perfil.SECAIA2) {
      mostrar = true;
    }
    return mostrar;
  }

  filtrar(): void {
    this.loading = !this.loading;
    this.resultadosFiltrados = this.resultados;
    this.columnas.forEach((columna) => {
      this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.loading = false;
    this.establecerValoresFiltrosTabla();
  }

  establecerValoresFiltrosTabla() {
    this.columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltrados.map((m: any) => m[f.nombre])),
      ];
      this.page = 1;
    });
  }

  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionadosDescarga();
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');
      return this.hacerScroll();
    }
    this.consultaRService
      .exportarResultadosExcel(muestreosSeleccionados, this.esAdmin)
      .subscribe({
        next: (response: any) => {
          this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;

          FileService.download(
            response,
            'RESULTADOS_APROBADOS_ORIGINALES.xlsx'
          );
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }

  obtenerSeleccionadosDescarga(): Array<any> {
    var selec = this.resultadosFiltrados.filter((f) => f.isChecked);
    this.camposDescarga = [];
    if (selec.length > 0) {
      for (var i = 0; i < selec.length; i++) {
        let campodes = {
          noEntregaOCDL: selec[i].noEntregaOCDL,
          claveSitioOriginal: selec[i].claveSitioOriginal,
          claveSitio: selec[i].claveSitio,
          claveMonitoreo: selec[i].claveMonitoreo,
          fechaRealizacion: selec[i].fechaRealizacion,
          laboratorio: selec[i].laboratorio,
          tipoCuerpoAgua: selec[i].tipoCuerpoAgua,
          tipoHomologado: selec[i].tipoHomologado,
          tipoSitio: selec[i].tipoSitio,
          lstParametros: selec[i].lstParametros,
        };
        this.camposDescarga.push(campodes);
      }
      this.camposDescarga[0].lstParametrosOrden = this.paramTotalOrdenados;
    }

    return this.camposDescarga;
  }
}
