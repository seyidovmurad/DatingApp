import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
  error: any;
  constructor(router: Router) {
     const nav = router.getCurrentNavigation();
     this.error = nav?.extras?.state?.['error'];

     if(!this.error) 
      router.navigateByUrl('/not-found');
   }

  ngOnInit(): void {
  }

}
