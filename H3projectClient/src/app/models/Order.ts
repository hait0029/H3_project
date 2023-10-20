import { productOrderList } from "./ProductOrderList";
import { User } from "./User";

export interface Order{
  "orderID": number;
  "orderDate": Date;
  productOrderList?: productOrderList[];
  user?: User[];
}
