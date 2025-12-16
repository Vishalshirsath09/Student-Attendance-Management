import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SubjectService } from '../../services/subject.service';
import { Subject } from '../../models/subject';
import { TeacherService } from '../../services/teacher.service';
import { Teacher } from '../../models/teacher';

@Component({
  selector: 'app-subject',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {

  subjects: Subject[] = [];
  filteredSubjects: Subject[] = [];
  teachers: Teacher[] = [];
  error = '';

  filterType: 'name' | 'code' | '' = '';
  filterValue = '';

  nameList: string[] = [];
  codeList: string[] = [];

  isEditModalOpen = false;
  selectedSubject: Subject | null = null;

  constructor(
    private subjectService: SubjectService,
    private teacherService: TeacherService
  ) {}

  ngOnInit(): void {
    this.loadSubjects();
    this.loadTeachers();
  }

  loadSubjects() {
    this.subjectService.getAllSubjects().subscribe({
      next: (res) => {
        this.subjects = res;
        this.filteredSubjects = res;
        this.nameList = [...new Set(res.map(s => s.subjectName))];
        this.codeList = [...new Set(res.map(s => s.subjectCode))];
      },
      error: () => this.error = 'Failed to load subjects'
    });
  }

  loadTeachers() {
    this.teacherService.getAllTeachers().subscribe({
      next: (res) => this.teachers = res,
      error: () => console.error('Failed to load teachers')
    });
  }

  applyFilter() {
    if (!this.filterType || !this.filterValue) {
      this.filteredSubjects = this.subjects;
      return;
    }

    this.filteredSubjects = this.subjects.filter(s =>
      this.filterType === 'name'
        ? s.subjectName === this.filterValue
        : s.subjectCode === this.filterValue
    );
  }

  clearFilter() {
    this.filterType = '';
    this.filterValue = '';
    this.filteredSubjects = this.subjects;
  }

  openEdit(subject: Subject) {
    this.selectedSubject = { ...subject };
    this.isEditModalOpen = true;
  }

  closeEdit() {
    this.isEditModalOpen = false;
    this.selectedSubject = null;
  }

  saveEdit() {
    if (!this.selectedSubject) return;

    this.subjectService.addOrUpdateSubject(this.selectedSubject).subscribe({
      next: () => {
        alert('Subject updated successfully');
        this.closeEdit();
        this.loadSubjects();
      }
    });
  }

  toggleStatus(subject: Subject) {
    this.subjectService.addOrUpdateSubject({
      ...subject,
      isActive: !subject.isActive
    }).subscribe(() => this.loadSubjects());
  }
}
