import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VisitorLayoutComponent } from './modules/visitor-layout/pages/visitor-layout/visitor-layout.component';
import { UserLayoutComponent } from './modules/user-layout/pages/user-layout/user-layout.component';
import { AuthGuard } from './core/auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: VisitorLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./modules/visitor-layout/visitor-layout.module').then(m => m.VisitorLayoutModule)
      }
    ]
  },
  {
    path: '',
    component: UserLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'app',
        loadChildren: () => import('./modules/user-layout/user-layout.module').then(m => m.UserLayoutModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
