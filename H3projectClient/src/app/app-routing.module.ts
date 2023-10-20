import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './FrontPage/home/home.component';

import { LoginComponent } from './Signin/login/login.component';
import { SignUpComponent } from './Register/sign-up/sign-up.component';
import { CategoryComponent } from './Categories/category/category.component';
import { CategorysComponent } from './Admin/category/categorys/categorys.component';
import { ProductComponent } from './Admin/product/product/product.component';
import { CartComponent } from './Cart/cart/cart.component';
import { TestboolComponent } from './testbool/testbool.component';


const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'categoryproduct', component:CategoryComponent},
  {path:'category/:categoryID',component:CategoryComponent},
  {path:'Login',component:LoginComponent},
  {path:'Signup',component:SignUpComponent},
  {path:'Admin/category', component:CategorysComponent},
  {path:'Admin/product', component:ProductComponent},
  {path:'app/cart', component:CartComponent},
  {path:'test', component:TestboolComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
