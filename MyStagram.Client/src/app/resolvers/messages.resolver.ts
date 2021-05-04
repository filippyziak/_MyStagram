import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { Messenger } from '../services/messenger.service';
import { MessagesResponse } from './responses/messages-response';
import { MessagesRequest } from './requests/messages-request';
import { environment } from 'src/environments/environment';

@Injectable()

export class MessagesResolver implements Resolve<PaginatedResult<MessagesResponse>> {
    constructor(private router: Router, private messenger: Messenger, private notifier: Notifier) { }

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<MessagesResponse>> {
        const messagesRequest = new MessagesRequest();

        messagesRequest.recipientId = route.params.recipientId;
        return this.messenger.getMessagesThread(messagesRequest).pipe(
            catchError(() => {
                this.notifier.push('Error occurred during loading data', 'error');
                this.router.navigate(['']);

                return of(null);
            }),
        );
    }
}
