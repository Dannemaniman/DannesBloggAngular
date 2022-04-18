import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ThreadDetailPageComponent } from 'src/app/modules/thread-detail-page/thread-detail-page.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanActivate {
  canActivate(component: ThreadDetailPageComponent): boolean {
   /*  if(component.editForm.dirty) {
      return confirm("Are you sure you want to continue? Any unsaved changes will be lost.")
    } */
    return true;
  }
  
}
