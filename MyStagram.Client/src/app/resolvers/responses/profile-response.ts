import { UserProfile } from 'src/app/models/domain/profile/user-profile';
import { BaseResponse } from './base-response';

export class ProfileResponse extends BaseResponse {
    userProfile: UserProfile;
}