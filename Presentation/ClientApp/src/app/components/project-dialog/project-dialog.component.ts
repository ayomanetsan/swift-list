import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';
import { ProjectService } from 'src/app/core/services/project.service';

@Component({
  selector: 'app-project-dialog',
  templateUrl: './project-dialog.component.html',
  styleUrls: ['./project-dialog.component.sass']
})
export class ProjectDialogComponent {
  createForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(32)]],
    description: ['', [Validators.required, Validators.minLength(8)]]
  });

  constructor(
    private fb: FormBuilder,
    private projectsService: ProjectService,
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
      this.projectsService.create(
        this.createForm.value.title,
        this.createForm.value.description
      ).subscribe(() => {
        this.dialogRef.close();
        this.toastr.success('Project created successfully!', 'Success!');
      });
    }
  }
}
