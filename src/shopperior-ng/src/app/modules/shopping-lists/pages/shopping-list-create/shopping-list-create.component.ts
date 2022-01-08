import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { Guid } from 'src/app/shared/classes/guid';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shopping-list-create',
  templateUrl: './shopping-list-create.component.html',
  styleUrls: ['./shopping-list-create.component.scss']
})
export class ShoppingListCreateComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: typeof ShoppingListCreateComponent
  });
  listForm: FormGroup = this.buildListForm();

  constructor(
    private shoppingListService: ShoppingListService,
    private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this._logger.warn(this.listForm.value);
    const shoppingListModel: ShoppingListModel = {
      guid: Guid.newGuid().toString(),
      name: this.listForm.controls.name.value,
      description: this.listForm.controls.description.value,
      items: []
    };
    this.shoppingListService.add(shoppingListModel).subscribe(() => {
      this.router.navigateByUrl('/lists');
    });
  }

  private buildListForm(): FormGroup {
    const form = new FormGroup({
      name: new FormControl('', Validators.required),
      description: new FormControl('')
    });
    return form;
  }
}
