import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../types/Product';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:8000/api/';

  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + 'Products/' + id);
  }
}
