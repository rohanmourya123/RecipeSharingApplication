import { Component, inject } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../Shared/form-field/form-field.component';
import { NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { CardComponent } from '../../Shared/card/card.component';
import { RecipeService } from '../../Services/recipe.service';
import { Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-post-recipe',
  imports: [
    CardComponent,
    ReactiveFormsModule,
    FormFieldComponent,
    NgFor, NgIf,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCardModule,
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './post-recipe.component.html',
  styleUrls: ['./post-recipe.component.css']
})
export class PostRecipeComponent {
  recipeService = inject(RecipeService);
  router = inject(Router);
  snackBar = inject(MatSnackBar);

  // Category options
  categories: string[] = ['Breakfast', 'Lunch', 'Dinner', 'Dessert', 'Snack', 'Beverage'];

  // Fields excluding category
  Fields = [
    { name: 'title', placeholder: 'Enter title', type: 'text' },
    { name: 'description', placeholder: 'Enter description', type: 'text' },
    { name: 'imageUrl', placeholder: 'Enter image URL', type: '' }
  ];

  recipeForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    category: new FormControl('', [Validators.required]),
    imageUrl: new FormControl('', [Validators.required]),
    ingredients: new FormArray([new FormControl('', Validators.required)]),
    instructions: new FormArray([new FormControl('', Validators.required)])
  });

  get ingredients(): FormArray {
    return this.recipeForm.get('ingredients') as FormArray;
  }

  get instructions(): FormArray {
    return this.recipeForm.get('instructions') as FormArray;
  }

  getControl(name: string): FormControl {
    return this.recipeForm.get(name) as FormControl;
  }

  get ingredientControl() {
    return this.ingredients.controls as FormControl[];
  }

  get instructionControl() {
    return this.instructions.controls as FormControl[];
  }

  addIngredient() {
    this.ingredients.push(new FormControl('', Validators.required));
  }

  removeIngredient(index: number) {
    this.ingredients.removeAt(index);
  }

  addInstruction() {
    this.instructions.push(new FormControl('', Validators.required));
  }

  removeInstruction(index: number) {
    this.instructions.removeAt(index);
  }

  onSubmit() {
    if (this.recipeForm.valid) {
      const formValue = this.recipeForm.value;

      const recipeData = {
        title: formValue.title!,
        description: formValue.description!,
        category: formValue.category!,
        imageUrl: formValue.imageUrl!,
        ingredients: (formValue.ingredients ?? []).filter(i => i !== null) as string[],
        instructions: (formValue.instructions ?? []).filter(i => i !== null) as string[],
      };

      this.recipeService.createRecipe(recipeData).subscribe({
        next: (res: any) => {
          // alert('Recipe posted successfully!');
          this.snackBar.open('Recipe posted successfully!', 'Close', {
            duration: 3000, // duration in ms
            verticalPosition: 'top', // or 'bottom'
            horizontalPosition: 'right' // or 'left', 'center'
          });
          
          this.router.navigate(['/home']);

          this.recipeForm.reset();
          this.ingredients.clear();
          this.instructions.clear();
          this.addIngredient();
          this.addInstruction();
        },
        error: (err: any) => {
          // alert('Failed to post the recipe. Please try again.');
           this.snackBar.open('Failed  Post Recipe. Please try again', 'Close', {
            duration: 3000, // duration in ms
            verticalPosition: 'top', // or 'bottom'
            horizontalPosition: 'right' // or 'left', 'center'
          });
        }
      });

    } else {
      this.recipeForm.markAllAsTouched();
       this.snackBar.open('Please fill out all fields correctly.', 'Close', {
            duration: 3000, // duration in ms
            verticalPosition: 'top', // or 'bottom'
            horizontalPosition: 'right' // or 'left', 'center'
          });
      // alert('Please fill out all fields correctly.');
    }
  }


  selectedFile: File | null = null;

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.recipeForm.patchValue({ imageUrl: this.selectedFile.name });
    }
  }
}
