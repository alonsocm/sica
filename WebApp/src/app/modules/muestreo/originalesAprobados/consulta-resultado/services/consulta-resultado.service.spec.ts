import { TestBed } from '@angular/core/testing';

import { ConsultaResultadoService } from './consulta-resultado.service';

describe('ConsultaResultadoService', () => {
  let service: ConsultaResultadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConsultaResultadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
