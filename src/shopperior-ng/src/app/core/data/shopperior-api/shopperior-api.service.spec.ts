import { TestBed } from '@angular/core/testing';

import { ShopperiorApiService } from './shopperior-api.service';

describe('ShopperiorApiService', () => {
  let service: ShopperiorApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShopperiorApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
