import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/Services/category.service';
import { Category } from 'src/app/models/Category';

@Component({
  selector: 'app-categorys',
  templateUrl: './categorys.component.html',
  styleUrls: ['./categorys.component.css']
})


export class CategorysComponent implements OnInit {
  categories: Category[] = [];
  category: Category = { categoryID: 0, categoryName: '', id: 0 }

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getAll()
      .subscribe(x => this.categories = x);
  }

  edit(category: Category): void {
    this.category = category;
  }

  //delete method
  //this is a delete, it deletes whatever given id and ensures, that there is an existing object by using filter
  delete(category: Category): void {
    if (confirm('are you want to delete this object?')) {
      this.categoryService.delete(category.categoryID)
        .subscribe(() => {
          this.categories = this.categories.filter(x => x.categoryID != category.categoryID);
        });
    }
  }

  //cancel method
  //incase i have filed some of the fields and i wish not execute them, i can just cancel and it will delete whatever the user have filled
  cancel(): void {
    this.category = { categoryID: 0, categoryName: '', id: 0 }
  }

  save(): void {
    //this is a method combination between create and update
    //create method
    //if the id number is equal to 0, it means we are using create method
    if (this.category.categoryID == 0) {
      this.categoryService.create(this.category)
        .subscribe({
          // pushes newly created object to our database
          next: (x) => {
            this.categories.push(x);
            //empties the object form
            this.category = { categoryID: 0, categoryName: '', id: 0 }
          },

          error: (err) => {
            console.log(Object.keys(err.error.errors).join(', '));
          }
        });
      }
      //update
      //otherwise if their is an id number then its an update
        else {
      this.categoryService.update(this.category)
      .subscribe({
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        },
        complete: () =>{
          this.category = { categoryID: 0, categoryName: '', id: 0 };
        }
      })
    }
  }
}
