import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { concatMap, take } from 'rxjs/operators';
import { CategoryService } from 'src/app/core/data/category/category.service';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { UserListPermissionModel } from 'src/app/core/data/list/models/user-list-permission-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';
import { environment } from 'src/environments/environment';
import { CategoryDialogComponent } from '../../components/category-dialog/category-dialog.component';
import { UserListPermissionDialogComponent } from '../../components/user-list-permission-dialog/user-list-permission-dialog.component';

@Component({
  selector: 'app-shopping-list-settings',
  templateUrl: './shopping-list-settings.component.html',
  styleUrls: ['./shopping-list-settings.component.scss']
})
export class ShoppingListSettingsComponent implements OnInit, OnDestroy {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ShoppingListSettingsComponent'
  });
  private ngUnsubscribe = new Subject<void>();
  listForm: FormGroup;
  shoppingList: ShoppingListModel;
  guid: string;
  showPermissionForm: boolean = false;
  permissionForm: FormGroup;
  newPermission: UserListPermissionModel;

  constructor(
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
    private shoppingListService: ShoppingListService,
    private _categoryService: CategoryService,
    private navService: NavigationService) { }

  ngOnInit(): void {
    this.guid = this.route.snapshot.params.list;
    this.shoppingListService.getOne(this.guid).pipe()
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

  openCategoryDialog(): void {
    const dialogRef = this.dialog.open(CategoryDialogComponent, {
      width: '100%',
      data: <CategoryModel>{
        shoppingListGuid: this.guid,
        name: ''
      }
    });
    dialogRef.afterClosed().pipe(
      concatMap((result: CategoryModel) => {
        if (!!result) {
          this._logger.debug('Saving category: ' + JSON.stringify(result));
          return this._categoryService.add(result);
        }
      })
    ).subscribe();
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

  private buildListForm(shoppingList: ShoppingListModel): FormGroup {
    const form = new FormGroup({
      name: new FormControl(shoppingList.name, Validators.required)
    });
    return form;
  }
}
