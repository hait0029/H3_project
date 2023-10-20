import { cartItems } from 'src/app/models/CartItems';
import { ProductService } from './../../Services/product.service';
import { Product } from './../../models/Product';
import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
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
