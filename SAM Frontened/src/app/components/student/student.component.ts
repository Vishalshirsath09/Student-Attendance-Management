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

  // FILTER
  filterType: 'name' | 'class' | 'div' | '' = '';
  filterValue = '';

  // OPTIONS
  nameList: string[] = [];
  classList: string[] = [];
  divList: string[] = [];

  // EDIT
  isEditModalOpen = false;
  selectedStudent: Student | null = null;

  constructor(private studentService: StudentService) {}

  ngOnInit() {
    this.loadStudents();
  }

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

  /** SEARCH */
  applyFilter() {
    if (!this.filterType || !this.filterValue) {
      this.filteredStudents = this.students;
      return;
    }

    this.filteredStudents = this.students.filter(s => {
      if (this.filterType === 'name') {
        return s.studentName === this.filterValue;
      }
      if (this.filterType === 'class') {
        return s.class === this.filterValue;
      }
      if (this.filterType === 'div') {
        return s.div === this.filterValue;
      }
      return true;
    });
  }

  clearFilter() {
    this.filterType = '';
    this.filterValue = '';
    this.filteredStudents = this.students;
  }

  /** EDIT */
  openEdit(student: Student) {
    this.selectedStudent = { ...student };
    this.isEditModalOpen = true;
  }

  closeEdit() {
    this.isEditModalOpen = false;
    this.selectedStudent = null;
  }

  saveEdit() {
    if (!this.selectedStudent) return;

    this.studentService.addOrUpdateUser(this.selectedStudent).subscribe({
      next: () => {
        alert('Student updated');
        this.closeEdit();
        this.loadStudents();
      }
    });
  }

  /** STATUS */
  toggleStatus(student: Student) {
    const updated = { ...student, isActive: !student.isActive };

    this.studentService.addOrUpdateUser(updated).subscribe({
      next: () => this.loadStudents()
    });
  }
}
