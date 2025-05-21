import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rating } from '../Models/Intefaces/Rating.model';
import { RatingForm } from '../Models/UiModels/RaitingForm.model';


@Injectable({
  providedIn: 'root'
})
export class RatingService {

  url: string = "https://localhost:7091/api/Rating";
  http = inject(HttpClient);

  getRatingsByRecipeId(recipeId: number): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.url}/Recipe/${recipeId}`);
  }


  createRating(rating: RatingForm): Observable<Rating> {
    return this.http.post<Rating>(this.url, rating);
  }

 
  updateRating(id: number, rating: RatingForm): Observable<Rating> {
    return this.http.put<Rating>(`${this.url}/${id}`, rating);
  }

  
  deleteRating(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
