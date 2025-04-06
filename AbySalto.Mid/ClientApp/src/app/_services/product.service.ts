import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { map, Observable, of, tap } from 'rxjs';
import { ProductResponseDto } from '../_models/product/productResponse';
import { ProductDto } from '../_models/product/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  private productCache = new Map<string, ProductResponseDto>();

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

  getProductsCached(
    skip: number = 0,
    limit: number = 30,
    sortBy: string = 'title',
    descending: boolean = false
  ): Observable<ProductResponseDto> {
    const cacheKey = `${skip}-${limit}-${sortBy}-${descending}`;
    if (this.productCache.has(cacheKey)) {
      return of(this.productCache.get(cacheKey)!);
    }

    const url = `${this.baseUrl}products?skip=${skip}&limit=${limit}&sortBy=${sortBy}&descending=${descending}`;
    return this.http.get<ProductResponseDto>(url).pipe(
      tap((response) => {
        this.productCache.set(cacheKey, response);
      })
    );
  }

  clearCache() {
    this.productCache.clear();
  }
}
