import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { ShoppingListsViewComponent } from './pages/shopping-lists-view/shopping-lists-view.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { ShoppingListCreateComponent } from './pages/shopping-list-create/shopping-list-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShoppingListItemsViewComponent } from './pages/shopping-list-items-view/shopping-list-items-view.component';
import { ShoppingListItemAddComponent } from './pages/shopping-list-item-add/shopping-list-item-add.component';
import { ListItemFormComponent } from './components/list-item-form/list-item-form.component';
import { ShoppingListSettingsComponent } from './pages/shopping-list-settings/shopping-list-settings.component';
import { UserListPermissionDialogComponent } from './components/user-list-permission-dialog/user-list-permission-dialog.component';

@NgModule({
  declarations: [
    ShoppingListsViewComponent,
    ShoppingListCreateComponent,
    ShoppingListItemsViewComponent,
    ShoppingListItemAddComponent,
    ListItemFormComponent,
    ShoppingListSettingsComponent,
    UserListPermissionDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    ShoppingListsRoutingModule
  ]
})
export class ShoppingListsModule { }
