import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MessageGet } from '../../models/messages/messageGet';
import { MessagesService } from '../../services/messages-service/messages.service';

@Component({
  selector: 'app-delete-message-dialog',
  templateUrl: './delete-message-dialog.component.html',
  styleUrls: ['./delete-message-dialog.component.css']
})
export class DeleteMessageDialogComponent implements OnInit {

  constructor(
    private messagesService: MessagesService,
    private dialogRef: MatDialogRef<DeleteMessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data : any) {
      this.messageToDelete = data.messageToDelete
     }

  isDeleteForEveryone: boolean = false;
  messageToDelete: MessageGet;

  ngOnInit(): void {
  }

  cancel(): void {
    this.dialogRef.close()
  }

  deleteMessage(): void {
    this.messagesService.deleteMessage(
      this.messageToDelete, this.isDeleteForEveryone)
      .subscribe();

    this.dialogRef.close()
  }
}
