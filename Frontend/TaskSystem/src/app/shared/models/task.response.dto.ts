import { AssignedUserDto } from "./assigned-task-dto";

export interface TaskResponseDto {
  taskId: number;
  title: string;
  description?: string;
  status: string; 
  createdByUserName: string; 

   // match enum backend
  createdDate: Date;      // Date
  dueDate?: Date;
  closedDate?: Date;
  
  assignedUsers: AssignedUserDto[];

}