import { Component, OnInit } from '@angular/core'

@Component({
  selector: 'app-thread-detail-page',
  templateUrl: './thread-detail-page.component.html',
  styleUrls: ['./thread-detail-page.component.scss']
})
export class ThreadDetailPageComponent implements OnInit {
  public thread = {
    title: "Daniel often references the Braille version of the bible. Why?",
    content: "Hi Everybody! <br/>First time poster here..So I recently heard on a podcast that supposedly this 'Daniel Langås', is a renowned man and cultured.Which is kind of a weird thing<br/>to say about yourself..Also, he kept insisting on referencing the Braille version of the Bible..Which is also weird..<br/>Who is this guy ? And who does he think he is?",
    replies: "10 148",
    views: "214 002",
    author: "kalle@korv.se",
    date: "20:48  18/6-2022"
  }

  public reply = {
    title: "Dude what?",
    content: "Dude.. who even are you",
    replies: "10 148",
    views: "214 002",
    author: "kalle@korv.se",
    date: "20:48  18/6-2022"
  }

  public toggleReply: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  public toggleReplying() {
    this.toggleReply = !this.toggleReply
  }

}
