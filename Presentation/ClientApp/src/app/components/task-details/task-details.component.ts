import { Component, ElementRef, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { TasksService } from 'src/app/core/services/tasks.service';
import { EmptyTask, Label, TaskWithDetails, ToDoItem } from 'src/app/models/taskWithDetails';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { GlobalConstants } from 'src/app/global/constants';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.sass']
})
export class TaskDetailsComponent implements OnChanges {

  @Input() taskId: string = GlobalConstants.EmptyGuid;
  @Input() projectId: string = GlobalConstants.EmptyGuid;

  @Output() updatedTask = new EventEmitter<Task>();

  currentTask: TaskWithDetails = EmptyTask;
  taskDueDate: string = GlobalConstants.EmptyString;
  taskCreationDate: string = GlobalConstants.EmptyString;
  taskCreator: string = GlobalConstants.EmptyString;
  labelColor: string = GlobalConstants.EmptyString;

  private isUpdated: boolean = false;

  constructor(
    private taskService: TasksService,
    private elementRef: ElementRef,
    private toastr: ToastrService
  ) { }

  ngOnChanges(): void {
    if (this.taskId == GlobalConstants.EmptyGuid || this.taskId == GlobalConstants.EmptyString) {
      this.currentTask = EmptyTask;
      this.taskCreator = 'You';
      this.setDates(this.currentTask, true);
      this.setTitleAndDescription();
    } else {
      this.taskService.get(this.taskId).subscribe(res => {
        this.currentTask = res;
        this.taskCreator =
          this.currentTask.createdBy == localStorage.getItem('userEmail')
            ? 'You'
            : this.currentTask.createdBy;
        this.setDates(this.currentTask);
        this.setTitleAndDescription();
      });
    }
  }

  close(): void {
    if (!this.validateFields()) {
      return;
    }

    this.updateFields();
    this.tryAddingToDoItem();

    if (this.taskId == GlobalConstants.EmptyGuid) {
      this.currentTask.projectId = this.projectId;

      this.taskService.create(this.currentTask).subscribe(
        (res: string) => {
          this.currentTask.id = res;
        }
      );
    } else if (this.isUpdated) {
      this.taskService.update(this.currentTask).subscribe();
    }

    this.isUpdated = false;
    this.updatedTask.emit(this.currentTask);
    this.hideFields();
  }

  onTitleChanged(event: Event): void {
    if (event instanceof KeyboardEvent) {
      if (event.key === 'Enter') {
        event.preventDefault();
      }

      if (event.key !== 'Backspace' && (event.target as HTMLInputElement).innerText.length > 31) {
        event.preventDefault();
        this.toastr.error('Title cannot exceed 32 characters', 'Error');
      }
    }

    this.isUpdated = true;
  }

  onDescriptionChanged(event: Event): void {
    this.isUpdated = true;
  }

