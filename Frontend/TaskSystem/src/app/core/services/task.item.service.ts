import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateTaskDto } from '../../shared/models/create-task.dto';
import { UpdateTaskDto } from '../../shared/models/update.task.dto';
import { TaskResponseDto } from '../../shared/models/task.response.dto';
import { environment } from '../../../environments/environment';
import { AssignUserDto } from '../../shared/models/assign-user-dto';
import { RemoveUsersDto } from '../../shared/models/remove-users-dto';

@Injectable({
  providedIn: 'root',
})
export class TaskItemService {
  private baseUrl = environment.apiUrl + '/Tasks';
  private userTaskAssignmentUrl = environment.apiUrl + '/TaskAssignment';
  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<TaskResponseDto[]> {
    return this.http.get<TaskResponseDto[]>(`${this.baseUrl}/GetAllTasks`);
  }

  getTaskById(taskId: number): Observable<TaskResponseDto> {
    return this.http.get<TaskResponseDto>(`${this.baseUrl}/gettaskbyid/${taskId}`);
  }
  

  addTask(taskData: CreateTaskDto): Observable<string> {
  return this.http.post(`${this.baseUrl}/AddTask`, taskData, { responseType: 'text' });
}

  updateTask(taskId: number, taskData: UpdateTaskDto): Observable<TaskResponseDto> {
    return this.http.put<TaskResponseDto>(`${this.baseUrl}/UpdateTask/${taskId}`, taskData);
  }

  deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/DeleteTask/${taskId}`);
  }

 assignTask(taskId: number, dto: any): Observable<any> {
  
  return this.http.put(`${this.userTaskAssignmentUrl}/AssignUsers/${taskId}`, dto, {
    responseType: 'text' 
  });
}

removeAssignment(taskId: number, dto: any): Observable<any> {
  
  return this.http.put(`${this.userTaskAssignmentUrl}/RemoveUsers/${taskId}`, dto, {
    responseType: 'text' 
  });
}


}
