import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;
  constructor(private spinnerservices: NgxSpinnerService) { }
  busy() {
    this.busyRequestCount++;
    this.spinnerservices.show(undefined, {
      type: 'timer',
      bdColor: 'rgb(255,255,255,.7)',
      color: '#333333'
    });
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerservices.hide();
    }
  }
}
