import { Component, inject, signal } from '@angular/core';
import { LoginModel } from '../types/LoginModel';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { RouterLink } from '@angular/router';
import { LoginRegisterNavbarComponent } from '../login-register-navbar/login-register-navbar.component';
import { LoginRegisterFooterComponent } from '../login-register-footer/login-register-footer.component';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    LoginRegisterNavbarComponent,
    LoginRegisterFooterComponent,
  ],
  templateUrl: './login-page.component.html',
})
export class LoginPageComponent {
  loginModel: LoginModel | null = null;
  passwordVisibility = signal<boolean>(false);
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  login() {
    console.log(this.loginForm.value);
    console.log(this.loginForm);
    const loginData: LoginModel = {
      email: this.loginForm.value.email || '',
      password: this.loginForm.value.password || '',
    };
    this.accountService.login(loginData);
  }
  togglePasswordVisibility() {
    this.passwordVisibility.set(!this.passwordVisibility());
  }
}
