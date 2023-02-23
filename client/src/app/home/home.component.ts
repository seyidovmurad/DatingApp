import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isRegister = false;

  constructor() { }

  ngOnInit(): void {
  }

  toggleRegister() {
    this.isRegister = !this.isRegister;
    
  }

  cancel(evet: boolean) {
    this.isRegister = evet;
  }
}
