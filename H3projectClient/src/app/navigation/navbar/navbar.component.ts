import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';
import { CategoryService } from 'src/app/Services/category.service';
import { cartItems } from 'src/app/models/CartItems';
import { Category } from 'src/app/models/Category';



@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  title = 'H3project.client';
  Categories: Category[] = [];
  basket: cartItems[] = [];

  showAdminPanel: boolean = false;

  toggleAdminPanel() {
    this.showAdminPanel = !this.showAdminPanel;
}


  constructor(private categoryService: CategoryService, private cartService: CartService){}

  ngOnInit(): void {
    console.log("test");
    this.cartService.currentBasket.subscribe(x => {
      this.basket = x;
    });

    this.categoryService.getAll()
        .subscribe(x => this.Categories = x);

  }
}
