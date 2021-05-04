import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { FollowersRequest } from '../resolvers/requests/followers-request';
import { FollowersResponse } from '../resolvers/responses/followers-response';

@Injectable({
  providedIn: 'root'
})
export class FollowersService {

  private readonly url = environment.apiUrl + 'social/';

  constructor(private httpClient: HttpClient) { }

  public followUser(recipientId: string) {
    return this.httpClient.post(this.url + 'follow', { recipientId }, { observe: 'response' });
  }

  public unFollowUser(recipientId: string) {
    return this.httpClient.delete(this.url + 'unfollow', { params: { recipientId } })
  }

  public acceptFollower(senderId: string, recipientId: string, accepted: boolean) {
    return this.httpClient.put(this.url + 'follow/accept', { senderId, recipientId, accepted })
  }

  public getFollowers(followersRequest: FollowersRequest) {
    const paginatedResult: PaginatedResult<FollowersResponse> = new PaginatedResult<FollowersResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', followersRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', followersRequest.pageSize.toString());
    httpParams = httpParams.append('userId', followersRequest.userId);

    if (followersRequest.userName) {
      httpParams = httpParams.append('userName', followersRequest.userName);
    }

    return this.httpClient.get<FollowersResponse>(this.url + 'followers', { observe: 'response', params: httpParams })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public countUnwatchedFollowers() {
    return this.httpClient.get<any>(this.url + 'followers/unwatched', { observe: 'response' });
  }

  public WatchFollower(senderId: string) {
    return this.httpClient.patch(this.url + 'follower/watch', { senderId });
  }
}
