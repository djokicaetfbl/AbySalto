import { ProductDto } from './product';

export interface ProductResponseDto {
  products: ProductDto[];
  total: number;
  skip: number;
  limit: number;
}
