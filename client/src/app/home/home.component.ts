import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // users: any;

  constructor(private http: HttpClient,) { }

  ngOnInit(): void {
    // this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;;
  }

    // get from api pass to usrs, html will pass the users to @input then child component can use it
    // getUsers() {
    // this.http.get('https://localhost:5001/api/user/')
    //   .subscribe(user =>  this.users = user);
    
    // }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}
