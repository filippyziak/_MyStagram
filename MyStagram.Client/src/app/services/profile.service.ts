import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { ProfilesRequest } from '../resolvers/requests/profiles-request';
import { ProfileResponse } from '../resolvers/responses/profile-response';
import { ProfilesResponse } from '../resolvers/responses/profiles-response';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private readonly url = environment.apiUrl + 'profile/';

  constructor(private httpClient: HttpClient) { }

  public getProfile(userId: string): Observable<ProfileResponse> {
    return this.httpClient.get<ProfileResponse>(this.url, { params: { userId } });
  }

  public getProfiles(request: ProfilesRequest) {
    const paginatedResult: PaginatedResult<ProfilesResponse> = new PaginatedResult<ProfilesResponse>();
    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', request.pageNumber.toString());
    httpParams = httpParams.append('pageSize', request.pageSize.toString());

    if (request.userName) {
      httpParams = httpParams.append('userName', request.userName);
    }

    return this.httpClient.get<ProfilesResponse>(this.url + 'profiles', { observe: 'response', params: httpParams })
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

  public updateProfile(request: any) {
    return this.httpClient.put(this.url + 'update', request);
  }

  public changePassword(request: any) {
    return this.httpClient.patch(this.url + 'changePassword', request);
  }

  public changeAvatar(avatar: File) {
    const formData = new FormData();

    formData.append('avatar', avatar);

    return this.httpClient.post(this.url + 'avatar/change', formData);
  }

  public deleteAvatar() {
    return this.httpClient.delete(this.url + 'avatar/delete');
  }
}
