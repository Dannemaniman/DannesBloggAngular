import { Component, OnInit } from '@angular/core'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  public popMenu() {
    console.log("Open menu clicked.")
  }

  public login(email: HTMLInputElement, password: HTMLInputElement) {
    console.log(email.value, password.value) 
  }

}
