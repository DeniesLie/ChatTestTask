import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/app/features/auth/services/auth-service/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private router: Router,
    public authService: AuthService) { }

  public get profileLetters() : string {
    var username : string = this.authService.getUsername();
    return username.split(' ').map(s => s[0].toUpperCase()).join('');
  }

  ngOnInit(): void {
  }

  onLogin() {
    this.router.navigate(['login'])
  }

  onLogout() {
    this.authService.logout()
    this.router.navigate(['login'])
  }
}
