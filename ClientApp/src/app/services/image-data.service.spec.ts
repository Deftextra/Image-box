import { TestBed } from '@angular/core/testing';

import { ImageDataService } from './image-data.service';

describe('ImageDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ImageDataService = TestBed.get(ImageDataService);
    expect(service).toBeTruthy();
  });
});
