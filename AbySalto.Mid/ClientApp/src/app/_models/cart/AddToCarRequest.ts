import { AddToCartProductDto } from "./AddToCartProduct";

export interface AddToCartRequestDto {
  userId: number;
  products: AddToCartProductDto[];
}
