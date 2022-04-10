import { Component, EventEmitter, OnInit, Output } from '@angular/core'

@Component({
  selector: 'app-reply',
  templateUrl: './reply.component.html',
  styleUrls: ['./reply.component.scss']
})
export class ReplyComponent implements OnInit {
  @Output() toggle: EventEmitter<boolean> = new EventEmitter();
  @Output() reply: EventEmitter<{ title: string, message: string }> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
    document.body.style.overflow = 'hidden'
  }

  toggleReplying() {
    document.body.style.overflow = 'visible'
    this.toggle.emit(false)
  }
  postReply(title: string, message: string) {
    this.reply.emit({ title, message })
  }
}
