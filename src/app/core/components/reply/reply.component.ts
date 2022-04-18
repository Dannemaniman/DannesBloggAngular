import { Component, ElementRef, EventEmitter, HostListener, OnInit, Output, ViewChild } from '@angular/core'
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-reply',
  templateUrl: './reply.component.html',
  styleUrls: ['./reply.component.scss']
})
export class ReplyComponent implements OnInit {
  @Output() 
  toggle: EventEmitter<boolean> = new EventEmitter();
  @Output() 
  reply: EventEmitter<{ title: string, message: string }> = new EventEmitter();
  @ViewChild("form")
  replyForm!: ElementRef;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    /*     if(this.editForm.dirty) {
          $event.returnValue = true;
        } */
  }

  constructor() { }

  ngOnInit(): void {
    document.body.style.overflow = 'hidden'
  }

  toggleReplying() {
    document.body.style.overflow = 'visible'
    this.toggle.emit(false)
  }
  postReply(form: NgForm) {
    console.log(form)
    // this.reply.emit({ form, message })
  }
}
