import { HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
// import {jwtDecode} from 'jwt-decode';



export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const platformId = inject(PLATFORM_ID);

  if (isPlatformBrowser(platformId)) {
    const token = localStorage.getItem('token');

    if (token) {
      try {
        // const decodedToken: any = jwtDecode(token);


        // console.log("details of token ",decodedToken);

        const clonedReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
            // 'X-Email': decodedToken.email || '',
            // 'X-Name': decodedToken.name || ''
          }
        });

        return next(clonedReq);
      } catch (error) {
        console.error('Invalid token', error);
      }
    }
  }

  return next(req);
};


