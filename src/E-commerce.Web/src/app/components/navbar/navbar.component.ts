import { Component, inject, signal } from '@angular/core';
import { NavbarButtonComponent } from '../navbar-button/navbar-button.component';
import {
  FormBuilder,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    NavbarButtonComponent,
    ReactiveFormsModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  private fb = inject(FormBuilder);
  searchForm = this.fb.group({
    searchPhrase: ['', Validators.required],
    category: [''],
  });

  onSubmit() {
    console.log(this.searchForm.value);
  }
}
