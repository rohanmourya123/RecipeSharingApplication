import { Component, inject } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthServiceService } from './Services/auth-service.service';
import { NgIf } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatSelectChange } from '@angular/material/select';

// import { DefaultHomePageComponent } from './default-home-page/default-home-page.component';

@Component({
  selector: 'app-root',
  // standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    RouterOutlet,
    RouterLink,
    MatSelectModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgIf
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RecipeApp';
  disableSelect = new FormControl(false);
  userName: string = '';
  email: string = '';

  router = inject(Router);
  authService = inject(AuthServiceService);


  onSelectionChange(event: MatSelectChange): void {
    // const value = (event.target as HTMLSelectElement).value;
    const value = event.value;
    if (value === 'logout') {
      this.logout();
    }
    if(value === 'profile'){
      this.router.navigate(['/profile']);
    }
  }

  isLoggedIn(): boolean {
    const res = this.authService.isLoggedIn();

    if (res) {
      const userJson = localStorage.getItem('user');
      if (userJson) {
        const user = JSON.parse(userJson);
        this.userName = user.name;
        this.email = user.email;
      }
    }

    return res;
  }


  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
