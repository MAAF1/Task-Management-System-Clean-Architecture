import { Component, Input, OnInit } from '@angular/core';
import { TaskDetailDto } from '../../../shared/models/task-detaildto';
import { Status } from '../../../shared/models/status.enum';
import { UserTasksService } from '../../../core/services/tasks.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { FeedbackDto } from '../../../shared/models/feedback-dto';

@Component({
  selector: 'app-task-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task.details.component.html',
  styleUrl: './task.details.component.css'
})

export class TaskDetailsComponent implements OnInit {
  private _task!: TaskDetailDto;
  feedbackInput: string = '';
  TaskStatus = Status;

 
  get task(): TaskDetailDto { return this._task; }

  constructor(
    private tasksService: UserTasksService, 
    private router: Router,
    private route: ActivatedRoute
  )
   {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state && navigation.extras.state['data']) {
      this._task = navigation.extras.state['data'];
      
      this.feedbackInput = this._task?.feedback || '';
    }
  }
   @Input() 
  set task(value: any) {
    if (value) {
      this._task = Array.isArray(value) ? value[0] : value;
      this.feedbackInput = this._task?.feedback || '';
    }
  }

 ngOnInit(): void {
  if (!this._task) {
    this._task = window.history.state?.data;
  }

  if (this._task) {
    
    console.log('Task Data:', this._task);

    setTimeout(() => {
      
      this.feedbackInput = 
        this._task.feedback ||  '';
        
      console.log('Feedback Check:', this.feedbackInput);
    }, 100);
  } else {
    this.router.navigate(['/dashboard']);
  }
}

  completeTask() {
    if (!this.task) return;
    this.tasksService.completeTask(this.task.taskId).subscribe({
      next: () => {
        alert('Task Completed! ');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => alert('Error: ' + err.message)
    });
  }

  uncompleteTask() {
  if (!this.task) return;
  
  console.log('Uncompleting task ID:', this.task.taskId);

  this.tasksService.uncompleteTask(this.task.taskId).subscribe({
    next: () => {
      alert('Task marked as Uncompleted! ↩');
      this.router.navigate(['/dashboard']);
    },
    error: (err) => {
      console.error('Full Error:', err); 
      alert('Error: ' + (err.error?.message || err.message));
    }
  });
}

  updateFeedback() {
    if (!this.task) return;
    
    
    const dto: FeedbackDto = { 
        Feedback: this.feedbackInput 
    };

    this.tasksService.updateFeedback(this.task.taskId, dto).subscribe({
      next: () => {
        alert('Feedback Updated! ');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.log('Error updating feedback:', err);
        alert('Error updating feedback')
    }});
  }
}