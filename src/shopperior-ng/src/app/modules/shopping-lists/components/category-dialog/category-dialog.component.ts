import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-dialog',
  templateUrl: './category-dialog.component.html',
  styleUrls: ['./category-dialog.component.scss']
})
export class CategoryDialogComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'CategoryDialogComponent'
  });

  constructor(
    public dialogRef: MatDialogRef<CategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CategoryModel) { }

  ngOnInit(): void {
  }

  saveCategory(category: CategoryModel): void {
    this._logger.debug('Sending category: ' + JSON.stringify(category));
    this.dialogRef.close(category);
    // this._categoryService.add(categoryModel).subscribe(() => {
    //   this.router.navigateByUrl('/app/categories');
    // });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
