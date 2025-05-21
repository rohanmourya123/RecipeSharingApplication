import { Component, signal, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../Shared/form-field/form-field.component';
import { NgFor } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { CardComponent } from '../../../Shared/card/card.component';
import { AuthServiceService } from '../../../Services/auth-service.service';
import { RegisterUser } from '../../../Models/UiModels/RegisterUser';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  imports: [CardComponent, ReactiveFormsModule, FormFieldComponent, NgFor, MatButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  // Injecting Service
  authService = inject(AuthServiceService);
  router = inject(Router)

  titleName = signal<string>("Register To Get Favorite Meals Recipes ");

  Fields = [
    { name: 'name', placeholder: 'Enter uour name', type: 'text' },
    { name: 'username', placeholder: 'Enter your userName', type: 'Text' },
    { name: 'email', placeholder: 'Enter your email', type: 'email' },
    { name: 'password', placeholder: 'Enter your password', type: 'password' }
  ]

  RegisterForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    username: new FormControl('', [Validators.required, Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.minLength(3)]),
    password: new FormControl('', [Validators.required, Validators.minLength(3)])
  });

  getControl(name: string): FormControl {
    return this.RegisterForm.get(name) as FormControl;
  }

  onSubmit(): void {
  if (this.RegisterForm.valid) {
    const formValues = this.RegisterForm.value;

    const registerData: RegisterUser = {
      name: formValues.name ?? '',
      username: formValues.username ?? '',
      email: formValues.email ?? '',
      password: formValues.password ?? '',
    };

    this.authService.getUserRegistered(registerData).subscribe({
      next: (res) => {
        console.log(' Registration successful:', res);
        alert('User registered successfully!');
        this.router.navigate(['/login']);
        this.RegisterForm.reset();
      },
      error: (err) => {
        console.error(' Registration failed:', err);
        alert('User registration failed. Please try again.');
      }
    });
  } else {
    this.RegisterForm.markAllAsTouched();
    alert('Invalid Form. Please fill out all required fields.');
  }
}


}
