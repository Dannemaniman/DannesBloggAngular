import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from "@angular/common/http"
import { RegisterUser } from "../models/register-user"
import { AccountService } from "./account.service"
import { User } from "../models/user"
import { ReplaySubject } from "rxjs"

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor() { }
}
