import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Student } from '../models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private baseUrl = 'http://localhost:5263/api/Student';

  constructor(private http: HttpClient) {}

  // GET all students (extract data from the response wrapper)
  getAllStudents(): Observable<Student[]> {
    return this.http.get<{ statusCode: number; message: string; data: Student[] }>(
      `${this.baseUrl}/GetAllStudents`
    ).pipe(
      map(res => res.data) // <-- extract the array of students
    );
  }

  // CREATE or UPDATE student/user
  addOrUpdateUser(payload: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/AddOrUpdate`, payload);
  }
}
