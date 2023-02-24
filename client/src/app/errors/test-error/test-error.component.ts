import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl = "https://localhost:5002/api/error/"
  
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  badReqGet() {
    this.http.get(this.baseUrl + 'badrequest').subscribe();
  }

  badReqPost() {
    this.http.post(this.baseUrl + 'badrequest', {}).subscribe();
  }

  auth() {
    this.http.get(this.baseUrl + 'auth').subscribe();
  }

  forbidden() {
    this.http.get(this.baseUrl + 'forbidden').subscribe();
  }

  notfound() {
    this.http.get(this.baseUrl + 'notfound').subscribe();
  }

  server() {
    this.http.get(this.baseUrl + 'server').subscribe();
  }
}
