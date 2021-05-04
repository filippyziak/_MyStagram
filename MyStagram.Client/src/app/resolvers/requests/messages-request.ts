import { environment } from 'src/environments/environment';
import { PaginationRequest } from './pagination-request';

export class MessagesRequest extends PaginationRequest {
    recipientId: string;
    pageSize = environment.messagesThreadPageSize;
}