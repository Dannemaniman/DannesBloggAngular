import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HeaderComponent } from './components/header/header.component'
import { FooterComponent } from './components/footer/footer.component'
import { MainMenuComponent } from './components/main-menu/main-menu.component'
import { ThreadBarComponent } from './components/thread-bar/thread-bar.component'


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent
  ]
})
export class CoreModule { }
