import { HttpClient } from "@angular/common/http"
import { Component, OnInit } from '@angular/core'
import { Observable, throwError } from 'rxjs'
import { catchError, retry, take } from 'rxjs/operators'
import { AppService } from "src/app/core/services/app.service"
import { ThreadService } from "src/app/core/services/thread.service"

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  public latestThreads: any;
  public showThreadCreater = false;

  constructor(public appService: AppService, private threadService: ThreadService) { }

  ngOnInit(): void {
    this.threadService.getLatestThreads()
      .pipe(
        take(1)
      )
      .subscribe(response => {
        this.latestThreads = response
      })
  }

  public toggleCreateThread() {
    this.showThreadCreater = !this.showThreadCreater
  }
}
