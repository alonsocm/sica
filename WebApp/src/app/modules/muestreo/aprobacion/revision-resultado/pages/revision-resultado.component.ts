import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Filter } from '../../../../../interfaces/filtro.interface';
import { Revision } from '../../../../../interfaces/Replicas/RevisionResultado';
import { RevisionResultadoService } from '../services/revision-resultado.service';
import { NumberService } from 'src/app/shared/services/number.service';
import { FileService } from 'src/app/shared/services/file.service';
import { AuthService } from '../../../../login/services/auth.service';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { BaseService } from 'src/app/shared/services/base.service';
import { estatusMuestreo_1 } from 'src/app/shared/enums/estatusMuestreo_1'


const TIPO_MENSAJE = { alerta: 'warning', exito: 'success', error: 'danger' };

@Component({
  selector: 'app-revision-resultado',
  templateUrl: './revision-resultado.component.html',
  styleUrls: ['./revision-resultado.component.css'],
})
export class RevisionResultadoComponent extends BaseService implements OnInit {
  archivo: any = null;
  NombArchivo: string = '';
  Data: any[] = [];
  resultados: Array<Revision> = [];
  resultadosFiltrados: Array<Revision> = [];
  ResultadosAprobados: Array<Revision> = [];
  usuario: any;
  Columnas: Array<Columna> = [];

  @ViewChild('inputExcelResultados') inputExcelResultados: ElementRef =
    {} as ElementRef;

  constructor(
    private RevisionService: RevisionResultadoService,
    public numberService: NumberService,
    private authService: AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    this.AsignacionFiltro();
    this.consultarRevision();
  }

  //#region Consulta datos de los muestreo
  consultarRevision(): void {
    this.usuario = this.authService.getUser().usuarioId;
    this.RevisionService.getResultadosRevision(this.usuario).subscribe({
      next: (response: any) => {
        this.Data = response.data;
        this.resultados = response.data;
        this.resultadosFiltrados = this.Data;
        this.establecerValoresFiltrosTabla();
      },
      error: (error) => {},
    });
  }

  //#endregion Consulta datos de los muestreo

  //#region Filtro
  AsignacionFiltro() {
    this.Columnas = [
      {
        nombre: 'noEntrega',
        etiqueta: 'N° ENTREGA',
        orden: 1,
        filtro: new Filter(),
      },
      {
        nombre: 'claveUnica',
        etiqueta: 'CLAVE ÚNICA',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'claveSitio',
        etiqueta: 'CLAVE SITIO',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMonitoreo',
        etiqueta: 'CLAVE MONITOREO',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'nombreSitio',
        etiqueta: 'NOMBRE SITIO',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'claveParametro',
        etiqueta: 'CLAVE PARÁMETRO',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'LABORATORIO',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'TIPO CUERPO AGUA',
        orden: 8,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAguaOriginal',
        etiqueta: 'TIPO CUERPO AGUA ORIGINAL',
        orden: 9,
        filtro: new Filter(),
      },
      {
        nombre: 'resultado',
        etiqueta: 'RESULTADO',
        orden: 10,
        filtro: new Filter(),
      },
      {
        nombre: 'esCorrectoOCDL',
        etiqueta: 'RESULTADO CORRECTO POR OC/DL',
        orden: 11,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionOCDL',
        etiqueta: 'OBSERVACIÓN OC/DL',
        orden: 12,
        filtro: new Filter(),
      },
      {
        nombre: 'esCorrectoSECAIA',
        etiqueta: 'RESULTADO CORRECTO POR SECAIA',
        orden: 13,
        filtro: new Filter(),
      },
      {
        nombre: 'observacionSECAIA',
        etiqueta: 'OBSERVACIÓN SECAIA',
        orden: 14,
        filtro: new Filter(),
      },
      {
        nombre: 'clasificacionObservacion',
        etiqueta: 'CLASIFICACIÓN OBSERVACIÓN',
        orden: 15,
        filtro: new Filter(),
      },
      {
        nombre: 'apruebaResultado',
        etiqueta: 'SE APRUEBA RESULTADO',
        orden: 16,
        filtro: new Filter(),
      },
      {
        nombre: 'comentariosAprobacionResultados',
        etiqueta: 'COMENTARIOS',
        orden: 17,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaAprobRechazo',
        etiqueta: 'FECHA DE APROBACIÓN /RECHAZO',
        orden: 18,
        filtro: new Filter(),
      },
      {
        nombre: 'usuarioRevision',
        etiqueta: 'NOMBRE USUARIO QUE REVISÓ',
        orden: 19,
        filtro: new Filter(),
      },
      {
        nombre: 'estatus',
        etiqueta: 'ESTATUS DEL RESULTADO',
        orden: 20,
        filtro: new Filter(),
      },
    ];
  }

  filtrar(): void {
    this.resultadosFiltrados = this.resultados;
    this.Columnas.forEach((columna) => {
      this.resultadosFiltrados = this.resultadosFiltrados.filter((f: any) => {
        return columna.filtro.selectedValue == 'Seleccione'
          ? true
          : f[columna.nombre] == columna.filtro.selectedValue;
      });
    });
    this.establecerValoresFiltrosTabla();
  }

