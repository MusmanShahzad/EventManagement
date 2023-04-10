import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptorService implements HttpInterceptor {
  constructor(private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error instanceof ErrorEvent) {
          // Client-side error
          console.error('An error occurred:', error);
          this.toastr.error(
            'An error occurred: ' + error.error.message || error.message
          );
        } else {
          // Server-side error
          console.error(
            `Backend returned code ${error.status}, ` +
              `body was: ${error.error}`
          );
          if (error.status === 400 && error.error.errors) {
            // Validation error
            const validationErrors = error.error.errors;
            let message = '';
            for (const prop in validationErrors) {
              if (validationErrors.hasOwnProperty(prop)) {
                message += validationErrors[prop][0] + '<br>';
              }
            }
            this.toastr.error(message, 'Validation Error');
          } else {
            console.log(error);
            this.toastr.error(error.error.message, error.error.error);
          }
        }
        return throwError('Something bad happened; please try again later.');
      })
    );
  }
}
