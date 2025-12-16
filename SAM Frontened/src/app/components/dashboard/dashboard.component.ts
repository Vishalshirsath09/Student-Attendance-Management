import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { StudentService } from '../../services/student.service';
import { TeacherService } from '../../services/teacher.service';
import { SubjectService } from '../../services/subject.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  studentCount = 0;
  teacherCount = 0;
  subjectCount = 0;

  loading = true;

  constructor(
    private studentService: StudentService,
    private teacherService: TeacherService,
    private subjectService: SubjectService,
    private router: Router           // ✅ ADD THIS
  ) {}

  ngOnInit(): void {
    this.loadCounts();
  }

  loadCounts() {
    this.loading = true;

    this.studentService.getAllStudents().subscribe(res => {
      this.studentCount = res.length;
    });

    this.teacherService.getAllTeachers().subscribe(res => {
      this.teacherCount = res.length;
    });

    this.subjectService.getAllSubjects().subscribe(res => {
      this.subjectCount = res.length;
      this.loading = false;
    });
  }

  // ✅ NAVIGATION METHODS
  goToStudents() {
    this.router.navigate(['/student']);
  }

  goToTeachers() {
    this.router.navigate(['/teacher']);
  }

  goToSubjects() {
    this.router.navigate(['/subject']);
  }
}
