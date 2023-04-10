import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { Params } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from '../users.service';
import { User } from 'src/app/events/events.service';
import { UserModalComponent } from '../user-modal/user-modal.component';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.scss'],
})
export class UserViewComponent implements OnInit {
  public isLoading: boolean = false;
  public singleUser: User | null = null;
  constructor(
    private userService: UsersService,
    private router: ActivatedRoute,
    private route: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.router.params.subscribe((ele: Params) => {
      this.getUser(ele.id);
    });
  }

  private getUser(id: string): void {
    this.userService
      .getUserById({ id })
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe((singleUser: User) => {
        this.singleUser = singleUser;
      });
  }
  public openModal(): void {
    const modalRef = this.modalService.open(UserModalComponent);
    modalRef.componentInstance.id = this.singleUser?.id;
    modalRef.componentInstance.singleUser = this.singleUser;
    modalRef.result.then(
      (result) => {
        if (result) {
          this.singleUser && this.getUser(this.singleUser.id);
        }
      },
      (resolve) => {}
    );
  }

  public deleteEvent() {
    this.userService
      .deleteUser({
        id: this.singleUser?.id ? this.singleUser.id : '',
      })
      .subscribe((ele: boolean) => {
        if (ele) this.route.navigate(['./users']);
      });
  }

  public eventClick(id: string) {
    this.route.navigate(['./events', id]);
  }
}
