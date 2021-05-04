import { Post } from '../../models/domain/main/post';
import { BaseResponse } from './base-response';

export class PostsResponse extends BaseResponse {
  posts: Post[];
}
