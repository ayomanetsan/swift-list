import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/services/auth.service';
import { ProjectBrief } from 'src/app/models/projectBrief';
import { ProjectService } from 'src/app/core/services/project.service';

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
    private authService: AuthService) { }

  ngOnInit(): void {
    this.projectService.getProjects().subscribe(
      (resp: ProjectBrief[]) => {
        this.projects = resp;
      }
    );

    this.greeting = localStorage.getItem('userName')?.split(' ')[0] as string;
    this.authService.startAuthenticatedCheck();
  }

  openDialog() {
  }

}
