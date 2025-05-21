import { Routes } from '@angular/router';
import { LoginComponent } from './Componets/Auth/login/login.component';
import { RegisterComponent } from './Componets/Auth/register/register.component';
import { PublicHomePageComponent } from './public-home-page/public-home-page.component';
import { AppComponent } from './app.component';
import path from 'path';
import { DetailComponent } from './recipes/detail/detail.component';
import { PostRecipeComponent } from './recipes/post-recipe/post-recipe.component';
import { PageNotFoundComponent } from './Componets/page-not-found/page-not-found.component';
import { authGuard } from './guards/auth.guard';
import { DefaultHomePageComponent } from './default-home-page/default-home-page.component';



export const routes: Routes = [

   {path:'',component:DefaultHomePageComponent},
   {path:'home',
    component:PublicHomePageComponent,
    canActivate:[authGuard]
   },
   {path:'login',component:LoginComponent},
   {path:'register',component:RegisterComponent},
   {
      path:'detail/:id',
      component:DetailComponent,
       canActivate:[authGuard]
   },
   {
      path:'postRecipe',
      component:PostRecipeComponent,
      canActivate:[authGuard]
   },
   {path:'**',component:PageNotFoundComponent}
   
      
];
