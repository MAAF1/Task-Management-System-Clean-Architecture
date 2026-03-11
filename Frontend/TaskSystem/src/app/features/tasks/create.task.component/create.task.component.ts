import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskItemService } from '../../../core/services/task.item.service';
import { Userservice } from '../../../core/services/userservice';
import { CreateTaskDto } from '../../../shared/models/create-task.dto';

@Component({
  selector: 'app-create.task.component',
  imports: [CommonModule, FormsModule],
  templateUrl: './create.task.component.html',
  styleUrl: './create.task.component.css',
})
export class CreateTaskComponent {



allUsers: any[] = []; // we'll fill this with real user data from the server
  task: CreateTaskDto = {
    title: '',
    description: '',
    dueDate: null,
    assignedUserIds: [] 
  };

  loading = false;
  error?: string;

  constructor(
    private taskItemService: TaskItemService, 
    private userservice: Userservice, 
    private router: Router
  ) {}

  ngOnInit(): void {
   
    this.userservice.getAllUsers().subscribe({
      next: (users) => {
        this.allUsers = users;
      },
      error: (err) => {
        console.error('Failed to load users', err);
        this.error = 'Could not load users list.';
      }
    });
  }

  createTask() {
  if (!this.task.title) {
    this.error = "Please enter a task title";
    return;
  }

  this.loading = true;
  this.error = undefined;

  
  const payload: CreateTaskDto = {
    ...this.task,
    assignedUserIds: this.task.assignedUserIds.map(id => Number(id))
  };

  console.log('Sending Payload to Server:', payload);

  this.taskItemService.addTask(payload).subscribe({
    next: (response) => {
      console.log('Server Success Response:', response);
      alert('Task created and users assigned successfully! ✅');
      this.router.navigate(['/admindashboard']);
    },
    error: (err) => {
      console.error('Server Error Response:', err);
      this.error = 'Failed to create task: ' + (err.error?.message || err.message);
      this.loading = false;
    }
  });
}
}
