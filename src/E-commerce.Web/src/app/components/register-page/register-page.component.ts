import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PasswordValidator } from '../../_validators/passwordValidator';
import { LoginRegisterNavbarComponent } from '../login-register-navbar/login-register-navbar.component';
import { LoginRegisterFooterComponent } from '../login-register-footer/login-register-footer.component';
import { FormInputComponent } from '../app-form-input/form-input.component';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    LoginRegisterNavbarComponent,
    LoginRegisterFooterComponent,
    FormInputComponent,
    FormInputComponent,
  ],
  templateUrl: './register-page.component.html',
})
export class RegisterPageComponent {
  private fb = inject(FormBuilder);
  registerForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required]],
    gender: ['', [Validators.required]],
    dateOfBirth: ['', [Validators.required]],
    password: ['', [Validators.required, PasswordValidator]],
    confirmPassword: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required]],
  });

  change() {
    console.log(this.registerForm);
    console.log(this.registerForm.value);
  }
}
