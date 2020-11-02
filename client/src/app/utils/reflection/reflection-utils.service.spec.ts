import { TestBed } from '@angular/core/testing';

import { ReflectionUtilsService } from './reflection-utils.service';

describe('ReflectionUtilsService', () => {
  let service: ReflectionUtilsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReflectionUtilsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
