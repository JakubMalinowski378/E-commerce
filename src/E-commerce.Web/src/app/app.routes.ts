import { Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { OfferPageComponent } from './components/offer-page/offer-page.component';

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'logowanie', component: LoginPageComponent },
  { path: 'rejestracja', component: RegisterPageComponent },
  { path: 'oferta/:id', component: OfferPageComponent },
];
