import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private router: Router) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        const err: BaseResponse = error.error as BaseResponse;

        if (error instanceof HttpErrorResponse) {
          if (error.status === 422) {
            this.router.navigate(['']);
            return throwError(err.error?.message);
          }

          if (error.status === 429) {
            return throwError(error.error);
          }
          if (error.status === 415) {
            return throwError(error.error);
          }

          this.router.navigate(['']);
          return throwError(err.error?.message);
        }

        const applicationError = error.headers.get('Application-Error');
        if (applicationError) {
          this.router.navigate(['']);
          return throwError(applicationError);
        }

        this.router.navigate(['']);
        return throwError('Application is currently unavailable. Try later');
      })
    );
  }
}

export const InterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: InterceptorService,
  multi: true
};
