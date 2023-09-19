import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/services/auth.service';
import { ProjectBrief } from 'src/app/models/projectBrief';
import { ProjectService } from 'src/app/core/services/project.service';
import { ProjectDialogComponent } from '../project-dialog/project-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.sass']
})
export class ProjectListComponent {

  greeting = '';
  projects: ProjectBrief[] = [];

  constructor(
    private projectService: ProjectService,
    private dialogRef: MatDialog,
    private authService: AuthService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.projectService.getAll().subscribe(
      (res: ProjectBrief[]) => {
        this.projects = res;
      }
    );

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
    this.authService.startAuthenticatedCheck();
  }

  openDialog() {
    this.dialogRef.open(ProjectDialogComponent);

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.projectService.getAll().subscribe(
        (res: ProjectBrief[]) => {
          this.projects = res;
        }
      );
    });
  }

  viewProject(id: string) {
    this.router.navigate(['dashboard/projects/tasks', id]);
  }

}
