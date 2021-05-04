import { Post } from '../../models/domain/main/post';
import { BaseResponse } from './base-response';

export class PostResponse extends BaseResponse {
  post: Post;
}
