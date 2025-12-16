import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { User } from '../../../models/user';
import { Student } from '../../../models/student';
import { Teacher } from '../../../models/teacher';

import { StudentService } from '../../../services/student.service';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [CommonModule, FormsModule]
})
export class RegistrationComponent {

  constructor(private studentService: StudentService) {}

  user: User = {
    username: '',
    password: '',
    fullName: '',
    role: '' as 'Student' | 'Teacher'
  };

  student: Student = {
    studentId: 0,
    studentName: '',
    rollno: 0,
    class: '',
    div: '',
    phoneNo: '',
    address: '',
    emailAddress: '',
    isActive: true
  };

  teacher: Teacher = {
    teacherId: 0,
    teacherName: '',
    phoneNo: '',
    emailAddress: '',
    address: '',
    qualification: '',
    experience: 0,
    isActive: true
  };

  register() {

    if (this.user.role === 'Student') {

      const payload: Student = {
        studentId: 0, // New student
        studentName: this.student.studentName,
        rollno: this.student.rollno,
        class: this.student.class,
        div: this.student.div,
        phoneNo: this.student.phoneNo,
        emailAddress: this.student.emailAddress,
        address: this.student.address,
        isActive: true
      };

      // ✅ Pass `true` because this is a new student
      this.studentService.addOrUpdateStudent(payload, true).subscribe({
        next: (res: any) => {
          alert('Student registered successfully');
          console.log(res);
        },
        error: (err: any) => {
          alert('Student registration failed');
          console.error(err);
        }
      });

    } else if (this.user.role === 'Teacher') {
      alert('Teacher registration API not connected yet');
    } else {
      alert('Please select a role');
    }
  }
}
