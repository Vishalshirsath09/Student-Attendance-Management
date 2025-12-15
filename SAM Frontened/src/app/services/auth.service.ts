import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5263/api/User';

  constructor(private http: HttpClient) {}

  // -------------------------------
  // FIXED METHODS (required)
  // -------------------------------

  getUser() {
    const userJson = localStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;  // <-- No more error
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('user'); // <-- true if user exists
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
  }

  // -------------------------------
  // EXISTING METHODS
  // -------------------------------

  login(credentials: { username: string; password: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/Login`, credentials);
  }

  storeToken(token: string) {
    localStorage.setItem('token', token);
  }

  storeUser(user: any) {
    localStorage.setItem('user', JSON.stringify(user));
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