  establecerValoresFiltrosTabla() {
    this.Columnas.forEach((f) => {
      f.filtro.values = [
        ...new Set(this.resultadosFiltrados.map((m: any) => m[f.nombre])),
      ];
    });
  }

  limpiarFiltros() {
    this.Columnas.forEach((f) => {
      f.filtro.selectedValue = 'Seleccione';
    });
    this.filtrar();
    this.filtros.forEach((element: any) => {
      element.clear();
    });
    document.getElementById('dvMessage')?.click();
  }
  //#endregion Filtro

  //#region Excel
  exportarResultados(): void {
    let muestreosSeleccionados = this.obtenerSeleccionados();
    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje('Debe seleccionar al menos un monitoreo', 'warning');

      return this.hacerScroll();
    }
    this.RevisionService.exportarResultadosExcel(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.seleccionarTodosChck = false;
        FileService.download(response, 'REVISION_RESULTADO.xlsx');
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

  DescargarResultados(): void {
    this.RevisionService.DescargaResultadoExcel(this.resultados).subscribe({
      next: (response: any) => {
        this.resultadosFiltrados = this.resultadosFiltrados.map((m) => {
          m.isChecked = false;
          return m;
        });
        this.seleccionarTodosChck = false;
        FileService.download(response, 'Estatus OC/DL y SECAIA.xlsx');
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

  cargarArchivo(event: any): void {
    this.archivo = event.target.files[0];

    if (this.archivo == null) {
      return this.mostrarMensaje(
        'Debe seleccionar un archivo para cargar',
        TIPO_MENSAJE.alerta
      );
    }
    this.loading = !this.loading;
    this.usuario = this.authService.getUser().usuarioId;
    this.RevisionService.cargarRevision(this.archivo, this.usuario).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.mostrarMensaje(
          'Archivo procesado correctamente.',
          TIPO_MENSAJE.exito
        );
        this.consultarRevision();
      },
      error: (error: any) => {
        this.loading = false;
        const blob = new Blob(
          [error.error.Errors.toString().replaceAll(',', '\n')],
          {
            type: 'application/octet-stream',
          }
        );
        this.mostrarMensaje(
          'Se encontraron errores en el archivo procesado.',
          TIPO_MENSAJE.error
        );
        this.hacerScroll();
        FileService.download(blob, 'errores.txt');
      },
    });
    this.resetInputFile(this.inputExcelResultados);
  }

  EnvioMonitoreo(filtro: Array<any> = []) {
    this.RevisionService.Enviar(filtro).subscribe({
      next: (response: any) => {
        this.consultarRevision();
        this.mostrarMensaje('Monitoreos enviados correctamente', 'success');
        this.hacerScroll();
        this.seleccionarTodosChck = false;
      },
      error: (response: any) => {
        this.mostrarMensaje('No fue posible enviar el muestreo', 'danger');
      },
    });
    this.consultarRevision();
  }
  //#endregion Excel

  //#region AporvarRechazar
  asiganrAprovaRechazo(accion: string): Array<any> {
    let filtro;
    filtro = this.resultadosFiltrados.filter((f) => f.isChecked);
    filtro = filtro.filter(
      (f) =>
        f.apruebaResultado === '' &&
        f.esCorrectoOCDL === accion &&
        f.esCorrectoSECAIA === accion
    );
    return filtro;
  }

  AprobacionResultado() {
    let muestreosSeleccionados = this.asiganrAprovaRechazo('SI');

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo,  el cual cuente con resultados indicados, son un SI y no contar con una autorización.',
        'warning'
      );
      return this.hacerScroll();
    }
    // let claveMonitoreo: string;
    // let muestreoId: number = 0;
    // let datosMuestreos: Array<number> = [];
    for (let index = 0; index < muestreosSeleccionados.length; index++) {
      muestreosSeleccionados[index].usuarioRevisionId =
        localStorage.getItem('idUsuario');
      muestreosSeleccionados[index].apruebaResultado = 'SI';
      // claveMonitoreo = muestreosSeleccionados[index].claveMonitoreo;
      // muestreoId = muestreosSeleccionados[index].muestreoId;

      // ///////////////////////////////////////////

      // this.ResultadosAprobados = this.resultadosFiltrados.filter(
      //   (x) => x.claveMonitoreo == claveMonitoreo
      // );
      // let count: number = this.ResultadosAprobados.length;
      // let countAp: number = 0;

      // this.ResultadosAprobados = this.ResultadosAprobados.filter(
      //   (f) => f.apruebaResultado == 'SI'
      // );
      // countAp = this.ResultadosAprobados.length;

      // if (count == countAp) {
      //   console.log('Se han aprobado todos los ', countAp);
      //   datosMuestreos.push(muestreoId);
      // }

    }
    this.RevisionService.aprovarRechazResultado(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.consultarRevision();
        this.mostrarMensaje('Monitoreos enviados correctamente', 'success');
        this.hacerScroll();
        this.seleccionarTodosChck = false;
      },
      error: (response: any) => {
        this.mostrarMensaje('No fue posible autorizar el muestreo', 'danger');
      },
    });
    // console.log(datosMuestreos)
    // datosMuestreos.forEach((f) =>    
    //   this.RevisionService.cambioEstatus(f).subscribe({
    //     next: (resul: any) => {},
    //     error: (response: any) => {},
    //   })
    // );
  }


  RechazoResultado() {
    let muestreosSeleccionados = this.asiganrAprovaRechazo('NO');

    if (muestreosSeleccionados.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo,  el cual cuente con resultados indicados, son un NO y no contar con un rechazo.',
        'warning'
      );

      return this.hacerScroll();
    }

    for (let index = 0; index < muestreosSeleccionados.length; index++) {
      muestreosSeleccionados[index].usuarioRevisionId =
        localStorage.getItem('idUsuario');
      muestreosSeleccionados[index].apruebaResultado = 'NO';
    }
    this.RevisionService.aprovarRechazResultado(
      muestreosSeleccionados
    ).subscribe({
      next: (response: any) => {
        this.consultarRevision();
        this.mostrarMensaje('Monitoreos enviados correctamente', 'success');
        this.hacerScroll();
        this.seleccionarTodosChck = false;
      },
      error: (response: any) => {
        this.mostrarMensaje('No fue posible autorizar el muestreo', 'danger');
      },
    });
  }

  //#endregion AporvarRechazar

  //#region EnviarA

  ResultadosAprovados() {
    //Estatus 11, ENVIADO A RESULTADOS APROBADOS
    
    this.AproRechazoMonitoreo(11, 'SI');
    this.CambioEstatus();
  }

  CambioEstatus(){
    var muestreosSeleccionados = this.resultadosFiltrados.filter((f) => f.isChecked);

    let claveMonitoreo: string;
    let muestreoId: number = 0;
    let datosMuestreos: Array<number> = [];    
    for (let index = 0; index < muestreosSeleccionados.length; index++) {
      claveMonitoreo = muestreosSeleccionados[index].claveMonitoreo;
      muestreoId = muestreosSeleccionados[index].muestreoId;
   

      this.ResultadosAprobados = this.resultadosFiltrados.filter(
        (x) => x.claveMonitoreo == claveMonitoreo
      );
      let count: number = this.ResultadosAprobados.length;
      let countAp: number = 0;

      this.ResultadosAprobados = this.ResultadosAprobados.filter(
        (f) => f.apruebaResultado == 'SI'
          && f.estatusResultadoId == estatusMuestreo_1.EnviadoResultadosAprobados
      );    
      countAp = this.ResultadosAprobados.length;

      if (count == countAp) {       
        var dmuestreo = datosMuestreos.filter((f)=> f == muestreoId);
        if (dmuestreo.length == 0){   
          datosMuestreos.push(muestreoId);
        }       
      }
    }      
    datosMuestreos.forEach((f) =>    
      this.RevisionService.cambioEstatus(f).subscribe({
        next: (resul: any) => {},
        error: (response: any) => {},
      })
    );
  }

  Replicaresultados() {
    //Estatus 12, Enviado para réplica
    this.AproRechazoMonitoreo(12, 'NO');
  }

  Replicadiferente() {
    let filtro;
    filtro = this.resultadosFiltrados.filter((f) => f.isChecked);

    filtro = filtro.filter((f) => f.estatusResultadoId === 13); //Réplica con diferente dato para revisión

    if (filtro.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo,  el cual cuente con estatus de Réplica con diferente dato para revisión.',
        'warning'
      );
      return this.hacerScroll();
    }
    for (let index = 0; index < filtro.length; index++) {
      filtro[index].estatusResultadoId = 23; //Réplica con diferente dato
    }
    this.EnvioMonitoreo(filtro);
  }

  AproRechazoMonitoreo(EstatusId: number, resultado: string) {
    let filtro;
    filtro = this.resultadosFiltrados.filter((f) => f.isChecked);
    filtro = filtro.filter((f) => f.apruebaResultado === resultado);

    if (filtro.length === 0) {
      this.mostrarMensaje(
        'Debe seleccionar al menos un monitoreo,  el cual cuente con Aprueba el Resultado, son un ' +
          resultado,
        'warning'
      );
      return this.hacerScroll();
    }

    for (let index = 0; index < filtro.length; index++) {
      filtro[index].estatusResultadoId = EstatusId;
    }
    this.EnvioMonitoreo(filtro);
  }
  //#endregion EnviarA

  //#region AccionesVarias

  //Habilita los checks de los muesteos
  seleccionarTodos(): void {
    this.resultadosFiltrados.map((m) => {
      if (this.seleccionarTodosChck) {
        m.isChecked ? true : (m.isChecked = true);
      } else {
        m.isChecked ? (m.isChecked = false) : true;
      }
    });
    let muestreosSeleccionados = this.obtenerSeleccionados();
    this.RevisionService.muestreosSeleccionados = muestreosSeleccionados;
  }

  obtenerSeleccionados(): Array<any> {
    return this.resultadosFiltrados.filter((f) => f.isChecked);
  }
  //#endregion AccionesVarias
}
