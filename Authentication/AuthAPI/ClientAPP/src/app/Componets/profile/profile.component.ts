import { Component, inject, OnInit } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Recipe } from '../../Models/Intefaces/Recipe.model';
import { ApplicationUser } from '../../Models/Intefaces/ApplicationUser.model';
import { RecipeService } from '../../Services/recipe.service';
import { UserServiceService } from '../../Services/user-service.service';
import { TableComponent } from '../../Shared/table/table.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [MatSidenavModule,TableComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  PostedRecipe: Recipe[] = [];
  allRecipes: Recipe[] = [];
  user: any;
  UId: string = '';

  recipeService = inject(RecipeService);
  userService = inject(UserServiceService);
  router = inject(Router);

  ngOnInit(): void {
    const res = localStorage.getItem('user');

    if (res) {
      const userDetails = JSON.parse(res);
      this.user = userDetails;
      this.UId = userDetails.uID;

      this.getAllRecipesDataAndFilter(this.UId);
    }
  }

  getAllRecipesDataAndFilter(userId: string) {

    console.log("form function ",this.UId);
    console.log("from function ",this.user)
    this.recipeService.getAllRecipes().subscribe((data) => {
      this.allRecipes = data.items;
      this.PostedRecipe = this.allRecipes.filter(recipe => recipe.userId === userId);
      console.log('Posted recipes by user:', this.PostedRecipe);
    });
  }


    getDetails(id: number) {
    if (id != null) {
      this.router.navigate(['/detail/', id]);

    }

  }
}
