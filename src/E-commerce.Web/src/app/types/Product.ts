export interface Product {
  id: string;
  name: string;
  quantity: number;
  price: number;
  productCategories: string[];
  imageUrls: string[];
  additionalProperties: Record<string, string>;
}
