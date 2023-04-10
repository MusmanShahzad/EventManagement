import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getBaseUrl } from 'src/main';
export interface QueryParams {
  [key: string]: string | number | boolean;
}
@Injectable({
  providedIn: 'root',
})
export class HttpRequestService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}
  get(endpoint: string, params?: QueryParams): Observable<any> {
    const options = params ? { params } : {};
    return this.http.get<Object>(`${this.baseUrl}${endpoint}`, options);
  }

  post(endpoint: string, data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}${endpoint}`, data);
  }

  put(endpoint: string, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}${endpoint}`, data);
  }

  delete(endpoint: string, params?: QueryParams): Observable<any> {
    const options = params ? { params } : {};
    return this.http.delete(`${this.baseUrl}${endpoint}`, options);
  }
}
