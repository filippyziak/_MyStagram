import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StoriesResponse } from '../resolvers/responses/stories-response';
import { UserStoriesResponse } from '../resolvers/responses/user-stories-response';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private readonly url = environment.apiUrl + 'story/';

  constructor(private httpClient: HttpClient) { }

  public createStory(photo: File) {
    const formData = new FormData();

    formData.append('photo', photo);

    return this.httpClient.post(this.url + 'create', formData, { observe: 'response' });
  }

  public watchStory(storyId: string) {

    return this.httpClient.post(this.url + 'watch', { storyId });
  }

  public deleteStory(storyId: string) {
    return this.httpClient.delete(this.url + 'delete', { params: { storyId } });
  }

  public getThisUserStories(userId: string) {
    return this.httpClient.get<UserStoriesResponse>(this.url + 'user/stories', { params: { userId } })
  }

  public getStories() {
    return this.httpClient.get<StoriesResponse>(this.url + 'stories')
  }
}
