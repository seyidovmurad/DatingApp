import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}
  loggedIn = false;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => this.loggedIn = !!user,
      error: err => console.log(err)
    });
  }

  login(): void {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: err => console.log(err)
    })
  }

  logOut() {
    this.accountService.logout();
  }
}
