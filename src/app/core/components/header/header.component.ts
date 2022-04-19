import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { Observable } from "rxjs"
import { User } from "../../models/user"
import { AccountService } from "../../services/account.service"
import { HttpService } from "../../services/http.service"

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public showRegisterModal = false;

  constructor(public accountService: AccountService, private route: Router) { }

  ngOnInit(): void {

  }

  public toggleRegisterModal() {
    this.showRegisterModal = !this.showRegisterModal
  }

  public popMenu() {
    console.log("Open menu clicked.")
  }

  public onLoginHandler(username: HTMLInputElement, password: HTMLInputElement) {
    this.accountService.login(username.value, password.value)
  }

  public onLogoutHandler() {
    this.accountService.logout()
  }

  public goHome() {
    this.route.navigateByUrl("/")
  }

}
