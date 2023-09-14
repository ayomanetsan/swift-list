import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ProjectBrief } from 'src/app/models/projectBrief';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpService) { }

  getProjects() {
    return this.http.get<ProjectBrief[]>('projects/get');
  }

}
