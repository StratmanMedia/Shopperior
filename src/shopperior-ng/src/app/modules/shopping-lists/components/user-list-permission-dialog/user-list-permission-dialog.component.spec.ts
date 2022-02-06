import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserListPermissionDialogComponent } from './user-list-permission-dialog.component';

describe('UserListPermissionDialogComponent', () => {
  let component: UserListPermissionDialogComponent;
  let fixture: ComponentFixture<UserListPermissionDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserListPermissionDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListPermissionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
