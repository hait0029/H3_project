import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './FrontPage/home/home.component';
import { ProductsComponent } from './Sales/products/products.component';
import { SignUpComponent } from './Register/sign-up/sign-up.component';
import { LoginComponent } from './Signin/login/login.component';
import { NavbarComponent } from './navigation/navbar/navbar.component';
import { CategoryComponent } from './Categories/category/category.component';

import { AdministratorComponent } from './Admin/administrator/administrator.component';
import { ProductComponent } from './Admin/product/product/product.component';
import { CategorysComponent } from './Admin/category/categorys/categorys.component';
import { CartComponent } from './Cart/cart/cart.component';
import { FooterComponent } from './foot/footer/footer.component';
import { TestboolComponent } from './testbool/testbool.component';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductsComponent,
    SignUpComponent,
    LoginComponent,
    NavbarComponent,
    AdministratorComponent,
    CategoryComponent,
    ProductComponent,
    CategorysComponent,
    CartComponent,
    FooterComponent,
    TestboolComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
