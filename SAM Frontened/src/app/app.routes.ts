import { Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { StudentComponent } from './components/student/student.component';
import { TeacherComponent } from './components/teacher/teacher.component';
import { SubjectComponent } from './components/subject/subject.component';
import { RegistrationComponent } from './components/admin/register/register.component';
import { AttendanceComponent } from './components/attendance/attendance.component';




export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },

  { path: 'dashboard', component: DashboardComponent },
  { path: 'student', component: StudentComponent },
  { path: 'teacher', component: TeacherComponent },
  { path: 'subject', component: SubjectComponent },
  { path: 'attendance', component: AttendanceComponent },
  { path: 'register', component: RegistrationComponent },
  
];
