import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ArchivoDat } from '../../../../../interfaces/correo.interface';
import { Evidencia } from '../../../../../interfaces/evidencia';
import { Column } from '../../../../../interfaces/filter/column';
import { Item } from '../../../../../interfaces/filter/item';
import { ReplicasResultadosReglaVal } from '../../../../../interfaces/ReplicasResultadosReglaVal.interface';
import { estatusResultado } from '../../../../../shared/enums/estatusResultado';
import { NotificationType } from '../../../../../shared/enums/notification-type';
import { tipoReplicasValidacion } from '../../../../../shared/enums/tipoReplicasValidacion';
import { CorreoModel } from '../../../../../shared/models/correo-model';
import { BaseService } from '../../../../../shared/services/base.service';
import { FileService } from '../../../../../shared/services/file.service';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { AuthService } from '../../../../login/services/auth.service';
import { MuestreoService } from '../../../liberacion/services/muestreo.service';
import { IncidenciasResultadosService } from '../../services/incidencias-resultados.service';

@Component({
  selector: 'app-incidecias-resultados',
  templateUrl: './incidecias-resultados.component.html',
  styleUrls: ['./incidecias-resultados.component.css']
})
export class IncideciasResultadosComponent extends BaseService implements OnInit {
  columnsAvance: Array<Column> = [];
  replicasResultados: Array<ReplicasResultadosReglaVal> = [];
  resultadosFiltrados: Array<ReplicasResultadosReglaVal> = [];
  envioCorreo: CorreoModel = {
    destinatarios: '',
    copias: '',
    asunto: 'Incidencias reglas de validación',
    cuerpo: '',
    archivos: []
  };
  archivodat: ArchivoDat | undefined;
  rutas: Array<string> = [];
  tipoArchivo: number = 0;
  ReplicaSrenameca: number = tipoReplicasValidacion.ReplicaSrenameca;
  ReplicaLaboratorioExterno: number = tipoReplicasValidacion.ReplicaLaboratorioExterno;
  ReplicaAprobacion: number = tipoReplicasValidacion.ReplicasAprobacion;
  archivo: any;
  @ViewChild('inputExcelLaboratorio') inputExcelLaboratorio: ElementRef =
    {} as ElementRef;
  @ViewChild('inputExcelSRENAMECA') inputExcelSRENAMECA: ElementRef =
    {} as ElementRef;
  @ViewChild('inputExcelAprobacionRechazo') inputExcelAprobacionRechazo: ElementRef =
    {} as ElementRef;
  @ViewChild('fileUpload') fileUpload: ElementRef =
    {} as ElementRef;

  estatusReplicas: Array<number> = [estatusResultado.IncidenciasResultados, estatusResultado.EnvíoLaboratorioExterno, estatusResultado.CargaRéplicasLaboratorioExterno,
  estatusResultado.EnvíoaSRENAMECA, estatusResultado.CargaValidaciónSRENAMECA, estatusResultado.EnviadoResumenResultados];

  constructor(private IncidenciasResultadoService: IncidenciasResultadosService,
    private notificationService: NotificationService,
    private muestreoService: MuestreoService,
   private authService: AuthService,) { super(); }

  ngOnInit(): void {
    this.definirColumnas();
    this.consultarReplicas();

   
  }

