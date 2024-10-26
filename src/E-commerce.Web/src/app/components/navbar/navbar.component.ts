import { Component } from '@angular/core';
import { NavbarButtonComponent } from "../navbar-button/navbar-button.component";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NavbarButtonComponent],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {}
