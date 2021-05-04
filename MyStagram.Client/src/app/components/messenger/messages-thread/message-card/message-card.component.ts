
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Message } from 'src/app/models/domain/messenger/message';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.scss']
})
export class MessageCardComponent implements OnInit {
  @Input() message: Message;
  currentUserId: string;
  @Output() messageDeleted = new EventEmitter<DeleteEmitter>();

  constructor(public authService: AuthService) { }

  ngOnInit() {
    this.currentUserId = this.authService.currentUser.id;
  }

}
