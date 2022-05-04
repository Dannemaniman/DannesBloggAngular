import { Component, OnDestroy, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router';
import { lastValueFrom, Subscription } from 'rxjs';
import { Reply } from 'src/app/core/models/reply';
import { AccountService } from 'src/app/core/services/account.service';
import { ReplyService } from 'src/app/core/services/reply.service';
import { ThreadService } from 'src/app/core/services/thread.service';

@Component({
  selector: 'app-thread-detail-page',
  templateUrl: './thread-detail-page.component.html',
  styleUrls: ['./thread-detail-page.component.scss']
})
export class ThreadDetailPageComponent implements OnInit, OnDestroy {
  public thread: any;
  public reply: any; 
  private subscription: Subscription[] = [];
  public toggleReply: boolean = false;
  public editAvailable: boolean = false
  public isEditing: boolean = false
  public replyToUser = ''
  public repliesEditing: string[] = []

  constructor(
    private accountService: AccountService,
    private replyService: ReplyService, 
    private threadService: ThreadService, 
    private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.subscription.push(
        this.threadService.getThread(params['threadId']).subscribe(response => {
          this.thread = response 

          if(response.userName === this.accountService.currentUser?.userName) {
            this.editAvailable = true
          }
        })
      )
    })
  }

  public ngOnDestroy(): void {
    this.subscription.forEach(subscription => {
      subscription.unsubscribe()
    })
  }

  public canUserEditReply(username: string) {
    return username === this.accountService.currentUser?.userName
  }

  public toggleEditReply(id: string) {
    const foundIndex = this.repliesEditing.findIndex(i => i === id)

    if(foundIndex > -1) {
      this.repliesEditing.splice(foundIndex, 1)
    } else {
      this.repliesEditing.push(id)
    }
  }

  public toggleEditThread() {
    this.isEditing = !this.isEditing
  }

  public async saveThreadChanges(){
    await this.threadService.updateThread(this.thread.title, this.thread.content, this.thread.id)
    this.isEditing = !this.isEditing
    
    window.location.reload()
  }

  public async saveReplyChanges(id: string, title: string, content: string){
    await this.replyService.updateReply(id, title, content)
    this.toggleEditReply(id)
    
    window.location.reload()
  }

  public onValuesEmitted(title: string, content: string) {
    let threadId = this.thread.id.toString()
    this.replyService.createNewReply(title, content, threadId)
      .subscribe(response => window.location.reload())
  }

  public onToggleCreateWindow(replyTo?: string, replyTitle?: string) {
    if(replyTo) {
      this.replyToUser = replyTo + ' | ' + replyTitle
    }
    console.log(this.replyToUser);
    
    this.toggleReply = !this.toggleReply
  }
}
