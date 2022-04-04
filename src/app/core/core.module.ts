import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HeaderComponent } from './components/header/header.component'
import { FooterComponent } from './components/footer/footer.component'
import { MainMenuComponent } from './components/main-menu/main-menu.component'


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent
  ]
})
export class CoreModule { }
