import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Task } from 'src/app/models/task';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.sass']
})
export class MainComponent {

  greeting: string = '';
  tasks: Task[] = [];

  constructor(
    private tasksService: TasksService, 
    private dialogRef: MatDialog,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.tasksService.getTasks().subscribe(
      (resp: Task[]) => {
        this.tasks = resp;
      }
    );

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
    this.authService.startAuthenticatedCheck();
  }

  openDialog() {
    this.dialogRef.open(TaskDialogComponent);

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.tasksService.getTasks().subscribe(
        (resp: Task[]) => {
          this.tasks = resp;
        }
      );
    });
  }

  changeCompletion(task: Task) {
    this.tasksService.changeCompletion(task.id).subscribe();
    task.isCompleted = !task.isCompleted;
  }
}
