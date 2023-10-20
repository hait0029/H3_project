import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';
import { OrderService } from 'src/app/Services/order.service';
import { cartItems } from 'src/app/models/CartItems';
import { Order } from 'src/app/models/Order';
import { Product } from 'src/app/models/Product';
import { productOrderList } from 'src/app/models/ProductOrderList';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit{
  cartItems: cartItems[] = [];
  productOrderList: productOrderList = { productOrderListID: 0, quantity: 0 }


  amount: number = 1;
  constructor(private cartService: CartService, private orderService: OrderService){}

  ngOnInit(): void {
    this.amount = Math.floor(Math.random() * 10) + 1;
    this.cartService.currentBasket.subscribe(x => this.cartItems = x);
    //this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  getBasketTotal(): number {
    let total: number = 0;
    this.cartItems.forEach(item => {
      total += item.price * item.quantity;
    })
    return total;
  }

   // this method belongs on productpage and other places where items can be placed in basket
   addToCart(product: Product): void {

    let item: cartItems = {
      productId: product.productID,
      productName: product.productName,
      price: product.price,
      quantity: 1
    };
    this.amount = item.price * item.price
    this.cartService.addToBasket(item);
  }

  clearCart(): void {
    this.cartService.clearBasket();
  }

  updateCart(): void {
    this.cartService.saveBasket(this.cartItems);
  }

  removeItem(item: cartItems): void {
    // if (confirm(`Er du sikker på du vil fjerne ${item.productId} ${item.title}?`)) {
    this.cartItems = this.cartItems.filter(x => x.productId != item.productId);
    this.cartService.saveBasket(this.cartItems);
    // }
  }
}
