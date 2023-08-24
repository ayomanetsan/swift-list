import { Injectable } from '@angular/core';
import { HttpService } from './http.service';

import jwtDecode from 'jwt-decode';
import { UserToken } from 'src/app/models/tokens/userToken';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpService, private router: Router) { }

  login(email: string, password: string) {
    this.http.post<string>('auth/login', { email, password })
      .subscribe(
        (token: string) => {
          this.handleAuthentication(token);
        }
      );
  }

  register(fullname: string, email: string, password: string) {
    this.http.post<string>('auth/register', { fullname, email, password })
    .subscribe(
      (token: string) => {
        this.handleAuthentication(token);
      }
    );
  }

  handleAuthentication(token: string) {
    localStorage.setItem('token', token);

    const decodedToken: UserToken = jwtDecode(token);

    localStorage.setItem('userName', decodedToken.name);
    localStorage.setItem('userEmail', decodedToken.email);

    this.router.navigate(['/tasks']);
  }
}
