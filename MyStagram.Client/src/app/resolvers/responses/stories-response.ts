import { Story } from 'src/app/models/domain/story/story';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';
import { BaseResponse } from './base-response';

export class StoriesResponse extends BaseResponse {
    storyWrappers: StoryWrapper[];
}
