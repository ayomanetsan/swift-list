import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule }  from '@angular/common/http';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './components/main/main.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { TaskDialogComponent } from './components/task-dialog/task-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { ProjectListComponent } from './components/project-list/project-list.component';
import { TaskListComponent } from './components/task-list/task-list.component';
import { ProjectDialogComponent } from './components/project-dialog/project-dialog.component';
import { ProjectViewComponent } from './components/project-view/project-view.component';
import { TaskDetailsComponent } from './components/task-details/task-details.component';
import { ColorPickerModule } from 'ngx-color-picker';
import { MatNativeDateModule } from '@angular/material/core';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProjectAccessComponent } from './components/project-access/project-access.component';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainComponent,
    TaskDialogComponent,
    SidebarComponent,
    ProjectListComponent,
    TaskListComponent,
    ProjectDialogComponent,
    ProjectViewComponent,
    TaskDetailsComponent,
    PageNotFoundComponent,
    ProjectAccessComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ColorPickerModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatNativeDateModule,
    MatInputModule,
    MatSelectModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      preventDuplicates: true,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
