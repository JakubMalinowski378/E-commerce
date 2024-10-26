import { NgIf } from '@angular/common';
import { Component, input } from '@angular/core';

@Component({
  selector: 'app-navbar-button',
  standalone: true,
  imports: [NgIf],
  templateUrl: './navbar-button.component.html',
})
export class NavbarButtonComponent {
  imageName = input.required<string>();
  numberToDisplay = input<number>(0);
}
