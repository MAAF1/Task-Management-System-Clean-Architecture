import { TaskDetailDto } from "./task-detaildto";

export interface UserTasksDetailsDto {
    /*public int UserId { get; set; }
public string UserName { get; set; }

public string UserEmail { get; set; }

public List<TaskDetailsDto> AssignedTasks { get; set; }*/

    userId: number;
    userName: string;
    userEmail: string;
    assignedTasks: TaskDetailDto[];
}
