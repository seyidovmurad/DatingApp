import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  requestCount = 0;

  constructor(private spinner: NgxSpinnerService) { }

  busy() {
    this.requestCount++;
    this.spinner.show(undefined, {
      type: 'square-spin',
      bdColor: 'rgba(0, 0, 0, .7)',
      color: '#fff'
    })
  }

  idle() {
    this.requestCount--;
    if(this.requestCount <= 0) {
      this.requestCount=0;
      this.spinner.hide();
    }
  }
}
