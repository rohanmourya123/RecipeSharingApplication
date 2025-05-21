import { Recipe } from './Recipe.model'
import { ApplicationUser } from './ApplicationUser.model';

export interface Rating {
  id: number;
  value: number;
  userId: string;
  user?: ApplicationUser;
  recipeId: number;
  recipe?: Recipe;
}
