import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SubjectService } from '../../services/subject.service';
import { Subject } from '../../models/subject';

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
  error = '';

  // FILTER
  filterType: 'name' | 'code' | '' = '';
  filterValue = '';

  // OPTIONS
  nameList: string[] = [];
  codeList: string[] = [];

  // EDIT
  isEditModalOpen = false;
  selectedSubject: Subject | null = null;

  constructor(private subjectService: SubjectService) {}

  ngOnInit(): void {
    this.loadSubjects();
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

  /** SEARCH */
  applyFilter() {
    if (!this.filterType || !this.filterValue) {
      this.filteredSubjects = this.subjects;
      return;
    }

    this.filteredSubjects = this.subjects.filter(s => {
      if (this.filterType === 'name') {
        return s.subjectName === this.filterValue;
      }
      if (this.filterType === 'code') {
        return s.subjectCode === this.filterValue;
      }
      return true;
    });
  }

  clearFilter() {
    this.filterType = '';
    this.filterValue = '';
    this.filteredSubjects = this.subjects;
  }

  /** EDIT */
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

  /** STATUS */
  toggleStatus(subject: Subject) {
    const updated = { ...subject, isActive: !subject.isActive };

    this.subjectService.addOrUpdateSubject(updated).subscribe({
      next: () => this.loadSubjects()
    });
  }
}
