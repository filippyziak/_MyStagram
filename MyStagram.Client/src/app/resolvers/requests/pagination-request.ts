import { environment } from 'src/environments/environment';

export abstract class PaginationRequest {
  pageNumber = 1;
  pageSize: number = environment.pageSize;
}
