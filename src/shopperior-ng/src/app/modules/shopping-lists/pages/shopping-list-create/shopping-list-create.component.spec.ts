import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingListCreateComponent } from './shopping-list-create.component';

describe('ShoppingListCreateComponent', () => {
  let component: ShoppingListCreateComponent;
  let fixture: ComponentFixture<ShoppingListCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingListCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingListCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
