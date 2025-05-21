import { Component, OnInit, inject, input, signal } from '@angular/core';
import { RecipeService } from '../Services/recipe.service';
import { Recipe } from '../Models/Intefaces/Recipe.model';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatBadgeModule } from '@angular/material/badge';
// import { CardComponent } from "../Shared/card/card.component";
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';
import { AuthServiceService } from '../Services/auth-service.service';
import { TableComponent } from "../Shared/table/table.component";
import { MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-public-home-page',
  standalone: true,
  imports: [
    FormsModule,
    // NgFor,
    // CardComponent,
    MatBadgeModule,
    MatIconModule,
    MatButtonModule,
    MatGridListModule,
    TableComponent,
     MatPaginatorModule
  ],
  templateUrl: './public-home-page.component.html',
  styleUrl: './public-home-page.component.css'
})
export class PublicHomePageComponent implements OnInit {

  searchTerm: string = '';
  category: string = '';
  ingredient: string = '';
  pageNumber: number = 1;
  pageSize: number = 10;
  totalCount: number = 0;
  AllRecipes: Recipe[] = [];

  router = inject(Router);
  authService = inject(AuthServiceService);
  recipeService = inject(RecipeService);

  ngOnInit(): void {
    this.getAllData();

  }

getAllData() {
  this.recipeService.getAllRecipes(
    this.searchTerm,
    this.category,
    this.ingredient,
    this.pageNumber,
    this.pageSize
  ).subscribe(res => {
    this.AllRecipes = res.items;
    this.totalCount = res.totalCount;
    console.log(res);
  });
}

  getDetails(id: number) {
    if (id != null) {
      this.router.navigate(['/detail/', id]);

    }

  }

  togglePostForm() {
    var token = this.authService.getToken();
    if (token == null) {
      this.router.navigate(['/login']);
    }
    this.router.navigate(['/postRecipe']);
  }

//   onSearch() {
//   this.pageNumber = 1;
//   this.getAllData();
// }

// onPageChange(event: any) {
//   this.pageNumber = event.pageIndex + 1;
//   this.pageSize = event.pageSize;
//   this.getAllData();
// }

}