import { Injectable } from '@angular/core';
import {
  HttpRequestService,
  QueryParams,
} from '../Common/Services/HttpRequest.service';
import { Observable } from 'rxjs';
import { User } from '../events/events.service';
export interface UserInput {
  id: string;
  name: string;
  email: string;
  eventIds: string[];
  allergies: string[];
}

const USER_URL = 'api/user';
@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private httpRequestService: HttpRequestService) {}

  public getUsers(): Observable<User[]> {
    return this.httpRequestService.get('api/user');
  }

  public getUserById(params: QueryParams): Observable<User> {
    return this.httpRequestService.get(USER_URL, params);
  }

  public updateUser(data: UserInput): Observable<User> {
    return this.httpRequestService.put(USER_URL, data);
  }

  public addUser(data: UserInput): Observable<User> {
    return this.httpRequestService.post(USER_URL, data);
  }

  public deleteUser(Params: QueryParams): Observable<boolean> {
    return this.httpRequestService.delete(USER_URL, Params);
  }
}
