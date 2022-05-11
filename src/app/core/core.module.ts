import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HeaderComponent } from './components/header/header.component'
import { FooterComponent } from './components/footer/footer.component'
import { MainMenuComponent } from './components/main-menu/main-menu.component'
import { ThreadBarComponent } from './components/thread-bar/thread-bar.component'
import { ReplyComponent } from './components/reply/reply.component';
import { RegisterModalComponent } from './components/register-modal/register-modal.component'
import { FormsModule } from "@angular/forms"
import { AppRoutingModule } from '../app-routing.module'
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component'


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent,
    ReplyComponent,
    RegisterModalComponent,
    AdminPanelComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    AppRoutingModule,
    PaginationModule.forRoot()
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainMenuComponent,
    ThreadBarComponent,
    ReplyComponent,
    PaginationModule,
    AdminPanelComponent
  ]
})
export class CoreModule { }
