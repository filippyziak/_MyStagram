import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { UserStoriesResponse } from './responses/user-stories-response';
import { StoryService } from '../services/story.service';
import { StoriesResponse } from './responses/stories-response';
import { AuthService } from '../services/auth.service';

@Injectable()

export class StoriesResolver implements Resolve<StoriesResponse> {
    constructor(private router: Router, private storyService: StoryService, private authService: AuthService,
        private notifier: Notifier) { }

    resolve(route: ActivatedRouteSnapshot): Observable<StoriesResponse> {
        if (this.authService.isLoggedIn()) {

            return this.storyService.getStories().pipe(
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
