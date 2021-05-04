import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MainService } from '../services/main.service';
import { Notifier } from '../services/notifier.service';
import { PostResponse } from './responses/post-response';

@Injectable()
export class PostResolver implements Resolve<PostResponse> {

    constructor(private router: Router, private notifier: Notifier, private mainService: MainService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PostResponse> {
        if (!route.queryParams.createMode) {
            return this.mainService.getPost(route.params.postId).pipe(
                catchError(() => {
                    this.notifier.push('Error occreed during loading data', 'error');
                    this.router.navigate(['']);

                    return of(null);
                })
            );
        }
        return of(null);
    }

}