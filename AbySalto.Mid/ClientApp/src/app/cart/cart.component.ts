import { Component, inject, OnInit } from '@angular/core';
import { ShoppingCartService } from '../_services/shoppingCart.service';
import { PagedShoppingCartResponseDto } from '../_models/cart/PagedShoppingCartResponse';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {
  shoppingCartService = inject(ShoppingCartService);
  cart: PagedShoppingCartResponseDto | null = null;
  userId: number = 6; //
  skip: number = 0;
  limit: number = 5;

  ngOnInit(): void {
    this.getCart();
  }

  getCart() {
    this.shoppingCartService
      .getShoppingCartByUser(this.userId, this.skip, this.limit)
      .subscribe({
        next: (response) => {
          this.cart = response;
        },
        error: (error) => {
          console.error('Error fetching cart', error);
        },
      });
  }

  loadMore() {
    this.skip += this.limit;
    this.getCart();
  }
}
