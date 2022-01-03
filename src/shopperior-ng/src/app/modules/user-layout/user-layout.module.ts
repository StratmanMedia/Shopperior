import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLayoutRoutingModule } from './user-layout-routing.module';
import { UserLayoutComponent } from 'src/app/modules/user-layout/pages/user-layout/user-layout.component';
import { MaterialModule } from 'src/app/core/material/material.module';

@NgModule({
  declarations: [
    UserLayoutComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    UserLayoutRoutingModule
  ]
})
export class UserLayoutModule { }
