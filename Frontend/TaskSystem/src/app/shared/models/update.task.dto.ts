
export interface UpdateTaskDto {
    title?: string;
    description?: string;
    dueDate?: Date | null;
    isInProgress?: boolean;
    isCompleted?: boolean;
 
}
