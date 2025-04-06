import { DimensionsDto } from './dimensions';
import { ProductMetaDto } from './productMeta';
import { ProductReviewDto } from './productReview';

export interface ProductDto {
  id: number;
  title: string;
  description: string;
  category: string;
  price: number;
  discountPercentage: number;
  rating: number;
  stock: number;
  tags: string[];
  brand: string;
  sku: string;
  weight: string;
  dimensions: DimensionsDto;
  warrantyInformation: string;
  shippingInformation: string;
  availabilityStatus: string;
  reviews: ProductReviewDto[];
  returnPolicy: string;
  minimumOrderQuantity: string;
  meta: ProductMetaDto;
  thumbnail: string;
  images: string[];
  quantity: number;
}
