import { TestBed } from '@angular/core/testing';
import { environment } from 'src/environments/environment';

import { BaseUrlInterceptor } from './base-url.interceptor';

describe('BaseUrlInterceptor', () => {
  beforeEach(() =>
    TestBed.configureTestingModule({
      providers: [
        BaseUrlInterceptor,
        {
          provide: 'BASE_API_URL',
          useValue: environment.apiUrl,
        },
      ],
    })
  );

  it('should be created', () => {
    const interceptor: BaseUrlInterceptor = TestBed.inject(BaseUrlInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
