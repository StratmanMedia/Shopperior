import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';

@Component({
  selector: 'app-shopping-list-items-view',
  templateUrl: './shopping-list-items-view.component.html',
  styleUrls: ['./shopping-list-items-view.component.scss']
})
export class ShoppingListItemsViewComponent implements OnInit {

  private shoppingListSubject = new BehaviorSubject<ShoppingListModel>(null);
  guid: string;

  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService) {

    this.guid = this.route.snapshot.params.list;
    this.shoppingListService.getOne(this.guid).subscribe(list => {
      this.shoppingListSubject.next(list);
    });
  }

  ngOnInit(): void {
  }

  public get shoppingList(): ShoppingListModel {
    return this.shoppingListSubject.value;
  }

  public goBack(): void {
    this.navService.goBack();
  }

}
