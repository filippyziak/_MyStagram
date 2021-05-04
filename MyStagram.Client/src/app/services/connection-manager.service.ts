import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConnectionManagerService {

  private readonly url = environment.apiUrl + 'connection/';

  constructor(private httpClient: HttpClient) { }

  public startConnection(connectionId: string) {
    return this.httpClient.post(this.url + 'start', { connectionId })
  }

  public closeConnection(connectionId: string) {
    return this.httpClient.delete(this.url + 'close', { params: { connectionId } })
  }

}
