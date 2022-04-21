import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core'
import { NgForm, NgModel } from '@angular/forms';
import { Reply } from '../../models/reply';

@Component({
  selector: 'app-reply',
  templateUrl: './reply.component.html',
  styleUrls: ['./reply.component.scss']
})
export class ReplyComponent implements OnInit {
  @Output() 
  toggleWindow: EventEmitter<boolean> = new EventEmitter();
  @Output() 
  emitValues: EventEmitter<{title: string, content: string }> = new EventEmitter();
  @ViewChild("form")
  replyForm!: NgForm;

  @Input('title') modalTitle: string = '';
  @Input() buttonTitle: string = '';

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    /*     if(this.editForm.dirty) {
          $event.returnValue = true;
        } */
  }

  constructor() { }

  ngOnInit(): void {
    document.body.style.overflow = 'hidden'
  }

  onClickToggleWindow() {
    document.body.style.overflow = 'visible'
    this.toggleWindow.emit(false)
  }
  onEmitValues(title: NgModel, content: NgModel) {
    const titleValue = title.control.value
    const messageValue = content.control.value
    this.emitValues.emit({title: titleValue, content: messageValue })
    this.onClickToggleWindow()
  }
}
