import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Conversation } from 'src/app/models/helpers/messenger/conversation';
import { Pagination } from 'src/app/models/helpers/pagination';
import { ConversationsRequest } from 'src/app/resolvers/requests/conversations-request';
import { AuthService } from 'src/app/services/auth.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { SignalRService } from 'src/app/services/signalR.service';
import { constants, SIGNALR_ACTIONS } from 'src/environments/environment';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {

  conversations: Conversation[];
  pagination: Pagination;
  currentUserId: string;
  userName: string;

  constants = constants;

  constructor(private messenger: Messenger, public router: Router, private route: ActivatedRoute,
    private signalR: SignalRService, private authService: AuthService, private listener: ListenerService) { }

  ngOnInit() {
    this.subscribeData();
    this.subscribeSignalR();
  }

  public onScroll() {
    if (this.conversations.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.loadConversations(true);
    }
  }

  private loadConversations(onScroll = false) {
    if (this.authService.isLoggedIn()) {
      const conversationsRequest = new ConversationsRequest();
      conversationsRequest.pageNumber = onScroll ? this.pagination.currentPage : 1;
      this.messenger.getConversations(conversationsRequest).subscribe(res => {
        const conversations = res.result?.conversations;
        this.pagination = res.pagination;
        this.conversations = onScroll ? this.conversations.concat(conversations) : conversations;
      });
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.conversations = data.conversationsResponse.result.conversations;
      this.pagination = data.conversationsResponse.pagination;
    });
    this.currentUserId = this.authService.currentUser.id;
  }

  private subscribeSignalR() {
    this.signalR.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_SEND, () => {
      if (this.authService.isLoggedIn()) {
        this.loadConversations();
      }
    });
  }
}
