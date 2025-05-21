import { ApplicationUser } from './ApplicationUser.model';
import { Recipe } from './Recipe.model';

export interface Comment {
  id: number;
  content: string;
  createdAt: Date;
  userId: string;
  user?: ApplicationUser;
  recipeId: number;
  recipe?: Recipe;
}
