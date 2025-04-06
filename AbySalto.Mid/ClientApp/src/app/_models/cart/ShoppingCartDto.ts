import { ShoppingCartItemDto } from './ShoppingCartItem';

export interface ShoppingCartDto {
  id: number;
  userId: number;
  total: number;
  discountedTotal: number;
  totalProducts: number;
  totalQuantity: number;
  products: ShoppingCartItemDto[];
}
