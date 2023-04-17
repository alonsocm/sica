import { TestBed } from '@angular/core/testing';

import { ConsultaEvidenciaService } from './consulta-evidencia.service';

describe('ConsultaEvidenciaService', () => {
  let service: ConsultaEvidenciaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConsultaEvidenciaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