  definirColumnas() {
    let nombresColumnas: Array<Column> = [
      {
        name: 'numentrega',
        label: 'N° ENTREGA',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#C9C9C9',
        width: 50.68,
      },
      {
        name: 'claveUnica',
        label: 'CLAVE ÚNICA',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 61.96,
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
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 66.95,
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
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 63.53,
      },
      {
        name: 'nombreParametro',
        label: 'NOMBRE',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 244.76,
      },
      {
        name: 'claveParametro',
        label: 'CLAVE PARÁMETRO',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 65.79,
      },
      {
        name: 'laboratorio',
        label: 'LABORATORIO',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 75.79,
      },
      {
        name: 'tipoCuerpoAgua',
        label: 'TIPO CUERPO DE AGUA',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 104.93,
      },
      {
        name: 'tipoCuerpoAguaOriginal',
        label: 'TIPO CUERPO DE AGUA ORIGINAL',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 104.94,
      },
      {
        name: 'resultado',
        label: 'RESULTADO',
        order: 10,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F5B084',
        width: 85.96,
      },
      {
        name: 'esCorrecto',
        label: 'ES CORRECTO EL RESULTADO POR REGLAS DE VALIDACIÓN',
        order: 11,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#8EA9DB',
        width: 127.93,
      },
      {
        name: 'observacionReglasValidacion',
        label: 'OBSERVACIÓN REGLAS DE VALIDACIÓN',
        order: 12,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#8EA9DB',
        width: 151.9,
      },
      {
        name: 'aceptaRechazo',
        label: 'SE ACEPTA RECHAZO SI/NO',
        order: 13,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 51.24,
      },
      {
        name: 'resultadoReplica',
        label: 'RESULTADO RÉPLICA',
        order: 14,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 62.76,
      },
      {
        name: 'mismoResultado',
        label: 'ES EL MISMO RESULTADO',
        order: 15,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 62.76,
      },
      {
        name: 'observacionLaboratorio',
        label: 'OBSERVACIÓN LABORATORIO',
        order: 16,
        selectAll: true,
        filtered: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 75.81,
      },
      {
        name: 'fechaReplicaLaboratorio',
        label: 'FECHA DE RÉPLICA LABORATORIO',
        order: 17,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 75.79,
      },
      {
        name: 'observacionSrenameca',
        label: 'OBSERVACIÓN SRENAMECA',
        order: 18,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#00B0F0',
        width: 75.81,
      },
      {
        name: 'esCorrectoDato',
        label: '¿EL DATO ES CORRECTO?',
        order: 19,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#00B0F0',
        width: 65.21,
      },
      {
        name: 'fechaObservacionSrenameca',
        label: 'FECHA DE OBSERVACIÓN SRENAMECA',
        order: 20,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#00B0F0',
        width: 75.81,
      },
      {
        name: 'observacionReglasReplica',
        label: 'OBSERVACIONES REGLAS RÉPLICA',
        order: 21,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F8CBAD',
        width: 211.8,
      },
      {
        name: 'aprobacíonResultado',
        label: 'SE APRUEBA EL RESULTADO DESPUES DE LA RÉPLICA',
        order: 22,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#9966FF',
        width: 225.89
      },
      {
        name: 'fechaEstatusFinal',
        label: 'FECHA DE ESTATUS FINAL',
        order: 23,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#A9D08E',
        width: 87.93,
      },
      {
        name: 'usuarioReviso',
        label: 'USUARIO QUE REVISÓ',
        order: 24,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#A9D08E',
        width: 89.93,
      },
      {
        name: 'estatusResultado',
        label: 'ESTATUS DEL RESULTADO',
        order: 25,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FFFF00',
        width: 453.43
      },
      {
        name: 'arcivo',
        label: 'NOMBRE ARCHIVO DE EVIDENCIAS',
        order: 26,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 92.95,
      },
      {
        name: 'evidenciasReplica',
        label: 'EVIDENCIAS RÉPLICA',
        order: 27,
        selectAll: true,
        filtered: false,
        desc: false,
        asc: false,
        data: [],
        filteredData: [],
        dataType: 'date',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 98.94
      }
    ];

    let avanceEstatusColumnas: Array<Column> = [
      {
        name: 'claveUnica',
        label: 'CLAVE ÚNICA',
        order: 1,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#F4B084',
        width: 168.8,
      },
      {
        name: '',
        label: '1.RECHAZADO POR REGLAS DE VALIDACIÓN',
        order: 2,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#8EA9DB',
        width: 110.94,
      },
      {
        name: '',
        label: '2.RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO EXTERNO',
        order: 3,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 214.8,
      },
      {
        name: '',
        label: '3.RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO EXTERNO - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA',
        order: 4,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'number',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FF66FF',
        width: 252.74,
      },
      {
        name: '',
        label: '4.RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO (CONALAB) - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA - ENVIADO A SRENAMECA',
        order: 5,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#00B0F0',
        width: 313.65
      },
      {
        name: '',
        label: '5.RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO (CONALAB) - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA - ENVIADO A SRENAMECA - CARGA VALIDACIÓN SRENAMECA',
        order: 6,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#00B0F0',
        width: 387.55,
      },
      {
        name: '',
        label: '3.1 ENVIADO A REGISTRO ORIGINAL ACUMULACIÓN DE RESULTADOS',
        order: 7,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#99FFCC',
        width: 161.88,
      },
      {
        name: '',
        label: '3.2 ENVIADO A REGISTRO ORIGINAL ACUMULACIÓN DE RESULTADOS -INICIAL DE REGLAS',
        order: 8,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#99FFCC',
        width: 188.84,
      },
      {
        name: '',
        label: '3.3ENVIADO A REGISTRO ORIGINAL ACUMULACIÓN DE RESULTADOS -INICIAL DE REGLAS - MÓDULO DE REGLAS',
        order: 9,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#99FFCC',
        width: 214.8
      },
      {
        name: '',
        label: '3.4 ENVIADO A REGISTRO ORIGINAL ACUMULACIÓN DE RESULTADOS - INICIAL DE REGLAS - MÓDULO DE REGLAS - RESUMEN REGLAS',
        order: 10,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#99FFCC',
        width: 184.84,
      },
      {
        name: '',
        label: '6.1 RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO (CONALAB) - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA - ENVIADO A SRENAMECA - CARGA VALIDACIÓN SRENAMECA - ENVIADO A RESUMEN DE RESULTADOS (2.4)',
        order: 11,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#AA7EEA',
        width: 363.59
      },
      {
        name: '',
        label: '6.1.1 RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO (CONALAB) - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA - ENVIADO A SRENAMECA - CARGA VALIDACIÓN SRENAMECA - ENVIADO A RESUMEN DE RESULTADOS (2.4) - ENVIADO A LIBERACIÓN DE RESULTADOS',
        order: 12,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FFC000',
        width: 404.53,
      },
      {
        name: '',
        label: '6.1.2 RECHAZADO POR REGLAS DE VALIDACIÓN - ENVIADO A LABORATORIO (CONALAB) - CARGA RÉPLICAS LAB EXTERNO Y ENVIADO A EBASECA - ENVIADO A SRENAMECA - CARGA VALIDACIÓN SRENAMECA - ENVIADO A RESUMEN DE RESULTADOS (2.4)- ENVIADO A PENALIZACIÓN',
        order: 13,
        selectAll: true,
        filtered: false,
        asc: false,
        desc: false,
        data: [],
        filteredData: [],
        dataType: 'string',
        specialFilter: '',
        secondSpecialFilter: '',
        selectedData: '',
        color: '#FFC000',
        width: 420.69
      }
    ];

    this.columns = nombresColumnas;
    this.columnsAvance = avanceEstatusColumnas;
    this.setHeadersList(this.columns);
  }

