import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['../login/login.component.scss']
})
export class RegisterComponent {
  username = '';
  password = '';
  message = '';
  loading = false;

  constructor(private userService: UserService, private router: Router) {}
register() {
  this.loading = true;
  this.userService.register({ username: this.username, password: this.password })
    .subscribe({
      next: (res) => {
        this.loading = false;
        this.message = 'Registration successful!';
        setTimeout(() => this.router.navigate(['/feed']), 0); 
      },
      error: (err) => {
        this.loading = false;

        if (err.status === 409) {
          this.message = 'Username already exists. Try another one.';
        } else if (err.status === 400) {
          this.message = 'Invalid data. Please check your input.';
        } else {
          this.message = 'Error registering. Please try again.';
        }
      }
    });
}
}