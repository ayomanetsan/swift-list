import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ProjectBrief } from 'src/app/models/projectBrief';
import { Project } from 'src/app/models/project';

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

}
