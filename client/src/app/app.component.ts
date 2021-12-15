import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { setTheme } from 'ngx-bootstrap/utils';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
 
   
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'The Dating App';
  users : any;


  //added
  constructor(
    // private http: HttpClient,
    private accountService: AccountService
    ) {
    setTheme('bs3'); // or 'bs4'
  }


  ngOnInit() {
  //execute this when page launch
  // this.getUsers();
  this.setCurrentUser()
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }


  // getUsers() {
  //   this.http.get('https://localhost:5001/api/user/').subscribe(response=> {
  //     this.users = response;
  //   }, error =>{
  //     console.log(error);
  //   })
  // }
}