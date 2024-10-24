import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function PasswordValidator(): ValidatorFn {
  // length >=8, Contains upperCase, contains lowerCase, contains digit, constains one special character
  return (control: AbstractControl): ValidationErrors | null => {
    const containsUpperCase = new RegExp('[A-Z]').test(control.value);
    const containsLowerCase = new RegExp('[a-z]').test(control.value);
    const containsDigit = new RegExp('[0-9]').test(control.value);
    const containsSpecialCharacter = new RegExp('[^a-zA-Z0-9]').test(
      control.value
    );
    const errors: { [key: string]: string } = {};
    if (!containsUpperCase)
      errors['containsUpperCase'] =
        'Hasło musi zawierać conajmniej jedną wielką literę';
    if (!containsLowerCase)
      errors['containsLowerCase'] =
        'Hasło musi zawierać conajmniej jedną małą literę';
    if (!containsDigit)
      errors['containsDigit'] = 'Hasło musi zawierać conajmniej jedną cyfrę';
    if (!containsSpecialCharacter)
      errors['containsSpecialCharacter'] =
        'Hasło musi zawierać conajmniej jeden znak specjalny';
    return Object.keys(errors).length > 0 ? errors : null;
  };
}
