import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'app/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  username = '';
  password = '';
  constructor(private login: LoginService, private router: Router) {
  }
  
  Login() {
    this.login.getToken(
        this.username,
        this.password
      )
      .subscribe(
        r => {
          if (r.token) {
            localStorage.setItem('TOKEN', r.token)
            this.router.navigateByUrl('/presentations');
          }
        },
        r => {
          alert(r.error.error);
        });
  }
}
