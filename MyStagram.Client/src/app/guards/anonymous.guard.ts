import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Notifier } from '../services/notifier.service';

@Injectable({
    providedIn: 'root'
})
export class AnonymousGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router, private notifier: Notifier) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if (!this.authService.isLoggedIn()) {
            return true;
        }

        this.router.navigate([' ']);
        this.notifier.push('You are already logged in', 'info');

        return false;
    }
}
