import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { ProfilesResponse } from './responses/profiles-response';
import { ProfileService } from '../services/profile.service';
import { ProfilesRequest } from './requests/profiles-request';

@Injectable()

export class SearchResolver implements Resolve<PaginatedResult<ProfilesResponse>> {
    constructor(private router: Router, private profileService: ProfileService,
        private notifier: Notifier) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<ProfilesResponse>> {
        const profilesRequest = new ProfilesRequest();
        return this.profileService.getProfiles(profilesRequest).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            }),
        );
    }
}
