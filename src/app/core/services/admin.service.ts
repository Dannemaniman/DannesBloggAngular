import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Params, Route, Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private _categories$: any = new BehaviorSubject([]);
  private baseUrl = environment.apiUrl
  public categoryId: any;

  public get categories$() {
    return this._categories$.asObservable();
  }

  public get categories() {
    return this._categories$.getValue();
  }

  public set categories(categories: Category[]) {
    this._categories$.next(categories)
  }


  constructor(private http: HttpClient) { }

  public blockUserByUsername(username: string, duration: number) {
    console.log(username, duration);

    const blockUser = { UserName: username, Duration: duration}
    
    this.http.post<any>(this.baseUrl + '/admin/block', blockUser)
      .subscribe(response => {
        console.log(response);
        
        if(response) {
          console.log("Success!");
        } else {
          console.log("Error! Block Unsuccessful!")
        }
      })
  }

  public unBlockUserByUsername(username: string) {
    this.http.post<any>(this.baseUrl + '/admin/unblock/' + username, {username: username})
      .subscribe(response => {
        console.log(response);
        
        if(response) {
          console.log("Success!");
        } else {
          console.log("Error! Block Unsuccessful!")
        }
      })
  }

}
