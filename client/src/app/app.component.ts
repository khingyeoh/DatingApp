import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { setTheme } from 'ngx-bootstrap/utils';
 
   
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'The Dating App';
  users : any;


  //added
  constructor(private http: HttpClient) {
    setTheme('bs3'); // or 'bs4'
  }


  ngOnInit() {
   this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/user/').subscribe(response=> {
      this.users = response;
    })
  }
}