import { Injectable } from '@angular/core';
import { environment, SIGNALR_ACTIONS } from 'src/environments/environment';
import { ConnectionManagerService } from './connection-manager.service';

import * as signalr from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public static connectionId: string;

  private readonly hubApiUrl = environment.signalRUrl;
  private hubConnection: signalr.HubConnection;

  constructor(private connectionManager: ConnectionManagerService) { }

  public startConnection = () => {
    this.hubConnection = new signalr.HubConnectionBuilder()
      .withUrl(this.hubApiUrl)
      .build();

    this.hubConnection.start()
      .then(() => {
        this.initConnection();
        if (!environment.production) {
          console.log('SignalR: Connection started...');
        }
      })
      .catch(error => console.error('SignalR: ', error));
  }

  public subscribeAction = (actionName: string, action: (value?) => void) => {
    if (this.hubConnection) {
      this.hubConnection.on(actionName, action);
    }
  }

  public closeConnection = () => {
    this.hubConnection.stop();
    if (!environment.production) {
      console.log('SignalR: Connection closed...');
    }
    this.removeConnection();
  }

  private initConnection = () => {
    this.hubConnection.invoke(SIGNALR_ACTIONS.GET_CONNECTION_ID)
      .then((connectionId) => {
        SignalRService.connectionId = connectionId;
        this.createConnection();
      });
  }

  private createConnection = () => {
    this.connectionManager.startConnection(SignalRService.connectionId).subscribe(() => {
      if (!environment.production) {
        console.log('SignalR: Connection established and persisted in database...');
      }
    });
  }

  private removeConnection = () => {
    this.connectionManager.closeConnection(SignalRService.connectionId).subscribe(() => {
      if (!environment.production) {
        console.log('SignalR: Connection closed and removed from database...');
      }
    });
  }
}
