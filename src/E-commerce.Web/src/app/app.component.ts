import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'E-commerce';
  private accountService = inject(AccountService);
  ngOnInit(): void {
    this.setCurrentToken();
  }

  setCurrentToken(): void {
    const token = localStorage.getItem('token');
    if (!token) return;
    this.accountService.setCurrentToken({ token });
  }
}
