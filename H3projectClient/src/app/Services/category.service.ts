import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/envionments/envionments';
import { Category } from '../models/Category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl= environment.apiurl + 'Category';
  constructor(private http: HttpClient) { }

  getAll():Observable<Category[]>{
    return this.http.get<Category[]>(this.apiUrl)
  }
  getById(categoryId:number): Observable<Category>{
    return this.http.get<Category>(`${this.apiUrl}/${categoryId}`);
  }
  create(category:Category): Observable<Category>{
    return this.http.post<Category>(this.apiUrl, category);
  }

  update(category:Category): Observable<Category>{
    return this.http.put<Category>(`${this.apiUrl}/${category.categoryID}`, category);
  }

  delete(categoryId:number): Observable<Category>{
    return this.http.delete<Category>(`${this.apiUrl}/${categoryId}`);
  }
}
