import { Filter } from 'src/app/interfaces/filtro.interface';
import { Resultado } from 'src/app/interfaces/Resultado.interface';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NumberService } from '../../../../../../../shared/services/number.service';
import { TotalService } from '../../../../services/total.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusOcdlSecaia } from 'src/app/shared/enums/estatusOcdlSecaia';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje'
import { FileService } from 'src/app/shared/services/file.service';
import { estatusMuestreo } from '../../../../../../../shared/enums/estatusMuestreo';

@Component({
  selector: 'app-validados',
  templateUrl: './validados.component.html',
  styleUrls: ['./validados.component.css'],
})
export class ValidadosComponent extends BaseService implements OnInit {
  modalTitle: string = '¡ALERTA!';

  isModal: boolean = false;
  seleccionarTodosChckmodal: boolean = false;

  resultadosSeleccionadosDatos: any = null;

  muestreosagrupados: Array<any> = [];
  resultadosSeleccionados: Array<Resultado> = [];

  estatusAprobacionFinal: number = 0;
  estatusEnviado: number = 0;

  constructor(
    public numberService: NumberService,
    private totalService: TotalService
  ) {
    super();
    this.estatusAprobacionFinal = estatusOcdlSecaia.AprobacionFinal;
    this.estatusEnviado = estatusMuestreo.ResumenValidaciónReglas;
  }
  ngOnInit(): void {
    this.consultarMonitoreos();
    this.columnas = [
      {
        nombre: 'noEntregaOCDL',
        etiqueta: 'N° ENTREGA OC/DL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveUnica',
        etiqueta: 'CLAVE ÚNICA',
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
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveParametro',
        etiqueta: 'CLAVE PARÁMETRO',
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
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'resultado',
        etiqueta: 'RESULTADO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoAprobacion',
        etiqueta: 'TIPO APROBACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'esCorrectoResultado',
        etiqueta: 'RESULTADO CORRECTO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'observaciones',
        etiqueta: 'OBSERVACIÓN OC/DL',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaLimiteRevision',
        etiqueta: 'FECHA LIMITE DE REVISIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreUsuario',
        etiqueta: 'USUARIO QUE REVISÓ',
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
        nombre: 'estatusResultado',
        etiqueta: 'ESTATUS DEL RESULTADO',
        orden: 0,
        filtro: new Filter(),
      },
    ];
  }
  consultarMonitoreos(): void {
    this.loading = true;
    this.totalService.getResumenRevisionResultados(estatusOcdlSecaia.Validado, true).subscribe({
      next: (response: any) => {
        this.resultadosn = response.data;
        this.resultadosFiltradosn = this.resultadosn;
        if (this.resultadosn.length > 0) {
          this.establecerValoresFiltrosTablan();
        }
        this.loading = false;
      },
      error: (error) => { this.loading = false; },
    });
  }
  consultarMonitoreosmuestreo(): void {
    if (this.resultadosSeleccionados.length > 0) {
      this.isModal = true;
      let muestreosmodal = this.resultadosSeleccionados.map((m) => ({
        muestreo: m.muestreoId,
        clavemonitoreo: m.claveMonitoreo,
        clavesitio: m.claveSitio,
        noEntregaOCDL: m.noEntregaOCDL,
        nomnresitio: m.nombreSitio,
        tipocuerpoagua: m.tipoCuerpoAgua,
        fecharealizacion: m.fechaRealizacion,
        nombreusuario: m.nombreUsuario,
        muestreoId: m.muestreoId,
      }));
      this.muestreosagrupados = [
        ...new Map(muestreosmodal.map((m) => [m.muestreo, m])).values(),
      ];
    } else {
      this.isModal = false;
      this.mostrarMensaje(
        'Debe seleccionar al menos un muestreo para regresar a total de resultados',
        TipoMensaje.Alerta
      );
    }
    this.hacerScroll();
  }
  seleccionar(): void {
    if (this.seleccionarTodosChck) this.seleccionarTodosChck = false;
    this.resultadosSeleccionados = this.Seleccionados(
      this.resultadosFiltradosn
    );
  }
  seleccionarTodosmodal(): void {
    this.muestreosagrupados.map((m) => {
      if (this.seleccionarTodosChckmodal) {
        m.isCheckedmodal ? true : (m.isCheckedmodal = true);
      } else {
        m.isCheckedmodal ? (m.isCheckedmodal = false) : true;
      }
    });
    this.resultadosSeleccionados = this.Seleccionados(
      this.resultadosFiltradosn
    );
  }
  seleccionarmodal(): void {
    if (this.seleccionarTodosChckmodal) this.seleccionarTodosChckmodal = false;
  }
  enviarMonitoreos(): void {
    this.resultadosSeleccionados = this.Seleccionados(
      this.resultadosFiltradosn
    );
    if (!(this.resultadosSeleccionados.length > 0)) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para enviar',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    } else {
      document.getElementById('btnMuestraModal')?.click();
    }
  }
  regresaratotales(): void {
    let resultadosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    this.resultadosSeleccionadosDatos = resultadosSeleccionados;
  }
  cambiarEstatusMuestreo(estatus: number): void {
    let muestreosModificar = [];
    if (estatus === estatusOcdlSecaia.AprobacionFinal) {
      muestreosModificar = [
        ...new Set(this.resultadosSeleccionados.map((m) => m.muestreoId)),
      ];
    } else {
      muestreosModificar = [
        ...new Set(
          this.muestreosagrupados
            .filter((f) => f.isCheckedmodal)
            .map((m) => m.muestreoId)
        ),
      ];
    }

    if (muestreosModificar.length > 0) {
      let muestreos = {
        estatusOcdlId: estatus,
        estatusId: estatus,
        muestreoId: [...new Set(muestreosModificar)],
        IdUsuario: localStorage.getItem('idUsuario'),
      };
      this.loading = true;
      this.totalService.actualizarResultado(muestreos).subscribe({
        next: (response) => {
          this.consultarMonitoreos();
          this.loading = false;
          this.mostrarMensaje('Monitoreos enviados correctamente', TipoMensaje.Correcto);
          this.hacerScroll();
          this.seleccionarTodosChckmodal = false;
          this.seleccionarTodosChck = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
    } else {
      this.mostrarMensaje(
        'Debes de seleccionar al menos un muestreo para regresarlo a total de resultados',
        TipoMensaje.Alerta
      );
    }
    this.hacerScroll();
  }

  exportarResultados() {
    let muestreosSeleccionados = this.Seleccionados(this.resultadosFiltradosn);
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo para descargar la información',
        TipoMensaje.Alerta
      );
      return this.hacerScroll();
    }
    this.loading = true;
    this.totalService
      .exportarResultadosValidados(muestreosSeleccionados)
      .subscribe({
        next: (response: any) => {
          this.resultadosFiltradosn = this.resultadosFiltradosn.map((m) => {
            m.isChecked = false;
            return m;
          });
          this.seleccionarTodosChck = false;
          FileService.download(response, 'RESULTADOS_VALIDADOS.xlsx');
          this.loading = false;
        },
        error: (response: any) => {
          this.mostrarMensaje(
            'No fue posible descargar la información',
            TipoMensaje.Error
          );
          this.loading = false;
          this.hacerScroll();
        },
      });
  }
}


