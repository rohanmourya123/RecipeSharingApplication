import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApplicationUser } from '../Models/Intefaces/ApplicationUser.model';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  http = inject(HttpClient);

  getUserById(id:string):Observable<ApplicationUser>{
       return this.http.get<ApplicationUser>('https://localhost:7091/api/User/'+id);
  }

}
