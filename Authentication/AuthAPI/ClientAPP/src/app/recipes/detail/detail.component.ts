import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Recipe } from '../../Models/Intefaces/Recipe.model';
import { RecipeService } from '../../Services/recipe.service';
import { NgFor, NgIf } from '@angular/common';
import { CardComponent } from "../../Shared/card/card.component";
import { FormFieldComponent } from "../../Shared/form-field/form-field.component";
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommentService } from '../../Services/comment.service';
import { CommentForm } from '../../Models/UiModels/CommentForm.model';
import { MatButtonModule } from '@angular/material/button';
import { RatingService } from '../../Services/rating.service';
import { TableComponent } from '../../Shared/table/table.component';
import { UserServiceService } from '../../Services/user-service.service';
import { RatingForm } from '../../Models/UiModels/RaitingForm.model';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-detail',
  imports: [
    CardComponent,
    NgFor,
    NgIf,
    TableComponent,
    FormFieldComponent,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatSnackBarModule
  ],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.css'
})
export class DetailComponent implements OnInit {

  Title = signal("Recipe Details");
  recipeId: number | undefined;
  recipe: Recipe | undefined;
  // showCommentForm = false;
  showForm = false;
  showRatingForm = false;
  getFormField = false;
  BtnTitle = signal("Add Comment/Rating");
  // BtnTitleForComment = signal("Make Comment")


  route = inject(ActivatedRoute);
  router = inject(Router);
  recipeService = inject(RecipeService);
  commentService = inject(CommentService);
  ratingService = inject(RatingService);
  snackBar = inject(MatSnackBar);
  // userService = inject(UserServiceService);

  Fields = [
    { name: 'content', placeholder: 'leave your comment', type: 'text' },
    { name: 'value', placeholder: 'Rate us...', type: 'number' }
  ]

  getRatingAndComments = new FormGroup({
    content: new FormControl('', [Validators.required]),
    value: new FormControl('', [Validators.required, Validators.min(1), Validators.max(5)])
  })

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.recipeId = +idParam;
        this.LoadDetails();
      }
    });
  }

  LoadDetails() {
    this.recipeService.getRecipeById(this.recipeId!).subscribe(res => {
      if (res != null) {
        this.recipe = res;
        console.log(res);
      }
    });
  }

  getControl(name: string): FormControl {
    return this.getRatingAndComments.get(name) as FormControl;
  }


  toggleForm() {
    this.getFormField = !this.getFormField;
    this.BtnTitle.set(this.getRatingAndComments ? 'Close Comment' : 'Add Comment');
  }

  submitBtn(recipeId: number) {
    if (this.getRatingAndComments.valid) {
      const formValue = this.getRatingAndComments.value;

      const commentData: CommentForm = {
        RecipeId: recipeId,
        Content: formValue.content ?? ''
      };

      const ratingData: RatingForm = {
        recipeId: recipeId,
        value: Number(formValue.value) || 0
      }

      this.commentService.createComment(commentData).subscribe({
        next: (res) => {
          console.log(res);
            this.snackBar.open('Comment Submitted!', 'Close', {
            duration: 3000, // duration in ms
            verticalPosition: 'top', // or 'bottom'
            horizontalPosition: 'right' // or 'left', 'center'
          });
          // alert("Comment Posted!");
        },
        error: (err) => {
          console.error("Error posting comment:", err);
        }
      });
      this.ratingService.createRating(ratingData).subscribe(res => {
        console.log(res);
          this.snackBar.open('Rating Submitted!', 'Close', {
            duration: 3000, // duration in ms
            verticalPosition: 'top', // or 'bottom'
            horizontalPosition: 'right' // or 'left', 'center'
          });
        // alert("Rating Submitted!");
        this.getRatingAndComments.reset();
        this.LoadDetails();
      })
    }
  }

  previousPage(){
    this.router.navigate(['/home']);
  }


}





// Fields = [
//   { name: 'content', placeholder: 'leave your comment', type: 'text' }
// ]

// RatingFields = [
//   { name: 'value', placeholder: 'Rate us...', type: 'number' }
// ]

// CommentForms = new FormGroup({
//   content: new FormControl('', [Validators.required])
// });

// RatingForms = new FormGroup({
//   value: new FormControl('', [Validators.required, Validators.min(1), Validators.max(5)])
// });

// getControl(name: string): FormControl {
//   return this.CommentForms.get(name) as FormControl;
// }

// getControlForRating(name: string): FormControl {
//   return this.RatingForms.get(name) as FormControl;
// }

// toggleCommentForm() {
//   this.showCommentForm = !this.showCommentForm;
//   this.BtnTitleForComment.set("Close Comment");
// }


// submitComment(recipeId: number) {
//   if (this.CommentForms.valid) {
//     const formValue = this.CommentForms.value;

//     const commentData: CommentForm = {
//       RecipeId: recipeId,
//       Content: formValue.content ?? ''
//     };

//     this.commentService.createComment(commentData).subscribe({
//       next: (res) => {
//         console.log(res);
//         alert("Comment Posted!");
//         this.CommentForms.reset();
//         this.showCommentForm = false;
//         this.LoadDetails();
//       },
//       error: (err) => {
//         console.error("Error posting comment:", err);
//       }
//     });
//   }
// }


// toggleRatingForm() {
//   this.showRatingForm = !this.showRatingForm;
//   this.BtnTitle.set("Close Rating");
// }

// submitRating(recipeId: number) {
//   if (this.RatingForms.valid) {
//     const formValue = this.RatingForms.value;

//     const ratingData = {
//       recipeId: recipeId,
//       value: Number(formValue.value) || 0
//     };

//     this.ratingService.createRating(ratingData).subscribe({
//       next: (res) => {
//         console.log(res);
//         alert("Rating Submitted!");
//         this.RatingForms.reset();
//         this.showRatingForm = false;
//         this.LoadDetails();
//       },
//       error: (err) => {
//         console.error("Error posting rating:", err);
//       }
//     });
//   }
// }
