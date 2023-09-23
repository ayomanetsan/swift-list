import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { TasksService } from 'src/app/core/services/tasks.service';
import { Label, TaskWithDetails, ToDoItem } from 'src/app/models/taskWithDetails';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.sass']
})
export class TaskDetailsComponent {

  @Input() id: string | undefined;
  @Input() projectId: string | undefined;
  @Output() isVisible = new EventEmitter<Task>();
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
    if (this.id == '00000000-0000-0000-0000-000000000000') {
      this.task = {
        id: '00000000-0000-0000-0000-000000000000',
        title: '',
        description: '',
        isCompleted: false,
        createdAt: new Date(),
        createdBy: '',
        dueDate: new Date(),
        labels: [],
        toDoItems: [],
        projectId: this.projectId
      }

      this.dueDate = 'No due date';
      this.creationDate = moment(this.task.createdAt).format('Do MMMM YYYY');
      this.creator = 'You';
      return;
    }

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
    this.color = '';
    if (this.elementRef.nativeElement.querySelector('.to-do-item.temp > span').innerText.length >= 4) {
      const newToDoItem: ToDoItem = {
        id: '00000000-0000-0000-0000-000000000000',
        title: this.elementRef.nativeElement.querySelector('.to-do-item.temp').innerText,
        isCompleted: false
      }

      this.task!.toDoItems!.push(newToDoItem);
    }
    
    this.task!.title = this.elementRef.nativeElement.querySelector('.title-input').innerText;
    this.task!.description = this.elementRef.nativeElement.querySelector('.description-input').innerText;

    const task: Task = {
      id: this.task!.id!,
      title: this.task!.title,
      description: this.task!.description,
      isCompleted: this.task!.isCompleted
    };

    if (this.task!.id == '00000000-0000-0000-0000-000000000000') {
      this.taskService.create(this.task!).subscribe(
        (res: string) => {
          task.id = res;
        }
      );
    } else {
      this.taskService.update(this.task!).subscribe();
    }

    

    this.isVisible.emit(task);
      
    this.elementRef.nativeElement.querySelector('.label.temp').classList.add('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.remove('invisible');
    this.elementRef.nativeElement.querySelector('.to-do-item.temp').classList.add('invisible');
    this.inputLabel!.nativeElement.innerText = '';
    this.elementRef.nativeElement.querySelector('.to-do-item.temp > span').innerText = '';
    this.elementRef.nativeElement.querySelector('.to-do-item.temp > span[contenteditable]:empty::before').content = 'Type a task';
  }

  showInputLabel() {
    this.elementRef.nativeElement.querySelector('.label.temp').classList.remove('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.add('invisible');
  }

  onTitleChanged(event: Event) {
    if (event instanceof KeyboardEvent) {
      if (event.key === 'Enter') {
        event.preventDefault();
      }

      if (event.key !== 'Backspace' && (event.target as HTMLInputElement).innerText.length > 31) {
        event.preventDefault();
        this.toastr.error('Title cannot exceed 32 characters', 'Error');
      }
    }
  }

  onDescriptionChanged(event: Event) {
    if (event instanceof KeyboardEvent) {
      if ((event.target as HTMLInputElement).innerText.length < 4) {
        this.toastr.error('Description must be at least 4 characters long', 'Error');
      }
    }
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

  showToDoItemInput() {
    if (this.elementRef.nativeElement.querySelector('.to-do-item.temp').classList.contains('invisible')) {
      this.elementRef.nativeElement.querySelector('.to-do-item.temp').classList.remove('invisible');
      return;
    }
    
    if (this.elementRef.nativeElement.querySelector('.to-do-item.temp > span').innerText.length >= 4) {
      const newToDoItem: ToDoItem = {
        id: '00000000-0000-0000-0000-000000000000',
        title: this.elementRef.nativeElement.querySelector('.to-do-item.temp').innerText,
        isCompleted: false
      }

      this.task!.toDoItems!.push(newToDoItem);
      this.elementRef.nativeElement.querySelector('.to-do-item.temp > span').innerText = '';
      this.elementRef.nativeElement.querySelector('.to-do-item.temp > span[contenteditable]:empty::before').content = 'Type a task';
    } else {
      this.toastr.error('To Do Item must be at least 4 characters long', 'Error');
    }
  }

  onDateChanged(event: MatDatepickerInputEvent<any>): void {
    this.dueDate = moment(event.value).calendar();

    if (this.dueDate.includes('/')) {
      this.dueDate = moment(event.value).format('D MMM');
    }
  }
}
