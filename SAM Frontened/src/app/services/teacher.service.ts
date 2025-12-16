import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Teacher } from '../models/teacher';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private baseUrl = 'http://localhost:5263/api/Teacher';

  constructor(private http: HttpClient) {}

  /** GET ALL TEACHERS */
  getAllTeachers(): Observable<Teacher[]> {
    return this.http
      .get<any>(`${this.baseUrl}/GetAllTeachers`)
      .pipe(
        map(res => res.data)   // ✅ unwrap API response
      );
  }

  /** ADD OR UPDATE TEACHER */
  addOrUpdateTeacher(data: Teacher): Observable<any> {
    return this.http.post(`${this.baseUrl}/AddOrUpdate`, data);
  }
}
