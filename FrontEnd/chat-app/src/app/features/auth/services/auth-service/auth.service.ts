import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { TokensResponse } from '../../models/token-response.model';
import * as moment from "moment";
import jwtDecode from 'jwt-decode';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private tokenUrl: string = `${environment.authProviderUrl}/connect/token`;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded'
    }),
  };
  login$: Subject<TokensResponse> = new Subject<TokensResponse>();
  private _isRefreshing : boolean = false;


  constructor(private http: HttpClient) {
  }

  public login(email:string, password:string ): Observable<TokensResponse> {
    let body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('grant_type', "password");
    body.set('client_id', "angular_client");
    body.set('client_secret', "angular_client_secret");
    body.set('scope', 'apiAccess offline_access');

    return this.http.post<TokensResponse>(this.tokenUrl, body, this.httpOptions).pipe(
      tap(tokenResponse => {
        this.setSession(tokenResponse);
        this.login$.next(tokenResponse);
      })
    );
  }
  
  public sendRefreshRequest(refreshToken:string){
    this._isRefreshing = true

    let body = new URLSearchParams();
    body.set('grant_type', "refresh_token");
    body.set('client_id', "angular_client");
    body.set('client_secret', "angular_client_secret");
    body.set('refresh_token', refreshToken);
    body.set('scope', 'apiAccess offline_access') 

    return this.http.post<TokensResponse>(this.tokenUrl, body, this.httpOptions)
  }

  private setSession(response:TokensResponse) {
        const tokenResponse = response as TokensResponse;
        const expiresAt = moment().add(tokenResponse.expires_in,'second');

        localStorage.setItem('access_token', tokenResponse.access_token);
        localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()) );
        localStorage.setItem('token_type', tokenResponse.token_type);

        if (tokenResponse.refresh_token != null)
          localStorage.setItem('refresh_token', tokenResponse.refresh_token);
  }          

  logout() {
      localStorage.removeItem('access_token');
      localStorage.removeItem('expires_at');
      localStorage.removeItem('token_type');
      localStorage.removeItem('refresh_token');
  }

  public getUserId(){
    const tokenStr = localStorage.getItem('access_token');
    if (tokenStr != null){
        try{
          const bearerToken : any = jwtDecode(tokenStr);
          return bearerToken.sub;
        }
        catch(err){
          return null;
        }
    }
  }

  public getUsername(){
    const tokenStr = localStorage.getItem('access_token');
    if (tokenStr != null){
        try{
          const bearerToken : any = jwtDecode(tokenStr);
          return bearerToken.username;
        }
        catch(err){
          return null;
        }
    }
  }

  public getAccessToken(){
    return localStorage.getItem('access_token');
  }

  public setAccessToken(accessToken:string){
    localStorage.setItem('access_token', accessToken);
  }

  public setRefreshToken(refreshToken:string){
    localStorage.setItem('refresh_token', refreshToken);
  }

  public getRefreshToken(){
    return localStorage.getItem('refresh_token');
  }

  public isAuthorized() {
    const isExists = localStorage.getItem('access_token') != undefined;

    if ( isExists && moment().isAfter(this.getExpiration())) {
      var refreshToken = this.getRefreshToken()
      if (refreshToken && !this._isRefreshing){
        this.sendRefreshRequest(refreshToken).subscribe((tokensResponse) => {
          this.setSession(tokensResponse)
          this._isRefreshing = false
        });
      }
    }
    return isExists
  }

  getExpiration() {
      const expiration = localStorage.getItem("expires_at");
      if (expiration != null){
        const expiresAt = JSON.parse(expiration);
        return moment(expiresAt);
      }
      return null;
  }    
}
