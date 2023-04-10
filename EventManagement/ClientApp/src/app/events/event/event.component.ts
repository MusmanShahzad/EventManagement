import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { IEvents, User } from '../events.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditModalComponent } from '../edit-modal/edit-modal.component';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss'],
})
export class EventComponent implements OnInit {
  @Input() public singleEvent: IEvents | null = null;
  @Output() public doReload: EventEmitter<boolean> = new EventEmitter();

  public get userCount(): number {
    return this.singleEvent?.totalUsers
      ? this.singleEvent?.totalUsers - this.singleEvent?.users.length
      : 0;
  }
  constructor(private route: Router, private modalService: NgbModal) {}

  ngOnInit(): void {}

  openModal() {
    const modalRef = this.modalService.open(EditModalComponent);
    modalRef.componentInstance.id = this.singleEvent?.id;
    modalRef.result.then(
      (result) => {
        if (result) {
          this.doReload.emit(true);
        }
      },
      (resolve) => {}
    );
  }

  public editClick(event: MouseEvent) {
    event.stopPropagation();
    this.openModal();
  }

  public mapUsers(users: User[] | null | undefined): string {
    return users ? users.map((x) => x.name).join(',') : '';
  }

  public cardClick() {
    this.route.navigate(['./events', this.singleEvent?.id]);
  }
}
