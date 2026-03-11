export interface CreateTaskDto {
  title: string;             
  description?: string;      
  dueDate?: Date | null;               
  assignedUserIds: number[]; 

}
