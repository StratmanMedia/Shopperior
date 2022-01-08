import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VisitorLayoutRoutingModule } from './visitor-layout-routing.module';
import { VisitorLayoutComponent } from 'src/app/modules/visitor-layout/pages/visitor-layout/visitor-layout.component';
import { MaterialModule } from 'src/app/core/material/material.module';

@NgModule({
  declarations: [
    VisitorLayoutComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    VisitorLayoutRoutingModule
  ]
})
export class VisitorLayoutModule { }
