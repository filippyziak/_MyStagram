import { Follower } from 'src/app/models/domain/social/follower';
import { BaseResponse } from './base-response';

export class FollowersResponse extends BaseResponse {
  followers: Follower[];
  following: Follower[];
}
