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

  // GET ALL STUDENTS
  getAllStudents(): Observable<Student[]> {
    return this.http.get<{ statusCode: number; message: string; data: Student[] }>(
      `${this.baseUrl}/GetAllStudents`
    ).pipe(
      map(res => res.data)
    );
  }

  // ADD OR UPDATE STUDENT
  addOrUpdateStudent(student: Student, isNew: boolean): Observable<any> {
    // For both creation and update, include userId if it exists
    const payload = { ...student };

    return this.http.post<any>(
      `${this.baseUrl}/AddOrUpdate`,
      payload
    );
  }

  // DELETE STUDENT (Soft Delete)
  deleteStudent(studentId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/Delete/${studentId}`);
  }
}
