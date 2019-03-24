import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from 'app/models/user';
import { environment } from 'environments/environment'

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  constructor(private http: HttpClient) {

  }
  getToken(username: string, password: string): Observable<User> {
    return this.http.post<User>(`${environment.apiUrl}/login`, {
      username : username,
      password : password
    });
  }
}
