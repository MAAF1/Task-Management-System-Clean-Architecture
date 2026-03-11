import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { LoginRequest } from '../../shared/models/Auth/login-request';
import { RegisterRequest } from '../../shared/models/Auth/register-request';
import { LoginResponseDto } from '../../shared/models/Auth/login-response-dto';
import { RegisterResponseDto } from '../../shared/models/Auth/register-response.dto';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private baseUrl = environment.apiUrl + '/Auth';

  private userSubject = new BehaviorSubject<LoginResponseDto | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  login(dto: LoginRequest) {
    return this.http.post<LoginResponseDto>(`${this.baseUrl}/login`, dto);
  }

  register(dto: RegisterRequest) {
    return this.http.post<RegisterResponseDto>(`${this.baseUrl}/register`, dto);
  }

  private loadUserFromStorage() {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    const username = localStorage.getItem('username');
    const isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';
    const message = localStorage.getItem('message') || 'Loaded from storage';
    
    // Only set user if all required info is present

    if (token && role && username && isAuthenticated) {
      const user: LoginResponseDto = {
        token,
        role,
        username,
        message: 'Loaded from storage',
        isAuthenticated: true
      };
      this.userSubject.next(user);
    }
  }

  setUser(user: LoginResponseDto) {
    this.userSubject.next(user);
    localStorage.setItem('token', user.token);
    localStorage.setItem('role', user.role);
    localStorage.setItem('username', user.username);
    localStorage.setItem('isAuthenticated', user.isAuthenticated.toString());
    localStorage.setItem('message', user.message);
  }

  logout() {
    this.userSubject.next(null);
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('username');
    localStorage.removeItem('isAuthenticated');
    localStorage.removeItem('message');
    
  }

  /* ===================== ROLE CHECKS ===================== */
  isLoggedIn() {
    return !!this.userSubject.value?.token;
  }

  isSuperAdmin() {
    return this.userSubject.value?.role === 'SuperAdmin';
  }

  isAdmin() {
    return this.userSubject.value?.role === 'Admin';
  }

  isUser() {
    return this.userSubject.value?.role === 'User';
  }
}