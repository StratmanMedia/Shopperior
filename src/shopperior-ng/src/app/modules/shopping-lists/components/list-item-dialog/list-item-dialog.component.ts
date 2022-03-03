import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CategoryService } from 'src/app/core/data/category/category.service';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
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
  categories: Observable<CategoryModel[]>;
  
  constructor(
    public dialogRef: MatDialogRef<ListItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ListItemModel,
    private _categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categories = this._categoryService.getMine();
    if (!this.data.quantity) {
      this.data.quantity = 1;
    }
    if (!this.data.measurement) {
      this.data.measurement = "ea";
    }
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
    this._logger.debug(`Saving item: ${JSON.stringify(this.data)}`);
    this.dialogRef.close(this.data);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
