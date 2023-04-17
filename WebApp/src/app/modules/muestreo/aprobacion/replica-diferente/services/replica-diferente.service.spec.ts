import { TestBed } from '@angular/core/testing';

import { ReplicaDiferenteService } from './replica-diferente.service';

describe('ReplicaDiferenteService', () => {
  let service: ReplicaDiferenteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReplicaDiferenteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
