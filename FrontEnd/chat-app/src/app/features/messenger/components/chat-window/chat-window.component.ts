import { ChangeDetectorRef, Component, ElementRef, Input, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatMenuTrigger } from '@angular/material/menu';
import { AuthService } from 'src/app/features/auth/services/auth-service/auth.service';
import { Chat } from '../../models/chat';
import { MessageEdit } from '../../models/messages/messageEdit';
import { MessageGet } from '../../models/messages/messageGet';
import { MessageSend } from '../../models/messages/messageSend';
import { MessagesService } from '../../services/messages-service/messages.service';
import { DeleteMessageDialogComponent } from '../delete-message-dialog/delete-message-dialog.component';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.css']
})
export class ChatWindowComponent implements OnInit {

  @Input() chat?: Chat;
  @ViewChild('messagesContainer') viewportRef!: ElementRef;
  @ViewChild(MatMenuTrigger, {static: true}) matMenuTrigger!: MatMenuTrigger; 

  private _pagesLoaded: number = 0
  private _messagesToTake = 20
  private _leftUntilLoad: number = 0
  private _hasReadToEnd: boolean = false
  private _scrollY: number = 0
  private _scrolledToBottom: boolean = false
  private _changeDetectionRef: ChangeDetectorRef;
   
  messageToEdit?: MessageGet; 
  messageToReply?: MessageGet;
  messageMenuPosition = { x: '0', y: '0' } 
  messageTextInput: string = ''

  get scrollHeight(): number {
    return this.viewportRef.nativeElement.scrollHeight;
  }

  get scrollTop(): number {
    return this.viewportRef.nativeElement.scrollTop;
  }

  private set scrollTop(currentScrollTop: number) {
    this.viewportRef.nativeElement.scrollTop = currentScrollTop;
  }

  constructor(
    private messagesService: MessagesService,
    public authService: AuthService,
    private changeDetectionRef: ChangeDetectorRef,
    private deleteDialog: MatDialog
  ) { 
    this._changeDetectionRef = changeDetectionRef;
  }

  ngOnInit(): void 
  { 
    this.messagesService.getMessage$
      .subscribe(_ => this.onGetMessage())
      this.authService.getUserId
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.chat) 
      this.loadMessages();
  } 

  ngAfterViewChecked() {
    if (!this._scrolledToBottom && this.chat?.messages?.length != 0){
      this.scrollTop = this.scrollHeight;
      this._scrolledToBottom = true;
    }
  }

  onGetMessage() {
    this._changeDetectionRef.detectChanges();
    this.scrollTop = this.scrollHeight;
  }

  onSendMessage(messageText: string) {
    if (this.chat && messageText.trim())
    {
      if (this.messageToEdit)
      {
        var messageToEdit: Partial<MessageEdit> = {
          id: this.messageToEdit.id,
          text: messageText
        }
        this.messagesService.editMessage(messageToEdit)
          .subscribe(() => {
            this.messageToEdit = undefined;
            this.messageTextInput = ''
          });
      }
      else 
      {
        var messageToSend: MessageSend = {
          text: messageText,
          chatroomId: this.chat.id
        };

        if (this.messageToReply)
          messageToSend.repliedMessageId = this.messageToReply.id
  
        this.messagesService.sendMessage(messageToSend, this.messageToReply)
          .subscribe( () => {
            this.messageToReply = undefined;
            this.messageTextInput = ''
            this._changeDetectionRef.detectChanges()
            this.scrollTop = this.scrollHeight
          })
      }
    }
  }
  
  private loadMessages() {
    if (this.chat?.id != undefined){
      this.messagesService.getMessagesInChatRoom(this.chat.id, this._pagesLoaded + 1)
        .subscribe(messages => 
      {
        var preScrollHeight = this.scrollHeight;

        const length = messages.length;

        this._changeDetectionRef.detectChanges();

        var postScrollHeight = this.scrollHeight; 
        
        if (preScrollHeight != postScrollHeight) {
          var delta = ( postScrollHeight - preScrollHeight );
          this.scrollTop = delta;   
        }

        if (length < this._messagesToTake)
          this._hasReadToEnd = true;
        
        this._pagesLoaded++
      });
    }
  }

  onReadMessage(message: MessageGet) {
    if(this.chat?.messages 
        && message.id === this.chat.messages[0].id 
        && !this._hasReadToEnd) {
      this.loadMessages();
    }
  }

  onRightClickMessage(event: MouseEvent, clickedMessage: MessageGet) {
    event.preventDefault();

    this.messageMenuPosition.x = event.clientX + 'px'
    this.messageMenuPosition.y = event.clientY + 'px'

    this.matMenuTrigger.menuData = { item: clickedMessage };

    this.matMenuTrigger.openMenu();
  }

  onReplyMessage(message: MessageGet) {
    this.messageToEdit = undefined
    this.messageToReply = message
  }
  onCancelReplyMessage() {
    this.messageToReply = undefined;
  }

  onEditMessage(message: MessageGet) {
    this.messageToReply = undefined
    this.messageToEdit = message
    this.messageTextInput = this.messageToEdit.text
  }
  onCancelEditMessage() {
    this.messageToEdit = undefined;
    this.messageTextInput = '';
  }

  onDeleteMessage(message: MessageGet) {
    const dialogConfig =  new MatDialogConfig();
    dialogConfig.data = { messageToDelete: message }

    this.deleteDialog.open(DeleteMessageDialogComponent, dialogConfig);
  }
}

