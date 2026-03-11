import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskItemService } from '../../../core/services/task.item.service';
import { TaskResponseDto } from '../../../shared/models/task.response.dto';
import { UpdateTaskDto } from '../../../shared/models/update.task.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AssignUserDto } from '../../../shared/models/assign-user-dto';
import { UserDto } from '../../../shared/models/User.dto';
import { Userservice } from '../../../core/services/userservice';
@Component({
  selector: 'app-admin.task.details.component',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.task.details.component.html',
  styleUrl: './admin.task.details.component.css',
})
export class AdminTaskDetailsComponent implements OnInit {
  taskId!: number;
  taskDetails!: TaskResponseDto;
  allUsers: UserDto[] = []; 
  loading = true;
  error?: string;
selectedUserIdsToAssign: number[] = []; 
selectedUserIdsToRemove: number[] = [];
  
  userIdToAssign?: number; 
  userIdToRemove?: number;

  constructor(
    private route: ActivatedRoute, 
    private taskItemService: TaskItemService, 
    private userservice: Userservice, 
    private router: Router, 
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.taskId = Number(this.route.snapshot.paramMap.get('taskId'));
    this.fetchTask();
    this.fetchAllUsers();
  }

  
  deleteTask() {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskItemService.deleteTask(this.taskId).subscribe({
        next: () => {
          alert('Task deleted successfully');
          this.router.navigate(['/admindashboard']);
        },
        error: (err) => alert( err.message)
      });
    }
  }


  fetchAllUsers() {
    this.userservice.getAllUsers().subscribe({
      next: (users) => this.allUsers = users,
      error: (err) => console.error('Error loading users', err)
    });
  }

  fetchTask() {
    this.loading = true;
    this.taskItemService.getTaskById(this.taskId).subscribe({
      next: (response: any) => {
        const task = Array.isArray(response) ? response[0] : response;
        if (task) {
          if (task.dueDate) {
            task.dueDate = new Date(task.dueDate).toISOString().split('T')[0];
          }
          this.taskDetails = task;
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => { this.error = err.message; this.loading = false; }
    });
  }

  assignUser() {
  if (this.selectedUserIdsToAssign.length === 0) {
    return alert('Please select at least one user to assign');
  }

  const dto = { userIds: this.selectedUserIdsToAssign.map(id => Number(id)) }; 
  
  this.taskItemService.assignTask(this.taskId, dto).subscribe({
    next: () => {
      alert('Users assigned to task successfully!');
      this.selectedUserIdsToAssign = []; 
      this.fetchTask(); 
    },
    error: (err) => {
      if (err.status === 200 || err.status === 201) {
        alert('Users added successfully!');
        this.selectedUserIdsToAssign = []; 
        this.fetchTask();
      } else {
        console.error('Real Error:', err);
        alert('Failed to remove users. Check console for details.');
      }
    }
  });
}

removeUser() {
  if (this.selectedUserIdsToRemove.length === 0) return alert('Please select users to remove');

  const dto = { userIds: this.selectedUserIdsToRemove.map(id => Number(id)) };
  
  this.taskItemService.removeAssignment(this.taskId, dto).subscribe({
    next: (response) => {
      alert('Users removed successfully!');
      this.selectedUserIdsToRemove = []; 
      this.fetchTask(); 
    },
    error: (err) => {
      
      if (err.status === 200 || err.status === 201) {
        alert('Users removed successfully!');
        this.selectedUserIdsToRemove = []; 
        this.fetchTask();
      } else {
        console.error('Real Error:', err);
        alert('Failed to remove users. Check console for details.');
      }
    }
  });
}
  isUpdating = false;

updateTask() {
  if (!this.taskDetails) return;
  this.isUpdating = true;

  
  const dto: UpdateTaskDto = {
    title: this.taskDetails.title,
    description: this.taskDetails.description,
    dueDate: this.taskDetails.dueDate
  };

  this.taskItemService.updateTask(this.taskId, dto).subscribe({
    next: () => {
      alert('Task updated successfully! ✅');
      this.isUpdating = false;
      this.fetchTask(); 
    },
    error: (err) => {
      alert('Error updating task: ' + err.message);
      this.isUpdating = false;
    }
  });
}
}

  

