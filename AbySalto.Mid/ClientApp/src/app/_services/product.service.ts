import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { map, Observable } from 'rxjs';
import { ProductResponseDto } from '../_models/product/productResponse';
import { ProductDto } from '../_models/product/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getProducts(skip: number, limit: number): Observable<ProductResponseDto> {
    return this.http.get<ProductResponseDto>(
      `${this.baseUrl}products?skip=${skip}&limit=${limit}`
    );
  }

  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.baseUrl}products/${id}`);
  }

  addToFavorites(productId: number) {
    //use jwt interceptors
    const url = `${this.baseUrl}favorite/add-to-favorite/${productId}`;
    return this.http.post(url, null);
  }
}
