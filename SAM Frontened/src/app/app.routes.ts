import { Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { Dashboard } from './components/dashboard/dashboard';
import { StudentComponent } from './components/student/student.component';
import { Teacher } from './components/teacher/teacher';
import { Subject } from './components/subject/subject';
import { Attendance } from './components/attendance/attendance';
import { RegistrationComponent } from './components/admin/register/register.component';




export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },

  { path: 'dashboard', component: Dashboard },
  { path: 'student', component: StudentComponent },
  { path: 'teacher', component: Teacher },
  { path: 'subject', component: Subject },
  { path: 'attendance', component: Attendance },
  { path: 'register', component: RegistrationComponent },
  
];
