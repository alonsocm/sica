import { TestBed } from '@angular/core/testing';

import { FormatoResultadoService } from './formato-resultado.service';

describe('FormatoResultadoService', () => {
  let service: FormatoResultadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormatoResultadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
