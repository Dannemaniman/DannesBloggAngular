import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Reply } from 'src/app/core/models/reply';
import { Thread } from 'src/app/core/models/thread';
import { AccountService } from 'src/app/core/services/account.service';

export interface UserChangePassword {
  currentPassword: string
  newPassword: string
  confirmedPassword: string
}

@Component({
  selector: 'app-user-profile-page',
  templateUrl: './user-profile-page.component.html',
  styleUrls: ['./user-profile-page.component.scss']
})
export class UserProfilePageComponent implements OnInit, OnDestroy {
  public currentPassword: any = "";
  public newPassword: any = "";
  public confirmedPassword: any = "";

  public allReplies: any;
  public allThreads: any;

  private subscription: any[] = [];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.subscription.push(
    this.accountService.getAllUserReplies().subscribe(response => {
      console.log(response);
      
      if(response) {
        this.allReplies = response
      }
    }),
    this.accountService.getAllUserThreads().subscribe(response => {
      console.log(response);
      
      if(response) {
        this.allThreads = response
      }
    })
    )
  }

  ngOnDestroy(): void {
    this.subscription.forEach(sub => sub.unsubscribe())
  }

  public onChangePassword() {
    const change: UserChangePassword = {
      currentPassword: this.currentPassword,
      newPassword: this.newPassword,
      confirmedPassword: this.confirmedPassword,
    }

    console.log(change)
    this.accountService.changePassword(change)
  }

}
