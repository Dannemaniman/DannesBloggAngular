import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, firstValueFrom, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult, Pagination } from '../models/pagination';
import { Thread } from '../models/thread';
import { AppService } from './app.service';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {
  private baseUrl = environment.apiUrl

  private _threadsByCategory$ = new BehaviorSubject<Thread[]>([]);
  private paginatedResult: PaginatedResult<Thread[]> = new PaginatedResult<Thread[]>();
  public pagination: Pagination | any;
  public pageNumber = 1;
  public pageSize = 5;
  
  public get threadsByCategory$() {
    return this._threadsByCategory$.asObservable();
  }

  public get threadsByCategory() {
    return this._threadsByCategory$.getValue();
  }

  public set threadsByCategory(threads: Thread[]) {
    this._threadsByCategory$.next(threads)
  }

  private _threadsByLatest$ = new BehaviorSubject<Thread[]>([]);
  private latestPaginatedResult: PaginatedResult<Thread[]> = new PaginatedResult<Thread[]>();
  public latestPagination: Pagination | any;
  public latestPageNumber = 1;
  public latestPageSize = 5;

  public get threadsByLatest$() {
    return this._threadsByLatest$.asObservable();
  }

  public get threadsByLatest() {
    return this._threadsByLatest$.getValue();
  }

  public set threadsByLatest(threads: Thread[]) {
    this._threadsByLatest$.next(threads)
  }

  constructor(
    private http: HttpClient, 
    private appService: AppService,
    private router: Router) { }

  public async updateThread(title: string, content: string, threadId: string) {
    const headers = { "Content-Type": "application/json" };
    const body = {title, content}

    return firstValueFrom(this.http
      .put<Thread>(`${this.baseUrl}/thread/${threadId}`, body, { headers }))
  }

  public getThreadsByCategory(page?: number, itemsPerPage?: number) {
    const categoryId = this.appService.categoryId

    let params = new HttpParams();

    if(page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    this.http.get<Thread[]>(this.baseUrl + '/thread/category/' + categoryId, {observe: 'response', params})
      .pipe(map(response => {
          this.paginatedResult.result = response.body;
          const pagination = response.headers.get('Pagination')

          if(pagination) this.paginatedResult.pagination = JSON.parse(pagination)
          
          return this.paginatedResult;
        }))
      .subscribe(response => {
        if(response) {
          this.threadsByCategory = response.result
          this.pagination = response.pagination
        }
      })
  }

  public getLatestThreads(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if(page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    this.http.get<Thread[]>(this.baseUrl + '/thread/latest', {observe: 'response', params})
      .pipe(map(response => {
        this.latestPaginatedResult.result = response.body;
        const pagination = response.headers.get('Pagination')

        if(pagination) this.latestPaginatedResult.pagination = JSON.parse(pagination)
        
        return this.latestPaginatedResult;
      }))
      .subscribe(response => {
        if(response) {
          this.threadsByLatest = response.result
          this.latestPagination = response.pagination
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
