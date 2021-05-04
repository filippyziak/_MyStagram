import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Notifier } from '../services/notifier.service';
import { ProfileService } from '../services/profile.service';
import { ProfileResponse } from './responses/profile-response';

@Injectable()
export class ProfileResolver implements Resolve<ProfileResponse> {

    constructor(private router: Router, private notifier: Notifier, private profileService: ProfileService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<ProfileResponse> {
        return this.profileService.getProfile(route.params.userId).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            })
        );
    }

}