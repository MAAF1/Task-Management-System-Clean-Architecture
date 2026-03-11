import { Status } from "./status.enum";
export interface AssignedUserDto {
  userId: number;
  userName: string;
  userEmail: string;
  userStatusInTask: number;  
  feedback?: string;
  userDueDate?: Date;
  assignedDate?: Date;  
  userClosedDate?: Date;
  

  /*public int UserId { get; set; }
public string UserName { get; set; } = null!;
public string UserEmail { get; set; } = null!;

       
public string UserStatusInTask { get; set; } = null!; 
public string? Feedback { get; set; }
public DateTime? AssignedDate { get; set; }
public DateTime? UserClosedDate { get; set; }*/
}