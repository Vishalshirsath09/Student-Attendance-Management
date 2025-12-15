import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student.service';
import { Student } from '../../models/student';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  students: Student[] = [];
  error: string = '';

  // Modal controls
  isEditModalOpen: boolean = false;
  selectedStudent: Student | null = null;

  constructor(private studentService: StudentService) {}

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents() {
    this.studentService.getAllStudents().subscribe({
      next: (res) => this.students = res,
      error: (err) => {
        console.error(err);
        this.error = 'Failed to load students';
      }
    });
  }

  openEditModal(student: Student) {
    this.selectedStudent = { ...student }; // clone to avoid changing table directly
    this.isEditModalOpen = true;
  }

  closeEditModal() {
    this.isEditModalOpen = false;
    this.selectedStudent = null;
  }

  updateStudent() {
    if (!this.selectedStudent) return;

    this.studentService.addOrUpdateUser(this.selectedStudent).subscribe({
      next: () => {
        alert('Student updated successfully');
        this.closeEditModal();
        this.loadStudents(); // refresh table
      },
      error: (err) => {
        console.error(err);
        alert('Failed to update student');
      }
    });
  }
}
