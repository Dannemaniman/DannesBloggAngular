import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { CategoryPageComponent } from "./modules/category-page/category-page.component"
import { HomePageComponent } from "./modules/home-page/home-page.component"

const routes: Routes = [
  { path: "", component: HomePageComponent, pathMatch: 'full' },
  { path: "category/:id", component: CategoryPageComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
