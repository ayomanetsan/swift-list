import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TasksService } from 'src/app/core/services/tasks.service';
import { TaskWithDetails, ToDoItem } from 'src/app/models/taskWithDetails';
import * as moment from 'moment';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.sass']
})
export class TaskDetailsComponent {

  @Input() id: string | undefined;
  @Output() isVisible = new EventEmitter<boolean>();

  task: TaskWithDetails | undefined;
  dueDate = '';
  creationDate = '';
  creator = '';

  constructor(private taskService: TasksService) { }

  ngOnChanges() {
    if (this.id) {
      this.taskService.get(this.id).subscribe(res => {
        this.task = res;
        this.dueDate = moment(this.task.dueDate).calendar();

        if (this.dueDate.includes('/')) {
          this.dueDate = moment(this.task.dueDate).format('D MMM');
        }

        this.creationDate = moment(this.task.createdAt).format('Do MMMM YYYY');
        this.creator = this.task.createdBy == localStorage.getItem('userEmail') ? 'You' : this.task.createdBy;
      });
    }
  }

  hide() {
    this.isVisible.emit(false);
    this.id = undefined;
  }

  calculateContrast(hexcolor: string) {
      hexcolor = hexcolor.replace(/^#/, '');
      const r = parseInt(hexcolor.slice(0, 2), 16);
      const g = parseInt(hexcolor.slice(2, 4), 16);
      const b = parseInt(hexcolor.slice(4, 6), 16);
  
      const relativeLuminance = 0.299 * r + 0.587 * g + 0.114 * b;
  
      if (relativeLuminance > 128) {
          return 'black';
      } else {
          return 'white';
      }
  }

  changeCompletion(toDoItem: ToDoItem) {
    toDoItem.isCompleted = !toDoItem.isCompleted;
  }
}
