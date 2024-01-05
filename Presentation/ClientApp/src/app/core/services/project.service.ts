import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ProjectBrief } from 'src/app/models/projectBrief';
import { AccessRights, Project } from 'src/app/models/project';
import { UserAccessRights } from 'src/app/models/userAccessRights';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpService) { }

  get(id: string) {
    return this.http.get<Project | null>(`projects/get/${id}`);
  }

  getAll() {
    return this.http.get<ProjectBrief[]>('projects/get');
  }

  create(title: string, description: string) {
    return this.http.post('projects/create', { title, description });
  }

  getProjectAccessRights(projectId: string) {
    return this.http.get<UserAccessRights[]>(`projects/${projectId}/access-rights`);
  }

  grantAccessRights(projectId: string, email: string, accessRights: AccessRights) {
    return this.http.post<AccessRights>('projects/grant-access-rights', { projectId, email, accessRights })
  }

}
