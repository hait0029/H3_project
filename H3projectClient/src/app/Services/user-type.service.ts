import { environment } from 'src/envionments/envionments';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserType } from '../models/UserType';
@Injectable({
  providedIn: 'root'
})
export class UserTypeService {
private apiUrl = environment.apiurl + 'UserType';
  constructor(private http: HttpClient) { }

  getAll():Observable<UserType[]>{
    return this.http.get<UserType[]>(this.apiUrl)
  }

  getById(userTypeId:number): Observable<UserType>{
    return this.http.get<UserType>(`${this.apiUrl}/${userTypeId}`);
  }
  create(userType:UserType): Observable<UserType>{
    return this.http.post<UserType>(this.apiUrl, userType);
  }

  update(userType:UserType): Observable<UserType>{
    return this.http.put<UserType>(`${this.apiUrl}/${userType.usertypeID}`, userType);
  }

  delete(userTypeId:number): Observable<UserType>{
    return this.http.delete<UserType>(`${this.apiUrl}/${userTypeId}`);
  }
}
