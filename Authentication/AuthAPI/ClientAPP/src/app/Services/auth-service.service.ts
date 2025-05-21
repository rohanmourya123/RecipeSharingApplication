import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, computed } from '@angular/core';
import { RegisterUser } from '../Models/UiModels/RegisterUser';
import { Observable } from 'rxjs';
import { LoginUser } from '../Models/UiModels/LoginUser.model';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private http = inject(HttpClient);
  private token = signal<string | null>(this.getInitialToken());

  url: string = 'https://localhost:7091/api/Auth';

  //  Safe browser-only token access
  private getInitialToken(): string | null {
    return typeof window !== 'undefined' ? localStorage.getItem('token') : null;
  }

  // Computed signal to check login status
  isLoggedIn = computed(() => !!this.token());


  getUserRegistered(data: RegisterUser): Observable<RegisterUser> {
    return this.http.post<RegisterUser>(`${this.url}/register`, data);
  }

  getUserLogedIn(data: LoginUser): Observable<any> {
    return this.http.post<any>(`${this.url}/login`, data);
  }

  // Save token safely
  saveToken(token: string) {
    if (typeof window !== 'undefined') {
      localStorage.setItem('token', token);
      this.token.set(token);
    }
  }

  
  logout() {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.token.set(null);
    }
  }

  
  getToken(): string | null {
    return this.token();
  }
}
