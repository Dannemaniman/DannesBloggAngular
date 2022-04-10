import { Component, OnInit } from '@angular/core'

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
    { title: "All", threadCount: "10" },
    { title: "Spirituality of Daniel", threadCount: "1" },
    { title: "Crime", threadCount: "3" },
    { title: "Philosophy", threadCount: "4" },
    { title: "Discipline", threadCount: "2" },
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
