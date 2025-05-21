import { Recipe } from "./Recipe.model";

export interface Instruction {
  id: number;
  step: string;
  order: number;
  recipeId: number;
  recipe: Recipe;
}
