import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserTasksDetailsDto } from '../../shared/models/user-tasks-details-dto';
import { environment } from '../../../environments/environment';
import { FeedbackDto } from '../../shared/models/feedback-dto';
@Injectable({
  providedIn: 'root',
})
export class UserTasksService {
  private baseUrl = environment.apiUrl + '/UserTask';

  constructor(private http: HttpClient) {}

  getMyTasks(): Observable<UserTasksDetailsDto[]> {
    return this.http.get<UserTasksDetailsDto[]>(`${this.baseUrl}/Getmytasks`);
  }

  completeTask(taskId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/CompleteTask/${taskId}`,{});
  }

  uncompleteTask(taskId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/UncompleteTask/${taskId}`,{});
  }

  updateFeedback(taskId: number, dto: FeedbackDto): Observable<any> {
    return this.http.put(`${this.baseUrl}/WriteFeedback/${taskId}`, dto,{ 
        responseType: 'text' 
    });
  }

}
