import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Task } from 'src/app/models/task';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  constructor(private http: HttpService) { }

  getTasks() {
    return this.http.get<Task[]>('tasks');
  }
}
