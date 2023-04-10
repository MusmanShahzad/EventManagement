import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsComponent } from './events.component';
import { EventsRoutingModule } from './events-routing.module';
import { EventViewComponent } from './event-view/event-view.component';
import { EventComponent } from './event/event.component';
import { CommonModuler } from '../Common/common.module';
import { EditModalComponent } from './edit-modal/edit-modal.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    EventsComponent,
    EventViewComponent,
    EventComponent,
    EditModalComponent,
  ],
  imports: [
    CommonModule,
    CommonModuler,
    EventsRoutingModule,
    NgbDatepickerModule,
    NgMultiSelectDropDownModule,
    ReactiveFormsModule,
    FontAwesomeModule,
  ],
})
export class EventsModule {}
