import { Component, NgModule } from '@angular/core';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../shared/models/Auth/login-request';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router'; 
import { RouterModule } from '@angular/router';



@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
  standalone: true
})
export class Login 
{
  loginData: LoginRequest = {
    email: '',
    password: ''
  };

  constructor(private auth: Auth, private router: Router) {}

 onSubmit(){
  this.auth.login(this.loginData).subscribe({
    next: (response) => {
      // Update auth state with the logged-in user info
      this.auth.setUser({
        token: response.token,
        username: response.username,
        isAuthenticated: response.isAuthenticated,
        role: response.role,
        message: response.message
      });

      //store in localStorage for persistence
      localStorage.setItem('token', response.token);
      localStorage.setItem('username', response.username);
      localStorage.setItem('role', response.role);
      localStorage.setItem('isAuthenticated', response.isAuthenticated.toString());
      localStorage.setItem('message', response.message);

      this.router.navigate(['/dashboard']);
      console.log('Login successful:', response);
      console.log('Token stored in localStorage:', localStorage.getItem('token'));
      console.log('Is logged in:', this.auth.isLoggedIn());  
      console.log('is user', this.auth.isUser());
      console.log('is admin', this.auth.isAdmin());
      console.log('is super admin', this.auth.isSuperAdmin());
    },
    error: (error) => {
      console.error('Login failed:', error);
    }
  });
}
}
