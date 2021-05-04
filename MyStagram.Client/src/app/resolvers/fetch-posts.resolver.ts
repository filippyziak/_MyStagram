import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { MainService } from '../services/main.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { PostsResponse } from './responses/posts-response';
import { FetchPostsRequest } from './requests/fetch-posts-request';
import { AuthService } from '../services/auth.service';
import { PostsRequest } from './requests/posts-request';
import { constants } from 'src/environments/environment';

@Injectable()

export class FetchPostsResolver implements Resolve<PaginatedResult<PostsResponse>> {
    constructor(private router: Router, private mainService: MainService, private notifier: Notifier,
        private authService: AuthService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<PostsResponse>> {
        if (this.authService.isLoggedIn()) {
            const fetchPostsRequest = new FetchPostsRequest();
            return this.mainService.fetchPosts(fetchPostsRequest).pipe(
                catchError(() => {
                    this.notifier.push('Error occurred during loading data', 'error');
                    this.router.navigate(['']);

                    return of(null);
                }),
            );
        }
        else {
            return of(null);
        }
    }
}
