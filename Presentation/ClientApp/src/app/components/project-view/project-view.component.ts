import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/core/services/project.service';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.sass']
})
export class ProjectViewComponent implements OnInit {

  id = '';
  project: Project | null = null;
  baseUrl = '/dashboard/projects/';
  greeting: string = '';

  constructor(
    private route: ActivatedRoute, 
    private projectService: ProjectService,
    private tasksService: TasksService
    ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.projectService.get(this.id).subscribe(
      (res: Project | null) => {
        this.project = res;
      });

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
  }

  changeCompletion(task: Task) {
    this.tasksService.changeCompletion(task.id).subscribe();
    task.isCompleted = !task.isCompleted;
  }
}

