import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { ConversationsRequest } from '../resolvers/requests/conversations-request';
import { MessagesRequest } from '../resolvers/requests/messages-request';
import { ConversationsResponse } from '../resolvers/responses/conversations-response';
import { MessagesResponse } from '../resolvers/responses/messages-response';

@Injectable({
  providedIn: 'root'
})
export class Messenger {
  private readonly url = environment.apiUrl + 'messenger/';

  constructor(private httpClient: HttpClient) { }

  public sendMessage(recipientId: string, content: string) {
    return this.httpClient.post(this.url + 'message/send', { recipientId, content }, { observe: 'response' });
  }

  public getMessagesThread(messagesRequest: MessagesRequest) {
    const paginatedResult: PaginatedResult<MessagesResponse> = new PaginatedResult<MessagesResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', messagesRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', (messagesRequest.pageSize).toString());
    httpParams = httpParams.append('recipientId', messagesRequest.recipientId);

    return this.httpClient.get<MessagesResponse>(this.url + 'messages', { observe: 'response', params: httpParams })
      .pipe(
        map(res => {
          paginatedResult.result = res.body;
          if (res.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(res.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public getConversations(conversationsRequest: ConversationsRequest) {
    const paginatedResult: PaginatedResult<ConversationsResponse> = new PaginatedResult<ConversationsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', conversationsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', conversationsRequest.pageSize.toString());

    if (conversationsRequest.userName) {
      httpParams = httpParams.append('userName', conversationsRequest.userName);
    }

    return this.httpClient.get<ConversationsResponse>(this.url + 'conversations', { observe: 'response', params: httpParams })
      .pipe(
        map(res => {
          paginatedResult.result = res.body;
          if (res.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(res.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public countUnreadConversations() {
    return this.httpClient.get<any>(this.url + 'conversations/count', { observe: 'response' });
  }

  public countUnreadMessages(userId: string) {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('userId', userId);
    return this.httpClient.get<any>(this.url + 'messages/count', { observe: 'response', params: httpParams });
  }

  public ReadMessage(messageId: string) {
    return this.httpClient.patch(this.url + 'message/read', { messageId });
  }
}
