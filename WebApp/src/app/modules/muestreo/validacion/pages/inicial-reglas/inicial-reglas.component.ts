import { Component, OnInit } from '@angular/core';
import { ValidacionReglasService } from '../../services/validacion-reglas.service';
import { FileService } from 'src/app/shared/services/file.service';
import { BaseService } from 'src/app/shared/services/base.service';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { acumuladosMuestreo } from '../../../../../interfaces/acumuladosMuestreo.interface';
import { estatusMuestreo } from 'src/app/shared/enums/estatusMuestreo';

@Component({
  selector: 'app-inicial-reglas',
  templateUrl: './inicial-reglas.component.html',
  styleUrls: ['./inicial-reglas.component.css'],
})
export class InicialReglasComponent extends BaseService implements OnInit {
  constructor(private validacionService: ValidacionReglasService) {
    super();
  }

  resultadosMuestreo: Array<acumuladosMuestreo> = [];
  aniosSeleccionados: Array<number> = [];
  entregasSeleccionadas: Array<number> = [];
  resultadosEnviados: Array<number> = [];
  anios: Array<number> = [];
  entregas: Array<number> = [];

  ngOnInit(): void {
    this.columnas = [
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MUESTREO',
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
        nombre: 'fechaRealizacion',
        etiqueta: 'FECHA REALIZACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaVisifechaProgramadata',
        etiqueta: 'FECHA PROGRAMADA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'diferenciaDias',
        etiqueta: 'DIFERENCIA EN DÍAS',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaEntregaTeorica',
        etiqueta: 'FECHA DE ENTREGA TEORICA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorioRealizoMuestreo',
        etiqueta: 'LABORATORIO BASE DE DATOS',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'cuerpoAgua',
        etiqueta: 'CUERPO DE AGUA',
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
        nombre: 'subtipoCuerpoAgua',
        etiqueta: 'SUBTIPO CUERPO AGUA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'numParametrosEsperados',
        etiqueta: 'NÚMERO DE DATOS ESPERADOS',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'numParametrosCargados',
        etiqueta: 'NÚMERO DE DATOS REPORTADOS',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'muestreoCompletoPorResultados',
        etiqueta: 'MUESTREO COMPLETO POR RESULTADOS',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'cumpleReglasCondic',
        etiqueta: '¿CUMPLE CON LA REGLAS CONDICIONANTES?',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'Observaciones',
        etiqueta: 'OBSERVACIONES CONDICIONANTES',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'cumpleFechaEntrega',
        etiqueta: 'CUMPLE CON LA FECHA DE ENTREGA',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'reglaValicdacion',
        etiqueta: 'SE CORRE REGLA DE VALIDACIÓN',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'autorizacionRegla',
        etiqueta: 'AUTORIZACIÓN DE REGLAS CUANDO ESTE INCOMPLETO (SI)',
        orden: 0,
        filtro: new Filter(),
      },
      {
        nombre: 'cumpleTodosCriterios',
        etiqueta: 'CUMPLE CRITERIOS PARA APLICAR REGLAS (SI/NO)',
        orden: 0,
        filtro: new Filter(),
      },
    ];
    this.validacionService.obtenerMuestreos().subscribe({
      next: (response: any) => {
        this.anios = response.data;
      },
      error: (error) => {},
    });
    this.validacionService.obtenerNumerosEntrega().subscribe({
      next: (response: any) => {
        this.entregas = response.data;
      },
      error: (error) => { },
    });

  }
  cargaResultados() {
    if (
      this.entregasSeleccionadas.length == 0 &&
      this.aniosSeleccionados.length == 0
    ) {
      this.mensajeAlerta =
        'Debes de seleccionar al menos un año y un número de entrega';
      this.mostrarAlerta = true;
      this.tipoAlerta = 'warning';
      return this.hacerScroll();
    }
    this.loading = true;
    this.validacionService
      .getResultadosporMonitoreo(
        this.aniosSeleccionados,
        this.entregasSeleccionadas,
        estatusMuestreo.InicialReglas
      )
      .subscribe({
        next: (response: any) => {
          this.resultadosMuestreo = response.data;
          this.resultadosFiltradosn = this.resultadosMuestreo;
          this.resultadosn = this.resultadosMuestreo;
          this.loading = false;
        },
        error: (error) => {
          this.loading = false;
        },
      });
  }
  onDownload(): void {
    if (this.resultadosFiltradosn.length == 0) {
      this.mostrarMensaje(
        'No hay información existente para descargar',
        'warning'
      );
      return this.hacerScroll();
    }
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn);
    this.validacionService
      .exportExcelResultadosaValidar(
        this.resultadosEnviados.length > 0
          ? this.resultadosEnviados
          : this.resultadosFiltradosn
      )
      .subscribe({
        next: (response: any) => {
          this.loading = true;
          FileService.download(response, 'ResultadosaValidar.xlsx');
          this.loading = false;
        },
        error: (response: any) => {
          this.loading = false;
          this.mostrarMensaje(
            'No fue posible descargar la información',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }
  enviaraValidacion(): void {
    this.resultadosEnviados = this.Seleccionados(this.resultadosFiltradosn).map(
      (m) => {
        return m.muestreoId;
      }
    );

    if (this.resultadosEnviados.length == 0) {
      this.hacerScroll();
      return this.mostrarMensaje(
        'Debes de seleccionar al menos un muestreos para enviar a validar',
        'danger'
      );
    }

    this.validacionService
      .enviarMuestreoaValidar(
        estatusMuestreo.SeleccionadoParaValidar,
        this.resultadosEnviados
      )
      .subscribe({
        next: (response: any) => {
          this.loading = true;
          if (response.succeded) {
            this.loading = false;
            this.cargaResultados();
            this.mostrarMensaje(
              'Los muestreos fueron enviados a validar correctamente',
              'success'
            );
            this.hacerScroll();
          }
        },
        error: (response: any) => {
          this.loading = false;
          this.mostrarMensaje(
            ' Error al enviar los muestreos a validar',
            'danger'
          );
          this.hacerScroll();
        },
      });
  }
}
