import {
  AfterViewInit,
  Component,
  Input,
  ViewChild,
  OnChanges,
  SimpleChanges,
  EventEmitter,
  Output,
  inject,
} from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { RecipeService } from '../../Services/recipe.service';
import { UserServiceService } from '../../Services/user-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table',
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule
  ],
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements AfterViewInit, OnChanges {
  @Input() tableData: any[] = [];
  @Input() columnKeys: string[] = [];
  @Output() dataChanged = new EventEmitter<void>();
    @Output() detailsClick = new EventEmitter<number>();
  router = inject(Router);

  recipeService = inject(RecipeService);
  userService = inject(UserServiceService);



  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tableData'] || changes['columnKeys']) {
      this.dataSource = new MatTableDataSource(this.tableData);
      this.displayedColumns = this.columnKeys;

      // re-assign paginator and sort if available
      if (this.paginator) this.dataSource.paginator = this.paginator;
      if (this.sort) this.dataSource.sort = this.sort;
    }
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getDisplayValue(row: any, key: string): any {
    const keys = key.split('.');
    let value = row;
    for (const k of keys) {
      value = value?.[k];
      if (value === undefined || value === null) return '';
    }
    return typeof value === 'object' ? JSON.stringify(value) : value;
  }

  value: string = '';

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    // this.dataSource.filter = this.value;

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  onDetails(id: number) {
    this.detailsClick.emit(id);
  }

  onDelete(id: number) {
    var logedInUser = localStorage.getItem('user');
    // console.log(logedInUser);

    if (logedInUser) {
      const user = JSON.parse(logedInUser);
      this.recipeService.getRecipeById(id).subscribe((res => {
        // console.log(res);
        if ((res !== null) && (res.userId === user.uID)) {
              this.recipeService.deleteById(id).subscribe(r=>{
                // console.log(r);
                 alert('Deleted');
                 this.dataChanged.emit();
                //  this.router.navigate(['/home']);


              }) 
        }
        else {
          alert('Dont have Access');
        }

      }))
    }


  }
}

