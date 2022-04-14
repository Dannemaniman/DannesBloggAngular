import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HeaderComponent } from './components/header/header.component'
import { FooterComponent } from './components/footer/footer.component'
import { MainMenuComponent } from './components/main-menu/main-menu.component'
import { ThreadBarComponent } from './components/thread-bar/thread-bar.component'
import { ReplyComponent } from './components/reply/reply.component';
import { RegisterModalComponent } from './components/register-modal/register-modal.component'
import { FormsModule } from "@angular/forms"


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent,
    ReplyComponent,
    RegisterModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent,
    ReplyComponent
  ]
})
export class CoreModule { }
