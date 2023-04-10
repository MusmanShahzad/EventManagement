import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpRequestService } from '../Common/Services/HttpRequest.service';
import { EventsService, IEvents } from './events.service';
import { finalize } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditModalComponent } from './edit-modal/edit-modal.component';

@Component({
  selector: 'app-events-component',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
})
export class EventsComponent implements OnInit {
  public isLoading = false;
  public events: IEvents[] = [];
  constructor(
    private eventsService: EventsService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.fetchEvents();
  }

  public fetchEvents() {
    this.isLoading = true;
    this.eventsService
      .getEvents()
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe((events: IEvents[]) => {
        this.events = events;
      });
  }

  public openModal(): void {
    const modalRef = this.modalService.open(EditModalComponent);
    modalRef.result.then((result) => {
      if (result) {
        this.fetchEvents();
      }
    });
  }
}
