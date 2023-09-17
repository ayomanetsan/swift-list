import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.sass']
})
export class SidebarComponent implements OnInit {
  
  name = '';
  email = '';

  constructor(private authService: AuthService) { }
  
  ngOnInit(): void {
    this.name = localStorage.getItem('userName') as string;
    this.email = localStorage.getItem('userEmail') as string;
    this.authService.startAuthenticatedCheck();
  }

  logout() {
    this.authService.logout();
  }
}
