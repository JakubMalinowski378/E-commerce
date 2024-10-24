import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function PasswordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const passwordRegex: RegExp = new RegExp('[^a-zA-Z0-9]');
    const forbidden = passwordRegex.test(control.value);
    return forbidden ? { forbiddenName: { value: control.value } } : null;
  };
}
