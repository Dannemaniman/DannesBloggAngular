import { Component, OnInit } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router'
import { Category } from 'src/app/core/models/category'
import { AppService } from 'src/app/core/services/app.service'
import { ThreadService } from 'src/app/core/services/thread.service'

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.scss']
})
export class CategoryPageComponent implements OnInit {
  public data: Category[] = [
    {
      id: "223",
      title: "Daniel often references the Braille version of the bible. Why?",
      author: "kalle@korv.se",
      replies: "123 543",
      views: "11 000"
    },
    {
      id: "193",
      title: "Why is Buddha giving me that smarmy smile..",
      author: "chet_sausage@gmail.com",
      replies: "0",
      views: "3"
    },
    {
      id: "22",
      title: "Daniel often references the Braille version of the bible. Why?",
      author: "kalle@korv.se",
      replies: "345",
      views: "20"
    },
    {
      id: "2233",
      title: "The dude should stop..",
      author: "Pilsner_89@hotmail.com",
      replies: "23 235",
      views: "3230"
    },
    {
      id: "145",
      title: "Why is Buddha giving me that smarmy smile..",
      author: "Carl@gmail.com",
      replies: "33 213",
      views: "1200"
    },
    {
      id: "2324",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
    {
      id: "1111",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
    {
      id: "2353",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
    {
      id: "75",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
    {
      id: "633",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
    {
      id: "262",
      title: "VARFÖR GÖR HAN SÅHÄR",
      author: "Cricketman_22@hotmail.com",
      replies: "123 543",
      views: "11 233"
    },
  ]

  public categories = [
    { title: "All", threadCount: "2", code: 'f1' },
    { title: "Spirituality of Daniel", threadCount: "1", code: 'f2' },
    { title: "Crime", threadCount: "3", code: 'f3' },
    { title: "Philosophy", threadCount: "4", code: 'f4' },
    { title: "Discipline", threadCount: "2", code: 'f5' },
    { title: "Birdwatching", threadCount: "2", code: 'f6' },
    { title: "Technology", threadCount: "2", code: 'f7' },
    { title: "Space", threadCount: "2", code: 'f8' },
    { title: "Beverages", threadCount: "2", code: 'f9' },
    { title: "Tostitos", threadCount: "2", code: 'f10' },
    { title: "Raw Lyrics", threadCount: "2", code: 'f11' },
  ]

  public categoryTitle: string = ""
  public showCreateThread: boolean = false;

  constructor(
    private route: Router, 
    private activatedRoute: ActivatedRoute, 
    public appService: AppService,
    public threadService: ThreadService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      const categoryId = params['id']
      const foundCategory = this.categories.find(category => category.code === categoryId)
  
      if(foundCategory) {
        this.categoryTitle = foundCategory?.title
      }
    })
  }

  public navigateByCategory(categoryCode: string) {
    this.route.navigateByUrl('category/' + categoryCode)
  }

  public toggleCreateThread() {
    this.showCreateThread = !this.showCreateThread
  }

  public onCreateThread(threadData: {title: string, content: string}) {
    console.log(threadData)
    this.threadService.createNewThread(threadData.title, threadData.content) 
  }

}


//category-route branchen...

//----------FRONTEND------------
//1. lägg till metod i ny service som heter AppService
//2. Anropa från både /category/:id och homepagen..
//----------Backend------------
//1. Skapa ny Controller vid namn AppController
//2. Antingen returna direkt där en string..
//3. Eller anropa en fil som läser in info från en "fil där jag sparar alla hårdkodade värden"
//
//