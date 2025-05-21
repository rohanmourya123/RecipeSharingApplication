import { Recipe } from './Recipe.model';

export interface Ingredient {
  id: number;
  name: string;
  recipeId: number;
  recipe: Recipe;
}
