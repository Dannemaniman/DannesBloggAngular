import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reply } from '../models/reply';
import { AppService } from './app.service';

@Injectable({
  providedIn: 'root'
})
export class ReplyService {
  private _replies$: any = new BehaviorSubject([]);
  private baseUrl = environment.apiUrl
  
  public get replies$() {
    return this._replies$.asObservable();
  }

  public get replies() {
    return this._replies$.getValue();
  }

  public set replies(replies: Reply[]) {
    this._replies$.next(replies)
  }

  constructor(
    private http: HttpClient, 
    private appService: AppService,
    private router: Router) {}

  public getReplyById(replyId: string) {
    return this.http.get<Reply>(this.baseUrl + '/reply/thread/' + replyId)
  }

  public getRepliesByThreadId(threadId: string) {
    const categoryId = this.appService.categoryId
    this.http.get<Reply[]>(this.baseUrl + '/reply/thread/' + threadId).subscribe(replies => {
      if(replies) {
        this.replies = replies
      }
    })
  }

  public getReplies(threadId: string) {
    return this.http.get<Reply>(this.baseUrl + '/thread/' + threadId)
  }

  public createNewThread(title: string, content: string) {
    const categoryId = this.appService.categoryId

    this.http.post<Reply>(this.baseUrl + '/thread', {title, content, categoryId})
    .subscribe(thread => {
      this.router.navigateByUrl(`/category/${thread.categoryId}/thread/${thread.id}`)
    })
  }
}
