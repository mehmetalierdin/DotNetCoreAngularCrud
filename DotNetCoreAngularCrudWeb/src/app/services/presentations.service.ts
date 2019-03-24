import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Presentations } from 'app/models/presentations';
import { environment } from 'environments/environment'

@Injectable({
  providedIn: 'root'
})

export class PresentationsService {
  constructor(private http: HttpClient) {

  }
  getData(title?: string): Observable<Presentations[]> {
    let token = localStorage.getItem('TOKEN');
    let param = title ? `${title}` : '';
    return this.http.get<Presentations[]>(`${environment.apiUrl}/data/${param}`, {
        headers: {
            "Authorization": `Bearer ${token}`
        }
      });
  }
}
