import { Component, Inject, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TasksService } from 'src/app/core/services/tasks.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { TaskDialogData } from 'src/app/models/taskDialogData';

@Component({
  selector: 'app-task-dialog',
  templateUrl: './task-dialog.component.html',
  styleUrls: ['./task-dialog.component.sass']
})
export class TaskDialogComponent {
  
  createForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(32)]],
    description: ['', [Validators.required, Validators.minLength(8)]]
  });

  constructor(
    private fb: FormBuilder,
    private tasksService: TasksService,
    private dialogRef: MatDialogRef<TaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TaskDialogData,
    private toastr: ToastrService
  ) { }

  get title() {
    return this.createForm.get('title');
  }

  get description() {
    return this.createForm.get('description');
  }

  onSubmit() {
  }
}
