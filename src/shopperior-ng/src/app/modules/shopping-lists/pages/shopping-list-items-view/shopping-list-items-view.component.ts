import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';

@Component({
  selector: 'app-shopping-list-items-view',
  templateUrl: './shopping-list-items-view.component.html',
  styleUrls: ['./shopping-list-items-view.component.scss']
})
export class ShoppingListItemsViewComponent implements OnInit {

  shoppingList = new Observable<ShoppingListModel>();
  guid: string;

  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService) {

    this.guid = this.route.snapshot.params.list;
    this.shoppingList = this.shoppingListService.getOne(this.guid);
  }

  ngOnInit(): void {
  }

  public goBack(): void {
    this.navService.goBack();
  }

}
