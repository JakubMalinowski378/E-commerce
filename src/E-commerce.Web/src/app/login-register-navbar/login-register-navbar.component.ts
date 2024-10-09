import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login-register-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './login-register-navbar.component.html',
  styleUrl: './login-register-navbar.component.css',
})
export class LoginRegisterNavbarComponent {}
