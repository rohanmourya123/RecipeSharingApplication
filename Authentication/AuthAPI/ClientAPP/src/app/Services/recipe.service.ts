import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from '../Models/Intefaces/Recipe.model';
import { RecipeForm } from '../Models/UiModels/RecipeForm.model';
import { PagedResult } from '../Models/Intefaces/PagedResult.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  url: string = "https://localhost:7091/api/Recipe";
  http = inject(HttpClient);

 
  getAllRecipes(title?: string, category?: string, ingredients?: string, pageNumber: number = 1,
    pageSize: number = 10): Observable<PagedResult<Recipe>> {
    let params = new HttpParams()
    .set('pageNumber',pageNumber.toString())
    .set('pageSize',pageSize.toString());
    if (title) params = params.set('title', title);
    if (category) params = params.set('category', category);
    if (ingredients) params = params.set('ingredients', ingredients);

    return this.http.get<PagedResult<Recipe>>(this.url, { params });
  }

  getRecipeById(id: number): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.url}/${id}`);
  }


  createRecipe(recipe: RecipeForm): Observable<Recipe> {
    return this.http.post<Recipe>(this.url, recipe);
  }


  updateByID(id: number, data: RecipeForm): Observable<Recipe> {
    return this.http.put<Recipe>(`${this.url}/${id}`, data);
  }


  deleteById(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
