import { Component, ElementRef, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/core/services/project.service';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';

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
      });

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
    this.elementRef.nativeElement.querySelector('.sorting > div:first-child').classList.add('active');
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
        this.tasks = this.project?.tasks as Task[];
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
  }

  hide(updatedTask: Task) {
    this.elementRef.nativeElement.querySelector('.task-details').classList.add('invisible');
    this.tasks[this.tasks.findIndex(task => task.id == updatedTask.id)] = updatedTask;
  }

  changeCompletion(task: Task) {
    this.tasksService.changeCompletion(task.id).subscribe();
    task.isCompleted = !task.isCompleted;
    this.applySorting(this.elementRef.nativeElement.querySelector('.sorting > div.active').className.split(' ')[0]);
  }
}
