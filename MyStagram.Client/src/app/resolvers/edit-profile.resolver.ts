import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { Notifier } from '../services/notifier.service';
import { ProfileService } from '../services/profile.service';
import { ProfileResponse } from './responses/profile-response';

@Injectable()
export class EditProfileResolver implements Resolve<ProfileResponse> {

    constructor(private router: Router, private notifier: Notifier, private profileService: ProfileService,
        private authService: AuthService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<ProfileResponse> {
        return this.profileService.getProfile(this.authService.currentUser.id).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            })
        );
    }

}