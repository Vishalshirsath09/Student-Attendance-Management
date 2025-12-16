import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AttendanceService } from '../../services/attendance';
import { Attendance } from '../../models/attendance';

@Component({
  selector: 'app-attendance',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  attendances: Attendance[] = [];
  error = '';

  /** ROLE FLAGS */
  isTeacher = false;
  isAdmin = false;

  constructor(private attendanceService: AttendanceService) {}

  ngOnInit(): void {
    this.setUserRole();
    this.loadAttendances();
  }

  /** Get role from JWT */
  setUserRole() {
    const token = localStorage.getItem('token');
    if (!token) return;

    const payload = JSON.parse(atob(token.split('.')[1]));
    const role =
      payload['role'] ||
      payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    this.isTeacher = role === 'Teacher';
    this.isAdmin = role === 'Admin' || role === 'HOD';
  }

  loadAttendances() {
    this.attendanceService.getAttendances().subscribe({
      next: res => this.attendances = res,
      error: () => this.error = 'Failed to load attendance'
    });
  }

  /** ONLY TEACHER CAN CHANGE */
  markAttendance(att: Attendance, isPresent: boolean) {
    if (!this.isTeacher) return;

    const payload = {
      attendanceId: att.attendanceId,
      studentId: att.studentId,
      subjectId: att.subjectId,
      teacherId: att.teacherId,
      isPresent: isPresent,
      attendanceDate: att.attendanceDate
    };

    this.attendanceService.addOrUpdateAttendance(payload).subscribe({
      next: () => att.isPresent = isPresent,
      error: () => alert('Unable to update attendance')
    });
  }
}
