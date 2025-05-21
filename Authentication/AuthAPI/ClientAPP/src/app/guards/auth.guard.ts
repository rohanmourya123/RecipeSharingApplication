import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthServiceService } from '../Services/auth-service.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthServiceService);
  const router = inject(Router);

  if(authService.isLoggedIn()){
    return true;
  }
  else{
    router.navigate(['/login']);
    return false;
  }
};
