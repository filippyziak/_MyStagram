import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Post } from '../models/domain/main/post';
import { PaginatedResult } from '../models/helpers/pagination';
import { FetchPostsRequest } from '../resolvers/requests/fetch-posts-request';
import { PostsRequest } from '../resolvers/requests/posts-request';
import { PostsResponse } from '../resolvers/responses/posts-response';

@Injectable({
  providedIn: 'root'
})
export class MainService {
  private readonly url = environment.apiUrl + 'main/';

  constructor(private httpClient: HttpClient) { }

  public createPost(description: string, photo: File) {
    const formData = new FormData();
    formData.append('description', description);
    formData.append('photo', photo, photo?.name);

    return this.httpClient.post(this.url + 'post/create', formData);
  }

  public deletePost(postId: string) {
    return this.httpClient.delete(this.url + 'post/delete', { params: { postId } });
  }

  public updatePost(postId: string, description: string) {
    const formData = new FormData();
    formData.append('description', description);
    formData.append('postId', postId);

    return this.httpClient.put(this.url + 'post/update', formData);
  }

  public getPost(postId: string) {
    return this.httpClient.get<Post>(this.url + 'post', { params: { postId } });
  }

  public getPosts(postsRequest: PostsRequest) {
    const paginatedResult: PaginatedResult<PostsResponse> = new PaginatedResult<PostsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', postsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', postsRequest.pageSize.toString());

    if (postsRequest.userId) {
      httpParams = httpParams.append('userId', postsRequest.userId);
    }

    return this.httpClient.get<PostsResponse>(this.url + 'posts', { observe: 'response', params: httpParams })
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

  public fetchPosts(request: FetchPostsRequest) {
    const paginatedResult: PaginatedResult<PostsResponse> = new PaginatedResult<PostsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', request.pageNumber.toString());
    httpParams = httpParams.append('pageSize', request.pageSize.toString());

    return this.httpClient.get<PostsResponse>(this.url + 'fetch/posts', { observe: 'response', params: httpParams })
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

  public createComment(content: string, postId: string) {
    return this.httpClient.post(this.url + 'comment/create', { content, postId }, { observe: 'response' });
  }

  public deleteComment(commentId: string) {
    return this.httpClient.delete(this.url + 'comment/delete', { params: { commentId } });
  }

  public likePost(postId: string) {
    return this.httpClient.put(this.url + 'like', { postId }, { observe: 'response' });
  }
}
