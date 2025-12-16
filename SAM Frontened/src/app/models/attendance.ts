export interface Attendance {
  teacherId: any;
  studentId: any;
  subjectId: any;
  attendanceId: any;
  studentName?: string;
  subjectName?: string;
  teacherName?: string;
  attendanceDate: string;
  isPresent: boolean;
}
