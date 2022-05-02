import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'
import { AccountService } from "../../services/account.service"

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
