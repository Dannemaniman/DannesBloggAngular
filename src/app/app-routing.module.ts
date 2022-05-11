import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { AuthGuard } from './core/guards/auth.guard'
import { CategoryPageComponent } from "./modules/category-page/category-page.component"
import { HomePageComponent } from "./modules/home-page/home-page.component"
import { ThreadDetailPageComponent } from "./modules/thread-detail-page/thread-detail-page.component"
import { UserProfilePageComponent } from "./modules/user-profile-page/user-profile-page.component"

const routes: Routes = [
  { path: "", component: HomePageComponent, pathMatch: 'full' },
  { path: "category/:categoryId", component: CategoryPageComponent, canActivate: [AuthGuard] },
  { path: "category/:categoryId/thread/:threadId", component: ThreadDetailPageComponent, canActivate: [AuthGuard] },
  { path: "user-page", component: UserProfilePageComponent, canActivate: [AuthGuard] },
  { path: "**", component: HomePageComponent, pathMatch: "full" }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
