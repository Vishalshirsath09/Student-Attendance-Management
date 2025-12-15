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

  constructor(private userService: StudentService) {}

  user: User = {
    username: '',
    password: '',
    fullName: '',
    role: '' as 'Student' | 'Teacher'
  };

  student: Student = {
    studentName: '',
    rollno: 0,
    class: '',
    div: '',
    phoneNo: '',
    address: '',
    emailAddress: ''
  };

  teacher: Teacher = {
    teacherName: '',
    phoneNo: '',
    emailAddress: '',
    address: '',
    qualification:'',
    experience: 0
  };

  register() {

    let payload: any;

    if (this.user.role === 'Student') {
      payload = {
        username: this.user.username,
        password: this.user.password,
        role: this.user.role,
        fullName: this.user.fullName,

        // FLAT JSON FOR STUDENT
        studentName: this.student.studentName,
        rollno: this.student.rollno,
        class: this.student.class,
        div: this.student.div,
        phoneNo: this.student.phoneNo,
        emailAddress: this.student.emailAddress,
        address: this.student.address
      };
    } 

    else if (this.user.role === 'Teacher') {
      payload = {
        username: this.user.username,
        password: this.user.password,
        role: this.user.role,
        fullName: this.user.fullName,

        // FLAT JSON FOR TEACHER
        teacherName: this.teacher.teacherName,
        phoneNo: this.teacher.phoneNo,
        emailAddress: this.teacher.emailAddress,
        address: this.teacher.address,
        qulification:this.teacher.qualification,
        experience: this.teacher.experience,

      };
    } 
    
    else {
      alert('Please select a role');
      return;
    }

    console.log("Payload sent:", payload);

  this.userService.addOrUpdateUser(payload).subscribe({
  next: (response: any) => {
    alert("Registration Successful");
    console.log("API Response:", response);
  },
  error: (error: any) => {
    alert("Registration Failed");
    console.error(error);
  }
});

  }
}
