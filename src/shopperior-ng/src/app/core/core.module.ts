import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { LocalDataService } from './data/local/local-data.service';
import { ShoppingListService } from './data/list/shopping-list.service';
import { AuthModule } from '@auth0/auth0-angular';
import { AuthGuard } from './auth/auth.guard';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    MaterialModule,
    AuthModule.forRoot({
      domain: environment.oidc.authority,
      audience: environment.oidc.audience,
      clientId: environment.oidc.client_id,
      redirectUri: environment.oidc.redirect_uri
    })
  ],
  providers: [
    // AuthService,
    AuthGuard,
    LocalDataService,
    ShoppingListService
  ]
})
export class CoreModule { }
