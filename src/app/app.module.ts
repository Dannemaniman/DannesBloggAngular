import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { CoreModule } from './core/core.module'
import { HomePageComponent } from './modules/home-page/home-page.component'
import { CategoryPageComponent } from './modules/category-page/category-page.component'
import { ThreadDetailPageComponent } from './modules/thread-detail-page/thread-detail-page.component'
import { UserProfilePageComponent } from './modules/user-profile-page/user-profile-page.component'
import { ToastrModule } from "ngx-toastr"
import { JwtInterceptor } from './core/interceptors/jwt.interceptor'
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination'
//VI TAR SEDAN BORT IMPORTS SOM VI INTE ANVÄNDER FÖR MER CLEAN CODE... 
//DOCK SÅ KOMMER ANGULAR NÄR DEN BUILDAR APPEN ATT ÄNDÅ INTE COMPILEA KOD SOM INTE ANVÄNDS.. 

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
    HttpClientModule,
    FormsModule,
    CoreModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
