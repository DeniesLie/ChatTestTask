import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../features/auth/services/auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService) 
    { }

    canActivate(
      route: ActivatedRouteSnapshot, 
      state: RouterStateSnapshot) 
    {
      var isAuthorized = this.authService.isAuthorized();
      
      if (!isAuthorized) {
        this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
      }
  
      return isAuthorized;
    }
    
  
}
