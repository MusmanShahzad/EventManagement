import { Component, OnInit } from '@angular/core';
import { EventsService, IEvents } from '../events.service';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { Params } from '@angular/router';
import { EditModalComponent } from '../edit-modal/edit-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-event-view',
  templateUrl: './event-view.component.html',
  styleUrls: ['./event-view.component.scss'],
})
export class EventViewComponent implements OnInit {
  public isLoading: boolean = false;
  public singleEvent: IEvents | null = null;
  constructor(
    private eventService: EventsService,
    private router: ActivatedRoute,
    private route: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.router.params.subscribe((ele: Params) => {
      this.getEvent(ele.id);
    });
  }

  private getEvent(id: string): void {
    this.eventService
      .getEventById({ id })
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe((singleEvent: IEvents) => {
        this.singleEvent = singleEvent;
      });
  }
  public openModal(): void {
    const modalRef = this.modalService.open(EditModalComponent);
    modalRef.componentInstance.id = this.singleEvent?.id;
    modalRef.componentInstance.singleEvent = this.singleEvent;
    modalRef.result.then(
      (result) => {
        if (result) {
          this.singleEvent && this.getEvent(this.singleEvent.id);
        }
      },
      (resolve) => {}
    );
  }

  public deleteEvent() {
    this.eventService
      .deleteEvent({
        id: this.singleEvent?.id ? this.singleEvent.id : '',
      })
      .subscribe((ele: boolean) => {
        if (ele) this.route.navigate(['./events']);
      });
  }

  public userClick(id: string) {
    this.route.navigate(['./users', id]);
  }
}