  consultarReplicas(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.IncidenciasResultadoService
      .getReplicasResultadosPaginados(
        this.estatusReplicas,
        page,
        pageSize,
        filter,
        this.orderBy
      )
      .subscribe({
        next: (response: any) => {          
          this.selectedPage = false;
          this.replicasResultados = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;

          this.getPreviousSelected(
            this.replicasResultados,
            this.resultadosFiltrados
          );
          this.selectedPage = this.anyUnselected(this.replicasResultados)
            ? false
            : true;
        },
        error: (error) => { },
      });
  }

  getPreviousSelected(
    muestreos: Array<ReplicasResultadosReglaVal>,
    muestreosSeleccionados: Array<ReplicasResultadosReglaVal>
  ) {
    muestreos.forEach((f) => {
      let muestreoSeleccionado = muestreosSeleccionados.find(
        (x) => f.resultadoMuestreoId === x.resultadoMuestreoId
      );

      if (muestreoSeleccionado != undefined) {
        f.selected = true;
      }
    });
  }

  cargarArchivo(event: Event, tipoArchivo: number) {
    this.archivo = (event.target as HTMLInputElement).files ?? new FileList();
    if (this.archivo) {
      this.loading = true;

      this.IncidenciasResultadoService
        .cargarArchivo(this.archivo[0], tipoArchivo, this.authService.getUser().usuarioId)
        .subscribe({
          next: (response: any) => {
            if (response) {
              this.loading = false;
              switch (tipoArchivo) {
                case this.ReplicaLaboratorioExterno:
                  this.resetInputFile(this.inputExcelLaboratorio);
                  break;
                case this.ReplicaSrenameca:
                  this.resetInputFile(this.inputExcelSRENAMECA);
                  break;
                case this.ReplicaAprobacion:
                  this.resetInputFile(this.inputExcelAprobacionRechazo);
                  break;
                default:
                  break;
              }
              
              this.consultarReplicas();
              return this.notificationService.updateNotification({
                show: true,
                type: NotificationType.success,
                text: 'Archivo procesado correctamente.',
              });
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
            FileService.download(archivoErrores, 'errores.txt');
            this.resetInputFile(this.inputExcelLaboratorio);
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.danger,
              text: 'Se encontraron errores en el archivo procesado.',
            });
          },
        });
    }
  }

  enviarCorreo() {
    this.IncidenciasResultadoService
      .sendEmailFile(this.resultadosFiltrados, this.tipoArchivo, this.envioCorreo)
      .subscribe({
        next: (response: any) => {
          if (response) {
            return this.notificationService.updateNotification({
              show: true,
              type: NotificationType.success,
              text: 'Se envio el correo exitosamente',
            });
          }
        },
        error: (error) => {
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'Error al enviar el correo',
          });
        },
      });  }

  onFilterIconClick(column: Column) {
    this.collapseFilterOptions(); //Ocultamos el div de los filtros especiales, que se encuetren visibles

    let filteredColumns = this.getFilteredColumns(); //Obtenemos la lista de columnas que están filtradas
    this.muestreoService.filtrosSeleccionados = filteredColumns; //Actualizamos la lista de filtros, para el componente de filtro
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
      this.IncidenciasResultadoService
        .getDistinctValuesFromColumn(column.name,this.estatusReplicas, this.cadena,)
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
          error: (error) => { },
        });
    }

    if (esFiltroEspecial) {
      column.selectAll = false;
      this.getPreseleccionFiltradoColumna(column, esFiltroEspecial);
    }
  }

  sort(column: string, type: string) {
    this.orderBy = { column, type };
    this.IncidenciasResultadoService
      .getReplicasResultadosPaginados(this.estatusReplicas, this.page, this.NoPage, this.cadena, {
        column: column,
        type: type,
      })
      .subscribe({
        next: (response: any) => {
          this.replicasResultados = response.data;
        },
        error: (error) => { },
      });
  }

  onDeleteFilterClick(columName: string) {
    this.deleteFilter(columName);
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.consultarReplicas();
  }

  filtrar(columna: Column, isFiltroEspecial: boolean) {
    this.existeFiltrado = true;
    this.cadena = !isFiltroEspecial
      ? this.obtenerCadena(columna, false)
      : this.obtenerCadena(this.columnaFiltroEspecial, true);
    this.consultarReplicas();

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
    this.muestreoService.filtrosSeleccionados = this.getFilteredColumns();
    this.hideColumnFilter();
  }

  onSelectClick(replica: ReplicasResultadosReglaVal) {
    if (this.selectedPage) this.selectedPage = false;
    if (this.selectAllOption) this.selectAllOption = false;
    if (this.allSelected) this.allSelected = false;

    //Vamos a agregar este registro, a los seleccionados
    if (replica.selected) {
      this.resultadosFiltrados.push(replica);
      this.selectedPage = this.anyUnselected(this.replicasResultados) ? false : true;
    } else {
      let index = this.resultadosFiltrados.findIndex(
        (m) => m.resultadoMuestreoId === replica.resultadoMuestreoId
      );
      if (index > -1) {
        this.resultadosFiltrados.splice(index, 1);
      }
    }
  }

  exportarResultados(): void {
    if (this.resultadosFiltrados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para descargar',
      });
    }
    this.loading = true;
    this.IncidenciasResultadoService
      .descargarInformacion(this.resultadosFiltrados)
      .subscribe({
        next: (response: any) => {
          FileService.download(response, 'ReplicasResultadosValidados.xlsx');
          this.resetValues();
          this.unselectResultados();
          this.loading = false;
        },
        error: (response: any) => {
          this.loading = false;
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible descargar la información',
          });
        },
      });
  }

  private resetValues() {
    this.resultadosFiltrados = [];
    this.selectAllOption = false;
    this.allSelected = false;
    this.selectedPage = false;
  }

  private unselectResultados() {
    this.replicasResultados.forEach((m) => (m.selected = false));
  }

  resetCorreo(tipoArchivo: number) {
    this.tipoArchivo = tipoArchivo;
    let nombreArchivo = (tipoArchivo == this.ReplicaLaboratorioExterno) ? "ReplicasLaboratorioExterno.xlsx" : "ReplicasSrenameca.xlsx";
    this.envioCorreo.destinatarios = "";
    this.envioCorreo.cuerpo = "";
    this.envioCorreo.copias = "";
    this.envioCorreo.archivos = [];
    this.envioCorreo.archivos.push({ ruta: '', nombreArchivo: nombreArchivo, extension: 'xlsx' });
    document.getElementById('btnMdlConfirmacion')?.click();
  }

  cargarEvidencias(
    event: Event,
    nombreEvidencia: string = ''
  ) {
    let evidencias = (event.target as HTMLInputElement).files ?? new FileList();
    let errores = this.validarTamanoArchivos(evidencias);   

    if (errores !== '') {
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text:
          'Se encontraron errores en las evidencias seleccionadas: ' + errores,
      });
    }

    this.loading = !this.loading;
    this.IncidenciasResultadoService.cargarEvidencias(evidencias).subscribe({
      next: (response) => {
        this.resetInputFile(this.fileUpload);
        this.notificationService.updateNotification({
          show: true,
          type: NotificationType.success,
          text: 'Evidencias cargadas correctamente',
        });
        this.loading = !this.loading;
      },
      error: (response: any) => {
        this.resetInputFile(this.fileUpload);        
        this.loading = !this.loading;
        let archivo = this.generarArchivoDeErrores(
          response.error.Errors.toString()
        );
        FileService.download(archivo, 'errores.txt');
      },
    });
  }
  descargarEvidencias() {
    if (this.resultadosFiltrados.length === 0) {
      this.notificationService.updateNotification({
        show: true,
        text: 'No ha seleccionado ningúna replica',
        type: NotificationType.warning,
      });
      return this.hacerScroll();
    }

    this.loading = true;
    this.IncidenciasResultadoService
      .descargarArchivos(this.resultadosFiltrados.map((m) => m.id))
      .subscribe({
        next: (response: any) => {
          this.loading = !this.loading;
          this.seleccionarTodosChck = false;
          this.resultadosFiltrados.map((m) => (m.selected = false));
          FileService.download(response, 'EvidenciasReplicas.zip');
        },
        error: (response: any) => {
          this.loading = !this.loading;
          this.resultadosFiltrados.map((m) => (m.selected = false));
          this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible descargar la información',
          });
          this.hacerScroll();
        },
      });

    this.resetValues();
    this.unselectResultados();
  }
  onPreviewOrDownloadFileClick() { }
  existeEvidencia(evidencias: Array<Evidencia>) {
    if (evidencias.length > 1) {
      return 'zip';
    }
    return evidencias.find((f) => f.sufijo);
    
  }
  enviarResumen() {

    if (this.resultadosFiltrados.length == 0 && !this.allSelected) {
      this.hacerScroll();
      return this.notificationService.updateNotification({
        show: true,
        type: NotificationType.warning,
        text: 'No hay información seleccionada para ser eviada a resumen de resultados',
      });
    }
    this.loading = true;
    this.IncidenciasResultadoService
      .enviarResumenResultados(this.resultadosFiltrados)
      .subscribe({
        next: (response: any) => {
         
          this.resetValues();
          this.unselectResultados();
          this.loading = false;
        },
        error: (response: any) => {
          this.loading = false;
          this.hacerScroll();
          return this.notificationService.updateNotification({
            show: true,
            type: NotificationType.danger,
            text: 'No fue posible descargar la información',
          });
        },
      });

  }
}
