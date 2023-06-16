import { TestBed } from '@angular/core/testing';

import { RevisionResultadoService } from './revision-resultado.service';

describe('RevisionResultadoService', () => {
  let service: RevisionResultadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RevisionResultadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
