import { AfterViewInit, Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core'
import { NgForm, NgModel } from '@angular/forms';
import { Reply } from '../../models/reply';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {
  @Output() 
  toggleWindow: EventEmitter<boolean> = new EventEmitter();
  @Output() 
  emitValues: EventEmitter<{title: string, content: string }> = new EventEmitter();
  @ViewChild("form")
  replyForm!: NgForm;

  @ViewChild("content")
  contentInput!: ElementRef;

  public blockUser: string = ""
  public blockDuration: number = 0
  public unBlockUser: string = ""

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    document.body.style.overflow = 'hidden'
  }

  onClickToggleWindow() {
    document.body.style.overflow = 'visible'
    this.toggleWindow.emit(false)
  }
  // onEmitValues(title: NgModel, content: NgModel) {
  //   const titleValue = title.control.value
  //   const messageValue = content.control.value
  //   this.emitValues.emit({title: titleValue, content: messageValue })
  //   this.onClickToggleWindow()
  // }

  public blockUserSubmit() {
    this.adminService.blockUserByUsername(this.blockUser, this.blockDuration)
  }

  public unBlockUserSubmit() {
    console.log(this.unBlockUser);
    
    this.adminService.unBlockUserByUsername(this.unBlockUser)
  }
}
