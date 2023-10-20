import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/Services/product.service';
import { Product } from 'src/app/models/Product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit{
  products: Product[] = [];
  product: Product = {productID: 0, productName: '', price: 0, categoryId: 0
  }

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getAll()
      .subscribe(x => this.products = x);
  }

  edit(product: Product): void {
    this.product = product;
  }

  //delete method
  //this is a delete, it deletes whatever given id and ensures, that there is an existing object by using filter
  delete(product: Product): void {
    if (confirm('are you want to delete this object?')) {
      this.productService.delete(product.productID)
        .subscribe(() => {
          this.products = this.products.filter(x => x.productID != product.productID);
        });
    }
  }

  //cancel method
  //incase i have filed some of the fields and i wish not execute them, i can just cancel and it will delete whatever the user have filled
  cancel(): void {
    this.product = { productID: 0, productName: '', price: 0, categoryId: 0 }
  }

  save(): void {
    //this is a method combination between create and update
    //create method
    //if the id number is equal to 0, it means we are using create method
    if (this.product.productID == 0) {
      this.productService.create(this.product)
        .subscribe({
          // pushes newly created object to our database
          next: (x) => {
            this.products.push(x);
            //empties the object form
            this.product = { productID: 0, productName: '', price: 0, categoryId: 0}
          },

          error: (err) => {
            console.log(Object.keys(err.error.errors).join(', '));
          }
        });
      }
      //update
      //otherwise if their is an id number then its an update
        else {
      this.productService.update(this.product)
      .subscribe({
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        },
        complete: () =>{
          this.product = { productID: 0, productName: '', price: 0, categoryId: 0 };
        }
      })
    }
  }
}
