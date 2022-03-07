import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingListAddItemComponent } from './shopping-list-add-item.component';

describe('ShoppingListAddItemComponent', () => {
  let component: ShoppingListAddItemComponent;
  let fixture: ComponentFixture<ShoppingListAddItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingListAddItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingListAddItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
