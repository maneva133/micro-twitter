import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../models/post.model';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from '../post-card/post-card.component';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-my-posts',
  templateUrl: './my-posts.component.html',
  styleUrls: ['./my-posts.component.scss'],
  standalone: true,
  imports: [CommonModule, PostCardComponent]
})
export class MyPostsComponent implements OnInit {
  myPosts: Post[] = [];
  currentUser: string | null = null;

  constructor(
    private postsService: PostsService,
    public userService: UserService
  ) {}

  ngOnInit(): void {
    this.currentUser = this.userService.getCurrentUser();
    if (this.currentUser) {
      this.loadPosts();
    }
  }

  loadPosts(): void {
    if (!this.currentUser) return;
    this.postsService.getByUsername(this.currentUser)
      .subscribe(posts => this.myPosts = posts);
  }

  onDeletePost(id: number | undefined): void {
    if (!id) return;
    this.postsService.delete(id)
      .subscribe(() => this.loadPosts());
  }
}
