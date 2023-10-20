//import { UserType } from 'src/app/models/UserType';
//import { UserTypeService } from './../../Services/user-type.service';
import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';
import { ProductService } from 'src/app/Services/product.service';
import { cartItems } from 'src/app/models/CartItems';
import { Product } from 'src/app/models/Product';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {


  Products: Product[] = [];

  constructor(private productService: ProductService,private cartService: CartService) {}

  ngOnInit(): void {
    this.productService.getAll().subscribe((x) => (this.Products = x));
  }

  addToCart(product: Product): void {

    // this.productService.getById(product.productId).subscribe(t => this.product = t)
    let item: cartItems = {
      productId: product.productID,
      productName: product.productName,
      price: product.price,
      quantity: 0
    };
    this.cartService.addToBasket(item);
  }
}
