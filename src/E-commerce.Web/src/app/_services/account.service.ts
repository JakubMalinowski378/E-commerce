import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { JwtToken } from '../types/JwtToken';
import { LoginModel } from '../types/LoginModel';
import { RegisterModel } from '../types/RegisterModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:8000/api/';
  private http = inject(HttpClient);
  private router = inject(Router);
  token = signal<JwtToken | null>(null);

  login(loginModel: LoginModel): void {
    this.http
      .post<JwtToken>(this.baseUrl + 'Account/login', loginModel)
      .subscribe({
        next: (token) => {
          this.setCurrentToken(token);
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error(error);
        },
      });
  }

  register(registerModel: RegisterModel): void {
    this.http
      .post<JwtToken>(this.baseUrl + 'Account/register', registerModel)
      .subscribe({
        next: (token) => {
          this.setCurrentToken(token);
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error(error);
        },
      });
  }

  setCurrentToken(token: JwtToken): void {
    this.token.set(token);
    localStorage.setItem('token', token.token);
  }
}
