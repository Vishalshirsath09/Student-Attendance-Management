import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private baseUrl = 'http://localhost:5263/api/User';

  constructor(private http: HttpClient) { }

  addOrUpdateUser(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/AddOrUpdate`, data);
  }

}