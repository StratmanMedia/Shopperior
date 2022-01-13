import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ShopperiorApiService } from '../data/shopperior-api/shopperior-api.service';
import { LoggingService } from '../logging/logging.service';

@Injectable()
export class AuthGuard implements CanActivate {
  private _logger = new LoggingService({callerName: 'AuthGuard', minimumLogLevel: 'debug'});

  constructor(
      private _router: Router,
      private _authService: AuthService,
      private _api: ShopperiorApiService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      this._logger.debug('AuthGuard hit.');
      return this._authService.isAuthenticated$.pipe(
        tap((loggedIn) => {
          if (!loggedIn) {
            this._authService.loginWithRedirect({redirect_uri: environment.oidc.redirect_uri});
          }
        })
      );
      return true;
  }
}
