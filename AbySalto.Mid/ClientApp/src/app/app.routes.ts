import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { MeComponent } from './me/me.component';
import { authGuard } from './_guards/auth_guard';
import { ProductDetailModalComponent } from './product-detail-modal/product-detail-modal.component';
import { CartComponent } from './cart/cart.component';

export const routes: Routes = [
  { path: '', component: ProductsComponent },
  {
    path: '',
    children: [
      { path: 'products', component: ProductsComponent },
      { path: 'me', component: MeComponent, canActivate: [authGuard] },
      {
        path: 'cart',
        component: CartComponent,
        canActivate: [authGuard],
      },
    ],
  },
  { path: '**', component: ProductsComponent, pathMatch: 'full' },
];
