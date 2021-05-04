import { Story } from 'src/app/models/domain/story/story';
import { BaseResponse } from './base-response';

export class UserStoriesResponse extends BaseResponse {
    stories: Story[];
    storyToWatch: Story;
}
