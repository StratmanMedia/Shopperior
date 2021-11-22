import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Observable, Subscriber, ReplaySubject, from } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable()
export class AuthService {

  private userManager = new UserManager(environment.oidcClientSettings as UserManagerSettings);
  private currentUserSubject = new ReplaySubject<User>(1);
  private currentUser: User | null;
  private initializedSubject = new ReplaySubject<boolean>(1);

  constructor() {
    console.log('AuthService constructor');
    this.initialize();
  }

  signin(): void {
    console.log('signin');
    this.userManager.signinRedirect();
  }

  signout(): void {
    console.log('signout');
    this.setCurrentUser(null);
    this.userManager.signoutRedirect();
  }

  isLoggedIn(): Observable<boolean> {
    return new Observable((subscriber: Subscriber<boolean>) => {
      this.initializedSubject.subscribe((initialized: boolean) => {
        if (initialized) { subscriber.next(this.currentUser != null); }
      });
    });
  }

  private initialize(): void {
    console.log('initialize');
    from(this.userManager.getUser()).subscribe((user: User) => {
      console.log('... userManager user:', user);
      this.setCurrentUser(user);
      this.initializedSubject.next(true);
    });
  }

  private setCurrentUser(user?: User): void {
    console.log('setCurrentUser');
    this.saveUserToStorage(user);
    this.currentUserSubject.next(user);
    this.currentUser = user;
  }

  private saveUserToStorage(user?: User) {
    console.log('saveUserToStorage');
    if (user == null) {
      console.log('... clear session');
      sessionStorage.clear();
    } else {
      console.log('... set storage: ' + JSON.stringify(user));
      if (sessionStorage.getItem('currentUser')) { sessionStorage.removeItem('currentUser'); }
      sessionStorage.setItem('currentUser', JSON.stringify(user));
    }
  }

  private loadUserFromStorage(): User {
    const storedUser = sessionStorage.getItem('currentUser');
    if (storedUser == null) {
      return null;
    }
    return JSON.parse(storedUser) as User;
  }

  getHeaders(headers: HttpHeaders): HttpHeaders {
    const currentUser = this.loadUserFromStorage();
    if (currentUser && currentUser.access_token) {
      return headers.append('Authorization', 'Bearer: ' + currentUser.access_token);
    }
    return headers;
  }
}
