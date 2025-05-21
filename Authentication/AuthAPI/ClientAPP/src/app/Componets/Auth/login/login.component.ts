import { Component, signal, inject } from '@angular/core';
import { CardComponent } from "../../../Shared/card/card.component";
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { LoginUser } from '../../../Models/UiModels/LoginUser.model';
import { FormFieldComponent } from "../../../Shared/form-field/form-field.component";
import { NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../../Services/auth-service.service';
import {jwtDecode} from 'jwt-decode'; 

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CardComponent, ReactiveFormsModule, FormFieldComponent, NgFor, MatButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  router = inject(Router);
  authService = inject(AuthServiceService);

  titleName = signal<string>("Login To Explore More");

  Fields = [
    { name: 'email', placeholder: 'Enter Email', type: 'text' },
    { name: 'password', placeholder: 'Enter Password', type: 'password' }
  ];

  LoginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(3)]),
  });

  getControl(name: string): FormControl {
    return this.LoginForm.get(name) as FormControl;
  }

  onSubmit(): void {
    if (this.LoginForm.valid) {
      const formValues = this.LoginForm.value;

      const loginData: LoginUser = {
        email: formValues.email ?? '',
        password: formValues.password ?? '',
      };

      this.authService.getUserLogedIn(loginData).subscribe({
        next: (res: any) => {
          console.log('Login successful:', res);

          const decodeToken: any = jwtDecode(res.token);
          // console.log("Decoded Token:", decodeToken);
          // console.log("Email from Token:", decodeToken?.email);
          const user = {
               email : decodeToken.email,
               name : decodeToken.name,
               uID:decodeToken.sub
          };
        

          // Save token and User
          localStorage.setItem('token', res.token);
          localStorage.setItem('user', JSON.stringify(user));
          this.authService.saveToken(res.token);

          alert('User logged in successfully!');
          this.router.navigate(['/home']);
          this.LoginForm.reset();
        },
        error: (err) => {
          console.error('Login failed:', err);
          alert('User login failed. Please try again.');
        }
      });
    } else {
      this.LoginForm.markAllAsTouched();
      alert('Invalid form. Please fill out all required fields.');
    }
  }
}
