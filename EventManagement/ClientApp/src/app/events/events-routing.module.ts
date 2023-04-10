import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsComponent } from './events.component';
import { EventViewComponent } from './event-view/event-view.component';

const routes: Routes = [
  { path: '', component: EventsComponent },
  { path: ':id', component: EventViewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EventsRoutingModule {}
