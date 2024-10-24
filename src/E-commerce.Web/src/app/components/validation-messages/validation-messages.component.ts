import { CommonModule, NgFor } from '@angular/common';
import { Component, input } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-validation-messages',
  standalone: true,
  imports: [NgFor, CommonModule],
  templateUrl: './validation-messages.component.html',
})
export class ValidationMessagesComponent {
  control = input.required<AbstractControl>();
}
