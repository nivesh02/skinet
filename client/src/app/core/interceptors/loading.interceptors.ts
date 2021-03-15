import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../services/busy.service';

@Injectable()
export class Loadingintercepter implements HttpInterceptor {
    constructor(private busyservices: BusyService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.busyservices.busy();
        return next.handle(req).pipe(
            delay(1000),
            finalize(() => {
                this.busyservices.idle();
            })
        );
    }
}
