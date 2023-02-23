import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  baseUrl = 'https://localhost:5002/api';
  private currentUser = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUser.asObservable();
  
  constructor(private http: HttpClient) { }

  login(model: User) {
    return this.http.post<User>(this.baseUrl + '/account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if(user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.next(user);
        }
      })
    );
  }

  register(model: User) {
    return this.http.post<User>(this.baseUrl + '/account/register', model).pipe(
      map((response: User) => {
        const user = response;
        if(user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUser.next(user);
  }

  logout() {
    localStorage.removeItem("user");
    this.currentUser.next(null);
  }
}
