import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UserInput, UsersService } from 'src/app/users/users.service';
import * as moment from 'moment';
import { convertDateToNgDate } from 'src/app/Common/helper/util';
import { EventsService, IEvents, User } from 'src/app/events/events.service';
import { Allergy } from './../../events/events.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.css'],
})
export class UserModalComponent implements OnInit {
  public isDataLoading = false;
  public isSaving = false;
  public id: string = '';
  public singleUser: User | null = null;
  public formGroup: FormGroup;
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

  public get allergyNameController(): FormControl {
    return new FormControl(null);
  }

  public get minDate(): NgbDate {
    return convertDateToNgDate(moment().format('YYYY-MM-DD'));
  }

  public get name(): FormControl {
    return this.formGroup.get('name') as FormControl;
  }

  public get email(): FormControl {
    return this.formGroup.get('email') as FormControl;
  }

  public get eventIds(): FormControl {
    return this.formGroup.get('eventIds') as FormControl;
  }
  public get allergies(): FormArray {
    return this.formGroup.get('allergies') as FormArray;
  }

  constructor(
    private modalService: NgbActiveModal,
    private usersService: UsersService,
    private eventService: EventsService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
    this.formGroup = this.fb.group(
      {
        name: [null, [Validators.required, Validators.maxLength(250)]],
        email: [null, [Validators.required, Validators.email]],
        eventIds: [[]],
        allergies: this.fb.array([]),
      },
      { validators: this.allergyNameValidator }
    );
  }

  ngOnInit(): void {
    this.eventService.getEvents().subscribe((events: IEvents[]) => {
      this.dropdownList = events.map((event) => ({
        id: event.id,
        name: event.name,
      }));
    });
    if (this.id && !this.singleUser) {
      this.usersService
        .getUserById({ id: this.id })
        .subscribe((event: User) => {
          this.populateForm(event);
        });
    } else {
      this.singleUser && this.populateForm(this.singleUser);
    }
  }

  public addFormGroupToAllergies(name: string) {
    this.allergies.push(
      new FormGroup({
        name: new FormControl(name),
      })
    );
  }

  public getAllergy(index: number): FormGroup {
    return this.allergies.controls[index] as FormGroup;
  }

  private populateForm(user: User): void {
    this.formGroup.setValue({
      name: user.name,
      email: user.email,
      eventIds: user.event.map((event: IEvents) => ({
        id: event.id,
        name: event.name,
      })),
      allergies: [],
    });
    user.allergies.forEach((allergy: Allergy) => {
      this.addFormGroupToAllergies(allergy.name);
    });
  }

  public dismiss(): void {
    this.modalService.dismiss();
  }

  public saveEvent() {
    const userInput: UserInput = {
      id: this.id,
      name: this.name.value,
      email: this.email.value,
      eventIds: this.eventIds.value.map((res: any) => res.id),
      allergies: this.allergies.value.map((res: any) => res.name),
    };
    if (this.id) {
      this.usersService.updateUser(userInput).subscribe((res: User) => {
        if (res) {
          this.toastr.success('User updated successfully', 'Success');
          this.modalService.close(true);
        }
      });
    } else {
      this.usersService.addUser(userInput).subscribe((res: User) => {
        if (res) {
          this.toastr.success('User added successfully', 'Success');
          this.modalService.close(true);
        }
      });
    }
  }

  public removeAllergy(index: number): void {
    this.allergies.removeAt(index);
  }

  allergyNameValidator(form: FormGroup) {
    const allergyNames = form.value.allergies.map((ele: any) => ele.name);
    return allergyNames.some(
      (name: string, index: number) =>
        allergyNames.indexOf(name) !== index || name.length == 0
    )
      ? { allergyNameNotUnique: true }
      : null;
  }
}
