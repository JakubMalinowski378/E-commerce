import { AbstractControl } from '@angular/forms';

export function passwordMatchValidator(control: AbstractControl) {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;
  if (password !== confirmPassword) {
    control.get('confirmPassword')?.setErrors({ passwordMismatch: "Hasła nie są takie same" });
  } else {
    control.get('confirmPassword')?.setErrors(null);
  }
  return null;
}
