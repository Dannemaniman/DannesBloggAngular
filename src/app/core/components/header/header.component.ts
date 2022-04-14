import { Component, OnInit } from '@angular/core'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public showRegisterModal = false;

  constructor() { }

  ngOnInit(): void {
  }

  public toggleRegisterModal() {
    this.showRegisterModal = !this.showRegisterModal
  }

  public popMenu() {
    console.log("Open menu clicked.")
  }

  public login(email: HTMLInputElement, password: HTMLInputElement) {
    console.log(email.value, password.value)
  }

}
