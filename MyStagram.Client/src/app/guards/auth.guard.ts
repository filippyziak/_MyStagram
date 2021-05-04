import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Notifier } from '../services/notifier.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private notifier: Notifier) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.authService.isLoggedIn();
    const routeRoles = next.firstChild.data.roles as string[];

    if (routeRoles && isLoggedIn) {
      const isPermitted = this.authService.permissionsCheck(routeRoles);

      if (isPermitted) {
        return true;
      }

      this.router.navigate([' ']);
      this.notifier.push('You are not authorized to access this page', 'warning');
    }

    if (isLoggedIn) {
      return true;
    }

    this.router.navigate(['login'], { queryParams: { returnTo: state.url } });

    return false;
  }
}
