import { Component, ElementRef, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from 'src/app/core/services/project.service';
import { TasksService } from 'src/app/core/services/tasks.service';
import { AccessRights, Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';
import { ProjectAccessComponent } from '../project-access/project-access.component';

@Component({
  selector: 'app-project-view-archived',
  templateUrl: './project-view-archived.component.html',
  styleUrls: ['./project-view-archived.component.sass']
})
export class ProjectViewArchivedComponent implements OnInit {
  id = '';
  project: Project | null = null;
  tasks: Task[] = [];
  baseUrl = '/dashboard/projects/';
  greeting: string = '';
  taskId = '';

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private router: Router,
    private tasksService: TasksService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.projectService.getArchived(this.id).subscribe(
      (res: Project | null) => {
        this.project = res;
        this.tasks = res?.tasks as Task[];
      },
      err => {
        this.router.navigate(['/page-not-found']);
      });

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
  }

  changeArchivation(task: Task) {
    this.tasksService.changeArchivation(task.id).subscribe();
    task.isArchived = !task.isArchived;
    this.tasks = this.project?.tasks?.filter(task => task.isArchived) as Task[];
  }

  public get accessRights(): typeof AccessRights {
    return AccessRights;
  }
}





