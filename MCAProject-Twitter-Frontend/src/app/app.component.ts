import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouterOutlet, RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from './services/user.service';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,RouterOutlet, RouterLink, MatToolbarModule, MatButtonModule],
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(public userService: UserService, private router: Router) {}

  logout() {
    this.userService.logout();
    this.router.navigate(['/login']);
  }

  get isLoggedIn(): boolean {
    return !!this.userService.getCurrentUser();
  }
}
