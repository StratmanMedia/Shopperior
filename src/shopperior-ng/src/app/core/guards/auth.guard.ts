import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { Observable, Subscriber } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
      private router: Router,
      private socialAuthService: SocialAuthService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      return this.socialAuthService.authState.pipe(
        map((socialUser: SocialUser) => !!socialUser),
        tap((isLoggedIn: boolean) => {
          if (!isLoggedIn) {
            this.router.navigateByUrl('/login');
          }
        })
      );
  }
}
