import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './auth/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { LocalDataService } from './data/local/local-data.service';
import { ShoppingListService } from './data/list/shopping-list.service';

@NgModule({
  declarations: [],
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
