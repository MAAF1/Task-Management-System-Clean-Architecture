import { Status } from "./status.enum";
export interface TaskDetailDto {
    taskId: number;
    title: string;
    description: string;
    createdBy: string;
    createdDate?: Date;
    dueDate?: Date;
    closedDate?: Date;
    Status: string;
    feedback: string;
    userTaskStatus: Status;
    taskuserassignedDate?: Date;
    userTaskClosedDate?: Date;  

    /*
        public int TaskId { get; set; }
 
 public string Title { get; set; }
 public string Description { get; set; }
 public string CreatedBy { get; set; }
 public DateTime CreatedDate { get; set; } 
 
 public DateTime? DueDate { get; set; }

 public DateTime? ClosedDate { get; set; }

 public string Status { get; set; }  
 public DateTime? TaskUserAssignedDate { get; set; }
 public string? Feedback {  get; set; }
 public DateTime? UserClosedDate { get; set; }
 public string UserTaskStatus { get; set; }

    */
}
