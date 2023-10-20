import { Category } from "./Category";

export interface Product{
  "productID": number;
  "productName": string;
  "price": number;
  "category"?: Category;
  "categoryId": number;
}
