import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TasksService } from 'src/app/core/services/tasks.service';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';

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
    private toastr: ToastrService
  ) { }

  get title() {
    return this.createForm.get('title');
  }

  get description() {
    return this.createForm.get('description');
  }

  onSubmit() {
    if (this.createForm.valid) {
      this.tasksService.createTask(
        this.createForm.value.title,
        this.createForm.value.description
      ).subscribe(() => {
        this.dialogRef.close();
        this.toastr.success('Task created successfully!', 'Success!');
      });
    }
  }
}