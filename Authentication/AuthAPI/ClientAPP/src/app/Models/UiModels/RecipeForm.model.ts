
export interface RecipeForm {
  title: string;
  description: string;
  category: string;
  imageUrl: string;
  // imageData?: File[]; // Uncomment if handling file uploads

  ingredients: string[];
  instructions: string[];
}

