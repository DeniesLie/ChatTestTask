import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginFormModel } from '../../models/login-form.model';
import { TokensResponse } from '../../models/token-response.model';
import { AuthService } from '../../services/auth-service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public isLoginSucceeded:boolean|undefined;

  constructor(@Inject(FormBuilder) private formBuilder: FormBuilder,
    private authService : AuthService,
    private router: Router) 
    { }

  form = new FormGroup({
    username: new FormControl("", Validators.minLength(4)),
    password: new FormControl("", Validators.minLength(6))  
  });
  hide = true;

  ngOnInit(): void {
  }

  onSubmit(): void{
    const model : LoginFormModel = this.form.value as LoginFormModel;
    this.authService.login(model.username, model.password)
      .pipe(catchError(err => {
        this.handleLoginFail();
        return throwError(err)
      }))
      .subscribe(response => this.handleLoginSuccess(response));
  }

  private handleLoginFail(){
    this.isLoginSucceeded = false;
  }

  private handleLoginSuccess(tokenResponse: TokensResponse){
    this.router.navigate(['/messenger']);
  }
}
