import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult, Pagination } from '../models/pagination';
import { Reply } from '../models/reply';

@Injectable({
  providedIn: 'root'
})
export class ReplyService {
  private _replies$: any = new BehaviorSubject([]);
  private baseUrl = environment.apiUrl

  private paginatedResult: PaginatedResult<Reply[]> = new PaginatedResult<Reply[]>();
  public pagination: Pagination | any;
  public pageNumber = 1;
  public pageSize = 5;
  
  public get replies$() {
    return this._replies$.asObservable();
  }

  public get replies() {
    return this._replies$.getValue();
  }

  public set replies(replies: Reply[]) {
    this._replies$.next(replies)
  }

  constructor(private http: HttpClient) {}

  public getReplyById(replyId: string) {
    return this.http.get<Reply>(this.baseUrl + '/reply/thread/' + replyId)
  }

  public getRepliesByThreadId(threadId: string, page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if(page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    this.http.get<Reply[]>(this.baseUrl + '/reply/thread/' + threadId, {observe: 'response', params})
    .pipe(map(response => {
        this.paginatedResult.result = response.body;
        const pagination = response.headers.get('Pagination')

        if(pagination)  this.paginatedResult.pagination = JSON.parse(pagination)
        
        return this.paginatedResult;
      }))
    .subscribe(response => {
      if(response) {
        this.replies = response.result
        this.pagination = response.pagination
      }
    })
  } 

  public async updateReply(replyId: string, title: string, content: string, ) {
    const headers = { "Content-Type": "application/json" };
    const body = {title, content}

    return firstValueFrom(this.http
      .put<Reply>(`${this.baseUrl}/reply/${replyId}`, body, { headers }))
  }

  public createNewReply(title: string, content: string, threadId: string) {
    return this.http.post<Reply>(this.baseUrl + '/reply', {title, content, threadId})
  }
}
