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
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.sass']
})
export class ProjectViewComponent implements OnInit {

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
    private tasksService: TasksService,
    private dialogRef: MatDialog,
    private elementRef: ElementRef
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.projectService.get(this.id).subscribe(
      (res: Project | null) => {
        this.project = res;
        this.tasks = res?.tasks as Task[];
      },
      err => {
        this.router.navigate(['/page-not-found']);
      });

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
  }

  openDialog() {
    this.dialogRef.open(TaskDialogComponent, {
      data: {
        projectId: this.id
      }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.projectService.get(this.id).subscribe(
        (res: Project | null) => {
          this.project = res;
          this.tasks = res?.tasks as Task[];
          this.applySorting(this.elementRef.nativeElement.querySelector('.sorting > div.active').className.split(' ')[0]);
        });
    });
  }

  selectSorting(selectedElement: HTMLElement) {
    const sortingElements = this.elementRef.nativeElement.querySelectorAll('.sorting > div');

    sortingElements.forEach((element: HTMLElement) => {
      element.classList.remove('active');
    });

    this.applySorting(selectedElement.className);
    selectedElement.classList.add('active');
  }

  applySorting(sorting: string) {
    switch (sorting) {
      case 'all':
        this.tasks = this.project?.tasks?.filter(task => !task.isArchived) as Task[];
        break;
      case 'in-progress':
        this.tasks = this.project?.tasks?.filter(task => !task.isCompleted) as Task[];
        break;
      case 'completed':
        this.tasks = this.project?.tasks?.filter(task => task.isCompleted) as Task[];
        break;
    }
  }

  viewTask(id: string) {
    this.elementRef.nativeElement.querySelector('.task-details').classList.remove('invisible');
    this.taskId = id;
    console.log(this.taskId);
  }

  updateTasks(updatedTask: Task) {
    this.elementRef.nativeElement.querySelector('.task-details').classList.add('invisible');
    if (this.tasks[this.tasks.findIndex(task => task.id == updatedTask.id)]) {
      this.tasks[this.tasks.findIndex(task => task.id == updatedTask.id)] = updatedTask;
    } else {
      this.tasks.push(updatedTask);
    }
  }

  changeCompletion(task: Task) {
    this.tasksService.changeCompletion(task.id).subscribe();
    task.isCompleted = !task.isCompleted;
    this.applySorting(this.elementRef.nativeElement.querySelector('.sorting > div.active').className.split(' ')[0]);
  }

  changeArchivation(task: Task) {
    this.tasksService.changeArchivation(task.id).subscribe();
    task.isArchived = !task.isArchived;
    if (this.project && this.project.tasks) {
      this.project.tasks = this.project.tasks.filter(task => !task.isArchived);
      this.tasks = this.project.tasks;
    }
  }

  openTaskCreation() {
    this.elementRef.nativeElement.querySelector('.task-details').classList.remove('invisible');
    this.taskId = '00000000-0000-0000-0000-000000000000';
  }

  openAccessManagement() {
    this.dialogRef.open(ProjectAccessComponent, {
      data: {
        title: this.project?.title,
        projectId: this.id
      }
    });
  }

  preventViewTask(event: Event) {
    event.stopPropagation();
  }  

  public get accessRights(): typeof AccessRights {
    return AccessRights;
  }
}
