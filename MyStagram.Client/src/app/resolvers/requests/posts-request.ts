import { environment } from 'src/environments/environment';
import { PaginationRequest } from './pagination-request';

export class PostsRequest extends PaginationRequest {
    userId: string;
    pageSize = environment.postsPageSize;
}