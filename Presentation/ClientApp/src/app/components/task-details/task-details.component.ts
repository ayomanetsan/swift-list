import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Label, TaskWithDetails, ToDoItem } from 'src/app/models/taskWithDetails';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.sass']
})
export class TaskDetailsComponent {

  @Input() id: string | undefined;
  @Output() isVisible = new EventEmitter<boolean>();
  @ViewChild('label') inputLabel: ElementRef | null = null; 

  task: TaskWithDetails | undefined;
  dueDate = '';
  creationDate = '';
  creator = '';
  color = '';

  constructor(
    private taskService: TasksService, 
    private elementRef: ElementRef,
    private toastr: ToastrService
    ) { }

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
    this.color = '';
    this.elementRef.nativeElement.querySelector('.label.temp').classList.add('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.remove('invisible');
    this.inputLabel!.nativeElement.innerText = '';
  }

  showInputLabel() {
    this.elementRef.nativeElement.querySelector('.label.temp').classList.remove('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.add('invisible');
  }

  onKeyDown(event: Event) {
    if (event instanceof KeyboardEvent) {
      if (event.key === 'Enter') {
        event.preventDefault();
      }

      if (event.key !== 'Backspace' && this.inputLabel!.nativeElement.innerText.length > 15) {
        event.preventDefault();
      }
    }
  }

  onColorSelect() {
    if (this.inputLabel!.nativeElement.innerText.length < 4) {
      this.toastr.error('Label must be at least 4 characters long', 'Error');
      return;
    } else if (this.task!.labels!.find(label => label.title === this.inputLabel!.nativeElement.innerText)) {
      this.toastr.error('Label already exists', 'Error');
      return;
    }

    const newLabel: Label = {
      taskId: this.task!.id!,
      title: this.inputLabel!.nativeElement.innerText,
      color: this.color
    }

    this.task!.labels!.push(newLabel);
    this.taskService.createLabel(newLabel).subscribe();

    this.elementRef.nativeElement.querySelector('.label.temp').classList.add('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.remove('invisible');
    this.color = '';
    this.inputLabel!.nativeElement.innerText = '';
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
