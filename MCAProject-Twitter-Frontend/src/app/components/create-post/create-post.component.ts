import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PostsService } from '../../services/posts.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule
  ]
})
export class CreatePostComponent {
  form;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private postsService: PostsService,
    private router: Router,
    private userService: UserService   
  ) {
    this.form = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(12), Validators.maxLength(140)]]
    });
  }

 createPost() {
  if (this.form.invalid) return;
  this.loading = true;

  const currentUser = this.userService.getCurrentUser(); 
  if (!currentUser) {
    this.loading = false;
    return; 
  }

  const dto = {
    username: currentUser,
    content: this.form.value.content
  };

  this.postsService.create(dto).subscribe({
    next: () => {
      this.loading = false;
      this.router.navigate(['/']);
    },
    error: () => this.loading = false
  });
}
}