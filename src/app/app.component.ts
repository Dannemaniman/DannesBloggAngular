import { Component, OnInit } from '@angular/core'
import { User } from "./core/models/user"
import { AccountService } from './core/services/account.service'
import { AppService } from './core/services/app.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(
    private accountService: AccountService,
    private appService: AppService) { }

  ngOnInit() {
    this.setCurrentUserFromStorage()
    this.appService.getAppCategories();
  }

  setCurrentUserFromStorage() {
    const storedUser = localStorage.getItem("user")
    if (storedUser) {
      const user: User = JSON.parse(storedUser)
      this.accountService.setCurrentUser(user)
    }
  }
}
