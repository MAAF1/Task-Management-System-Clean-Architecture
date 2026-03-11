import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UserDto } from '../../shared/models/User.dto';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class Userservice {
  private baseUrl = environment.apiUrl + '/users';

  constructor(private http: HttpClient) {}
  getAllUsers(): Observable<UserDto[]> {
      return this.http.get<UserDto[]>(`${this.baseUrl}/GetAllUsers`);
    }
  getUserById(userId: number): Observable<UserDto> {

    return this.http.get<UserDto>(`${this.baseUrl}/GetUser/${userId}`);
  }
}
