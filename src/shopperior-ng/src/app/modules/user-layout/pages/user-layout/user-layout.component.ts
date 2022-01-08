import { Component, OnInit } from '@angular/core';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.scss']
})
export class UserLayoutComponent implements OnInit {
  user: SocialUser;

  constructor(private _socialAuthService: SocialAuthService) { }

  ngOnInit() {
    this._socialAuthService.authState.subscribe(
      socialUser => {
        this.user = socialUser;
      }
    );
  }

}
