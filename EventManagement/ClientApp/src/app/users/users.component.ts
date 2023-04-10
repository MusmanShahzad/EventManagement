import { Component } from '@angular/core';
import { UsersService } from './users.service';
import { User } from '../events/events.service';
import { finalize } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserModalComponent } from './user-modal/user-modal.component';

@Component({
  selector: 'app-user-component',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent {
  public isLoading = false;
  public users: User[] | null = null;
  constructor(
    private usersService: UsersService,
    private modalService: NgbModal
  ) {}

  openModal() {
    const modalRef = this.modalService.open(UserModalComponent);
    modalRef.result.then(
      (result) => {
        if (result) {
          this.fetchUsers();
        }
      },
      (resolve) => {}
    );
  }
  ngOnInit(): void {
    this.fetchUsers();
  }
  public fetchUsers(): void {
    this.isLoading = true;
    this.usersService
      .getUsers()
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe((users: User[]) => {
        this.users = users;
      });
  }
}
