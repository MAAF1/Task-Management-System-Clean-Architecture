import { Component } from '@angular/core';
import { Auth } from '../../../core/services/auth';
import { RegisterRequest } from '../../../shared/models/Auth/register-request';
import { RegisterResponseDto } from '../../../shared/models/Auth/register-response.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.html',
  styleUrls: ['./register.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule]  
})
export class RegisterComponent {
  registerData: RegisterRequest = {
    username: '',
    email: '',
    password: ''
  };
  
  constructor(private auth: Auth, private router: Router  ) {}

  onSubmit() {
  this.auth.register(this.registerData).subscribe({
    next: (response: RegisterResponseDto) => {
      console.log('Register successful:', response.message);
      this.router.navigate(['/login']);
    },
    error: (error) => {
      console.error('Register failed:', error);
    }
  });
  }
}