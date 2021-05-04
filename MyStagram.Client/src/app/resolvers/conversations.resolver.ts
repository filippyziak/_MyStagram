import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { Messenger } from '../services/messenger.service';
import { ConversationsResponse } from './responses/conversations-response';
import { ConversationsRequest } from './requests/conversations-request';
import { environment } from 'src/environments/environment';


@Injectable()

export class ConversationsResolver implements Resolve<PaginatedResult<ConversationsResponse>> {
    constructor(private router: Router, private messenger: Messenger, private notifier: Notifier) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<ConversationsResponse>> {
        const conversationsRequest = new ConversationsRequest();
        return this.messenger.getConversations(conversationsRequest).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            }),
        );
    }
}
