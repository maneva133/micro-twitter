import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Post } from '../../models/post.model';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss'],
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
})
export class PostCardComponent {
  @Input() post!: Post;
  @Input() isMine: boolean = false; 
  @Output() delete = new EventEmitter<number>();

  deletePost() {
    if (!this.post.id) return;
    this.delete.emit(this.post.id);
  }
}