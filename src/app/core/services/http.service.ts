import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from "@angular/common/http"
import { RegisterUser } from "../models/register-user"

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private readonly ROOT_URL = "https://localhost:5001/api"


  constructor(public http: HttpClient) { }

  public registerUser(registerUser: RegisterUser) {
    // let params = new HttpParams()
    this.http.post(`${this.ROOT_URL}/account/register`, registerUser)
      .subscribe(response => console.log(response))
  }

}
