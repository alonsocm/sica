import { TestBed } from '@angular/core/testing';

import { GlobalerrorHandlerServiceService } from './globalerror-handler-service.service';

describe('GlobalerrorHandlerServiceService', () => {
  let service: GlobalerrorHandlerServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalerrorHandlerServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
