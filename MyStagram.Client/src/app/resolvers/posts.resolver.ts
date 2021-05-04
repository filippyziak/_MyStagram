import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { MainService } from '../services/main.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { PostsResponse } from './responses/posts-response';
import { PostsRequest } from './requests/posts-request';

@Injectable()

export class PostsResolver implements Resolve<PaginatedResult<PostsResponse>> {
    constructor(private router: Router, private mainService: MainService,
        private notifier: Notifier) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<PostsResponse>> {
        const postRequest = new PostsRequest();
        postRequest.userId = route.params.userId;

        return this.mainService.getPosts(postRequest).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            }),
        );
    }
}
