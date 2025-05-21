import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment } from '../Models/Intefaces/Comment';
import { CommentForm } from '../Models/UiModels/CommentForm.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

   url: string = "https://localhost:7091/api/Comments";
  http = inject(HttpClient);

 createComment(comment: CommentForm): Observable<Comment> {
    return this.http.post<Comment>(this.url, comment);
  }


}
