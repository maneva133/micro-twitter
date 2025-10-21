import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, MatProgressSpinnerModule, MatSnackBarModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username = '';
  password = '';
  loading = false;
  hidePassword = true;

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  login() {
    if (!this.username || !this.password) {
      this.snackBar.open('Please fill in all fields', 'Close', { duration: 3000 });
      return;
    }

    this.loading = true;
    this.userService.login({ username: this.username, password: this.password })
      .subscribe({
        next: (res) => {
          this.snackBar.open(`Welcome, ${res.user}!`, 'Close', { duration: 3000 });
          this.userService.setCurrentUser(res.user); 
          this.router.navigate(['/feed']);
        },
        error: (err) => {
          this.snackBar.open(err.error || 'Login failed.', 'Close', { duration: 3000 });
          this.loading = false;
        }
      });
  }

  togglePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  }
}