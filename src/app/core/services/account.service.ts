import { HttpClient } from "@angular/common/http"
import { Injectable } from '@angular/core'
import { ReplaySubject } from "rxjs"
import { RegisterUser } from "../models/register-user"
import { User } from "../models/user"

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly ROOT_URL = "https://localhost:5001/api"
  private _currentUserSource = new ReplaySubject<User | null>(1);
  public currentUser$ = this._currentUserSource.asObservable();

  constructor(public http: HttpClient) { }

  public registerUser(registerUser: RegisterUser) {
    // let params = new HttpParams()
    this.http.post(`${this.ROOT_URL}/account/register`, registerUser)
      .subscribe(response => console.log(response))
  }

  public login(UserName: string, Password: string) {
    const user = { UserName, Password }

    this.http.post<User>(`${this.ROOT_URL}/account/login`, user)
      .subscribe(
        response => {
          const user = response

          if (user) {
            localStorage.setItem("user", JSON.stringify(user))
            this.setCurrentUser(user)
          }
        }, error => {
          console.log(error)
        })
  }

  public logout() {
    localStorage.removeItem("user")
    this._currentUserSource.next(null)
  }

  public setCurrentUser(user: User) {
    this._currentUserSource.next(user)
  }
}
