/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ConnectionManagerService } from './connection-manager.service';

describe('Service: ConnectionManager', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConnectionManagerService]
    });
  });

  it('should ...', inject([ConnectionManagerService], (service: ConnectionManagerService) => {
    expect(service).toBeTruthy();
  }));
});
