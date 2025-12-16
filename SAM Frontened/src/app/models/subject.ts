export interface Subject{
  isActive: any;
  subjectId: number;
  subjectName: string;
  subjectCode: string;
  teacherId?: number;
  teacherName?: string;   // ✅ ADD THIS
}
