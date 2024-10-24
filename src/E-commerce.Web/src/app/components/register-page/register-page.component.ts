import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PasswordValidator } from '../../_validators/passwordValidator';
import { LoginRegisterNavbarComponent } from '../login-register-navbar/login-register-navbar.component';
import { LoginRegisterFooterComponent } from '../login-register-footer/login-register-footer.component';
import { FormInputComponent } from '../app-form-input/form-input.component';
import { AccountService } from '../../_services/account.service';
import { RegisterModel } from '../../types/RegisterModel';
import { passwordMatchValidator } from '../../_validators/passwordMatchValidator';

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
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  registerForm = this.fb.group(
    {
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      password: ['', [Validators.required, PasswordValidator]],
      confirmPassword: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
    },
    {
      validators: passwordMatchValidator,
    }
  );

  submit() {
    if (this.registerForm.valid) {
      const registerModel = this.registerForm.value as unknown as RegisterModel;
      console.log(registerModel);
      this.accountService.register(registerModel);
    } else {
      console.log('error');
      console.log(this.registerForm.errors);
    }
  }
}
