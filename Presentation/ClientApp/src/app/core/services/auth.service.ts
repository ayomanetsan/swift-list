import { Injectable } from '@angular/core';
import { HttpService } from './http.service';

import jwtDecode from 'jwt-decode';
import { UserToken } from 'src/app/models/tokens/userToken';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isAuthenticatedInterval: any;

  constructor(
    private http: HttpService, 
    private router: Router,
    private toastr: ToastrService) { }

  login(email: string, password: string) {
    this.http.post<string>('auth/login', { email, password })
      .subscribe(
        (token: string) => {
          this.handleAuthentication(token);
        },
        (err) => {
          this.toastr.error('Error', 'Warning!');
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

  isAuthenticated() {
    const token = localStorage.getItem('token');

    if (token) {
      const decodedToken: UserToken = jwtDecode(token);
      return decodedToken.exp > Date.now() / 1000;
    }

    return false;
  }

  startAuthenticatedCheck() {
    if (this.isAuthenticatedInterval) {
      clearInterval(this.isAuthenticatedInterval);
    }

    this.isAuthenticatedInterval = setInterval(() => {
      if (!this.isAuthenticated()) {
        this.toastr.error('Your session has expired. Please login again.', 'Warning!');
        this.router.navigate(['/login']);
      }
    }, 60000);
  }
}
