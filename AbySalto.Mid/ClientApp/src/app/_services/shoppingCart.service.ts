import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { AddToCartProductDto } from '../_models/cart/AddToCartProduct';
import { ShoppingCartDto } from '../_models/cart/ShoppingCartDto';
import { AddToCartRequestDto } from '../_models/cart/AddToCarRequest';
import { PagedShoppingCartResponseDto } from '../_models/cart/PagedShoppingCartResponse';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  addToShoppingCart(request: AddToCartRequestDto): Observable<ShoppingCartDto> {
    return this.http.post<ShoppingCartDto>(
      `${this.baseUrl}shoppingCart/add`,
      request
    );
  }

  getShoppingCartByUser(
    userId: number,
    skip: number = 0,
    limit: number = 30
  ): Observable<PagedShoppingCartResponseDto> {
    return this.http.get<PagedShoppingCartResponseDto>(
      `${this.baseUrl}shoppingCart/get-cart-by-user/${userId}?skip=${skip}&limit=${limit}`
    );
  }
}
