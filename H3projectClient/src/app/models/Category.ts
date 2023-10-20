import { Product } from "./Product";

export interface Category{
  "categoryID": number;
  "categoryName": string;
  "id": number;
  product?: Product[];
}
