import { Component, Inject, inject, OnInit } from '@angular/core';
import { ProductService } from '../_services/product.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductDto } from '../_models/product/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-detail-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-detail-modal.component.html',
  styleUrl: './product-detail-modal.component.css',
})
export class ProductDetailModalComponent implements OnInit {
  productService = inject(ProductService);
  activeModal = inject(NgbActiveModal);
  @Inject('productId') productId: number | null = null;

  product: ProductDto | null = null;

  ngOnInit(): void {
    this.getProductDetails();
  }

  getProductDetails() {
    if (this.productId) {
      this.productService.getProductById(this.productId).subscribe({
        next: (response: ProductDto) => {
          this.product = response;
        },
        error: (error) => {
          console.error('Error fetching product details', error);
        },
      });
    }
  }
}
