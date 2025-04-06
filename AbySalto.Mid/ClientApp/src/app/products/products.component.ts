import { Component, inject, OnInit } from '@angular/core';
import { ProductDto } from '../_models/product/product';
import { ProductService } from '../_services/product.service';
import { ProductResponseDto } from '../_models/product/productResponse';
import { CommonModule } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductDetailModalComponent } from '../product-detail-modal/product-detail-modal.component';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent implements OnInit {
  productService = inject(ProductService);
  activeModal = inject(NgbModal);
  user: any | null = null;

  products: ProductDto[] = [];
  skip = 0;
  limit = 30;

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts(this.skip, this.limit).subscribe({
      next: (response: ProductResponseDto) => {
        this.products = response.products;
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
}
