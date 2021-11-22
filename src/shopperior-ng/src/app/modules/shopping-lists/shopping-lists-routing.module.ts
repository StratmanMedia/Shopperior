import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShoppingListCreateComponent } from './pages/shopping-list-create/shopping-list-create.component';
import { ShoppingListItemAddComponent } from './pages/shopping-list-item-add/shopping-list-item-add.component';
import { ShoppingListItemsViewComponent } from './pages/shopping-list-items-view/shopping-list-items-view.component';
import { ShoppingListsViewComponent } from './pages/shopping-lists-view/shopping-lists-view.component';

const routes: Routes = [
  { path: '', component: ShoppingListsViewComponent },
  { path: 'create', component: ShoppingListCreateComponent },
  { path: ':list', component: ShoppingListItemsViewComponent },
  { path: ':list/items/add', component: ShoppingListItemAddComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShoppingListsRoutingModule { }
