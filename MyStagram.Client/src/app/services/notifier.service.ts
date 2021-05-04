import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';

@Injectable({
  providedIn: 'root'
})
export class Notifier {

  constructor(private notifierService: NotifierService) { }

  public push(message: string, type = 'info') {
    this.notifierService.notify(type, message);
  }
}
