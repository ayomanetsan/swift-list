import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ProjectService } from 'src/app/core/services/project.service';
import { GlobalConstants } from 'src/app/global/constants';
import { AccessRights } from 'src/app/models/project';
import { UserAccessRights } from 'src/app/models/userAccessRights';

@Component({
  selector: 'app-project-access',
  templateUrl: './project-access.component.html',
  styleUrls: ['./project-access.component.sass'],
})
export class ProjectAccessComponent {
  createForm: FormGroup = this.fb.group({
    email: ['', [Validators.email]],
  });

  users: UserAccessRights[] = [];
  unchangedUsers: UserAccessRights[] = [];
  accessRightsKeys = Array.from({ length: 3 }, (_, index) => index + 1);

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: { title: string; projectId: string }
  ) {}

  ngOnInit() {
    this.projectService.getProjectAccessRights(this.data.projectId).subscribe(
      res => {
        this.users = res;
        this.unchangedUsers = res;
      });
  }

  grantAccessRights() {
    if (this.createForm.valid) {
      this.projectService.grantAccessRights(this.data.projectId, this.email?.value, AccessRights.Read).subscribe(
        res => {
          this.email?.setValue(GlobalConstants.EmptyString);
          this.ngOnInit();
        },
        (err: HttpErrorResponse) => {
          this.toastr.error(err.error.detail, 'Error');
        }
      )
    }
  }

  updateAccessRights(email: string, accessRights: AccessRights) {
    this.projectService.grantAccessRights(this.data.projectId, email, accessRights).subscribe();
  }

  get email() {
    return this.createForm.get('email');
  }

  public get accessRights(): typeof AccessRights {
    return AccessRights;
  }
}
