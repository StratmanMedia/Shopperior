import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () => import('../dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'categories',
    loadChildren: () => import('../categories/categories.module').then(m => m.CategoriesModule)
  },
  {
    path: 'lists',
    loadChildren: () => import('../shopping-lists/shopping-lists.module').then(m => m.ShoppingListsModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserLayoutRoutingModule { }
