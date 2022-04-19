import { HttpClient } from "@angular/common/http"
import { Component, OnInit } from '@angular/core'
import { Observable, throwError } from 'rxjs'
import { catchError, retry } from 'rxjs/operators'

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  public data = [
    {
      title: "Daniel often references the Braille version of the bible. Why?",
      author: "kalle@korv.se",
      replies: "123 543",
      views: "11 000"
    },
    {
      title: "Why is Buddha giving me that smarmy smile..",
      author: "chet_sausage@gmail.com",
      replies: "0",
      views: "3"
    },
    {
      title: "Daniel often references the Braille version of the bible. Why?",
      author: "kalle@korv.se",
      replies: "345",
      views: "20"
    },
    {
      title: "The dude should stop..",
      author: "Pilsner_89@hotmail.com",
      replies: "23 235",
      views: "3230"
    },
    {
      title: "Why is Buddha giving me that smarmy smile..",
      author: "Carl@gmail.com",
      replies: "33 213",
      views: "1200"
    },
    {
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
  ]

  public categories = [
    { title: "All", threadCount: "10", code: 'f1' },
    { title: "Spirituality of Daniel", threadCount: "1", code: 'f2' },
    { title: "Crime", threadCount: "3", code: 'f3' },
    { title: "Philosophy", threadCount: "4", code: 'f4' },
    { title: "Discipline", threadCount: "2", code: 'f5' },
  ]

  public showThreadCreater = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    // this.http.get("https://localhost:5001/api/users").subscribe(response => {
    //   console.log(response)
    // }, error => {
    //   console.log(error)
    // })
  }

  public toggleCreateThread() {
    this.showThreadCreater = !this.showThreadCreater
  }
}
