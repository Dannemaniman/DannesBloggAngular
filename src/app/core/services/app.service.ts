import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Params, Route, Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class AppService {
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

  public getAppCategories() {
    this.http.get<Category[]>(this.baseUrl + '/app/categories')
      .subscribe(categories => {
        if(categories) {
          this.categories = categories 
        }
      })
  }
  
  public getFavoriteIdFromUrl() {

  } 
}
