import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Subject } from '../models/subject';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private baseUrl = 'http://localhost:5263/api/Subject';

  constructor(private http: HttpClient) {}

  /** GET ALL SUBJECTS (using filter API with no params) */
  getAllSubjects(): Observable<Subject[]> {
    return this.http
      .get<{ statusCode: number; message: string; data: Subject[] }>(
        `${this.baseUrl}/filter`
      )
      .pipe(
        map(res => res.data)
      );
  }

  /** FILTER SUBJECTS */
  getSubjectsByFilter(subjectName?: string, subjectCode?: string): Observable<Subject[]> {
    let params = new HttpParams();

    if (subjectName) params = params.set('subjectName', subjectName);
    if (subjectCode) params = params.set('subjectCode', subjectCode);

    return this.http
      .get<{ statusCode: number; message: string; data: Subject[] }>(
        `${this.baseUrl}/filter`,
        { params }
      )
      .pipe(
        map(res => res.data)
      );
  }

  /** ADD OR UPDATE SUBJECT */
  addOrUpdateSubject(payload: Subject): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/AddOrUpdate`, payload);
  }

  /** DELETE SUBJECT */
  deleteSubject(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
}
