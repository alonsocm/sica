import { Component, Input, OnInit } from '@angular/core';
import { DataTransferService } from '../../services/data-transfer.service';

@Component({
  selector: 'app-tabla',
  templateUrl: './tabla.component.html',
  styleUrls: ['./tabla.component.css']
})
export class TablaComponent implements OnInit {

  @Input() page: number | undefined;

  //Numero de paginas que quieres mostrar
  @Input() NoPage: number = 0;

  //Lista para llenar el body de la tabla
  @Input() Data!: any[];

  //Lista para llenar los encabezados de la tabla
  @Input() Headers!: any[];

  //Encabezado de la pagina 
  Title: string = "";

  constructor(private Dtransfer: DataTransferService) {
         

  }
 

  ngOnInit(): void {
    
    //COMPONENTE QUE RECIBE LA INFORMACION
    this.Dtransfer.trigger.subscribe(data => {      
      this.Headers = data.encabezados,
        this.Data = data.datos,
        this.Title = data.title
    })
  }

}
