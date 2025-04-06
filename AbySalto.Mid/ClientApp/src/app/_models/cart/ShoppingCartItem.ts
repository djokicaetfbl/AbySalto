export interface ShoppingCartItemDto {
  id: number;
  shoppingCartId: number;
  productId: number;
  title?: string;
  price: number;
  quantity: number;
  total: number;
  discountPercentage: number;
  discountedPrice: number;
  thumbnail?: string;
}
