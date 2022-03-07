import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-shopping-list-add-item',
  templateUrl: './shopping-list-add-item.component.html',
  styleUrls: ['./shopping-list-add-item.component.scss']
})
export class ShoppingListAddItemComponent implements OnInit {
  listGuid: string;

  constructor(
    private route: ActivatedRoute) {
      
    this.listGuid = this.route.snapshot.params.list;
  }

  ngOnInit(): void {
  }

}
