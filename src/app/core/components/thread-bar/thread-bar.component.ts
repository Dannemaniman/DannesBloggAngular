import { Component, Input, OnInit } from '@angular/core'

@Component({
  selector: 'app-thread-bar',
  templateUrl: './thread-bar.component.html',
  styleUrls: ['./thread-bar.component.scss']
})
export class ThreadBarComponent implements OnInit {
  @Input() title: string = ""
  @Input() author: string = ""
  @Input() replies: string = ""
  @Input() views: string = ""
  @Input() created: string = ""

  constructor() { }

  ngOnInit(): void {
  }

}
