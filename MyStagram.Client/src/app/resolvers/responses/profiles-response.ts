import { SearchUser } from 'src/app/models/domain/profile/search-user';
import { BaseResponse } from './base-response';

export class ProfilesResponse extends BaseResponse {
    userProfiles: SearchUser[];
}
