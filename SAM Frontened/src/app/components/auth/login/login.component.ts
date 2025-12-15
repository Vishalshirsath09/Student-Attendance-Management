import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';   // <-- ADD THIS
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule      // <-- REQUIRED FOR *ngIf, *ngFor, etc.
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  login = { username: '', password: '' };
  error: string = '';

  constructor(private auth: AuthService, private router: Router) {}

  onLogin() {
  this.auth.login(this.login).subscribe({
    next: (res: any) => {

      this.auth.storeToken(res.data.token);
      this.auth.storeUser(res.data.user); // <-- IMPORTANT

      const role = res.data.user.role;

      if (role === 'Admin' || role === 'HOD') {
        this.router.navigate(['/dashboard']);
      } else if (role === 'Teacher') {
        this.router.navigate(['/teacher']);
      } else if (role === 'Student') {
        this.router.navigate(['/student']);
      }
    },
    error: () => this.error = "Invalid username or password"
  });
}
}
