import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(
    private _authService: AuthService,
    private _router: Router) { }

  ngOnInit() {
    this._authService.isAuthenticated$.subscribe(
      isAuth => {
        if (isAuth) {
          this._router.navigateByUrl('/app/dashboard');
        }
      }
    )
  }

}
