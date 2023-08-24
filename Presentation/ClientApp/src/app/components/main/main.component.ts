import { Component } from '@angular/core';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Task } from 'src/app/models/task';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.sass']
})
export class MainComponent {

  tasks: Task[] = [];

  constructor(private tasksService: TasksService) { }

  ngOnInit(): void {
    this.tasksService.getTasks().subscribe(
      (resp: Task[]) => {
        this.tasks = resp;
      }
    );
  }
}
