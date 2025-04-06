import { ShoppingCartItemDto } from './ShoppingCartItem';

export interface PagedShoppingCartResponseDto {
  carts: ShoppingCartItemDto[];
  total: number;
  skip: number;
  limit: number;
}
