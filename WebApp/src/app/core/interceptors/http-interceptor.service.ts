import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';

@Injectable()

export class HttpInterceptorService implements HttpInterceptor {

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let request = req;
    const token = localStorage.getItem("token");

    if (!token) {
      request = req.clone({
        headers: req.headers.set("Authorization",
          "Bearer " + token)
      });
    }
      return next.handle(request).pipe(
        retry(0),
        // catchError((error: HttpErrorResponse) => {
        //     let errorMessage = '';
        //     if (error.error instanceof ErrorEvent) {
        //         // client-side error
        //         errorMessage = `Error: ${error.error.message}`;
        //     } else {
        //         // server-side error
        //         errorMessage = `Error Status: ${error.status}\nMessage: ${error.message}`;
        //     }
        //     console.log(errorMessage);
        //     return throwError(() => new Error(errorMessage));
        // })
    );
    }
}
