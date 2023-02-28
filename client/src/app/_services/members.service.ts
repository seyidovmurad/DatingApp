import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.baseUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    if(this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map((members) => this.members = members)
    );
  }

  getMember(username: string) {
    const member = this.members.find(m => m.userName === username);
    return member ? of(member) : this.http.get<Member>(this.baseUrl + `users/${username}`);
  }

  updateMember(member: Member) {
    return this.http.post(this.baseUrl + 'users', member);
  }

  setMainPhoto(photoId: number) {
    return this.http.post(this.baseUrl + `users/set-main-photo/${photoId}`, {});
  }

  deletePhoto(photoId: number) {
    return this.http.get(this.baseUrl + `users/delete-photo/${photoId}`);
  }
}
