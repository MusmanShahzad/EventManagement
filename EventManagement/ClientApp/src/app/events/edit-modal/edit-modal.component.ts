import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import {
  EventsService,
  IEvents,
  User,
  UserEventInput,
} from '../events.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UsersService } from 'src/app/users/users.service';
import * as moment from 'moment';
import { convertDateToNgDate } from 'src/app/Common/helper/util';
import { faCalendarDay } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.css'],
})
export class EditModalComponent implements OnInit {
  public isDataLoading = false;
  public isSaving = false;
  public id: string = '';
  public singleEvent: IEvents | null = null;
  public formGroup: FormGroup;
  public calenderIcon = faCalendarDay;
  public dropdownList: any[] = [];
  public selectedItems: any[] = [];
  public dropdownSettings: IDropdownSettings = {
    singleSelection: false,
    idField: 'id',
    textField: 'name',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    allowSearchFilter: true,
  };

  public get minDate(): NgbDate {
    return convertDateToNgDate(moment().format('YYYY-MM-DD'));
  }

  public get name(): FormControl {
    return this.formGroup.get('name') as FormControl;
  }

  public get startDate(): FormControl {
    return this.formGroup.get('startDate') as FormControl;
  }

  public get userIds(): FormControl {
    return this.formGroup.get('userIds') as FormControl;
  }

  constructor(
    private modalService: NgbActiveModal,
    private eventsService: EventsService,
    private usersService: UsersService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
    this.formGroup = this.fb.group({
      name: [null, [Validators.required, Validators.maxLength(250)]],
      startDate: [null, [Validators.required]],
      userIds: [null, [Validators.required, Validators.minLength(2)]],
    });
  }

  ngOnInit(): void {
    this.usersService.getUsers().subscribe((users: User[]) => {
      this.dropdownList = users.map((user) => ({
        id: user.id,
        name: user.name,
      }));
    });
    if (this.id && !this.singleEvent) {
      this.eventsService
        .getEventById({ id: this.id })
        .subscribe((event: IEvents) => {
          this.populateForm(event);
        });
    } else {
      this.singleEvent && this.populateForm(this.singleEvent);
    }
  }

  private populateForm(event: IEvents): void {
    this.formGroup.setValue({
      name: event.name,
      startDate: convertDateToNgDate(event.startDate),
      userIds: event.users.map((user: User) => ({
        id: user.id,
        name: user.email,
      })),
    });
  }

  public dismiss(): void {
    this.modalService.dismiss();
  }

  public saveEvent() {
    const userInput: UserEventInput = {
      Id: this.id,
      name: this.name.value,
      startDate: moment(
        `${this.startDate.value.year}-${this.startDate.value.month}-${this.startDate.value.day}`
      ).format('YYYY-MM-DD'),
      userIds: this.formGroup.value.userIds.map((res: any) => res.id),
    };
    if (this.id) {
      this.eventsService.updateEvent(userInput).subscribe((res: User) => {
        if (res) {
          this.toastr.success('event updated successfully', 'Success');
          this.modalService.close(true);
        }
      });
    } else {
      this.eventsService.addEvent(userInput).subscribe((res: User) => {
        if (res) {
          this.toastr.success('event added successfully', 'Success');
          this.modalService.close(true);
        }
      });
    }
  }
}
