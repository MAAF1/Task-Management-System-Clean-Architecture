import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { UserTasksService } from '../../../core/services/tasks.service';
import { TaskDetailDto } from '../../../shared/models/task-detaildto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Status } from '../../../shared/models/status.enum';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-my-tasks',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './my-tasks.html',
  styleUrls: ['./my-tasks.css'],
  standalone: true
})
export class MyTasks implements OnInit {
  TaskStatus = Status;
  myTasks: TaskDetailDto[] = [];
  loading = true;

  constructor(
    private tasksService: UserTasksService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.fetchTasks();
  }

  fetchTasks() {
  this.loading = true;
  this.tasksService.getMyTasks().subscribe({
    next: (res: any) => {
      
      this.myTasks = res.assignedTasks || []; 
      this.loading = false;
      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('Error fetching tasks:', error);
      this.loading = false;
      this.cdr.detectChanges();
    },
  });
}

openTask(task: TaskDetailDto) {
  
  this.router.navigate(['/task-details', task.taskId], { state: { data: task } });
}
}