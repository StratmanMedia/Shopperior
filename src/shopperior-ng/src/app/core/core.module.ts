import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VisitorLayoutComponent } from './visitor-layout/visitor-layout.component';
import { RouterModule } from '@angular/router';
import { UserLayoutComponent } from './user-layout/user-layout.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './auth/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { LocalDataService } from './data/local/local-data.service';
import { ShoppingListService } from './data/list/shopping-list.service';

@NgModule({
  declarations: [
    VisitorLayoutComponent,
    UserLayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    MaterialModule
  ],
  providers: [
    AuthGuard,
    AuthService,
    LocalDataService,
    ShoppingListService
  ]
})
export class CoreModule { }
