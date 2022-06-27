import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { UserListPermissionModel } from 'src/app/core/data/list/models/user-list-permission-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';
import { environment } from 'src/environments/environment';
import { UserListPermissionDialogComponent } from '../../components/user-list-permission-dialog/user-list-permission-dialog.component';

@Component({
  selector: 'app-shopping-list-settings',
  templateUrl: './shopping-list-settings.component.html',
  styleUrls: ['./shopping-list-settings.component.scss']
})
export class ShoppingListSettingsComponent implements OnInit, OnDestroy {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: typeof ShoppingListSettingsComponent
  });
  private ngUnsubscribe = new Subject<void>();
  listForm: UntypedFormGroup;
  shoppingList: ShoppingListModel;
  guid: string;
  showPermissionForm: boolean = false;
  permissionForm: UntypedFormGroup;
  newPermission: UserListPermissionModel;

  constructor(
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService) { }

  ngOnInit(): void {
    this.guid = this.route.snapshot.params.list;
    this.shoppingListService.getOne(this.guid).pipe(take(1))
      .subscribe(
        list => {
          this.shoppingList = JSON.parse(JSON.stringify(list));
          this.listForm = this.buildListForm(this.shoppingList);
        }
      );
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  goBack(): void {
    this.navService.goBack();
  }

  removePermission(list: UserListPermissionModel) {
    if (list.permission !== 'OWNER') {
      const index = this.shoppingList.permissions.findIndex(l => l.userGuid === list.userGuid);
      this.shoppingList.permissions.splice(index, 1);
    }
  }

  openPermissionDialog(): void {
    const dialogRef = this.dialog.open(UserListPermissionDialogComponent, {
      width: '100%',
      data: <UserListPermissionModel>{
        userEmail: '',
        shoppingListGuid: this.guid,
        permission: ''
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.shoppingList.permissions.push(result);
      }
    });
  }

  addPermission(): void {
    this.shoppingList.permissions.push(this.newPermission);
  }

  submitForm(): void {
    const shoppingListModel = <ShoppingListModel>{
      guid: this.guid,
      name: this.listForm.controls.name.value,
      permissions: this.shoppingList.permissions
    };
    this.shoppingListService.update(shoppingListModel).subscribe(() => {
      this.router.navigateByUrl(`/app/lists/${this.guid}`);
    });
  }

  cancel(): void {
    this.router.navigateByUrl(`/app/lists/${this.guid}`);
  }

  private buildListForm(shoppingList: ShoppingListModel): UntypedFormGroup {
    const form = new UntypedFormGroup({
      name: new UntypedFormControl(shoppingList.name, Validators.required)
    });
    return form;
  }
}
