import { PaginationRequest } from './pagination-request';

export class ProfilesRequest extends PaginationRequest {
    userName: string;
    pageSize = 15;
}