import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Allergy, IEvents, User } from 'src/app/events/events.service';
import { UserModalComponent } from '../user-modal/user-modal.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  @Input() public singleUser: User | null = null;
  @Output() public doReload: EventEmitter<boolean> = new EventEmitter();

  public get eventCount(): number {
    return this.singleUser?.totalEvent
      ? this.singleUser?.totalEvent - this.singleUser?.event.length
      : 0;
  }

  public get allergyCount(): number {
    return this.singleUser?.totalAllergies
      ? this.singleUser?.totalAllergies - this.singleUser?.allergies.length
      : 0;
  }
  constructor(private route: Router, private modalService: NgbModal) {}

  ngOnInit(): void {}

  openModal() {
    const modalRef = this.modalService.open(UserModalComponent);
    modalRef.componentInstance.id = this.singleUser?.id;
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

  public mapEvents(events: IEvents[] | null | undefined): string {
    return events ? events.map((x) => x.name).join(',') : '';
  }

  public mapAllergies(allergies: Allergy[] | null | undefined): string {
    return allergies ? allergies.map((x) => x.name).join(',') : '';
  }

  public cardClick() {
    this.route.navigate(['./users', this.singleUser?.id]);
  }
}
