import { Component, OnDestroy, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Reply } from 'src/app/core/models/reply';
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

  constructor(private replyService: ReplyService, private threadService: ThreadService, private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.subscription.push(
        this.threadService.getThread(params['threadId']).subscribe(response => {
          this.thread = response
        })
      )
    })
  }

  public ngOnDestroy(): void {
    this.subscription.forEach(subscription => {
      subscription.unsubscribe()
    })
  }

  public toggleEditThread() {
    console.log("clicked toggleEditThread!");
  }

  public onValuesEmitted(title: string, content: string) {
    let threadId = this.thread.id.toString()
    this.replyService.createNewReply(title, content, threadId)
      .subscribe(response => window.location.reload())
  }

  public onToggleCreateWindow(data?: any) {
    console.log(data);
    
    this.toggleReply = !this.toggleReply
  }
}
