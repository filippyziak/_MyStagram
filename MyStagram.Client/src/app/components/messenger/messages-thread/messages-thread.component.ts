import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Message } from 'src/app/models/domain/messenger/message';
import { Recipient } from 'src/app/models/domain/messenger/recipient';
import { Pagination } from 'src/app/models/helpers/pagination';
import { MessagesRequest } from 'src/app/resolvers/requests/messages-request';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';
import { SignalRService } from 'src/app/services/signalR.service';
import { constants, SIGNALR_ACTIONS } from 'src/environments/environment';

@Component({
  selector: 'app-messages-thread',
  templateUrl: './messages-thread.component.html',
  styleUrls: ['./messages-thread.component.scss']
})
export class MessagesThreadComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatSection') chatSection: ElementRef;

  messages: Message[];
  pagination: Pagination;
  recipient: Recipient;

  messageForm: FormGroup;
  messagesRequest = new MessagesRequest();

  constants = constants;

  isScrolled = false;

  constructor(private messenger: Messenger, private formBuilder: FormBuilder, private formHelper: FormHelper,
    private route: ActivatedRoute, private signalR: SignalRService, private authService: AuthService,
    private notifier: Notifier, private notificationService: NotificationService, private router: Router,
    private listener: ListenerService) { }

  ngOnInit() {
    this.subscribeData();
    this.createMessageForm();
    this.subscribeSignalR();
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  public sendMessage() {
    if (this.messageForm.valid) {
      this.isScrolled = false;
      this.messenger.sendMessage(this.recipient.id, this.messageForm.value.content).subscribe(res => {
        const response: any = res?.body;
        this.messages.push(response?.message);
        this.formHelper.resetForm(this.messageForm);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public onScroll() {
    if (this.messages.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.getMessagesThread();
      this.isScrolled = true;
    }
  }

  public goToUserProfile() {
    this.router.navigate(['profile/', this.recipient.id]);
  }

  private getMessagesThread() {
    this.messagesRequest.pageNumber = this.pagination.currentPage;
    this.messenger.getMessagesThread(this.messagesRequest).subscribe(res => {
      const messages = res.result?.messages;
      this.messages.reverse();
      this.messages = this.messages.concat(messages);
      this.messages.reverse();
      this.pagination = res?.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    })
  }


  private subscribeData() {
    this.route.data.subscribe(data => {
      this.pagination = data.messagesResponse.pagination;
      this.messages = data.messagesResponse.result?.messages;
      this.recipient = data.messagesResponse.result?.recipient;
    });
    this.messagesRequest.recipientId = this.recipient.id;
    this.messages.reverse();
    this.notificationService.countUnreadConversations();
  }

  private createMessageForm() {
    this.messageForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(constants.maximumCommentLength)]]
    });
  }

  private scrollToBottom() {
    if (this.chatSection && !this.isScrolled) {
      this.chatSection.nativeElement.scrollTop = this.chatSection.nativeElement.scrollHeight;
    }
  }

  private subscribeSignalR() {
    this.signalR.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_SEND, (value) => {
      if (this.authService.isLoggedIn() && value[0].senderId === this.recipient.id) {
        this.messages.push(value[0]);
        this.notificationService.countUnreadConversations();
      }
    });
  }
}
