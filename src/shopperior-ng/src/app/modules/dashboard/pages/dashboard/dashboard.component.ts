import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  shoppingLists: Observable<ShoppingListModel[]>;

  constructor(
    private _listService: ShoppingListService) {}

  ngOnInit() {
    this.shoppingLists = this._listService.getAll();
  }

}
