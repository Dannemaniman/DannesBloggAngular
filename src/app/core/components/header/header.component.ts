import { Component, OnInit } from '@angular/core'
import { HttpService } from "../../services/http.service"

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public showRegisterModal = false;

  constructor(private http: HttpService) { }

  ngOnInit(): void {
  }

  public toggleRegisterModal() {
    this.showRegisterModal = !this.showRegisterModal
  }

  public popMenu() {
    console.log("Open menu clicked.")
  }

  public login(username: HTMLInputElement, password: HTMLInputElement) {
    this.http.login(username.value, password.value)
    // console.log(email.value, password.value)
  }

}
