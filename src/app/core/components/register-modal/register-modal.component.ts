import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core'
import { NgForm } from "@angular/forms"
import { RegisterUser } from "../../models/register-user"

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.scss']
})
export class RegisterModalComponent implements OnInit, AfterViewInit {
  @Output() dismiss: EventEmitter<RegisterUser> = new EventEmitter();
  @ViewChild("form") signupForm: NgForm | undefined

  constructor() { }

  ngOnInit(): void {
    document.body.style.overflow = 'hidden'
  }

  ngAfterViewInit(): void {
    this.signupForm
  }

  public registerUser(form: NgForm) {
    document.body.style.overflow = 'visible'

    const user: RegisterUser = {
      firstname: this.signupForm?.value.userData.firstname,
      lastname: this.signupForm?.value.userData.lastname,
      age: this.signupForm?.value.userData.age,
      email: this.signupForm?.value.userData.email,
      password: this.signupForm?.value.userData.password,
    }

    this.signupForm?.reset()
    this.dismiss.emit(user)
  }

  public dismissModal() {
    document.body.style.overflow = 'visible'
    this.dismiss.emit()
  }

}
