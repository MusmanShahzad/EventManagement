import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users.component';
import { UserViewComponent } from './user-view/user-view.component';

const routes: Routes = [
  { path: '', component: UsersComponent },
  { path: ':id', component: UserViewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
