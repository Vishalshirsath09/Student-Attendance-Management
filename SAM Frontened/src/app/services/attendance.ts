import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Attendance } from '../models/attendance';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {
  private baseUrl = 'http://localhost:5263/api/Attendance';

  constructor(private http: HttpClient) {}

  /** GET ATTENDANCE */
  getAttendances(studentName?: string, subjectName?: string, teacherName?: string, date?: string): Observable<Attendance[]> {
    let params = new HttpParams();
    if (studentName) params = params.set('studentName', studentName);
    if (subjectName) params = params.set('subjectName', subjectName);
    if (teacherName) params = params.set('teacherName', teacherName);
    if (date) params = params.set('date', date);

    return this.http.get<{statusCode:number, message:string, data:Attendance[]}>(`${this.baseUrl}/filter`, { params })
      .pipe(map(res => res.data));
  }

  /** ADD OR UPDATE ATTENDANCE */
  addOrUpdateAttendance(payload: Attendance): Observable<any> {
    return this.http.post(`${this.baseUrl}/AddOrUpdate`, payload);
  }
}
