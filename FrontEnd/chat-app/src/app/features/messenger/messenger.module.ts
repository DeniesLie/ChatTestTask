import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessengerPageComponent } from './components/messenger-page/messenger-page.component';
import { ChatPreviewComponent } from './components/chat-preview/chat-preview.component';
import { ChatWindowComponent } from './components/chat-window/chat-window.component';
import { DeleteMessageDialogComponent } from './components/delete-message-dialog/delete-message-dialog.component';
import { MessageComponent } from './components/message/message.component';

import { ObserveVisibilityDirective } from 'src/app/attribute-directives/observe-visibility.directive';
import { FormsModule } from '@angular/forms'

import { MatIconModule } from '@angular/material/icon'
import { MatMenuModule } from '@angular/material/menu'
import { MatDialogModule } from '@angular/material/dialog'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatButtonModule } from '@angular/material/button'

@NgModule({
  declarations: [
    MessengerPageComponent,
    ChatPreviewComponent,
    ChatWindowComponent,
    MessageComponent,
    ObserveVisibilityDirective,
    DeleteMessageDialogComponent,
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatMenuModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatCheckboxModule
  ]
})
export class MessengerModule { }
