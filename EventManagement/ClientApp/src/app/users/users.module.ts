import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { CommonModuler } from '../Common/common.module';
import { UsersComponent } from './users.component';
import { UsersRoutingModule } from './users-routing.module';
import { UserViewComponent } from './user-view/user-view.component';
import { UserComponent } from './user/user.component';
import { UserModalComponent } from './user-modal/user-modal.component';

@NgModule({
  declarations: [
    UserComponent,
    UserViewComponent,
    UsersComponent,
    UserModalComponent,
  ],
  imports: [
    CommonModule,
    CommonModuler,
    UsersRoutingModule,
    NgMultiSelectDropDownModule,
    ReactiveFormsModule,
    FontAwesomeModule,
  ],
})
export class UsersModule {}
