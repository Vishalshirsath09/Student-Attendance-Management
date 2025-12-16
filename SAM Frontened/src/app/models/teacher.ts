// src/app/models/teacher.ts
export interface Teacher {
  teacherId: number;       // ✅ Add this
  teacherName: string;
  phoneNo: string;
  emailAddress: string;
  address: string;
  qualification: string;
  experience: number;
  isActive: boolean;
}
