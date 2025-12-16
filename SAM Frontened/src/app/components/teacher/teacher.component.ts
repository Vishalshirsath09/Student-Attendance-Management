import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TeacherService } from '../../services/teacher.service';
import { Teacher } from '../../models/teacher';

@Component({
  selector: 'app-teacher',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent implements OnInit {

  teachers: Teacher[] = [];
  filteredTeachers: Teacher[] = [];
  error = '';

  // FILTER
  filterType: 'name' | 'qualification' | '' = '';
  filterValue = '';

  // DROPDOWN OPTIONS
  nameList: string[] = [];
  qualificationList: string[] = [];

  // EDIT MODAL
  isEditModalOpen = false;
  selectedTeacher: Teacher | null = null;

  constructor(private teacherService: TeacherService) {}

  ngOnInit() {
    this.loadTeachers();
  }

  loadTeachers() {
    this.teacherService.getAllTeachers().subscribe({
      next: (res) => {
        this.teachers = res;
        this.filteredTeachers = res;

        this.nameList = [...new Set(res.map(t => t.teacherName))];
        this.qualificationList = [...new Set(res.map(t => t.qualification))];
      },
      error: () => this.error = 'Failed to load teachers'
    });
  }

  /** SEARCH */
  applyFilter() {
    if (!this.filterType || !this.filterValue) {
      this.filteredTeachers = this.teachers;
      return;
    }

    this.filteredTeachers = this.teachers.filter(t => {
      if (this.filterType === 'name') {
        return t.teacherName === this.filterValue;
      }
      if (this.filterType === 'qualification') {
        return t.qualification === this.filterValue;
      }
      return true;
    });
  }

  clearFilter() {
    this.filterType = '';
    this.filterValue = '';
    this.filteredTeachers = this.teachers;
  }

  /** EDIT */
  openEdit(teacher: Teacher) {
    this.selectedTeacher = { ...teacher };
    this.isEditModalOpen = true;
  }

  closeEdit() {
    this.isEditModalOpen = false;
    this.selectedTeacher = null;
  }

  saveEdit() {
    if (!this.selectedTeacher) return;

    this.teacherService.addOrUpdateTeacher(this.selectedTeacher).subscribe({
      next: () => {
        alert('Teacher updated successfully');
        this.closeEdit();
        this.loadTeachers();
      }
    });
  }

  /** STATUS */
  toggleStatus(teacher: Teacher) {
    const updated = { ...teacher, isActive: !teacher.isActive };

    this.teacherService.addOrUpdateTeacher(updated).subscribe({
      next: () => this.loadTeachers()
    });
  }
}