  showLabelInput(): void {
    this.elementRef.nativeElement.querySelector('.label.temp.invisible').classList.remove('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.add('invisible');
  }

  onLabelChange(event: Event): void {
    if (event instanceof KeyboardEvent) {
      if (event.key === 'Enter') {
        event.preventDefault();
      }

      if (event.key !== 'Backspace' && this.elementRef.nativeElement.querySelector('.label.temp').innerText.length > 15) {
        event.preventDefault();
      }
    }
  }

  calculateContrast(hexcolor: string): string {
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

  onColorSelect() {
    const labelInputWrapper: Element = this.elementRef.nativeElement.querySelector('.label.temp');
    const labelInput: Node = labelInputWrapper.children[0];

    if (labelInput.textContent!.length < 4) {
      this.toastr.error('Label must be at least 4 characters long', 'Error');
      return;
    } else if (this.currentTask.labels?.find(label => label.title === labelInput.textContent)) {
      this.toastr.error('Label already exists', 'Error');
      return;
    }

    const newLabel: Label = {
      taskId: this.taskId,
      title: labelInput.textContent!,
      color: this.labelColor
    }

    this.currentTask.labels?.push(newLabel);

    labelInputWrapper.classList.add('invisible');
    labelInput.textContent = GlobalConstants.EmptyString;
    this.labelColor = GlobalConstants.EmptyString;
    this.elementRef.nativeElement.querySelector('.label.add').classList.remove('invisible');

    this.isUpdated = true;
  }

  changeToDoItemCompletion(toDoItem: ToDoItem): void {
    toDoItem.isCompleted = !toDoItem.isCompleted;
    this.isUpdated = true;
  }

  showToDoItemInput(): void {
    const toDoItemInputWrapper: Element = this.elementRef.nativeElement.querySelector('.to-do-item.temp');
    const toDoItemInput: Node = toDoItemInputWrapper.children[1];

    if (toDoItemInputWrapper.classList.contains('invisible')) {
      toDoItemInputWrapper.classList.remove('invisible');
      return;
    }

    if (toDoItemInput.textContent!.length >= 4) {
      const newToDoItem: ToDoItem = {
        id: GlobalConstants.EmptyGuid,
        title: toDoItemInput.textContent!,
        isCompleted: false
      }

      this.currentTask.toDoItems?.push(newToDoItem);
      toDoItemInput.textContent = GlobalConstants.EmptyString;
      this.isUpdated = true;
    } else if (toDoItemInput.textContent!.length > 0) {
      this.toastr.error('To Do Item must be at least 4 characters long', 'Error');
    }
  }

  onDateChanged(event: MatDatepickerInputEvent<any>): void {
    this.currentTask.dueDate = event.value;
    this.setDates(this.currentTask);
    this.isUpdated = true;
  }

  private setDates(task: TaskWithDetails, initialSet: boolean = false): void {
    if (this.taskId == GlobalConstants.EmptyGuid && initialSet) {
      this.taskDueDate = 'Specify due date';
      this.taskCreationDate = moment(this.currentTask.createdAt).format('Do MMMM YYYY');
    } else {
      this.taskDueDate = moment(this.currentTask.dueDate).calendar();

      if (this.taskDueDate.includes('/')) {
        this.taskDueDate = moment(task.dueDate).format('D MMM');
      }

      this.taskCreationDate = moment(task.createdAt).format('Do MMMM YYYY');
    }
  }

  private setTitleAndDescription(): void {
    this.elementRef.nativeElement.querySelector('.title-input').innerText = this.currentTask.title;
    this.elementRef.nativeElement.querySelector('.description-input').innerText = this.currentTask.description;
  }

  private validateFields(): boolean {
    const titleInput = this.elementRef.nativeElement.querySelector('.title-input');
    const descriptionInput = this.elementRef.nativeElement.querySelector('.description-input');
    const toDoItemInput = this.elementRef.nativeElement.querySelector('.to-do-item.temp');

    if (titleInput.innerText.length < 4) {
      this.toastr.error('Title must be at least 4 characters long', 'Error');
      return false;
    } else if (titleInput.innerText.length > 32) {
      this.toastr.error('Title cannot exceed 32 characters', 'Error');
      return false;
    }

    this.currentTask.title = titleInput.innerText;

    if (descriptionInput.innerText.length < 8) {
      this.toastr.error('Description must be at least 8 characters long', 'Error');
      return false;
    }

    this.currentTask.description = descriptionInput.innerText;

    if (toDoItemInput.innerText.length > 0 && toDoItemInput.innerText.length < 4) {
      this.toastr.error('To-Do Item must be at least 4 characters long', 'Error');
      return false;
    }

    this.showToDoItemInput();

    return true;
  }

  private updateFields(): void {
    this.currentTask.title = this.elementRef.nativeElement.querySelector('.title-input').innerText;
    this.currentTask.description = this.elementRef.nativeElement.querySelector('.description-input').innerText;
  }

  private tryAddingToDoItem(): void {
    const toDoItemInput = this.elementRef.nativeElement.querySelector('.to-do-item.temp > span');
    
    if (toDoItemInput.innerText.length >= 4) {
      const newToDoItem: ToDoItem = {
        id: GlobalConstants.EmptyGuid,
        title: toDoItemInput.innerText,
        isCompleted: false
      }

      this.currentTask.toDoItems?.push(newToDoItem);
    }
  }

  private hideFields() {
    this.elementRef.nativeElement.querySelector('.label.temp').classList.add('invisible');
    this.elementRef.nativeElement.querySelector('.label.add').classList.remove('invisible');
    this.elementRef.nativeElement.querySelector('.to-do-item.temp').classList.add('invisible');
    this.elementRef.nativeElement.querySelector('.to-do-item.temp > span').innerText = GlobalConstants.EmptyString;
  }
}
