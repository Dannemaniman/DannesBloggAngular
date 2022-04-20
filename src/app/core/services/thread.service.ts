import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Thread } from '../models/thread';
import { AppService } from './app.service';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {
  private _threads$: any = new BehaviorSubject([]);
  private baseUrl = environment.apiUrl
  
  public get threads$() {
    return this._threads$.asObservable();
  }

  public get threads() {
    return this._threads$.getValue();
  }

  public set threads(threads: Thread[]) {
    this._threads$.next(threads)
  }

  constructor(
    private http: HttpClient, 
    private appService: AppService,
    private router: Router) {
      this.getThreadsByCategory()
     }

  public getThreadsByCategory() {
    const categoryId = this.appService.categoryId
    this.http.get<Thread[]>(this.baseUrl + '/thread/category/' + categoryId).subscribe(threads => {
      if(threads) {
        this.threads = threads
      }
    })
  }

  public getThread(threadId: string) {
    return this.http.get<Thread>(this.baseUrl + '/thread/' + threadId)
  }

  public createNewThread(title: string, content: string) {
    const categoryId = this.appService.categoryId

    this.http.post<Thread>(this.baseUrl + '/thread', {title, content, categoryId})
    .subscribe(thread => {
      this.router.navigateByUrl(`/category/${thread.categoryId}/thread/${thread.id}`)
    })
  }
}
