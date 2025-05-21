import { Ingredient } from "./Ingredient.model";
import { Instruction } from "./Instruction.model";
import { ApplicationUser } from "./ApplicationUser.model";
import { Rating } from "./Rating.model";

export interface Recipe {
  id: number;
  title: string;
  description: string;
  category: string;
  imageUrl: string;

  userId: string;
  user?: ApplicationUser;

  instructions: Instruction[];
  ingredients: Ingredient[];
  comments: Comment[];
  ratings: Rating[];
}
