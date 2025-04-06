import { Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { MeComponent } from './me/me.component';

export const routes: Routes = [
  { path: '', component: ProductsComponent },
  {
    path: '',
    children: [
      { path: 'products', component: ProductsComponent },
      { path: 'me', component: MeComponent },
    ],
  },
  { path: '**', component: ProductsComponent, pathMatch: 'full' },
];
