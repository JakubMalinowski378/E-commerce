export interface RegisterModel {
  firstName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  gender: 'M' | 'F';
  phoneNumber: string;
  password: string;
}
