export interface FilterList{
  entreocdl: Filter;
  claveunica:      Filter;
  clavesitio:      Filter;
  clavemonitoreo:  Filter;
  nombreSitio:     Filter;
  claparametro:    Filter;
  laboratorio:     Filter;
  tipocuerpoa:     Filter;
  tipocuerpoaoriginal: Filter;
  resultado:           Filter;
  tipoaprobacion:      Filter;
  correcto:            Filter;
  observa:             Filter;
  fecharevi:           Filter;
  nombreusu:           Filter;
  estatus:             Filter;
}

export class Filter{    
    values: string[];
    selectedValue: string;

    constructor(){
        this.values = [];
        this.selectedValue = "Seleccione";
    }
}
