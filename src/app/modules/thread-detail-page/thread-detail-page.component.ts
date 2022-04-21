import { Component, OnDestroy, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
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
  //= {
    // title: "Daniel often references the Braille version of the bible. Why?",
    // content: "Hi Everybody! <br/>First time poster here..So I recently heard on a podcast that supposedly this 'Daniel Langås', is a renowned man and cultured.Which is kind of a weird thing<br/>to say about yourself..Also, he kept insisting on referencing the Braille version of the Bible..Which is also weird..<br/>Who is this guy ? And who does he think he is?",
    // replies: "10 148",
    // views: "214 002",
    // author: "kalle@korv.se",
    // date: "20:48  18/6-2022"
  // }

  // public reply = {
  //   title: "Dude what?",
  //   content: "Dude.. who even are you",
  //   replies: "10 148",
  //   views: "214 002",
  //   author: "kalle@korv.se",
  //   date: "20:48  18/6-2022"
  // }

  public toggleReply: boolean = false;

  constructor(private threadService: ThreadService, private route: ActivatedRoute) { }

  public ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.subscription.push(
        this.threadService.getThread(param['id']).subscribe(response => {
          console.log(response)
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

  public toggleReplying(data?: any) {
    console.log(data);
    
    this.toggleReply = !this.toggleReply
  }
}
