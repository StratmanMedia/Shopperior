import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-list-item-dialog',
  templateUrl: './list-item-dialog.component.html',
  styleUrls: ['./list-item-dialog.component.scss']
})
export class ListItemDialogComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ListItemDialogComponent'
  });
  
  constructor(
    public dialogRef: MatDialogRef<ListItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ListItemModel,
    private _shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
  }

  calculateTotal(): void {
    if (this.data.unitPrice > 0) {
      const newTotal = Math.round(this.data.quantity * this.data.unitPrice * 100) / 100;
      this.data.totalPrice = newTotal;
    }
  }

  calculateUnitPrice(): void {
    if (this.data.quantity) {
      const newUnitPrice = Math.round((this.data.totalPrice / this.data.quantity) * 100) /100;
      this.data.unitPrice = newUnitPrice;
    }
  }

  saveItem(): void {
    this._logger.debug(`Saving item. ${JSON.stringify(this.data)}`);
    this._shoppingListService.addItem(this.data).subscribe(
      () => {
        this._logger.debug(`Item saved.`);
        this.dialogRef.close();
      },
      err => {
      }
    )
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
