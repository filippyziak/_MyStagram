import { PaginationRequest } from './pagination-request';

export class FollowersRequest extends PaginationRequest {
    userId: string;
    userName: string;
    areAccepted: boolean = true;
    pageSize = 15;
}