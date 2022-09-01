import { Component, OnDestroy } from '@angular/core';
import { AuthService } from './features/auth/services/auth-service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnDestroy {
  
  title = 'chat-app';

  constructor(private authService: AuthService) {}

  ngOnDestroy(): void {
    this.authService.logout();
  }
}
