import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkAdd } from './bulk-add';

describe('BulkAdd', () => {
  let component: BulkAdd;
  let fixture: ComponentFixture<BulkAdd>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BulkAdd]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BulkAdd);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
