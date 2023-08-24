import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Task } from 'src/app/models/task';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.sass']
})
export class MainComponent {

  tasks: Task[] = [];

  constructor(private tasksService: TasksService, private dialogRef: MatDialog) { }

  ngOnInit(): void {
    this.tasksService.getTasks().subscribe(
      (resp: Task[]) => {
        this.tasks = resp;
      }
    );
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
}
