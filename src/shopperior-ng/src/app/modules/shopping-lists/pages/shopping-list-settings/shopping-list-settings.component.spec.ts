import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingListSettingsComponent } from './shopping-list-settings.component';

describe('ShoppingListSettingsComponent', () => {
  let component: ShoppingListSettingsComponent;
  let fixture: ComponentFixture<ShoppingListSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingListSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingListSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
