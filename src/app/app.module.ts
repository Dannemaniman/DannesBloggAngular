import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { CoreModule } from './core/core.module'
import { HomePageComponent } from './modules/home-page/home-page.component'
import { CategoryPageComponent } from './modules/category-page/category-page.component'
import { ThreadDetailPageComponent } from './modules/thread-detail-page/thread-detail-page.component'
import { UserProfilePageComponent } from './modules/user-profile-page/user-profile-page.component'

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    CategoryPageComponent,
    ThreadDetailPageComponent,
    UserProfilePageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
