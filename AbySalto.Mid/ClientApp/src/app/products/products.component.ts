import { Component, inject, OnInit } from '@angular/core';
import { ProductDto } from '../_models/product/product';
import { ProductService } from '../_services/product.service';
import { ProductResponseDto } from '../_models/product/productResponse';
import { CommonModule } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductDetailModalComponent } from '../product-detail-modal/product-detail-modal.component';
import { AccountService } from '../_services/account.service';
import { AddToCartRequestDto } from '../_models/cart/AddToCarRequest';
import { ShoppingCartService } from '../_services/shoppingCart.service';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent implements OnInit {
  productService = inject(ProductService);
  activeModal = inject(NgbModal);
  user: any | null = null;
  shoppingCartService = inject(ShoppingCartService);

  products: ProductDto[] = [];
  skip = 0;
  limit = 30;

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService /*.getProducts*/
      .getProductsCached(this.skip, this.limit)
      .subscribe({
        next: (response: ProductResponseDto) => {
          this.products = response.products.map((product) => {
            if (!product.quantity) product.quantity = 1;
            return product;
          });
        },
        error: (error) => {
          console.error('Error fetching products', error);
        },
      });
  }

  loadMore() {
    this.skip += this.limit;
    this.getProducts();
  }

  openProductDetail(productId: number) {
    const modalRef = this.activeModal.open(ProductDetailModalComponent, {
      centered: true,
    });
    modalRef.componentInstance.productId = productId;
  }

  addToFavorites(productId: number) {
    this.productService.addToFavorites(productId).subscribe({
      next: (user) => {
        this.user = user;
      },
      error: (error) => {
        console.error('Error adding product to favorites', error);
      },
    });
  }

  addToShoppingCart(productId: number, quantity: number) {
    const addToShoppingCartDto: AddToCartRequestDto = {
      userId: 0,
      products: [
        {
          id: productId,
          quantity: quantity,
        },
      ],
    };

    this.shoppingCartService.addToShoppingCart(addToShoppingCartDto).subscribe({
      error: (error) => console.log('Error adding to shopping cart', error),
    });
  }
}
