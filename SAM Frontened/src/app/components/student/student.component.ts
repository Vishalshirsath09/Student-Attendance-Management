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
  filteredStudents: Student[] = [];
  error = '';

  filterType: 'name' | 'class' | 'div' | '' = '';
  filterValue = '';

  nameList: string[] = [];
  classList: string[] = [];
  divList: string[] = [];

  isEditModalOpen = false;
  selectedStudent: Student | null = null;

  constructor(private studentService: StudentService) {}

  ngOnInit() {
    this.loadStudents();
  }

  // LOAD ALL STUDENTS
  loadStudents() {
    this.studentService.getAllStudents().subscribe({
      next: (res) => {
        this.students = res;
        this.filteredStudents = res;

        this.nameList = [...new Set(res.map(s => s.studentName))];
        this.classList = [...new Set(res.map(s => s.class))];
        this.divList = [...new Set(res.map(s => s.div))];
      },
      error: () => this.error = 'Failed to load students'
    });
  }

  // FILTER STUDENTS
  applyFilter() {
    if (!this.filterType || !this.filterValue) {
      this.filteredStudents = this.students;
      return;
    }

    this.filteredStudents = this.students.filter(s => {
      if (this.filterType === 'name') return s.studentName === this.filterValue;
      if (this.filterType === 'class') return s.class === this.filterValue;
      if (this.filterType === 'div') return s.div === this.filterValue;
      return true;
    });
  }

  clearFilter() {
    this.filterType = '';
    this.filterValue = '';
    this.filteredStudents = this.students;
  }

  // OPEN EDIT MODAL
  openEdit(student: Student) {
    this.selectedStudent = { ...student }; // copy student
    this.isEditModalOpen = true;
  }

  closeEdit() {
    this.isEditModalOpen = false;
    this.selectedStudent = null;
  }

  // SAVE EDITED STUDENT
  saveEdit() {
    if (!this.selectedStudent) return;

    // REQUIRED FIELDS CHECK
    if (
      !this.selectedStudent.studentName ||
      !this.selectedStudent.rollno ||
      !this.selectedStudent.class ||
      !this.selectedStudent.div
    ) {
      alert('Please fill all required fields');
      return;
    }

    // SEND PAYLOAD (keep userId)
    this.studentService.addOrUpdateStudent(this.selectedStudent, false).subscribe({
      next: () => {
        alert('Student updated successfully');
        this.closeEdit();
        this.loadStudents();
      },
      error: err => {
        console.error('Error while updating student:', err);
        alert('Update failed. Make sure UserId exists.');
      }
    });
  }

  // TOGGLE ACTIVE/INACTIVE STATUS
  toggleStatus(student: Student) {
    if (!student.userId) {
      alert('faild to update');
      return;
    }

    const updatedStudent: Student = {
      ...student,
      isActive: !student.isActive
    };

    this.studentService.addOrUpdateStudent(updatedStudent, false).subscribe({
      next: () => this.loadStudents(),
      error: err => console.error('Error toggling status:', err)
    });
  }

  // SOFT DELETE STUDENT
  deleteStudent(studentId: number) {
    this.studentService.deleteStudent(studentId).subscribe({
      next: () => this.loadStudents(),
      error: err => console.error('Error deleting student:', err)
    });
  }
}
