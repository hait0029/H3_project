import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { cartItems } from '../models/CartItems';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private basketName = "webshopbasket";

  // a subject that expects to hold an instance of cart
  currentBasketSubject: BehaviorSubject<cartItems[]>;

  //user subscribe to our subject
  currentBasket: Observable<cartItems[]>;
  constructor() {


    this.currentBasketSubject = new BehaviorSubject<cartItems[]>(
      JSON.parse(localStorage.getItem(this.basketName) || "[]")
    );
    this.currentBasket = this.currentBasketSubject.asObservable();

  }
  get currentBasketValue(): cartItems[] {
    return this.currentBasketSubject.value;
  }

  saveBasket(basket: cartItems[]): void {
    localStorage.setItem(this.basketName, JSON.stringify(basket));
    this.currentBasketSubject.next(basket);
  }

  clearBasket(): void {
    let basket: cartItems[] = [];
    this.saveBasket(basket);
  }

  addToBasket(item: cartItems): void {
    let productFound = false;
    let basket = this.currentBasketValue;
    basket.forEach(basketItem => {
      if (basketItem.productId == item.productId) {
        basketItem.quantity += item.quantity;
        productFound = true;
        if (basketItem.quantity <= 0) {
          this.removeItemFromBasket(item.productId);
        }
      }
    });


    if (!productFound) {
      basket.push(item);
    }
    this.saveBasket(basket);
  }

  removeItemFromBasket(ProductId: number): void {
    let basket = this.currentBasketValue;
    for (let i = basket.length; i >= 0; i--) {
      if (basket[i].productId == ProductId) {
        basket.splice(i, 1);
      }
    }
    this.saveBasket(basket);
  }
}
