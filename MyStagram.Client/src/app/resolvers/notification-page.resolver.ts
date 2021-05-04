import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { FollowersResponse } from './responses/followers-response';
import { FollowersRequest } from './requests/followers-request';
import { FollowersService } from '../services/followers.service';
import { AuthService } from '../services/auth.service';

@Injectable()

export class NotificationPageResolver implements Resolve<PaginatedResult<FollowersResponse>> {
    constructor(private router: Router, private followersService: FollowersService, private notifier: Notifier,
        private authService: AuthService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<FollowersResponse>> {
        const followersRequest = new FollowersRequest();

        followersRequest.userId = this.authService.currentUser.id;
        followersRequest.areAccepted = !this.authService.currentUser.isPrivate
        
        return this.followersService.getFollowers(followersRequest).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            }),
        );
    }
}
