import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/Services/cart.service';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductService } from 'src/app/Services/product.service';
import { cartItems } from 'src/app/models/CartItems';
import { Category } from 'src/app/models/Category';
import { Product } from 'src/app/models/Product';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = []
  category: Category = {
    categoryID: 0, categoryName: '', product: [],
    id: 0
  }
  product: Product = {
    productID: 0, productName: '', price: 0, categoryId: 0,
    category: {
      categoryID: 0, categoryName: "",
      id: 0
    }
  };

  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private productService: ProductService, private cartService: CartService) { }
  ngOnInit(): void {
    this.categoryService.getAll()
      .subscribe(x => this.categories = x);

    console.warn("category id is", this.route.snapshot.paramMap.get('categoryID'));
    this.route.params.subscribe((params) => {

      this.categoryService.getById(parseInt(params["categoryID"])).subscribe(x => this.category = x);
    })
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
