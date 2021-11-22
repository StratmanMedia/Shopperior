import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { AuthService } from 'src/app/core/auth/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
      private router: Router,
      private authService: AuthService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    //   console.log('AuthGuard hit');
    //   return new Observable((subscriber: Subscriber<boolean>) => {
    //     this.authService.isLoggedIn().subscribe(isLoggedIn => {
    //       console.log('isLoggedIn:', isLoggedIn);
    //       subscriber.next(isLoggedIn);
    //       subscriber.complete();
    //       if (!isLoggedIn) { this.authService.signin(); }
    //     });
    //   });
    return true;
  }
}
