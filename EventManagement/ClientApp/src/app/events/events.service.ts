import { Injectable } from '@angular/core';
import {
  HttpRequestService,
  QueryParams,
} from '../Common/Services/HttpRequest.service';
import { Observable } from 'rxjs';
import { Params } from '@angular/router';
export interface User {
  id: string;
  name: string;
  email: string;
  event: IEvents[];
  totalEvent?: number;
  allergies: Allergy[];
  totalAllergies?: number;
}

export interface IEvents {
  id: string;
  name: string;
  startDate: string;
  createdOn: Date;
  lastUpdatedOn: Date;
  users: User[];
  totalUsers?: number;
}

export interface Allergy {
  id: string;
  name: string;
}

export interface UserEventInput {
  Id: string;
  name: string;
  startDate: string;
  userIds: string[];
}

const EVENT_URL = 'api/event';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  constructor(private httpRequestService: HttpRequestService) {}

  public getEvents(): Observable<IEvents[]> {
    return this.httpRequestService.get(EVENT_URL);
  }

  public getEventById(params: QueryParams): Observable<IEvents> {
    return this.httpRequestService.get(EVENT_URL, params);
  }

  public updateEvent(data: UserEventInput): Observable<User> {
    return this.httpRequestService.put(EVENT_URL, data);
  }

  public addEvent(data: UserEventInput): Observable<User> {
    return this.httpRequestService.post(EVENT_URL, data);
  }

  public deleteEvent(Params: QueryParams): Observable<boolean> {
    return this.httpRequestService.delete(EVENT_URL, Params);
  }
}
