import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.scss']
})
export class UserLayoutComponent implements OnInit {

  fillerNav = [
    { name: 'Dashboard', link: '/app/dashboard' },
    { name: 'Lists', link: '/app/lists' }
  ];

  constructor() { }

  ngOnInit() {
  }

}
