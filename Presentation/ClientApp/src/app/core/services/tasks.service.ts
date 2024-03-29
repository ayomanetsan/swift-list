import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Task } from 'src/app/models/task';
import { Label, TaskWithDetails } from 'src/app/models/taskWithDetails';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  constructor(private http: HttpService) { }

  get(id: string) {
    return this.http.get<TaskWithDetails>(`tasks/get/${id}`);
  }

  getTasks() {
    return this.http.get<Task[]>('tasks');
  }

  create(task: TaskWithDetails) {
    return this.http.post<string>('tasks/create', task);
  }

  changeCompletion(id: string) {
    return this.http.put(`tasks/mark/?id=${id}`, { });
  }

  changeArchivation(id: string) {
    return this.http.put(`tasks/archive/?id=${id}`, { });
  }

  changeToDoItemCompletion(id: string) {
    return this.http.put(`tasks/markToDoItem/?id=${id}`, { });
  }

  createLabel(label: Label) {
    return this.http.post('tasks/labels/create', label);
  }

  update(task: TaskWithDetails) {
    return this.http.put('tasks/update', task);
  }
}
